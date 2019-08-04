using SimpleToDoList.ViewModel.Events.EventArgs;

namespace SimpleToDoList.ViewModel.Events
{
    public delegate void ToDoListUpdated(object sender, ToDoListUpdatedArgs args);
}
