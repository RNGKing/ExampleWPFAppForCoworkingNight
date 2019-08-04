using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimpleToDoList.Model;
using SimpleToDoList.ViewModel.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;

namespace SimpleToDoList.ViewModel
{
    class ToDoListViewModel : INotifyPropertyChanged
    {

        #region Public Fields and Properties

        public int ItemCount
        {
            get
            {
                return Items.Count;
            }
        }

        #endregion

        #region Private Fields and Properties

        int counter = 0;

        #endregion

        #region Events

        public event ToDoListUpdated OnToDoListUpdated;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public ToDoListViewModel()
        {
            OnToDoListUpdated += ToDoListViewModel_OnToDoListUpdated;
        }

        #endregion

        #region Event Handlers

        private void ToDoListViewModel_OnToDoListUpdated(object sender, Events.EventArgs.ToDoListUpdatedArgs args)
        {
            UpdateItemBtnEnabled = Items.Count > 0;
            RemoveItemBtnEnabled = Items.Count > 0;
            SaveBtnEnabled = Items.Count > 0;
        }

        #endregion

        #region Data Binds

        #region DataGrid Binds

        public ObservableCollection<ToDoListItem> Items { get; set; } = new ObservableCollection<ToDoListItem>();

        public ICollectionView ItemsView
        {
            get
            {
                return CollectionViewSource.GetDefaultView(Items);
            }
        }

        #endregion

        #region GUI Enable Binds

        bool updateItemBtnEnabled = false;
        public bool UpdateItemBtnEnabled
        {
            get
            {
                return updateItemBtnEnabled;
            }
            set
            {
                updateItemBtnEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpdateItemBtnEnabled"));
            }
        }

        bool removeItemBtnEnabled = false;
        public bool RemoveItemBtnEnabled
        {
            get
            {
                return removeItemBtnEnabled;
            }
            set
            {
                removeItemBtnEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RemoveItemBtnEnabled"));
            }
        }

        bool saveBtnEnabled = false;
        public bool SaveBtnEnabled
        {
            get
            {
                return saveBtnEnabled;
            }
            set
            {
                saveBtnEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SaveBtnEnabled"));
            }
        }

        bool loadBtnEnabled = true;
        public bool LoadBtnEnabled
        {
            get
            {
                return loadBtnEnabled;
            }
            set
            {
                loadBtnEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoadBtnEnabled"));
            }
        }

        #endregion

        #endregion

        #region CRUD Operations

        public void AddItem(ToDoListItem item)
        {
            item.Id = counter;
            Items.Add(item);
            counter++;
            OnToDoListUpdated?.Invoke(this, new Events.EventArgs.ToDoListUpdatedArgs());
        }

        public void DeleteItem(ToDoListItem item)
        {
            Items.Remove(item);
            OnToDoListUpdated?.Invoke(this, new Events.EventArgs.ToDoListUpdatedArgs());
        }

        public void UpdateItem(ToDoListItem old, ToDoListItem newItem)
        {
            old.CreationTime = newItem.CreationTime;
            old.Description = newItem.Description;
            old.DueDate = newItem.DueDate;
            old.Name = newItem.Name;
            old.Completed = newItem.Completed;
            OnToDoListUpdated?.Invoke(this, new Events.EventArgs.ToDoListUpdatedArgs());
        }

        public bool RetrieveItem(int id, out ToDoListItem item)
        {
            try
            {
                item = (from i in Items where i.Id.Equals(id) select i).First();
                return true;
            }
            catch
            {
                item = null;
                return false;
            }
        }

        public bool RetrieveItem(string name, out ToDoListItem item)
        {
            try
            {
                item = (from i in Items where i.Name.Equals(name) select i).First();
                return true;
            }
            catch
            {
                item = null;
                return false;
            }
        }

        #endregion

        #region Data Persistence

        public void SaveItems(FileInfo file)
        {
            var itemCollection = Items.ToList();
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter writer = new StreamWriter(file.FullName))
            {
                using (JsonWriter json = new JsonTextWriter(writer))
                {
                    serializer.Serialize(json, itemCollection);
                }
            }
        }

        public void LoadItems(FileInfo file)
        {
            var objects = File.ReadAllText(file.FullName);
            var listOfItems = JsonConvert.DeserializeObject<List<ToDoListItem>>(objects);
            ClearData();
            foreach (var i in listOfItems) Items.Add(i);
            OnToDoListUpdated?.Invoke(this, new Events.EventArgs.ToDoListUpdatedArgs());
        }

        private void ClearData()
        {
            Items.Clear();
        }

        #endregion

    }
}
