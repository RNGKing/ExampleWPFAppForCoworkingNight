using SimpleToDoList.SubWindows.AddItemWindow.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.SubWindows.AddItemWindow.Events
{
    public delegate void ItemAdded(object sender, ItemAddedArgs args);
    public delegate void ItemUpdated(object sender, ItemUpdateArgs args);
}
