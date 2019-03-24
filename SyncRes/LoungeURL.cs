namespace SyncRes {
    public enum Difficulty {
        Nor = 0, Adv = 1, Tec = 2, Pnd = 3, Mul = 4
    }

    static class LoungeURL {
        public static readonly string Login = "https://lounge.synchronica.jp/";
        public static readonly string Home = "https://lounge.synchronica.jp/MyPage";
        public static readonly string ScoreHome = "https://lounge.synchronica.jp/ScoreData";
        public static string Player(string userId) {
            return "https://lounge.synchronica.jp/Friend/info/" + userId;
        }
        public static string Score(string userId, string songId, Difficulty difficulty) {
            if(difficulty == Difficulty.Mul) {
                return Multi(userId, songId);
            } else {
                return "https://lounge.synchronica.jp/PersonalScore/detail/" + userId + "/" + songId + "?level=" + (int)difficulty;
            }
        }
        public static string Multi(string userId, string songId) {
            return "https://lounge.synchronica.jp/PersonalScore/combo/" + userId + "/" + songId;
        }
        public static string ScoreRanking(string userId, string songId, Difficulty difficulty) {
            if(difficulty == Difficulty.Mul) {
                return MultiRanking(userId, songId);
            } else {
                return "https://lounge.synchronica.jp/ScoreRanking/detail/" + userId + "/0?s_id=" + songId + "&level=" + (int)difficulty;
            }
        }
        public static string MultiRanking(string userId, string songId) {
            return "https://lounge.synchronica.jp/ComboRanking/detail/" + userId + "/0?s_id=" + songId;
        }
    }
}
