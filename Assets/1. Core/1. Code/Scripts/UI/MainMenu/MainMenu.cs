using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.MainMenu
{
    public class MainMenu : WindowsRoot
    {
        [SerializeField] private TMP_Dropdown dropDown;
        [SerializeField] private CustomModeData customModeData;
        [SerializeField] private Button startButton;
        private int _indexOfCustomMode;
        private List<GameMode.Difficulty> _gameModes;
        private Action<GameMode> _onGameModeChosen;

        public void Initialize(Action<GameMode> onGameModeChosen)
        {
            _onGameModeChosen = onGameModeChosen;
            _gameModes = Enum.GetValues(typeof(GameMode.Difficulty)).Cast<GameMode.Difficulty>().ToList();
            SetupDropDown();
            startButton.onClick.AddListener(() => StartGame(_gameModes[dropDown.value]));
        }

        private void SetupDropDown()
        {
            _gameModes.ForEach(mode => dropDown.options.Add(new TMP_Dropdown.OptionData {text=mode.ToString()}));
            _indexOfCustomMode = _gameModes.IndexOf(GameMode.Difficulty.Custom);
            dropDown.onValueChanged.AddListener(CheckForCustomData);
            CheckForCustomData(dropDown.value);
        }

        private void CheckForCustomData(int value)
        {
            customModeData.gameObject.SetActive(value == _indexOfCustomMode);
        }

        private void StartGame(GameMode.Difficulty difficulty)
        {
            var gameMode = difficulty == GameMode.Difficulty.Custom
                ? GameMode.CustomMode(customModeData.FieldXSize, customModeData.FieldYSize, customModeData.NumOfMines)
                : GameMode.GameModeWithDifficulty(difficulty);
            _onGameModeChosen.Invoke(gameMode);
        }
    }
}