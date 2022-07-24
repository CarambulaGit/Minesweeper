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
        
        public GameMode GetGameMode()
        {
            if (_gameMode != null) {
                return _gameMode;
            }

            return _staticData.PreviousGameMode ?? GameMode.GameModeWithDifficulty(GameMode.Difficulty.Easy);
        }


    }
}