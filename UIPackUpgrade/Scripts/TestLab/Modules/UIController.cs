/*
 * Author: DevDaoSi
 * @2024
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Konzit.UI.SO;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VPackage.AudioSystem;

namespace Konzit.UI
{
    public class UIController : MonoBehaviour, IUIController
    {
        private Dictionary<string, IPopup> _popupDict;
        [SerializeField] private PopupSOs data;
        [SerializeField] private Transform parent;
        [Inject] private IObjectResolver _resolver;

        #region Constructor

        public void Start()
        {
            _popupDict = new Dictionary<string, IPopup>();
            
        }
        #endregion

        #region Interface implement

        public BasePopup OpenPopupByName(string popupName)
        {
            if (!_popupDict.ContainsKey(popupName))
            {
                // create popup by prefab in prefab container
                // if not have any popup prefab has name same with popupName parameter -> null object dp
                // if have, create new popup, and add it into dictionary to call after, show that popup
                // when create popup

                var popup = data.PopupPrefabContainer.FirstOrDefault(p => p.gameObject.name == popupName);
                if (popup == null)
                {
                    Debug.LogError($"NotFound {popupName}");
                    return null;
                }

                return CreatePopup(popup, popupName);
            }
            else
            {
                var popup = (BasePopup)_popupDict[popupName];
                popup.gameObject.SetActive(true);
                return popup;
            }
        }

        public BasePopup OpenPopupByName<T>(string popupName, T param, Action callback = null)
        {
            if (!_popupDict.ContainsKey(popupName))
            {
                // create popup by prefab in prefab container
                // if not have any popup prefab has name same with popupName parameter -> null object dp
                // if have, create new popup, and add it into dictionary to call after, show that popup
                // when create popup

                var popup = data.PopupPrefabContainer.FirstOrDefault(p => p.gameObject.name == popupName);
                if (popup == null)
                {
                    Debug.LogError($"NotFound {popupName}");
                    return null;
                }
                return CreatePopup(popup, popupName, param);
            }
            else
            {
                var popup = (BasePopup)_popupDict[popupName];
                popup.PopupInitialize(() => popup.gameObject.SetActive(true), param);
                return popup;
            }
        }

        private BasePopup CreatePopup<T>(GameObject popup, string popupName, T param)
        {
            var initPopup = _resolver.Instantiate(popup, parent).GetComponent<BasePopup>();
            initPopup.gameObject.SetActive(false);
            initPopup.PopupInitialize(() => initPopup.gameObject.SetActive(true), param);
            _popupDict.Add(popupName, initPopup);
            return initPopup;
        }

        private BasePopup CreatePopup(GameObject popup, string popupName)
        {
            var initPopup = _resolver.Instantiate(popup, parent).GetComponent<BasePopup>();
            initPopup.gameObject.SetActive(false);
            initPopup.PopupInitialize(() => initPopup.gameObject.SetActive(true));
            _popupDict.Add(popupName, initPopup);
            return initPopup;
        }

        /*
         * Function under this handle disappear of popups
         * Hide popup handle hide but not dispose the popup, using this when need to use the popup after
         * Close popup handle dispose the popup, using this when no need to use popup anymore
         */
        public void HidePopupByName(string popupName, Action callback = null)
        {
            if (!_popupDict.ContainsKey(popupName))
            {
                Debug.LogWarning($"<color=red>Object not contain on Dictionary: </color> {popupName}");
                return;
            }

            var popup = (BasePopup)_popupDict[popupName];
            popup.PopupHide(() => popup.gameObject.SetActive(false));

            callback?.Invoke();
        }

        public void ClosePopup(string popupName, Action callback = null)
        {
            Debug.Log("Popup disappear and destroy");
            if (!_popupDict.ContainsKey(popupName))
            {
                Debug.LogWarning($"<color=red>Object not contain on Dictionary: </color> {popupName}");
                return;
            }

            var popup = (BasePopup)_popupDict[popupName];
            popup.PopupClose(() => DestroyPopup(popup));
        }

        private void DestroyPopup(IPopup popup)
        {
            var popupObj = (BasePopup)popup;
            GameObject.Destroy(popupObj.gameObject);
        }

        #endregion
    }
}