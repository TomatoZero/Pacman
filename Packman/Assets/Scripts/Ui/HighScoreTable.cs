using Game;
using UnityEngine;

public class HighScoreTable : MonoBehaviour {
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _template;

    private void OnEnable() {
        _template.gameObject.SetActive(false);

        var highScore = SaveSystem.LoadFile("highScore.pacman") as Rating;
        highScore.Sort();
        
        for (var i = 0; i < highScore.rating.Count && i < 10; i++) {
            CreateLine((i+1).ToString(), highScore.rating[i].Login, highScore.rating[i].BestScore.ToString(), i);
        }

        if (highScore.currentPlayerId > 9) {
            CreateLine("...", "...", "...", 10);
            CreateLine(highScore.currentPlayerId.ToString(), highScore.rating[highScore.currentPlayerId].Login, highScore.rating[highScore.currentPlayerId].BestScore.ToString(), 11);
        }
    }

    private void CreateLine(string position, string login, string bestScore, int index) {
        var entryTransform = Instantiate(_template, _container);
        var entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -20 * index);
        entryTransform.gameObject.SetActive(true);

        var newTemplate = (entryTransform.GetComponent<HighscoreTemplate>());
        newTemplate.SetData(position, login, bestScore);
    }
}
