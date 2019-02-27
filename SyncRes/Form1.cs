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
using CefSharp;
using CefSharp.WinForms;

namespace SyncRes {
	public partial class Form1 : Form {
		ChromiumWebBrowser browser;

		string userPID;

		private bool webDocReadMode = false;
		private string currentUrl;

		bool dc = false; //dcで T/F切り替え

		public Form1() {
			InitializeComponent();

			var settings = new CefSettings();
			settings.Locale = "ja";
			settings.AcceptLanguageList = "ja-JP";
			Cef.Initialize(settings);
			browser = new ChromiumWebBrowser("https://lounge.synchronica.jp/");
			splitContainer1.Panel2.Controls.Add(browser);
			browser.Dock = DockStyle.Fill;
			browser.AddressChanged += Browser_AddressChanged;
			browser.FrameLoadEnd += Browser_FrameLoadEnd;
		}

		private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e) {
			dc = !dc;
		}

		private void Browser_AddressChanged(object sender, AddressChangedEventArgs e) {
			Action action = async () =>{
				string url = browser.Address;
				urlTextBox.Text = url;
				if(url == "https://lounge.synchronica.jp/MyPage" && statusLabel.Text == "Unlogin") {
					statusLabel.Text = "Login";
					statusLabel.ForeColor = Color.DarkCyan;
					DLButton.Enabled = true;
					RKDLButton.Enabled = true;

					BeginDocReadMode();
					var doc = await ReadDocument("https://lounge.synchronica.jp/ScoreData");
					userPID = Player.GetUserPID(doc);
					EndDocReadMode();

					userIDBox.Lines = new[] { userPID };
				}
			};
			browser.BeginInvoke(action);
		}

		public void BeginDocReadMode() {
			if(!webDocReadMode) {
				webDocReadMode = true;
				currentUrl = urlTextBox.Text;
				browser.Visible = false;
			}
		}

		public void EndDocReadMode() {
			if(webDocReadMode) {
				webDocReadMode = false;
				browser.Load(currentUrl);
				browser.Visible = true;
			}
		}

		public async Task<string> ReadDocument(string url) {
			if(!webDocReadMode) BeginDocReadMode();

			browser.Load(url);
			await WaitForDocumentDownload();

			return await browser.GetSourceAsync();
		}

		private async Task WaitForDocumentDownload() {
			bool cdc = dc;
			int timeout = 100;
			while(cdc == dc || timeout-- <= 0) await Task.Delay(66);
			await Task.Delay(100);
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

		private async void RKDLButton_Click(object sender, EventArgs e) {
			int musicIDmax = (int)MusicIDBox.Value;
			bool[] checkStates = { cbNormal.Checked, cbAdvanced.Checked, cbTechnical.Checked, cbPandora.Checked, cbMulti.Checked };
			string[] userIDs = GetUserIDs();

			string doc;
			string tb = "";
			Ranking ranking = null;

			StreamWriter writer;

			progressBar.Maximum = checkStates.Count((bool b) => b) * musicIDmax;
			progressBar.Value = 0;

			statusLabel.Text = "Reading";
			statusLabel.ForeColor = Color.Blue;

			BeginDocReadMode();
			for(int dif = 0; dif < checkStates.Length; dif++) {
				if(checkStates[dif]) {
					for(int id = 1; id <= musicIDmax; id++) {
						foreach(string user in userIDs) {
							try {
								string url;
								if(dif != 4) {
									url = "https://lounge.synchronica.jp/ScoreRanking/detail/" + user + "/0?s_id=" + id + "&level=" + dif;
								} else {
									url = "https://lounge.synchronica.jp/ComboRanking/detail/" + user + "/0?s_id=" + id;
								}

								doc = await ReadDocument(url);
							} catch(System.Reflection.TargetInvocationException) {
								continue;
							}

							ranking = null;
							try {
								ranking = new Ranking(doc);
							} catch(FileNotFoundException) { }
							if(ranking != null && ranking.Page == 1) break;
						}

						if(ranking != null && (!cbDeleted.Checked || !ranking.IsDeleted)) {
							foreach(string s in ranking.IDs) {
								if(s == "") break;
								tb += s + "\r\n";
							}
						}

						progressBar.Value++;
					}
				}
			}

			EndDocReadMode();

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
			string[] userIDs = GetUserIDs();
			int musicIDmax = (int)MusicIDBox.Value;
			bool[] checkStates = { cbNormal.Checked, cbAdvanced.Checked, cbTechnical.Checked, cbPandora.Checked };
			bool ignoreDeleted = cbDeleted.Checked;
			bool isDownload(int dif) => checkStates[dif] || cbDetail.Checked || (dif >= 2 && cbTecpnd.Checked);

			Player[] players = new Player[userIDs.Length];
			Result[][][] results = new Result[userIDs.Length][][];
			for(int i = 0; i < results.Length; ++i) {
				results[i] = new Result[4][];
				for(int j = 0; j < results[i].Length; ++j) {
					results[i][j] = new Result[musicIDmax];
				}
			}
			Multi[][] multis = new Multi[userIDs.Length][];
			for(int i = 0; i < multis.Length; ++i) {
				multis[i] = new Multi[musicIDmax];
			}

			StreamWriter writer;

			string doc;

			userIDs = userIDs.Except(new[] { "" }).ToArray();

			//データの読み込み
			progressBar.Value = 0;
			progressBar.Maximum = 0;
			if(cbPerson.Checked || cbDetail.Checked) {
				progressBar.Maximum = userIDs.Length;
			}
			for(int dif = 0; dif < 4; dif++) {
				if(isDownload(dif)) {
					progressBar.Maximum += userIDs.Length * musicIDmax;
				}
			}
			if(cbMulti.Checked) {
				progressBar.Maximum += userIDs.Length * musicIDmax;
			}
			if(progressBar.Maximum == 0) {
				progressBar.Maximum = 1;
				return;
			}
			statusLabel.Text = "Reading";
			statusLabel.ForeColor = Color.Blue;

			BeginDocReadMode();
			if(cbPerson.Checked || cbDetail.Checked) {
				for(int user = 0; user < userIDs.Length; user++) {
					doc = await ReadDocument("https://lounge.synchronica.jp/Friend/info/" + userIDs[user]);
					players[user] = Player.ParseFromSite(doc, userIDs[user]);
					progressBar.Value++;
				}
			}

			for(int dif = 0; dif < 4; dif++) {
				if(isDownload(dif)) {
					for(int user = 0; user < userIDs.Length; user++) {
						for(int music = 0; music < musicIDmax; music++) {
							try {
								if((!cbFast.Checked || user == 0 || results[0][dif][music] != null)) {
									doc = await ReadDocument("https://lounge.synchronica.jp/PersonalScore/detail/" + userIDs[user] + "/" + (music + 1) + "?level=" + dif);
									Result result = Result.ParseFromSite(doc, (music + 1).ToString());
									if((!ignoreDeleted || !result.IsDeleted)) {
										results[user][dif][music] = result;
									} else {
										results[user][dif][music] = null;
									}
								} else {
									results[user][dif][music] = null;
								}
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
					for(int music = 0; music < musicIDmax; music++) {
						try {
							if(!cbFast.Checked || user == 0 || multis[0][music] != null) {
								doc = await ReadDocument("https://lounge.synchronica.jp/PersonalScore/combo/" + userIDs[user] + "/" + (music + 1));
								Multi multi = Multi.ParseFromSite(doc, (music + 1).ToString(), userIDs[user], userPID);
								if(multi.Combo != "--" && (!ignoreDeleted || !multi.IsDeleted)) {
									multis[user][music] = multi;
								} else {
									multis[user][music] = null;
								}
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

			EndDocReadMode();

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
					writer.Write(Result.MakeResultCSV(results[0][dif], ignoreDeleted));
					writer.Close();
				}
			}

			if(cbTecpnd.Checked) {
				Result[] tpRes = new Result[musicIDmax];
				for(int i = 0; i < musicIDmax; i++) {
					tpRes[i] = results[0][3][i] ?? results[0][2][i];
				}
				writer = new StreamWriter("TP.csv", false, encoding);
				writer.Write(Result.MakeResultCSV(tpRes, ignoreDeleted));
				writer.Close();
			}

			if(cbMulti.Checked) {
				writer = new StreamWriter("Multi.csv", false, encoding);
				if(userIDs.Length == 1) {
					writer.Write(Multi.MakeResultCSV(multis[0], ignoreDeleted));
				} else {
					writer.Write(Multi.MakeResultCSV(multis, ignoreDeleted, musicIDmax));
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
			Cef.Shutdown();
		}
	}
}
