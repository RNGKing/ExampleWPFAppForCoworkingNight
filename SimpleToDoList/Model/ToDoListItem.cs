using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.Model
{
    public class ToDoListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DueDate { get; set; }
    }
}
