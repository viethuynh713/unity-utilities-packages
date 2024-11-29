
using UnityEngine;
using Konzit.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private UIControllerView _uiControllerView;

    private void Awake()
    {
        //UIController _uiController = new UIController(_uiControllerView);
        // KonzitContainer.SetModule<IUIController, UIController>(_uiControllerView);
        //KonzitContainer.SetModule<ILogger, Logger>();

        //KonzitContainer.SetModule<ControlWithLogger, ControlWithLogger>();
        //var control = KonzitContainer.GetModule<ControlWithLogger>();
        //control.Print("Konzit container say hello!");

        // var control = KonzitContainer.GetModule<IUIController>();
        //control.OpenPopupByName("MainPopup", 10);
    }

    
}

#region TEST
public interface ILogger
{
    void Print(string message);
}

public class Logger : ILogger
{
    public void Print(string message)
    {
        Debug.Log("game start with di: " + message);
    }
}

public class ControlWithLogger
{
    private readonly ILogger _logger;
    public ControlWithLogger(ILogger logger)
    {
        _logger = logger;
    }

    internal void Print(string message)
    {
        _logger.Print(message);
    }
}
#endregion