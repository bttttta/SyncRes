using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace SyncRes {
	class Player {
		public string ID { get; private set; } //プレイヤーID
		public string Name { get; private set; } //名前
		public string Area { get; private set; } //都道府県
		public string Rate { get; private set; } //レート XXX.XX
		public string Star { get; private set; } //星 1/.../7
		public string Title { get; private set; } //称号
		public string Comment { get; private set; } //コメント
		public string GoldCr { get; private set; } //金クラウン
		public string SilvCr { get; private set; } //銀クラウン
		public string CoppCr { get; private set; } //銅クラウン
		public string GoldCrG { get; private set; } //削除金クラウン
		public string SilvCrG { get; private set; } //削除銀クラウン
		public string CoppCrG { get; private set; } //削除銅クラウン

		internal Player() { }

		public static Player ParseFromSite(string site, string id = "") {
			Player ret = new Player();
			string reg;
			string match, match2;

			ret.ID = id;

			reg = "<div class = \"name child\"> ([^<]*[^<　])　* </div>";
			match = Parser.Match(site, reg);
			if(match == null) return null;
			ret.Name = match;

			reg = "<div class = \"area child\"> 都道府県： ([^<]*)</div>";
			match = Parser.Match(site, reg);
			ret.Area = match ?? throw new FormatException("プレイヤー\'" + ret.Name + "\'の都道府県のパースに失敗しました");

			reg = "<div class = \"rate child\">([^<]*)</div>";
			match = Parser.Match(site, reg);
			reg = "<div class = \"rate_harf child\">([^<]*)</div>";
			match2 = Parser.Match(site, reg);
			if(match == null || match2 == "－－") {
				ret.Rate = ret.Star = "";
			} else {
				ret.Rate = match + match2;
				reg = "<img class = \"rateimg child\" src = \"/css/images/player_rate_base(.)\\.png\">";
				match = Parser.Match(site, reg);
				ret.Star = match ?? throw new FormatException("プレイヤー\'" + ret.Name + "\'の☆数のパースに失敗しました");
			}

			reg = "<div class = \"title child\">([^<]*)</div>";
			match = Parser.Match(site, reg);
			ret.Title = match ?? throw new FormatException("プレイヤー\'" + ret.Name + "\'の称号のパースに失敗しました");

			reg = "<div class = \"comment child\">([^<]*)</div>";
			match = Parser.Match(site, reg);
			if(match == null) {
				reg = "<div class = \"text child\">([^<]*)</div>";
				match = Parser.Match(site, reg);
				if(match == null) {
					throw new FormatException("プレイヤー\'" + ret.Name + "\'のコメントのパースに失敗しました");
				}
			}
			ret.Comment = match;

			reg = "<div class = \"crown gold child\">([^<]*)</div>";
			match = Parser.Match(site, reg);
			ret.GoldCr = match ?? throw new FormatException("プレイヤー\'" + ret.Name + "\'の金クラウン数のパースに失敗しました");

			reg = "<div class = \"crown silver child\">([^<]*)</div>";
			match = Parser.Match(site, reg);
			ret.SilvCr = match ?? throw new FormatException("プレイヤー\'" + ret.Name + "\'の銀クラウン数のパースに失敗しました");
			
			reg = "<div class = \"crown copper child\">([^<]*)</div>";
			match = Parser.Match(site, reg);
			ret.CoppCr = match ?? throw new FormatException("プレイヤー\'" + ret.Name + "\'の銅クラウン数のパースに失敗しました");

			reg = "<div class = \"crown gold_ghost child\">\\(([^<]*)\\)</div>";
			match = Parser.Match(site, reg);
			ret.GoldCrG = match ?? "0";

			reg = "<div class = \"crown silver_ghost child\">\\(([^<]*)\\)</div>";
			match = Parser.Match(site, reg);
			ret.SilvCrG = match ?? "0";

			reg = "<div\\s+class=\"crown copper_ghost child\">\\(([^<]*)\\)</div>";
			match = Parser.Match(site, reg);
			ret.CoppCrG = match ?? "0";

			return ret;
		}

		public override string ToString() {
			return '\"' + Name + "\","
				+ '\"' + ID + "\","
				+ '\"' + Area + "\","
				+ '\"' + Rate + "\","
				+ '\"' + Star + "\","
				+ '\"' + Title + "\","
				+ '\"' + Comment + "\","
				+ '\"' + GoldCr + "\","
				+ '\"' + SilvCr + "\","
				+ '\"' + CoppCr + "\","
				+ '\"' + GoldCrG + "\","
				+ '\"' + SilvCrG + "\","
				+ '\"' + CoppCrG + "\"";
		}

		public static string GetUserPID(string scoreDataSite) {
			string reg;
			string match;

			reg = "<a class = \"btn\" href = \"/PersonalScore/index/([^\"]*)\">";
			match = Parser.Match(scoreDataSite, reg);
			if(match == null) throw new ArgumentException();
			return match;
		}

		static public string MakePlayerCSV(Player[] players) {
			string res = "\"名前\",\"ID\",\"都道府県\",\"レート\",\"☆\",\"称号\",\"コメント\",\"金クラウン\",\"銀クラウン\",\"銅クラウン\",\"旧金クラウン\",\"旧銀クラウン\",\"旧銅クラウン\"\r\n";

			foreach(var item in players) {
				if(item != null) {
					res += item.ToString() + "\r\n";
				}
			}

			return res;
		}
	}

	class Result {
        public string PlayerID { get; private set; }
        public string ID { get; private set; }
        public string SongName { get; private set; }
		public string Rank { get; private set; }
		public string Ranking { get; private set; }
		public string PlayNum { get; private set; }
		public string ClearNum { get; private set; }
		public string FCNum { get; private set; }
		public string Score { get; private set; }
		public string CR { get; private set; }
		public string Combo { get; private set; }
		public string ComboBonus { get; private set; }
		public string ReleaseBonus { get; private set; }
		public string Perfect { get; private set; }
		public string Great { get; private set; }
		public string Good { get; private set; }
		public string Fast { get; private set; }
		public string Slow { get; private set; }

		internal Result() { }

		public static Result ParseFromSite(string site, string id, string pid) {
			Result ret = new Result();
			string reg;
			string match;

            ret.PlayerID = pid;
			ret.ID = id;

			reg = "<div id = \"song_name\"> <p> 指定されたスコアは表示できません </p> </div>";
			match = Parser.Match(site, reg);
			if(match != null) throw new FileNotFoundException();

			reg = "<div id = \"song_name\"> <img src = \"/css/images/panel_wht_01.png\"> <div> ([^<]+) </div> </div>";
			match = Parser.Match(site, reg);
			ret.SongName = match ?? throw new FormatException("曲\'" + id + "\'の曲名のパースに失敗しました");

			reg = "<img name = \"rank\" class = \"child\" src = \"/css/images/rank_([^.]+).png\">";
			match = Parser.Match(site, reg);
			if(match == null) throw new FormatException("曲\'" + ret.SongName + "\'の曲名のパースに失敗しました");
			switch(match) {
				case "--":
					throw new FileNotFoundException();
				case "rz": ret.Rank = "Rz"; break;
				case "aaa": ret.Rank = "AAA"; break;
				case "aa": ret.Rank = "AA"; break;
				case "a": ret.Rank = "A"; break;
				case "b": ret.Rank = "B"; break;
				case "c": ret.Rank = "C"; break;
				case "d": ret.Rank = "D"; break;
				case "e": ret.Rank = "E"; break;
				default: ret.Rank = "?"; break;
			}

			reg = "<div class = \"td title\"> 全国ランキング </div> <div class = \"td data\"> ([^位]+)位 </div>";
			match = Parser.Match(site, reg);
			ret.Ranking = match ?? throw new FormatException("曲\'" + ret.SongName + "\'の順位のパースに失敗しました");
			reg = "<div class = \"td title\"> プレイ回数 </div> <div class = \"td data\"> (\\d+)回 </div>";
			match = Parser.Match(site, reg);
			ret.PlayNum = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のプレイ回数のパースに失敗しました");
			reg = "<div class = \"td title\"> クリア回数 </div> <div class = \"td data\"> (\\d+)回 </div>";
			match = Parser.Match(site, reg);
			ret.ClearNum = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のクリア回数のパースに失敗しました");
			reg = "<div class = \"td title\"> フルコンボ回数 </div> <div class = \"td data\"> (\\d+)回 </div>";
			match = Parser.Match(site, reg);
			ret.FCNum = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のフルコンボ回数のパースに失敗しました");
			reg = "<div class = \"td title\"> Score </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Score = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のスコアのパースに失敗しました");
			reg = "<div class = \"td title\"> Clear Rate </div> <div class = \"td data\"> ([^%]+)% </div>";
			match = Parser.Match(site, reg);
			ret.CR = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のCRのパースに失敗しました");
			reg = "<div class = \"td title\"> Best Combo </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Combo = match ?? throw new FormatException("曲\'" + ret.SongName + "\'の最大コンボ数のパースに失敗しました");
			reg = "<div class = \"td title\"> Combo Bonus </div> <div class = \"td data\"> ([^%]+)% </div>";
			match = Parser.Match(site, reg);
			ret.ComboBonus = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のコンボボーナスのパースに失敗しました");
			reg = "<div class = \"td title\"> Release Bonus </div> <div class = \"td data\"> ([^%]+)% </div>";
			match = Parser.Match(site, reg);
			ret.ReleaseBonus = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のリリースボーナスのパースに失敗しました");
			reg = "<div class = \"td title\"> Perfect </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Perfect = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のPerfectのパースに失敗しました");
			reg = "<div class = \"td title\"> Great </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Great = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のGreatのパースに失敗しました");
			reg = "<div class = \"td title\"> Good </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Good = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のGoodのパースに失敗しました");
			reg = "<div class = \"td title\"> Fast </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Fast = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のFastのパースに失敗しました");
			reg = "<div class = \"td title\"> Slow </div> <div class = \"td data\"> (\\d+) </div>";
			match = Parser.Match(site, reg);
			ret.Slow = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のSlowのパースに失敗しました");

			return ret;
		}

		public override string ToString() {
			return '\"' + ID + "\","
				+ '\"' + SongName + "\","
				+ '\"' + Rank + "\","
				+ '\"' + Ranking + "\","
				+ '\"' + PlayNum + "\","
				+ '\"' + ClearNum + "\","
				+ '\"' + FCNum + "\","
				+ '\"' + Score + "\","
				+ '\"' + CR + "\","
				+ '\"' + Combo + "\","
				+ '\"' + ComboBonus + "\","
				+ '\"' + ReleaseBonus + "\","
				+ '\"' + Perfect + "\","
				+ '\"' + Great + "\","
				+ '\"' + Good + "\","
				+ '\"' + Fast + "\","
				+ '\"' + Slow + "\"";
		}

		public bool IsDeleted {
			get { return Regex.Match(SongName, "^（.*）$").Success; }
		}

		static public string MakeResultCSV(Result[] results) {
			string res = "\"ID\",\"曲名\",\"ランク\",\"順位\",\"P回\",\"C回\",\"F回\",\"スコア\",\"CR\",\"コンボ\",\"CB\",\"RB\",\"Pf\",\"Gr\",\"Gd\",\"F\",\"S\"\r\n";

			foreach(var item in results) {
				if(item != null) {
					res += item.ToString() + "\r\n";
				}
			}

			return res;
		}

        static public string MakeResultCSV(Result[][] results, List<int> MusicList) {
            // 先頭行
            string res = "\"ID\"";
            foreach(int musicID in MusicList) {
                res += ",\"" + musicID + "\"";
            }
            res += "\r\n";

            foreach(Result[] result in results) {
                // プレイヤーID
                foreach(Result r in result) {
                    if(r != null) {
                        res += "\"" + r.PlayerID + "\"";
                        break;
                    }
                }
                // コンボ数
                foreach(Result r in result) {
                    if(r != null) {
                        res += ",\"" + r.Combo + "\"";
                    } else {
                        res += ",\"0\"";
                    }
                }
                res += "\r\n";
            }

            return res;
        }
    }

	class Multi {
		public string PlayerID { get; private set; }
		public string SongID { get; private set; }
		public string SongName { get; private set; }
		public string Ranking { get; private set; }
		public string Combo { get; private set; }
		public string PartnerName { get; private set; }
		public string PartnerID { get; private set; }

		internal Multi() { }

		public static Multi ParseFromSite(string site, string songId, string pid, string ownpid) {
			Multi ret = new Multi();
			string reg;
			string match;

			ret.PlayerID = pid;
			ret.SongID = songId;

			reg = "<div id = \"song_name\"> <p> 指定されたスコアは表示できません </p> </div>";
			match = Parser.Match(site, reg);
			if(match != null) throw new FileNotFoundException();

			reg = "<div id = \"song_name\"> <img src = \"/css/images/panel_wht_01.png\"> <div>([^<]+) </div> </div>";
			match = Parser.Match(site, reg);
			ret.SongName = match ?? throw new FormatException("曲\'" + songId + "\'の曲名のパースに失敗しました");

			reg = "<div class = \"td paramtitle\"> 全国ランキング </div> <div class = \"td data\"> ([^位]+)位 </div> </div>";
			match = Parser.Match(site, reg);
			ret.Ranking = match ?? throw new FormatException("曲\'" + ret.SongName + "\'の順位のパースに失敗しました");

			reg = "<div class = \"td paramtitle\"> ベストマルチコンボ </div> <div class = \"td data\"> ([^<]+) </div> </div>";
			match = Parser.Match(site, reg);
			ret.Combo = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のマルチコンボのパースに失敗しました");
			if(ret.Combo == "--") throw new FileNotFoundException();

			// パートナーは２人目なのでそこ以降を取り出す
			reg = "userplate_s_base[\\w\\W\\s]+userplate_s_base([\\w\\W\\s]+)";
			match = Parser.Match(site, reg);
			site = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のパートナーIDのパースに失敗しました");

			reg = "onclick = \"JavaScript\\:toUserInfo\\(\'([^\']*)\'\\)";
			ret.PartnerID = Parser.Match(site, reg);
			if(ret.PartnerID == null) {
				reg = "(ゲストの場合はアイコンも変更)";
				if(Parser.Match(site, reg) == null) {
					ret.PartnerID = ownpid;
				} else {
					ret.PartnerID = "";
				}
			}

			reg = "<div class = \"name\"> ([^<]*[^<　])　* <br>";
			match = Parser.Match(site, reg);
			ret.PartnerName = match ?? throw new FormatException("曲\'" + ret.SongName + "\'のパートナー名のパースに失敗しました");

			return ret;
		}

		public override string ToString() {
			return '\"' + SongID + "\","
				+ '\"' + '\'' + SongName + "\","
				+ '\"' + Ranking + "\","
				+ '\"' + Combo + "\","
				+ '\"' + PartnerName + "\","
				+ '\"' + PartnerID + "\"";
		}

		public bool IsDeleted {
			get { return Regex.Match(SongName, "^（.*）$").Success; }
		}

		static public string MakeResultCSV(Multi[] multis) {
			string res = "\"ID\",\"曲名\",\"順位\",\"コンボ\",\"相方\",\"ID\"\r\n";

			foreach(var item in multis) {
				if(item != null) {
					res += item.ToString() + "\r\n";
				}
			}

			return res;
		}

		static public string MakeResultCSV(Multi[][] multis, List<int> MusicList) {
            // 先頭行
			string res = "\"ID\"";
			foreach(int musicID in MusicList) {
				res += ",\"" + musicID + "\"";
			}
			res += "\r\n";

			foreach(Multi[] multi in multis) {
                // プレイヤーID
				foreach(Multi m in multi) {
					if(m != null) {
						res += "\"" + m.PlayerID + "\"";
						break;
					}
				}
                // コンボ数
				foreach(Multi m in multi) {
                    if(m != null) {
                        res += ",\"" + m.Combo + "\"";
                    } else {
                        res += ",\"0\"";
                    }
				}
				res += "\r\n";
			}

			return res;
		}
	}

	class Ranking {
		public string SongName { get; private set; }
		public int Page { get; private set; }
		public string[] IDs { get; private set; }

		public Ranking(string site) {
			int appear = 0;
			string[] IDs = new string[100];

			string curSite = site;
			const string songReg = "<div id = \"song_name\"> <[^>]+> <div>([^>]+)</div>";
			const string rankReg = "(<div>|<td class=\"rank\">)(\\d*)位(</div>|</td>)";
			const string reg = "id=\"(score|multi)rank_member_([^\"]+)\"([\\w\\W]+)";
			Match match;
			string sMatch;

			sMatch = Parser.Match(site, songReg);
			SongName = sMatch ?? throw new FileNotFoundException();
			match = Regex.Match(site, rankReg);
			int rank = int.Parse(match.Groups[2].Value);
			Page = (rank + 98) / 50;
			for(int i = 0; true; i++) {
				match = Regex.Match(curSite, reg);
				if(!match.Success) break;
				IDs[i] = match.Groups[2].Value;
				curSite = match.Groups[3].Value;
				appear++;
			}
			this.IDs = new string[appear];
			Array.Copy(IDs, 0, this.IDs, 0, appear);
		}

		public bool IsDeleted {
			get { return Regex.Match(SongName, "^（.*）$").Success; }
		}
	}
}
