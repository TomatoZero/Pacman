using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;


namespace Ui
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject singingPanel;
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject errorPanel;
        [Space(10)]
        [SerializeField] private TMP_InputField login;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_Text message;
        
        private GameObject _currentOpen;
        private List<PlayerSaveData> _savedData;
        private Rating _rating;

        private void Start()
        {
            _currentOpen = singingPanel;

            var objSave = SaveSystem.LoadFile("gameData.pacman");

            if (objSave == null)
                _savedData = new List<PlayerSaveData>();
            else
                _savedData = objSave as List<PlayerSaveData>;
            
            _rating = new Rating();

            foreach (var data in _savedData) {
                _rating.rating.Add(new PlayerData(data.Login, data.Score, false));
            }
        }

        public void OpenPanel(GameObject gameObject)
        {
            gameObject.SetActive(true);
            _currentOpen.SetActive(false);
            _currentOpen = gameObject;
        }
        
        public void Login() {
            var inputLogin = login.text.ToLowerInvariant();
            var inputPassword = password.text;

            if (inputLogin == "" || inputPassword == "") {
                ShowError("The login or password is incorrect. ");
                return;
            }

            for (int i = 0; i < _savedData.Count; i++) {
                if (inputLogin == _savedData[i].Login) {
                    if (inputPassword == _savedData[i].Password) {
                        _rating.currentPlayerId = i;
                        SaveSystem.SaveData(_rating as object, "highScore.pacman");
                        login.text = "";
                        password.text = "";
                        
                        OpenPanel(menuPanel);
                        return;
                    }
                    else {
                        ShowError("Incorrect password");
                        return;
                    }
                }
            }
            ShowError("Login dont exist");
        }
  
        public void Register()
        {
            var inputLogin = login.text.ToLowerInvariant();
            var inputPassword = password.text;

            if (inputLogin == "" || inputPassword == "") {
                ShowError("The login or password is incorrect. ");
                return;
            }
            
            for (int i = 0; i < _savedData.Count; i++) {
                if (inputLogin == _savedData[i].Login) {
                    ShowError("This login is already registered");
                    return;
                }
            }
            
            _savedData.Add(new PlayerSaveData(inputLogin.ToLowerInvariant(), inputPassword, 0));
            SaveSystem.SaveData(_savedData as object, "gameData.pacman");

            _rating.rating.Add(new PlayerData(inputLogin.ToLowerInvariant(), 0, true));
            _rating.currentPlayerId = _rating.rating.Count - 1;
            
            SaveSystem.SaveData(_rating as object, "highScore.pacman");
            
            login.text = "";
            password.text = "";
            
            OpenPanel(menuPanel);
        }

        private void ShowError(string errorMessage) {
            message.text = errorMessage;
            errorPanel.SetActive(true);
        }
        
        public void Continue() {
            errorPanel.SetActive(false);
        }
        
        public void QuitGame() {
            Application.Quit();
        }

        public void Setup() {
            Application.LoadLevel("SampleScene");
        }
    }
}