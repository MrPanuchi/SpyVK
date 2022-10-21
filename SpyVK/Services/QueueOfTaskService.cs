using SpyVK.Services.Interfaces;

namespace SpyVK.Services
{
    public class QueueOfTaskService : IQueueOfTask
    {
        private readonly PriorityQueue<Task,int> _priorityQueue;
        public QueueOfTaskService()
        {
            _priorityQueue = new PriorityQueue<Task, int>();
        }
        public void AddPrimaryTask(Task task)
        {
            _priorityQueue.Enqueue(task, 0);
        }

        public void AddSecondaryTask(Task task)
        {
            _priorityQueue.Enqueue(task, 1);
        }

        public Task GetTask()
        {
            Task task;
            bool result = _priorityQueue.TryDequeue(out task, out _);
            if (result)
            {
                return task;
            }
            else
            {
                return null;
            }
        }
    }
}
