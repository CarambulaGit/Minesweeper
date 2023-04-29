using CodeBase.Logic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Infrastructure.Services {
    public class StaticDataService {
        public GameMode PreviousGameMode {
            get {
                var previousGameMode = PlayerPrefs.GetString(Consts.GameModeKey);
                var result = JsonConvert.DeserializeObject<GameMode>(previousGameMode);
                return result;
            }
            set {
                var json = JsonConvert.SerializeObject(value);
                PlayerPrefs.SetString(Consts.GameModeKey, json);
            }
        }
    }
}