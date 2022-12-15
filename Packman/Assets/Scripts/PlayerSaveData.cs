using System;

namespace Game {
    [Serializable]
    public class PlayerSaveData {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }

        public PlayerSaveData(string login, string password, int score) {
            Login = login;
            Password = password;
            Score = score;
        }
    }
}