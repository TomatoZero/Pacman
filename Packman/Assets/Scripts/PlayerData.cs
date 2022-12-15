using System;
using System.Collections.Generic;
using System.Linq;

namespace Game {
    [Serializable]
    public class PlayerData {
        public string Login { get; set; }
        public int BestScore { get; set; }
        public bool IsNewPlayer { get; set; }

        public PlayerData(string login, int bestScore, bool isNewPlayer) {
            Login = login;
            BestScore = bestScore;
            IsNewPlayer = isNewPlayer;
        }
    }

    [Serializable]
    public class Rating {
        public List<PlayerData> rating { get; set; }
        public int currentPlayerId { get; set; }
        
        public Rating() {
            rating = new List<PlayerData>();
        }

        public void Sort() {
            var arrRating = rating.ToArray();
            string currentLogin = rating[currentPlayerId].Login;
            Array.Sort(arrRating, (data, playerData) => -data.BestScore.CompareTo(playerData.BestScore));

            for (var i = 0; i < arrRating.Length; i++)
                if (arrRating[i].Login == currentLogin)
                    currentPlayerId = i;

            rating = arrRating.ToList();
        }
    }
}