using System.ComponentModel;

public class TaskItem : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string name;
    private string schedule;
    private bool isComplete;

    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            OnPropertyChanged("Name");
        }
    }

    public string Schedule
    {
        get { return schedule; }
        set
        {
            schedule = value;
            OnPropertyChanged("Schedule");
        }
    }

    public bool IsComplete
    {
        get { return isComplete; }
        set
        {
            isComplete = value;
            OnPropertyChanged("IsComplete");
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
