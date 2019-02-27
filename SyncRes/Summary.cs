using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncRes {
	class Summary {
		Player player;
		Result[][] results;

		public Summary(Player player, Result[][] results) {
			this.player = player;
			this.results = new Result[4][];
			for(int i = 0; i < this.results.Length; i++) {
				var l = results[i].ToList();
				l.RemoveAll(r => r == null);
				this.results[i] = l.ToArray();
			}
		}

		public string MakeCSV() {
			int crAll = int.Parse(player.CoppCr) + int.Parse(player.SilvCr) + int.Parse(player.GoldCr);

			int[] songNum = new int[5];
			for(int i = 0; i < 4; i++) {
				songNum[4] += songNum[i] = results[i].Count(r => r.Score != "--");
			}

			int[] playNum = new int[5];
			for(int i = 0; i < 4; i++) {
				playNum[4] += playNum[i] = results[i].Sum(r => int.Parse(r.PlayNum));
			}
			int money = playNum[4] * 100 / 3;

			decimal[] averageCR = new decimal[5];
			for(int i = 0; i < 4; i++) {
				decimal crsum = results[i].Sum(r => decimal.Parse(r.CR));
				averageCR[i] = crsum / songNum[i];
				averageCR[4] += crsum / songNum[4];
			}

			int[] getCounts(Func<Result, bool> predicate) {
				int[] ret = new int[5];
				for(int i = 0; i < 4; i++) {
					ret[4] += ret[i] = results[i].Count(predicate);
				}
				return ret;
			}

			int[] playStage = getCounts(r => r.PlayNum != "0");
			int[] clearStage = getCounts(r => r.ClearNum != "0");
			int[] fcStage = getCounts(r => r.FCNum != "0");
			int[] aaafcStage = getCounts(r => (r.Rank == "AAA" || r.Rank == "Rz") && r.FCNum != "0");
			int[] rzfcStage = getCounts(r => r.Rank == "Rz" && r.FCNum != "0");
			
			// write
			string res = "";
			res += player.Name + ",Nor,Adv,Tec,Pnd,ALL,,,クラウン," + int.Parse(player.GoldCr) + ",レート・☆\n";
			res += "譜面数," + songNum[0] + "," + songNum[1] + "," + songNum[2] + "," + songNum[3] + "," + songNum[4] + ",,," + crAll + "," + player.SilvCr + "," + player.Rate + "\n";
			res += "プレイ回数," + playNum[0] + "," + playNum[1] + "," + playNum[2] + "," + playNum[3] + "," + playNum[4] + ",課金額,\\" + money + ",," + player.CoppCr + "," + player.Star + "\n";
			res += "平均CR," + averageCR[0].ToString("F2") + "," + averageCR[1].ToString("F2") + "," + averageCR[2].ToString("F2") + "," + averageCR[3].ToString("F2") + "," + averageCR[4].ToString("F2") + ",Nor,Adv,Tec,Pnd,ALL\n";
			res += ",,,達成済,,,,,未達成,,\n";
			res += "プレイ," + playStage[0] + "," + playStage[1] + "," + playStage[2] + "," + playStage[3] + "," + playStage[4] + "," + (songNum[0] - playStage[0]) + "," + (songNum[1] - playStage[1]) + "," + (songNum[2] - playStage[2]) + "," + (songNum[3] - playStage[3]) + "," + (songNum[4] - playStage[4]) + "\n";
			res += "クリア," + clearStage[0] + "," + clearStage[1] + "," + clearStage[2] + "," + clearStage[3] + "," + clearStage[4] + "," + (songNum[0] - clearStage[0]) + "," + (songNum[1] - clearStage[1]) + "," + (songNum[2] - clearStage[2]) + "," + (songNum[3] - clearStage[3]) + "," + (songNum[4] - clearStage[4]) + "\n";
			res += "フルコン," + fcStage[0] + "," + fcStage[1] + "," + fcStage[2] + "," + fcStage[3] + "," + fcStage[4] + "," + (songNum[0] - fcStage[0]) + "," + (songNum[1] - fcStage[1]) + "," + (songNum[2] - fcStage[2]) + "," + (songNum[3] - fcStage[3]) + "," + (songNum[4] - fcStage[4]) + "\n";
			res += "鳥コン," + aaafcStage[0] + "," + aaafcStage[1] + "," + aaafcStage[2] + "," + aaafcStage[3] + "," + aaafcStage[4] + "," + (songNum[0] - aaafcStage[0]) + "," + (songNum[1] - aaafcStage[1]) + "," + (songNum[2] - aaafcStage[2]) + "," + (songNum[3] - aaafcStage[3]) + "," + (songNum[4] - aaafcStage[4]) + "\n";
			res += "レゾ," + rzfcStage[0] + "," + rzfcStage[1] + "," + rzfcStage[2] + "," + rzfcStage[3] + "," + rzfcStage[4] + "," + (songNum[0] - rzfcStage[0]) + "," + (songNum[1] - rzfcStage[1]) + "," + (songNum[2] - rzfcStage[2]) + "," + (songNum[3] - rzfcStage[3]) + "," + (songNum[4] - rzfcStage[4]) + "\n";

			return res;
		}
	}
}
