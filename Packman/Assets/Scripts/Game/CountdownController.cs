using System.Collections;
using TMPro;
using UnityEngine;

namespace Game {
    public class CountdownController : MonoBehaviour
    {
        [SerializeField] private int countdownTime = 3;

        private TMP_Text _infoPanel;
        private int _countdown = 0;

        private void Start()
        {
            _countdown = countdownTime;
            _infoPanel = GameManager.Instance.InfoPanel;
            StartCoroutine(CountdownToStart());
        }

        public void StartNewCountdown(TMP_Text infoPanel)
        {
            _countdown = countdownTime;
            _infoPanel = infoPanel;
            _infoPanel.fontSize = 35;
            StartCoroutine(CountdownToStart());
        }
    
        IEnumerator CountdownToStart()
        {
            GameManager.Instance.StopGame();
        
            while(_countdown > 0)
            {
                _infoPanel.text = _countdown.ToString();
                yield return new WaitForSeconds(1f);
                _countdown--;
            }
            
            _infoPanel.text = "GO!";
            
            GameManager.Instance.StartGame();

            yield return new WaitForSeconds(1f);
            _infoPanel.text = "";
        }
    }
}
