﻿using System;
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

			browser.BeginDocReadMode(browser.GetURL());
			foreach(Difficulty dif in Enum.GetValues(typeof(Difficulty))) {
				if(checkStates[(int)dif]) {
					for(int id = 1; id <= musicIDmax; id++) {
						foreach(string user in userIDs) {
							try {
								string url = LoungeURL.ScoreRanking(user, id.ToString(), dif);

								doc = await browser.ReadDocument(url);
							} catch(System.Reflection.TargetInvocationException) {
								continue;
							}

							ranking = null;
							try {
								ranking = new Ranking(doc);
							} catch(FileNotFoundException) { }

							if(ranking != null && (!cbDeleted.Checked || !ranking.IsDeleted)) {
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
						for(int music = 0; music < musicIDmax; music++) {
							try {
								if(!cbFast.Checked || user == 0 || results[0][dif][music] != null) {
									doc = await browser.ReadDocument(LoungeURL.Score(userIDs[user], (music + 1).ToString(), (Difficulty)dif));
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
								doc = await browser.ReadDocument(LoungeURL.Multi(userIDs[user], (music + 1).ToString()));
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
            Browser.Shutdown();
		}
	}
}
