using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game {
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Ghost[] ghosts;
        [SerializeField] private Pallets pallets;
        [Space(10)]
        [SerializeField] private TMP_Text infoPanel;
        [SerializeField] private TMP_Text score;
        [SerializeField] private TMP_Text livesCount;
        [SerializeField] private TMP_Text level;
        [Space(10)]
        [SerializeField] private GameObject gameMenu;

        private CountdownController _countdownController;
        private static GameManager _instance;
        private GameMenu _gameMenu;
        private static bool _isPause;
        
        private int _level;
        private string _nickName;
        private int _bestScore;
        private Rating _rating;
        
        private const int palletsPerLevel = 248;

        public static GameManager Instance { get => _instance; }
        public bool IsPause { get => _isPause; }
        public TMP_Text InfoPanel { get => infoPanel; }

        public string NickName { get => _nickName; }
        public int CurrentScore { get => player.Score; }
        public int BestScore { get => _bestScore; }
        
        private void Awake()
        {
            _instance = this;
            _rating = SaveSystem.LoadFile("highScore.pacman") as Rating;
            _gameMenu = gameMenu.GetComponent<GameMenu>();
        }

        private void Start()
        {
            SetLevel();
            SetLives(player.Life);
            _countdownController = GetComponent<CountdownController>();
        }

        public void StartGame()
        {
            _isPause = false;
            player.GamePause(_isPause);
        }

        public void StopGame()
        {
            _isPause = true;
            player.GamePause(_isPause);
        }

        public void GameOver()
        {
            foreach (var ghost in ghosts) 
                ghost.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
            
            StopGame();
            infoPanel.text = $"Game Over";
            infoPanel.fontSize = 35;

            _nickName = _rating.rating[_rating.currentPlayerId].Login;
            _bestScore = _rating.rating[_rating.currentPlayerId].BestScore; 
            gameMenu.SetActive(true);
            
            if(BestScore < player.Score) {
                _rating.rating[_rating.currentPlayerId].BestScore = Convert.ToInt32(score.text);
            }

            _rating.Sort();
            SaveSystem.SaveData(_rating, "highScore.pacman");
            
            var playersFullData = SaveSystem.LoadFile("gameData.pacman") as List<PlayerSaveData>;
            
            for(var i = 0; i < playersFullData.Count; i++)
                if (playersFullData[i].Login == _nickName) {
                    playersFullData[i].Score = _bestScore;
                    break;
                }
            
            SaveSystem.SaveData(playersFullData ,"gameData.pacman");
        }

        public void NextLevel() {
            ResetState();
            pallets.ResetState();
            SetLevel();
            
            _countdownController.StartNewCountdown(infoPanel);
        }

        public void LoadNewGame() {
            foreach (var ghost in ghosts) {
                ghost.gameObject.SetActive(true);
                ghost.ResetState();
            }
            player.gameObject.SetActive(true);
            player.NewGameLoad();
            pallets.ResetState();
            
            _gameMenu.TurnOf();
            player.Life = 3;
            _level = 0;
            
            SetLevel();
            SetLives(player.Life);
            SetScore(player.Score);
            
            _countdownController.StartNewCountdown(infoPanel);
        }

        public void PacmanDie()
        {
            if(player.Life >= 0) {
                SetLives(player.Life);
                ResetState();
                _countdownController.StartNewCountdown(infoPanel);
            }
            else 
                GameOver();
        }

        public void SetScore(int numb) {
            score.text = numb.ToString();
            
            if(pallets.EatenPallets == palletsPerLevel)
                NextLevel();
        }

        public void SetLives(int lives) {
            livesCount.text = lives.ToString();
        }

        private void SetLevel() {
            level.text = $"{++_level}";
        }

        public void ResetState() {
            foreach (var ghost in ghosts) {
                ghost.ResetState();
            }
            
            player.ResetState();
        }

        public void PowerPelletEaten() {
            foreach (var ghost in ghosts) {
                ghost.SetGhostMod(GhostMod.Frighten, true);
            }
        }

        public void GhostEaten() {
            player.Score += 200;
        }
    }
}
