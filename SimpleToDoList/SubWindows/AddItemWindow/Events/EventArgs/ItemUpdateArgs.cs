using SimpleToDoList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.SubWindows.AddItemWindow.Events.EventArgs
{
    public class ItemUpdateArgs
    {
        public ToDoListItem old { get; set; }
        public ToDoListItem newItem { get; set; }
    }
}
