using System;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.Logic.Game;
using CodeBase.Infrastructure.Logic.GameField;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure {
    public class CellView : MonoBehaviour {
        private const string Mine = "*";
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TMP_Text text;

        [Header("Sprite data")] [SerializeField]
        private Sprite hiddenSprite;

        [SerializeField] private Sprite flaggedSprite;
        [SerializeField] private Sprite emptySprite;
        private GameField _field;
        private Point _point;
        private Game _game;

        private Cell Cell => _field.Field.Get(_point);

        public void Initialize(Game game, GameField field, Point point) {
            _game = game;
            _point = point;
            _field = field;
            InitView();
            Subscribe();
        }

        // todo rework
        private void OnMouseOver() {
            if (Input.GetMouseButtonUp(0))
                _game.OpenCell(_point);
            if (Input.GetMouseButtonUp(1))
                _game.SwapFlagState(_point);
        }

        private void InitView() {
            // switch (_cell) {
            //     case EmptyCell
            // }
            spriteRenderer.sprite = hiddenSprite;
            text.text = string.Empty;
            text.gameObject.SetActive(false);
        }

        private void Subscribe() {
            Cell.StateChanged += OnStateChanged;
        }

        private void OnStateChanged() {
            spriteRenderer.sprite = Cell.CurrentState switch {
                Cell.State.Hidden => hiddenSprite,
                Cell.State.Flagged => flaggedSprite,
                Cell.State.Opened => emptySprite,
                _ => throw new ArgumentOutOfRangeException()
            };

            switch (Cell) {
                case CellNearMine cellNearMine when Cell.CurrentState == Cell.State.Opened:
                    text.gameObject.SetActive(true);
                    text.text = cellNearMine.NumOfMinesNear.ToString();
                    break;
                case CellWithMine when Cell.CurrentState == Cell.State.Opened:
                    text.gameObject.SetActive(true);
                    text.text = Mine;
                    break;
            }
        }

        private void Unsubscribe() {
            Cell.StateChanged -= OnStateChanged;
        }

        private void OnDestroy() => Unsubscribe();
    }
}