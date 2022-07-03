using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

public class ServiceRegister : MonoInstaller {
    [SerializeField] private LoadingCurtain curtainPrefab;
    private IGameFactory _gameFactory;

    public override void InstallBindings() {
        BindCurtain();
        BindAssetProvider();
        BindFactory();
    }

    private void BindAssetProvider() {
        Container
            .Bind<IAssetsProvider>()
            .To<AssetsProvider>()
            .AsSingle();
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