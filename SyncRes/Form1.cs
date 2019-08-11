using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncRes {
	public partial class Form1 : Form {
		Browser browser;

		string userPID;

		public Form1() {
			InitializeComponent();

            browser = new Browser(LoungeURL.Login);
            Control browserControl = browser.GetControl();
			splitContainer1.Panel2.Controls.Add(browserControl);
			browserControl.Dock = DockStyle.Fill;
			browser.OnReadEnded += Browser_AddressChanged;

            cmMusic.SelectedIndex = 0;
		}

		private async void Browser_AddressChanged(object sender, EventArgs e) {
            string url = browser.GetURL();
            urlTextBox.Text = url;
            if(url == LoungeURL.Home && statusLabel.Text == "Unlogin") {
                statusLabel.Text = "Login";
                statusLabel.ForeColor = Color.DarkCyan;
                DLButton.Enabled = true;
                RKDLButton.Enabled = true;

                browser.BeginDocReadMode(url);
                string doc = await browser.ReadDocument(LoungeURL.ScoreHome);
                userPID = Player.GetUserPID(doc);
                browser.EndDocReadMode();

                userIDBox.Lines = new[] { userPID };
            }
		}

		private string[] GetUserIDs() {
			const string idPattern = @"^[\dabcdef]{40}$";
			List<string> idLists = new List<string>();

			foreach(var line in userIDBox.Lines) {
				Match match = Regex.Match(line, idPattern);
				if(match.Success) {
					idLists.Add(line);
				} else {
					if(File.Exists(line)) {
						StreamReader reader = new StreamReader(line);
						string user;
						while((user = reader.ReadLine()) != null) {
							idLists.Add(user);
						}
						reader.Close();
					}
				}
			}

			return idLists.ToArray();
		}

        /// <summary>
        /// 読み込む楽曲IDの一覧を返す
        /// </summary>
        private List<int> GetMusicLists(bool isPnd) {
            switch(cmMusic.SelectedIndex) {
                case 0: //全譜面
                    return isPnd ? MusicData.GetPndMusicIDs().ToList() : MusicData.GetExistsMusicIDs().ToList();
                case 1: //非サヨナラ
                    return isPnd ? MusicData.GetNonDeletedPndIDs().ToList() : MusicData.GetNonDeletedIDs().ToList();
                default: //レート対象
                    return isPnd ? MusicData.GetRateTargetIDs(Difficulty.Pnd).ToList() : MusicData.GetRateTargetIDs(Difficulty.Tec).ToList();
            }
        }

		private async void RKDLButton_Click(object sender, EventArgs e) {
			bool[] checkStates = { cbNormal.Checked, cbAdvanced.Checked, cbTechnical.Checked, cbPandora.Checked, cbMulti.Checked };
			string[] userIDs = GetUserIDs();
            List<int> musicListNATM = GetMusicLists(false), musicListPnd = GetMusicLists(true); //読み込む曲IDのリスト

            string doc;
			string tb = ""; // CSVに書き込むテキスト(表形式)
			Ranking ranking = null;

			StreamWriter writer;

            // 左のバーの初期設定
            progressBar.Maximum = 0;
            if(checkStates[3]) {
                progressBar.Maximum = (checkStates.Count((bool b) => b) - 1) * musicListNATM.Count + musicListPnd.Count;
            } else {
                progressBar.Maximum = checkStates.Count((bool b) => b) * musicListNATM.Count;
            }
            progressBar.Value = 0;

			statusLabel.Text = "Reading";
			statusLabel.ForeColor = Color.Blue;

            // 読み込み
			browser.BeginDocReadMode(browser.GetURL());
			foreach(Difficulty dif in Enum.GetValues(typeof(Difficulty))) {
				if(checkStates[(int)dif]) {
					foreach(int id in (dif == Difficulty.Pnd) ? musicListPnd : musicListNATM) {
						foreach(string user in userIDs) {
                            // サイトを読み込んで
							try {
								string url = LoungeURL.ScoreRanking(user, id.ToString(), dif);

								doc = await browser.ReadDocument(url);
							} catch(System.Reflection.TargetInvocationException) {
								continue;
							}

                            // パースして
							ranking = null;
							try {
								ranking = new Ranking(doc);
							} catch(FileNotFoundException) { }

                            // tbに書き込む
							if(ranking != null) {
								foreach(string s in ranking.IDs) {
									if(s == "") break;
									tb += s + "\r\n";
								}
							}
						}

						progressBar.Value++;
					}
				}
			}

			browser.EndDocReadMode();

			//データの書き出し
			UTF8Encoding encoding = new UTF8Encoding(true);

			statusLabel.Text = "Writing";

			writer = new StreamWriter("Ranking.csv", false, encoding);
			writer.Write(tb);
			writer.Close();

			statusLabel.Text = "Success!";
			statusLabel.ForeColor = Color.Green;
		}

		private async void DLButton_Click(object sender, EventArgs e) {
			string[] userIDs = GetUserIDs().Except(new[] { "" }).ToArray(); // 読み込み対象のプレイヤーID
			bool[] checkStates = { cbNormal.Checked, cbAdvanced.Checked, cbTechnical.Checked, cbPandora.Checked };
			bool isDownload(int dif) => checkStates[dif] || cbDetail.Checked || (dif >= 2 && cbTecpnd.Checked); // 各難易度を読み込むか
            List<int> musicListNATM = GetMusicLists(false), musicListPnd = GetMusicLists(true); //読み込む曲IDのリスト
            List<int> musicList(int dif) => dif == 3 ? musicListPnd : musicListNATM;
            //List<int> deletedList = new List<int>(); //サヨナラ曲の一覧

            // データ格納用の配列
            Player[] players = new Player[userIDs.Length];
			Result[][][] results = new Result[userIDs.Length][][]; //[プレイヤー][難易度][曲(MusicDataの配列と同じ。若い順に0123...)]
			for(int i = 0; i < results.Length; ++i) {
				results[i] = new Result[4][];
				for(int j = 0; j < results[i].Length; ++j) {
                    results[i][j] = new Result[musicList(j).Count];
				}
			}
			Multi[][] multis = new Multi[userIDs.Length][]; //[プレイヤー][曲(MusicDataの配列と同じ。)]
            for(int i = 0; i < multis.Length; ++i) {
				multis[i] = new Multi[musicListNATM.Count];
			}

			StreamWriter writer;

			string doc;
            
            // 左のバーの初期設定
            progressBar.Value = 0;
			progressBar.Maximum = 0;

            if(cbPerson.Checked || cbDetail.Checked) {
				progressBar.Maximum = userIDs.Length;
			}
			for(int dif = 0; dif < 4; dif++) {
				if(isDownload(dif)) {
                    progressBar.Maximum += userIDs.Length * musicList(dif).Count;
				}
			}
			if(cbMulti.Checked) {
                progressBar.Maximum += userIDs.Length * musicListNATM.Count;
			}
			if(progressBar.Maximum == 0) {
				progressBar.Maximum = 1;
				return;
			}
			statusLabel.Text = "Reading";
			statusLabel.ForeColor = Color.Blue;

            //データの読み込み
            browser.BeginDocReadMode(browser.GetURL());
			if(cbPerson.Checked || cbDetail.Checked) {
				for(int user = 0; user < userIDs.Length; user++) {
					doc = await browser.ReadDocument(LoungeURL.Player(userIDs[user]));
					players[user] = Player.ParseFromSite(doc, userIDs[user]);
					progressBar.Value++;
				}
			}

			for(int dif = 0; dif < 4; dif++) {
				if(isDownload(dif)) {
					for(int user = 0; user < userIDs.Length; user++) {
						for(int music = 0; music < musicList(dif).Count; music++) {
							try {
                                int musicID = musicList(dif)[music]; //楽曲ID(musicとは違う)
                                doc = await browser.ReadDocument(LoungeURL.Score(userIDs[user], musicID.ToString(), (Difficulty)dif));
                                Result result = Result.ParseFromSite(doc, musicID.ToString(), userIDs[user]);
                                results[user][dif][music] = result;
                            } catch(FileNotFoundException) {
								results[user][dif][music] = null;
							}
							progressBar.Value++;
						}
					}
				}
			}

			if(cbMulti.Checked) {
				for(int user = 0; user < userIDs.Length; user++) {
					for(int music = 0; music < musicListNATM.Count; music++) {
						try {
                            int musicID = musicListNATM[music];
                            doc = await browser.ReadDocument(LoungeURL.Multi(userIDs[user], musicID.ToString()));
                            Multi multi = Multi.ParseFromSite(doc, musicID.ToString(), userIDs[user], userPID);
                            if(multi.Combo != "--") {
                                multis[user][music] = multi;
                            } else {
                                multis[user][music] = null;
                            }
                        } catch(FileNotFoundException) {
							multis[user][music] = null;
						}
						progressBar.Value++;
					}
				}
			}

			browser.EndDocReadMode();

			//データの書き出し
			UTF8Encoding encoding = new UTF8Encoding(true);
			string[] fname = { "Normal.csv", "Advanced.csv", "Technical.csv", "Pandora.csv" };

			statusLabel.Text = "Writing";

			if(cbPerson.Checked) {
				writer = new StreamWriter("Player.csv", false, encoding);
				writer.Write(Player.MakePlayerCSV(players));
				writer.Close();
			}

			if(cbDetail.Checked) {
				writer = new StreamWriter("Summary.csv", false, encoding);
				writer.Write(new Summary(players[0], results[0]).MakeCSV());
				writer.Close();
			}

			for(int dif = 0; dif < 4; dif++) {
				if(checkStates[dif]) {
					writer = new StreamWriter(fname[dif], false, encoding);
                    if(userIDs.Length == 1) {
                        writer.Write(Result.MakeResultCSV(results[0][dif]));
                    } else {
                        Result[][] r = new Result[players.Length][];
                        for(int i = 0; i < players.Length; i++) {
                            r[i] = results[i][dif];
                        }
                        writer.Write(Result.MakeResultCSV(r, musicList(dif)));
                    }
                    writer.Close();
				}
			}

			if(cbTecpnd.Checked) {
				Result[] tpRes = new Result[musicListNATM.Count];
                int pndIndex;
				for(int i = 0; i < tpRes.Length; i++) {
                    //まず箱があるか探す
                    pndIndex = -1;
                    string musicID = results[0][2][i].ID;
                    for(int j = 0; j < results[0][3].Length; j++) {
                        if(results[0][3][j] != null && results[0][3][j].ID == musicID) {
                            pndIndex = j;
                            break;
                        }
                    }

                    if(pndIndex != -1) {
                        tpRes[i] = results[0][3][pndIndex];
                    } else {
                        tpRes[i] = results[0][2][i];
                    }
				}
				writer = new StreamWriter("TP.csv", false, encoding);
				writer.Write(Result.MakeResultCSV(tpRes));
				writer.Close();
			}

			if(cbMulti.Checked) {
				writer = new StreamWriter("Multi.csv", false, encoding);
				if(userIDs.Length == 1) {
					writer.Write(Multi.MakeResultCSV(multis[0]));
				} else {
					writer.Write(Multi.MakeResultCSV(multis, musicListNATM));
				}
				writer.Close();
			}

			statusLabel.Text = "Success!";
			statusLabel.ForeColor = Color.Green;
		}

		private void Form1_Resize(object sender, EventArgs e) {
			this.Width = 700;
			if(this.Height > 118) {
				splitContainer1.SplitterDistance = 118;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            Browser.Shutdown();
		}
	}
}
