using SimpleToDoList.Model;
using SimpleToDoList.SubWindows.AddItemWindow.Events;
using SimpleToDoList.SubWindows.AddItemWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleToDoList.SubWindows.AddItemWindow.View
{
    /// <summary>
    /// Interaction logic for AddUserSubMenu.xaml
    /// </summary>
    public partial class AddUserSubMenu : Window
    {
        AddItemSubMenuViewModel context;

        public SubMenuMode mode = SubMenuMode.NONE;

        public AddUserSubMenu()
        {
            InitializeComponent();
            DataContext = new AddItemSubMenuViewModel();
            context = DataContext as AddItemSubMenuViewModel;
            mode = SubMenuMode.ADD;
        }

        public event ItemAdded OnItemAdded;
        public event ItemUpdated OnItemUpdated;

        ToDoListItem oldItem;

        public void InitializeForUpdate(ToDoListItem item)
        {
            mode = SubMenuMode.UPDATE;
            oldItem = item;
            context.InitializeForUpdate(item);
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            switch (mode)
            {
                case SubMenuMode.ADD:
                    OnItemAdded?.Invoke(this, new Events.EventArgs.ItemAddedArgs { item = context.GetItem() });
                    break;
                case SubMenuMode.UPDATE:
                    var item = context.GetItem();
                    item.Completed = oldItem.Completed;
                    item.CreationTime = oldItem.CreationTime;
                    OnItemUpdated?.Invoke(this, new Events.EventArgs.ItemUpdateArgs { old = oldItem, newItem = item });
                    break;
                case SubMenuMode.NONE:
                    break;
            }
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
