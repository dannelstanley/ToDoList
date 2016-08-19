using System.Collections.Generic;
using System.Linq;

namespace ToDoListApp
{
    public class Repository
    {
        private List<Task> _taskList;

        public Repository()
        {
            _taskList = new List<Task>();
        }

        public void Add(Task task)
        {
            _taskList.Add(task);
            _taskList.Sort();
        }
        
        /// <summary>
        /// Attempts to delete taak from list. Returns false if unable to do so.
        /// </summary>
        /// <param name="taskId">integer value of task id to delete</param>
        /// <returns>boolean success</returns>
        public bool Delete(int taskId)
        {
            if (_taskList.Count > 0)
            {
                foreach (var task in _taskList)
                {
                    if (task.Id == taskId)
                    {
                        _taskList.Remove(task);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Attempts to mark identified task as complete. Returns false if unable to do so
        /// or there are no tasks in the list.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>boolean success</returns>
        public bool CompleteTask(int taskId)
        {
            // no tasks in list
            if (_taskList.Count <= 0) return false;

            foreach (var task in _taskList)
            {
                if (task.Id == taskId)
                {
                    task.IsCompleted = true;
                    return true;
                }
            }

            // id was not found
            return false;
        }

        /// <summary>
        /// Returns a list of tasks not marked IsComplete
        /// </summary>
        /// <returns>List of tasks</returns>
        public List<Task> GetPending()
        {
            return _taskList.Where(p => p.IsCompleted == false).ToList();
        }

        /// <summary>
        /// Returns a list of all tasks marked IsComplete
        /// </summary>
        /// <returns>List of Tasks</returns>
        public List<Task> GetCompleted()
        {
            return _taskList.Where(p => p.IsCompleted).ToList();
        }

        /// <summary>
        /// Returns all Tasks in a list.
        /// </summary>
        /// <returns>List of Tasks</returns>
        public List<Task> GetAll()
        {
            return _taskList;
        }
    }
}
