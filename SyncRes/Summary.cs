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

			int[] rzNum = new int[5];
			for(int i = 0; i < 4; i++) {
				rzNum[4] += rzNum[i] = results[i].Count(r => r.Rank == "Rz");
			}
			
			// write
			string res = "";
			res += player.Name + ",Nor,Adv,Tec,Pnd,ALL,,,クラウン," + int.Parse(player.GoldCr) + ",レート・☆\n";
			res += "譜面数," + songNum[0] + "," + songNum[1] + "," + songNum[2] + "," + songNum[3] + "," + songNum[4] + ",,," + crAll + "," + player.SilvCr + "," + player.Rate + "\n";
			res += "プレイ回数," + playNum[0] + "," + playNum[1] + "," + playNum[2] + "," + playNum[3] + "," + playNum[4] + ",課金額,\\" + money + ",," + player.CoppCr + "," + player.Star + "\n";
			res += "平均CR," + averageCR[0].ToString("F2") + "," + averageCR[1].ToString("F2") + "," + averageCR[2].ToString("F2") + "," + averageCR[3].ToString("F2") + "," + averageCR[4].ToString("F2") + ",Nor,Adv,Tec,Pnd,ALL\n";
			res += ",,,達成済,,,,,未達成,,\n";
			res += "Rz," + rzNum[0] + "," + rzNum[1] + "," + rzNum[2] + "," + rzNum[3] + "," + rzNum[4] + "\n";
			return res;
		}
	}
}
