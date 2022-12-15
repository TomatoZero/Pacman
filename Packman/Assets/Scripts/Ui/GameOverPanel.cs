using Game;
using TMPro;
using UnityEngine;

namespace Ui {
    public class GameOverPanel : MonoBehaviour {
        [SerializeField] private TMP_Text nickname;
        [SerializeField] private TMP_Text score;
        [SerializeField] private TMP_Text prevScore;
        [Space(10)] 
        [SerializeField] private GameManager manager;
        
        private void OnEnable() {
            nickname.text = manager.NickName;
            score.text = manager.CurrentScore.ToString();
            prevScore.text = manager.BestScore.ToString();
        }
    }
}