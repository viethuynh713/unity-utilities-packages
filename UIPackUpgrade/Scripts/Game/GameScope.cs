using Konzit.UI;
using Kozits.ZooCoffee.Manager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    // [SerializeField] PlayerManager _playerManager;

    /*    #region Mini Game Manager
        [SerializeField] private MergeGameManager _MergeGameManager;
        #endregion*/
    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("Run on this");
        builder.RegisterComponentInHierarchy<UIControllerView>();
        builder.Register<IUIController, UIController>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameEntry>(Lifetime.Singleton);

        // builder.RegisterComponent(_playerManager);
        builder.Register<MergeGameManager>(Lifetime.Singleton).AsSelf();

    }
}
