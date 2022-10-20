namespace SpyVK.Services.Interfaces
{
    public interface IQueueOfTask
    {
        void AddPrimaryTask(Task task);
        Task GetPrimaryTask();
        void AddSecondaryTask(Task task);
        Task GetSecondaryTask();
    }
}
