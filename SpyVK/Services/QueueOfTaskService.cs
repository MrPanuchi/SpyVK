using SpyVK.Services.Interfaces;
using SpyVK.Services.newVK;

namespace SpyVK.Services
{
    public class QueueOfTaskService : IQueueOfTask
    {
        private readonly PriorityQueue<TimeTask,int> _priorityQueue;
        public QueueOfTaskService()
        {
            _priorityQueue = new PriorityQueue<TimeTask, int>();
        }
        public void AddPrimaryTask(TimeTask task)
        {
            _priorityQueue.Enqueue(task, 0);
        }

        public void AddSecondaryTask(TimeTask task)
        {
            _priorityQueue.Enqueue(task, 1);
        }

        public TimeTask GetTask()
        {
            TimeTask task;
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
