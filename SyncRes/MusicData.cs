using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncRes {
    static class MusicData {
        // 表。曲ID - [1:サヨナラ2:エイプリル3:終曲] - パンドラ(1ならあり)
        static readonly int[][] musicData = {
            new int[]{1,0,1},
            new int[]{2,0,1},
            new int[]{3,0,0},
            new int[]{4,0,1},
            new int[]{6,1,0},
            new int[]{7,0,0},
            new int[]{8,0,0},
            new int[]{9,0,1},
            new int[]{10,0,0},
            new int[]{11,0,1},
            new int[]{12,1,0},
            new int[]{13,0,0},
            new int[]{14,0,0},
            new int[]{15,0,0},
            new int[]{17,1,1},
            new int[]{18,0,0},
            new int[]{19,1,0},
            new int[]{20,0,0},
            new int[]{21,1,0},
            new int[]{22,1,0},
            new int[]{23,0,0},
            new int[]{24,0,0},
            new int[]{25,0,0},
            new int[]{26,0,1},
            new int[]{27,0,0},
            new int[]{28,0,0},
            new int[]{29,0,1},
            new int[]{30,0,0},
            new int[]{31,0,0},
            new int[]{32,0,0},
            new int[]{33,0,1},
            new int[]{34,1,0},
            new int[]{35,0,0},
            new int[]{36,0,0},
            new int[]{37,0,0},
            new int[]{38,0,0},
            new int[]{39,0,0},
            new int[]{40,0,0},
            new int[]{41,1,1},
            new int[]{43,1,0},
            new int[]{45,1,0},
            new int[]{46,0,0},
            new int[]{47,0,0},
            new int[]{48,0,0},
            new int[]{49,0,0},
            new int[]{50,0,0},
            new int[]{51,0,0},
            new int[]{52,0,0},
            new int[]{53,0,0},
            new int[]{55,0,0},
            new int[]{56,0,0},
            new int[]{58,0,0},
            new int[]{59,0,1},
            new int[]{60,0,0},
            new int[]{61,0,0},
            new int[]{62,0,0},
            new int[]{64,0,0},
            new int[]{65,0,0},
            new int[]{66,0,0},
            new int[]{71,0,0},
            new int[]{72,0,0},
            new int[]{73,0,0},
            new int[]{74,0,0},
            new int[]{75,0,0},
            new int[]{76,0,0},
            new int[]{77,0,1},
            new int[]{78,0,0},
            new int[]{80,0,0},
            new int[]{81,0,0},
            new int[]{87,0,0},
            new int[]{88,0,0},
            new int[]{89,0,0},
            new int[]{90,0,0},
            new int[]{91,0,0},
            new int[]{92,0,0},
            new int[]{93,0,0},
            new int[]{94,0,0},
            new int[]{95,0,1},
            new int[]{96,0,1},
            new int[]{97,0,1},
            new int[]{98,0,0},
            new int[]{99,0,0},
            new int[]{100,0,0},
            new int[]{101,0,0},
            new int[]{102,0,0},
            new int[]{103,0,1},
            new int[]{104,0,0},
            new int[]{105,0,0},
            new int[]{106,0,0},
            new int[]{107,1,0},
            new int[]{108,1,0},
            new int[]{109,1,0},
            new int[]{110,1,0},
            new int[]{111,0,1},
            new int[]{112,0,0},
            new int[]{113,0,0},
            new int[]{114,0,0},
            new int[]{115,1,0},
            new int[]{116,0,0},
            new int[]{117,0,0},
            new int[]{118,0,0},
            new int[]{119,0,0},
            new int[]{120,0,0},
            new int[]{121,0,0},
            new int[]{122,0,0},
            new int[]{123,0,0},
            new int[]{125,0,0},
            new int[]{126,0,0},
            new int[]{127,0,0},
            new int[]{128,0,0},
            new int[]{129,1,0},
            new int[]{130,1,0},
            new int[]{131,1,0},
            new int[]{132,1,0},
            new int[]{133,0,0},
            new int[]{134,0,0},
            new int[]{135,0,0},
            new int[]{136,0,0},
            new int[]{137,0,0},
            new int[]{138,0,0},
            new int[]{139,0,0},
            new int[]{140,0,0},
            new int[]{141,0,0},
            new int[]{142,0,0},
            new int[]{143,0,0},
            new int[]{144,0,0},
            new int[]{145,0,0},
            new int[]{146,0,0},
            new int[]{147,0,0},
            new int[]{148,0,0},
            new int[]{149,0,0},
            new int[]{150,1,0},
            new int[]{151,0,0},
            new int[]{152,0,0},
            new int[]{153,0,0},
            new int[]{154,0,0},
            new int[]{156,0,0},
            new int[]{157,0,0},
            new int[]{158,0,0},
            new int[]{159,0,0},
            new int[]{160,0,0},
            new int[]{161,0,0},
            new int[]{162,0,0},
            new int[]{163,0,0},
            new int[]{164,0,0},
            new int[]{165,0,0},
            new int[]{166,0,0},
            new int[]{167,0,0},
            new int[]{168,0,0},
            new int[]{169,0,0},
            new int[]{170,0,0},
            new int[]{171,0,0},
            new int[]{173,0,0},
            new int[]{174,0,0},
            new int[]{175,0,0},
            new int[]{176,0,0},
            new int[]{177,0,0},
            new int[]{178,0,0},
            new int[]{179,0,0},
            new int[]{180,0,0},
            new int[]{184,0,0},
            new int[]{185,0,0},
            new int[]{186,0,0},
            new int[]{187,0,0},
            new int[]{188,0,0},
            new int[]{189,0,0},
            new int[]{190,0,0},
            new int[]{191,0,0},
            new int[]{192,0,0},
            new int[]{193,0,0},
            new int[]{194,0,0},
            new int[]{195,0,0},
            new int[]{196,0,0},
            new int[]{197,0,0},
            new int[]{198,0,0},
            new int[]{199,0,0},
            new int[]{200,0,0},
            new int[]{201,0,1},
            new int[]{401,2,1},
            new int[]{496,3,1},
            new int[]{505,2,0}
        };
        
        /// <summary>
        /// 存在する(した)曲IDの配列を返す
        /// </summary>
        public static IEnumerable<int> GetExistsMusicIDs() {
            List<int> ret = new List<int>();
            foreach(int[] musicDatum in musicData) {
                ret.Add(musicDatum[0]);
            }
            return ret;
        }

        /// <summary>
        /// パンドラがある(あった)曲IDの配列を返す
        /// </summary>
        public static IEnumerable<int> GetPndMusicIDs() {
            List<int> ret = new List<int>();
            foreach(int[] musicDatum in musicData) {
                if(musicDatum[2] == 1){
                    ret.Add(musicDatum[0]);
                }
            }
            return ret;
        }

        /// <summary>
        /// サヨナラでない曲IDの配列を返す
        /// </summary>
        public static IEnumerable<int> GetNonDeletedIDs() {
            List<int> ret = new List<int>();
            foreach(int[] musicDatum in musicData) {
                if(musicDatum[1] != 1) {
                    ret.Add(musicDatum[0]);
                }
            }
            return ret;
        }

        /// <summary>
        /// サヨナラでなくかつパンドラがある曲IDの配列を返す
        /// </summary>
        public static IEnumerable<int> GetNonDeletedPndIDs() {
            List<int> ret = new List<int>();
            foreach(int[] musicDatum in musicData) {
                if(musicDatum[1] != 1 && musicDatum[2] == 1) {
                    ret.Add(musicDatum[0]);
                }
            }
            return ret;
        }

        /// <summary>
        /// レート対象の曲IDの配列を返す
        /// </summary>
        public static IEnumerable<int> GetRateTargetIDs(Difficulty difficulty) {
            List<int> ret = new List<int>();
            foreach(int[] musicDatum in musicData) {
                if(difficulty != Difficulty.Pnd) {
                    if(musicDatum[1] == 0 || musicDatum[1] == 3) ret.Add(musicDatum[0]);
                } else {
                    if(musicDatum[1] == 0 && musicDatum[2] == 1) ret.Add(musicDatum[0]);
                }
            }
            return ret;
        }

    }
}
