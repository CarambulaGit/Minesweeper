using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class StaticDataService {
        public GameMode PreviousGameMode {
            get {
                var previousGameMode = PlayerPrefs.GetString(Consts.GameModeKey);
                var result = JsonUtility.FromJson<GameMode>(previousGameMode);
                return result;
            }
            set {
                var json = JsonUtility.ToJson(value);
                PlayerPrefs.SetString(Consts.GameModeKey, json);
            }
        }
    }
}