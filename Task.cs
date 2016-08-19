using System;

namespace ToDoListApp
{
    public class Task : IComparable<Task>
    {
        private static int _id = 0;
        private static string _title;

        public int Id { get; private set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }

        public string Title
        {
            get {return _title;}
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(Title, "Please enter a descriptive Title for the task!");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Title should be under 50 characters!");
                }
                _title = value;
            }
        }

        // non default constructor
        public Task(string title, string details, DateTime dueDate = new DateTime())
        {
            Title = title;
            Description = details;
            DueDate = dueDate;
            IsCompleted = false;
            Id = ++_id;
        }

        public int CompareTo(Task other)
        {
            if (other == null)
            {
                return 1;
            }
            if (this.DueDate > other.DueDate)
            {
                return 1;
            }
            if (this.DueDate == other.DueDate)
            {
                return 0;
            }
            
            return -1;
        }
    }
}