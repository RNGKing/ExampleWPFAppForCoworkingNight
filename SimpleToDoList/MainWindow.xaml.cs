using Microsoft.Win32;
using SimpleToDoList.Model;
using SimpleToDoList.SubWindows.AddItemWindow.View;
using SimpleToDoList.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ToDoListViewModel context;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ToDoListViewModel();
            context = DataContext as ToDoListViewModel;
            context.OnToDoListUpdated += Context_OnToDoListUpdated;
        }

        private void Context_OnToDoListUpdated(object sender, ViewModel.Events.EventArgs.ToDoListUpdatedArgs args)
        {
            
            dgToDoList.ItemsSource = null;
            dgToDoList.ItemsSource = context.ItemsView;
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            // This is where we bring up the add sub menu
            var subMenu = new AddUserSubMenu();
            subMenu.OnItemAdded += SubMenu_OnItemAdded;
            subMenu.Closing += SubMenu_Closing;
            subMenu.ShowDialog();
        }

        private void SubMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(sender is AddUserSubMenu menu)   
            {
                switch (menu.mode)
                {
                    case SubWindows.AddItemWindow.ViewModel.SubMenuMode.ADD:
                        menu.OnItemAdded -= SubMenu_OnItemAdded;
                        break;
                    case SubWindows.AddItemWindow.ViewModel.SubMenuMode.UPDATE:
                        menu.OnItemUpdated -= UpdateMenu_OnItemUpdated;
                        break;
                    case SubWindows.AddItemWindow.ViewModel.SubMenuMode.NONE:
                        break;
                }
                menu.Closing -= SubMenu_Closing;
            }
        }

        private void SubMenu_OnItemAdded(object sender, SubWindows.AddItemWindow.Events.EventArgs.ItemAddedArgs args)
        {
            context.AddItem(args.item);
        }

        private void UpdateMenu_OnItemUpdated(object sender, SubWindows.AddItemWindow.Events.EventArgs.ItemUpdateArgs args)
        {
            context.UpdateItem(args.old, args.newItem);
        }
    
        private void BtnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if(dgToDoList.SelectedItem == null)
            {
                MessageBox.Show("An item must be selected from the list for you to update it!");
                return;
            }
            
            // This is where we bring up the update sub menu

            if (dgToDoList.SelectedItem is ToDoListItem item)
            {
                var updateMenu = new AddUserSubMenu();
                updateMenu.InitializeForUpdate(item);
                updateMenu.OnItemUpdated += UpdateMenu_OnItemUpdated;
                updateMenu.Closing += SubMenu_Closing;
                updateMenu.ShowDialog();
            }
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgToDoList.SelectedItem == null)
            {
                MessageBox.Show("An item must be selected from the list for you to remove it!");
                return;
            }

            // This will bring up a "are you sure" menu, on accept, the item is removed     
            if (dgToDoList.SelectedItem is ToDoListItem item)
            {
                context.DeleteItem(item);
            }
        }

        private void BtnSaveItems_Click(object sender, RoutedEventArgs e)
        {
            // This brings up a file browser and lets the user save the file as a json file
            FileDialog fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                if (File.Exists(fileDialog.FileName))
                {
                    var result = MessageBox.Show("A file already exists here, do you wish to overwrite it?" , "Overwrite file?" , MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        File.Delete(fileDialog.FileName);
                        context.SaveItems(new FileInfo(fileDialog.FileName));
                    }
                }
                else
                {
                    context.SaveItems(new FileInfo(fileDialog.FileName));
                }
            }
        }

        private void BtnLoadItems_Click(object sender, RoutedEventArgs e)
        {
            // loads the data as json
            FileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                context.LoadItems(new FileInfo(fileDialog.FileName));
            }
        }
    }
}
