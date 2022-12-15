using Game;
using UnityEngine;

public class GameMenu : MonoBehaviour {
    [SerializeField] private GameManager gameManager; 
    [Space(10)]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject rankPanel;

    private GameObject _currentOpen;

    private void OnEnable() {
        OpenPanel(gameOverPanel);
    }

    public void NewGame() {
        gameManager.LoadNewGame();
    }
    
    public void QuitGame() {
        Application.Quit();
    }
    
    public void OpenPanel(GameObject gameObject)
    {
        gameObject.SetActive(true);
        
        if(_currentOpen != null)
            _currentOpen.SetActive(false);
        _currentOpen = gameObject;
    }

    public void TurnOf() {
        rankPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        _currentOpen = null;
        this.gameObject.SetActive(false);
    }
}
