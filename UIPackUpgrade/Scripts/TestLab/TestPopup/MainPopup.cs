using Konzit.UI;
using UnityEngine;

public class MainPopup : BasePopup
{
    public override void OnShow()
    {
        Debug.Log($"Show main popup {Parameter}");
    }

}
