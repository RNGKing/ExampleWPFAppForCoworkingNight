using SimpleToDoList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.SubWindows.AddItemWindow.Events.EventArgs
{
    public class ItemAddedArgs
    {
        public ToDoListItem item { get; set; }
    }
}
