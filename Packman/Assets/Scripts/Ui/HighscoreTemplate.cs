using TMPro;
using UnityEngine;

public class HighscoreTemplate : MonoBehaviour 
{
    [SerializeField] private TMP_Text position;
    [SerializeField] private TMP_Text login;
    [SerializeField] private TMP_Text score;

    public void SetData(string position, string login, string score) {
        this.position.text = position;
        this.login.text = login;
        this.score.text = score;
    }
}
