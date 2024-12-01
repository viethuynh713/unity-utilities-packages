/*
 * Author: DevDaoSi
 * @2024
 */
using UnityEngine;
using System;
using System.ComponentModel;
using VPackages.Utilities.VContainerHelper;

namespace Konzit.UI
{
    public class BasePopup : AutoInjectMonoBehaviour, IPopup
    {
        [Header("Function Check")]
        [Description("Check some boolean variable under to use function or use build in animation (animation will be update in another version of package)")]
        [SerializeField] private bool _alwaysOnTop = false;

        protected object Parameter;
        //Show template
        internal void PopupInitialize(Action popupActive, object param = null)
        {
            if(_alwaysOnTop) this.transform.SetAsLastSibling();

            OnShow();

            if(param != null) 
                Parameter = param;
            popupActive?.Invoke();
            OnShown();
        }

        // Hide template
        internal void PopupHide(Action popupHide)
        {
            OnHide();
            popupHide?.Invoke();
            OnHidden();

            if(Parameter != null) Parameter = null;
        }

        // Close template
        internal void PopupClose(Action popupClose)
        {
            OnClosed();
            popupClose?.Invoke();
        }


        #region Popup template implementation
        public virtual void OnShow() { }
        public virtual void OnShown() { }
        public virtual void OnHide() { }
        public virtual void OnHidden() { }
        public virtual void OnClosed() { }
        #endregion

        protected override void OnDependenciesResolved()
        {
            
        }
    }
}
