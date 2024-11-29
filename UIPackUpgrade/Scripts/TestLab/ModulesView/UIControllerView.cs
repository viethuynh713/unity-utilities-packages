/*
 * Author: DevDaoSi
 * @2024
 */
using Konzit.UI.SO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Konzit.UI
{
    public class UIControllerView : MonoBehaviour
    {

        [SerializeField] private PopupSOs _popupContainerSO;
        [SerializeField] private Transform _popupContainer;
        //public Dictionary<string, DefineWidgetOnPopup> _widgetsDict;

        #region Widget
        //public bool IsUsingWidget;
        //[ConditionalField("IsUsingWidget", true)] public List<DefineWidgetOnPopup> _widgets;
        //[Dictionary(] public Dictionary<string, DefineWidgetOnPopup> widgetsDict;
        #endregion

        public List<GameObject> PopupPrefabContainer => _popupContainerSO.PopupPrefabContainer;
        public Transform PopupContainer => _popupContainer;

    }

    [Serializable]
    public class DefineWidgetOnPopup
    {
        public string Name;
    }
}
