using CodeBase.Logic;
using Zenject;

namespace CodeBase.Infrastructure.Services {
    public class GameModeService {
        private GameMode _gameMode;
        private StaticDataService _staticData;

        [Inject]
        private GameModeService(StaticDataService staticData) {
            _staticData = staticData;
        }
        
        public GameMode GetGameMode() {
            GameMode result;
            if (_gameMode != null) {
                result = _gameMode;
            } else if (_staticData.PreviousGameMode != null) {
                result = _staticData.PreviousGameMode;
            }
            else {
                result = GameMode.EasyMode();
            }

            return result;
        }


    }
}