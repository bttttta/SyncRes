using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncRes {
	class Summary {
		Player player;
		Result[][] results;
        Multi[] multis;

		public Summary(Player player, Result[][] results, Multi[] multis) {
			this.player = player;
            this.results = results;
            this.multis = multis;
		}

		public static string MakeCSV(Summary summaries) {
			int crAll = int.Parse(summaries.player.CoppCr) + int.Parse(summaries.player.SilvCr) + int.Parse(summaries.player.GoldCr);

			int[] songNum = new int[5];
			for(int i = 0; i < 4; i++) {
				songNum[4] += songNum[i] = summaries.results[i].Count();
			}

			int[] playNum = new int[5];
			for(int i = 0; i < 4; i++) {
				playNum[4] += playNum[i] = summaries.results[i].Sum(r => r != null ? int.Parse(r.PlayNum) : 0);
			}
			int money = playNum[4] * 100 / 3;

			decimal[] averageCR = new decimal[5];
            decimal[] sumCR = new decimal[5];
			for(int i = 0; i < 4; i++) {
				sumCR[4] += sumCR[i] = summaries.results[i].Sum(r => r != null ? decimal.Parse(r.CR) : 0);
				averageCR[i] = sumCR[i] / songNum[i];
			}
            averageCR[4] = sumCR[4] / songNum[4];

            decimal mc = summaries.multis.Sum(m => m != null ? decimal.Parse(m.Combo) : 0);

            int[] getCounts(Func<Result, bool> predicate) {
				int[] ret = new int[5];
				for(int i = 0; i < 4; i++) {
					ret[4] += ret[i] = summaries.results[i].Count(predicate);
				}
				return ret;
			}

			int[] playStage = getCounts(r => r != null && r.PlayNum != "0");
			int[] clearStage = getCounts(r => r != null && r.ClearNum != "0");
			int[] fcStage = getCounts(r => r != null && r.FCNum != "0");
			int[] aaafcStage = getCounts(r => r != null && (r.Rank == "AAA" || r.Rank == "Rz") && r.FCNum != "0");
			int[] rzfcStage = getCounts(r => r != null && r.Rank == "Rz" && r.FCNum != "0");
			
			// write
			string res = "";
			res += summaries.player.Name + ",Nor,Adv,Tec,Pnd,ALL,,,クラウン," + int.Parse(summaries.player.GoldCr) + ",レート・☆\n";
            res += "譜面数," + songNum[0] + "," + songNum[1] + "," + songNum[2] + "," + songNum[3] + "," + songNum[4] + ",総MC," + mc + "," + crAll + "," + summaries.player.SilvCr + "," + summaries.player.Rate + "\n";
			res += "プレイ回数," + playNum[0] + "," + playNum[1] + "," + playNum[2] + "," + playNum[3] + "," + playNum[4] + ",課金額,\\" + money + ",," + summaries.player.CoppCr + "," + summaries.player.Star + "\n";
			res += "平均CR," + averageCR[0].ToString("F2") + "," + averageCR[1].ToString("F2") + "," + averageCR[2].ToString("F2") + "," + averageCR[3].ToString("F2") + "," + averageCR[4].ToString("F2") + ",Nor,Adv,Tec,Pnd,ALL\n";
			res += ",,,達成済,,,,,未達成,,\n";
			res += "プレイ," + playStage[0] + "," + playStage[1] + "," + playStage[2] + "," + playStage[3] + "," + playStage[4] + "," + (songNum[0] - playStage[0]) + "," + (songNum[1] - playStage[1]) + "," + (songNum[2] - playStage[2]) + "," + (songNum[3] - playStage[3]) + "," + (songNum[4] - playStage[4]) + "\n";
			res += "クリア," + clearStage[0] + "," + clearStage[1] + "," + clearStage[2] + "," + clearStage[3] + "," + clearStage[4] + "," + (songNum[0] - clearStage[0]) + "," + (songNum[1] - clearStage[1]) + "," + (songNum[2] - clearStage[2]) + "," + (songNum[3] - clearStage[3]) + "," + (songNum[4] - clearStage[4]) + "\n";
			res += "フルコン," + fcStage[0] + "," + fcStage[1] + "," + fcStage[2] + "," + fcStage[3] + "," + fcStage[4] + "," + (songNum[0] - fcStage[0]) + "," + (songNum[1] - fcStage[1]) + "," + (songNum[2] - fcStage[2]) + "," + (songNum[3] - fcStage[3]) + "," + (songNum[4] - fcStage[4]) + "\n";
			res += "鳥コン," + aaafcStage[0] + "," + aaafcStage[1] + "," + aaafcStage[2] + "," + aaafcStage[3] + "," + aaafcStage[4] + "," + (songNum[0] - aaafcStage[0]) + "," + (songNum[1] - aaafcStage[1]) + "," + (songNum[2] - aaafcStage[2]) + "," + (songNum[3] - aaafcStage[3]) + "," + (songNum[4] - aaafcStage[4]) + "\n";
			res += "レゾ," + rzfcStage[0] + "," + rzfcStage[1] + "," + rzfcStage[2] + "," + rzfcStage[3] + "," + rzfcStage[4] + "," + (songNum[0] - rzfcStage[0]) + "," + (songNum[1] - rzfcStage[1]) + "," + (songNum[2] - rzfcStage[2]) + "," + (songNum[3] - rzfcStage[3]) + "," + (songNum[4] - rzfcStage[4]) + "\n";

			return res;
		}

        public static string MakeCSV(Summary[] summaries) {
            string res = "ID,プレイ回数,課金額,総得点,平均CR,総MC,総FC譜面,総鳥コン譜面,総Rz(N),総Rz(A),総Rz(T),総Rz(R),総Rz\n";

            foreach(Summary summary in summaries) {
                string id = summary.player.ID;
                int playNum = summary.results.Sum(result => result.Sum(r => r != null ? int.Parse(r.PlayNum) : 0));
                int money = playNum * 100 / 3;
                int score = summary.results.Sum(result => result.Sum(r => r != null ? int.Parse(r.Score) : 0));
                double average = summary.results.Sum(result => result.Sum(r => r != null ? double.Parse(r.CR) : 0)) / summary.results.Sum(result => result.Count());
                int mc = summary.multis.Sum(multi => multi != null ? int.Parse(multi.Combo) : 0);
                int fc = summary.results.Sum(result => result.Count(r => r != null && r.FCNum != "0"));
                int aaafc = summary.results.Sum(result => result.Count(r => r != null && r.FCNum != "0" && (r.Rank == "AAA" || r.Rank == "Rz")));

                int[] rz = new int[5];
                for(int i = 0; i < 4; i++) {
                    rz[4] += rz[i] = summary.results[i].Count(r => r != null && r.Rank == "Rz");
                }

                res += id + "," + playNum + "," + money + "," + score + "," + average + "," + mc + "," + fc + "," + aaafc + "," + rz[0] + "," + rz[1] + "," + rz[2] + "," + rz[3] + "," + rz[4] + "\n";
            }

            return res;
        }
	}
}
