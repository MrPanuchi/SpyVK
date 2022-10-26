namespace SpyVK.Services.newVK
{
    public abstract class TimeTask
    {
        public DateTime TimeCreate { get; private set; }
        public DateTime TimeStartTask { get; private set; }
        public Task Task { get; private set; }
        public TimeTask(Task task)
        {
            TimeCreate = DateTime.Now;
            Task = task;
        }
        public void StartTask()
        {
            TimeStartTask = DateTime.Now;
            Task.Start();
        }
    }
    public class TimeTask<T> : TimeTask
    {
        public TimeTask(Task<T> task) : base(task) { }
        public T GetResult()
        {
            if (!Task.IsCompleted)
            {
                Task.Wait();
            }
            return (Task as Task<T>).Result;
        }
    }
}