using SimpleToDoList.Model;
using SimpleToDoList.SubWindows.AddItemWindow.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.SubWindows.AddItemWindow.ViewModel
{
    public enum SubMenuMode
    {
        ADD,
        UPDATE,
        NONE
    }

    class AddItemSubMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SubMenuMode mode = SubMenuMode.NONE;

        DateTime selectedTime = DateTime.Now;
        public DateTime SelectedDateTime
        {
            get
            {
                return selectedTime;
            }
            set
            {
                selectedTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedDateTime"));
            }
        }

        string name = string.Empty;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        string description = string.Empty;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }
       
        #region Value Operations

        public ToDoListItem GetItem()
        {
            return new ToDoListItem
            {
                Name = this.Name,
                CreationTime = DateTime.Now,
                Description = this.Description,
                DueDate = this.SelectedDateTime,
                
            };
        }

        public void InitializeForUpdate(ToDoListItem old)
        {
            Name = old.Name;
            Description = old.Description;
            SelectedDateTime = old.DueDate;
        }

        #endregion

    }
}
