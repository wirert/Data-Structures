using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.TaskManager
{
    public class TaskManager : ITaskManager
    {
        private LinkedList<Task> executionQueue = new LinkedList<Task>();
        private Dictionary<string, Task> allTasksById = new Dictionary<string, Task>();

        public void AddTask(Task task)
        {
            executionQueue.AddLast(task);
            allTasksById.Add(task.Id, task);
        }

        public bool Contains(Task task) => allTasksById.ContainsKey(task.Id);

        public void DeleteTask(string taskId)
        {
            if (!allTasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            var task = allTasksById[taskId];
            allTasksById.Remove(taskId);
            if (!task.IsExecuted) 
            { 
                executionQueue.Remove(task);
            }
        }

        public Task ExecuteTask()
        {
            if (executionQueue.Count == 0) 
            {
                throw new ArgumentException();
            }

            var task = executionQueue.First.Value;
            executionQueue.RemoveFirst();
            task.IsExecuted = true;

            return task;
        }

        public IEnumerable<Task> GetAllTasksOrderedByEETThenByName()
            => allTasksById.Values
            .OrderByDescending(t => t.EstimatedExecutionTime)
            .ThenBy(t => t.Name.Length);

        public IEnumerable<Task> GetDomainTasks(string domain)
        {
            var result = executionQueue
            .Where(t => t.Domain == domain);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }

            return result;
        }

        public Task GetTask(string taskId)
        {
            if (!allTasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            return allTasksById[taskId];
        }

        public IEnumerable<Task> GetTasksInEETRange(int lowerBound, int upperBound) 
            => executionQueue
                .Where(t => t.EstimatedExecutionTime >= lowerBound && t.EstimatedExecutionTime <= upperBound);

        public void RescheduleTask(string taskId)
        {
            if (!allTasksById.ContainsKey(taskId) || !allTasksById[taskId].IsExecuted)
            {
                throw new ArgumentException();
            }

            var task = allTasksById[taskId];
            executionQueue.AddLast(task);
            task.IsExecuted = false;
        }

        public int Size() => executionQueue.Count;
    }
}
