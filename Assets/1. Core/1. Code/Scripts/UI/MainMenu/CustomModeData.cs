using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.MainMenu
{
    public class CustomModeData : MonoBehaviour
    {
        [SerializeField] private TMP_InputField fieldXSize;
        [SerializeField] private TMP_InputField fieldYSize;
        [SerializeField] private TMP_InputField numOfMines;

        public int FieldXSize => int.Parse(fieldXSize.text);
        public int FieldYSize => int.Parse(fieldYSize.text);
        public int NumOfMines => int.Parse(numOfMines.text);
    }
}