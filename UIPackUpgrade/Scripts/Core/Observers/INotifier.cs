namespace Konzit.Core.Observer
{
    public interface INotifier
    {
        void AssignListener(IListener listener);
        void RemoveListener(IListener listener);
        void NotiToAllListener();
        void NotiToTargetListener(IListener listener);
    }
}
