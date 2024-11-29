
namespace Konzit.UI
{
    public interface IPopup
    {
        void OnShow();
        void OnShown();

        void OnHide();
        void OnHidden();
        void OnClosed();
    }

}
