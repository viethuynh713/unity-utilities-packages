using Konzit.UI;
using UnityEngine;
using VContainer.Unity;

public class GameEntry : IStartable
{
    readonly IUIController _controller;
    public GameEntry(IUIController controller)
    {
        _controller = controller;
    }

    void IStartable.Start()
    {
        //_controller.OpenPopupByName<int>("MainPopup", 12);
    }


}
