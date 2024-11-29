/*
 * Author: DevDaoSi
 * @2024
 */
using System;

namespace Konzit.UI
{
    public interface IUIController
    {
        BasePopup OpenPopupByName(string popupName);
        BasePopup OpenPopupByName<T>(string popupName, T param, Action callback = null);
        void HidePopupByName(string popupName, Action callback = null);
        void ClosePopup(string popupName, Action callback = null);
    }
}

