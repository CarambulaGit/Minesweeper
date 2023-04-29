using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {
    private const string WinText = "Win";
    private const string LoseText = "Lose";
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private TMP_Text endGameText;

    public void Initialize(Action restart, Action backToMenu, bool isWin) {
        restartButton.onClick.AddListener(restart.Invoke);
        backToMenuButton.onClick.AddListener(backToMenu.Invoke);
        endGameText.text = isWin ? WinText : LoseText;
    }
}