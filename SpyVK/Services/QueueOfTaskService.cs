using SpyVK.Services.Interfaces;

namespace SpyVK.Services
{
    public class QueueOfTaskService : IQueueOfTask
    {
        private readonly Queue<Task> _primaryQueue;
        private readonly Queue<Task> _secondaryQueue;
        public QueueOfTaskService()
        {
            _primaryQueue = new Queue<Task>();
            _secondaryQueue = new Queue<Task>();
        }
        public void AddPrimaryTask(Task task)
        {
            _primaryQueue.Enqueue(task);
        }

        public void AddSecondaryTask(Task task)
        {
            _secondaryQueue.Enqueue(task);
        }

        public Task GetPrimaryTask()
        {
            if (_primaryQueue.Count == 0)
            {
                return null;
            }
            return _primaryQueue.Dequeue();
        }

        public Task GetSecondaryTask()
        {
            if (_secondaryQueue.Count == 0)
            {
                return null;
            }
            return _secondaryQueue.Dequeue();
        }
    }
}
