using SpyVK.Services.newVK;

namespace SpyVK.Services.Interfaces
{
    public interface IQueueOfTask
    {
        void AddPrimaryTask(TimeTask task);
        void AddSecondaryTask(TimeTask task);
        TimeTask GetTask();
    }
}
