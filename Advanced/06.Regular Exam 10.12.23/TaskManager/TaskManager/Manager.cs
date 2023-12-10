namespace TaskManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Manager : IManager
    {
        private Dictionary<string, Task> tasksById = new Dictionary<string, Task>();

        public int Size() => tasksById.Count;

        public bool Contains(string taskId) => tasksById.ContainsKey(taskId);

        public void AddTask(Task task)
        {
            if (Contains(task.Id))
            {
                throw new ArgumentException();
            }

            tasksById.Add(task.Id, task);
        }

        public void RemoveTask(string taskId)
        {
            if (!Contains(taskId))
            {
                throw new ArgumentException();
            }
            var task = tasksById[taskId];

            foreach (var dependancy in task.Dependancies)
            {
                dependancy.Dependants.Remove(task);
            }

            foreach (var dependant in task.Dependants)
            {
                dependant.Dependancies.Remove(task);
            }

            tasksById.Remove(taskId);
        }

        public Task Get(string taskId)
        {
            if (!Contains(taskId))
            {
                throw new ArgumentException();
            }

            return tasksById[taskId];
        }

        public void AddDependency(string taskId, string dependentTaskId)
        {
            if (!Contains(taskId) || !Contains(dependentTaskId))
            {
                throw new ArgumentException();
            }

            var task = tasksById[taskId];
            var dependentTask = tasksById[dependentTaskId];

            if (HasDependancy(dependentTask, task))
            {
                throw new ArgumentException();
            }

            task.Dependancies.Add(dependentTask);
            dependentTask.Dependants.Add(task);
        }

        public void RemoveDependency(string taskId, string dependentTaskId)
        {
            if (!Contains(taskId) || !Contains(dependentTaskId))
            {
                throw new ArgumentException();
            }

            var task = tasksById[taskId];
            var dependentTask = tasksById[dependentTaskId];

            if (!task.Dependancies.Contains(dependentTask))
            {
                throw new ArgumentException();
            }

            task.Dependancies.Remove(dependentTask);
            dependentTask.Dependants.Remove(task);
        }

        public IEnumerable<Task> GetDependencies(string taskId)
        {
            if (!Contains(taskId))
            {
                return Enumerable.Empty<Task>();
            }

            var task = tasksById[taskId];            

            return GetDependencies(task);
        }

        public IEnumerable<Task> GetDependents(string taskId)
        {
            if (!Contains(taskId))
            {
                return Enumerable.Empty<Task>();
            }

            var task = tasksById[taskId];

            return GetDependants(task);
        }

        private IEnumerable<Task> GetDependants(Task task)
        {
            var queue = new Queue<Task>();
            foreach (var dep in task.Dependants)
            {
                queue.Enqueue(dep);
            }
            var result = new List<Task>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var curDependant in current.Dependants)
                {
                    queue.Enqueue(curDependant);
                }
            }

            return result;
        }

        private bool HasDependancy(Task task, Task dependOnTask)
        {
            var queue = new Queue<Task>();
            foreach (var dep in task.Dependancies)
            {
                queue.Enqueue(dep);
            }            

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Dependancies.Contains(dependOnTask))
                {
                    return true;
                }

                foreach (var curDependant in current.Dependancies)
                {
                    queue.Enqueue(curDependant);
                }
            }

            return false;
        }

        private IEnumerable<Task> GetDependencies(Task task)
        {
            var queue = new Queue<Task>();
            foreach (var dep in task.Dependancies)
            {
                queue.Enqueue(dep);
            }
            var result = new List<Task>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var curDependant in current.Dependancies)
                {
                    queue.Enqueue(curDependant);
                }
            }

            return result;
        }
    }
}