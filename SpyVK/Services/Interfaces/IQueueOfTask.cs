namespace SpyVK.Services.Interfaces
{
    public interface IQueueOfTask
    {
        void AddPrimaryTask(Task task);
        void AddSecondaryTask(Task task);
        Task GetTask();
    }
}
