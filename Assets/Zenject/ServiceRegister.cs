using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

public class ServiceRegister : MonoInstaller {
    [SerializeField] private LoadingCurtain curtainPrefab;
    private IGameFactory _gameFactory;

    public override void InstallBindings() {
        BindCurtain();
        BindFactory();
    }

    private void BindCurtain() {
        Container
            .Bind<LoadingCurtain>()
            .FromComponentInNewPrefab(curtainPrefab)
            .AsSingle();
    }

    private void BindFactory() {
        Container
            .Bind<IGameFactory>()
            .To<GameFactory>()
            .AsSingle();
    }
}