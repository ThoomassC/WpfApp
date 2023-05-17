using System.Collections.ObjectModel;
using System.Data.SQLite;

public class DataContext
{
    private const string DatabaseFile = "Tasks.db";
    private const string TableName = "TaskItems";

    public ObservableCollection<TaskItem> Tasks { get; private set; }

    public DataContext()
    {
        Tasks = new ObservableCollection<TaskItem>();
        CreateDatabase();
        LoadTasks();
    }

    private void CreateDatabase()
    {
        using (var connection = new SQLiteConnection("Data Source=" + DatabaseFile))
        {
            connection.Open();

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS " + TableName +
                                      "(Name TEXT, Schedule TEXT, IsComplete INTEGER)";
                command.ExecuteNonQuery();
            }
        }
    }

    private void LoadTasks()
    {
        Tasks.Clear();

        using (var connection = new SQLiteConnection("Data Source=" + DatabaseFile))
        {
            connection.Open();

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT * FROM " + TableName;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new TaskItem
                        {
                            Name = reader.GetString(0),
                            Schedule = reader.GetString(1),
                            IsComplete = reader.GetBoolean(2)
                        };
                        Tasks.Add(task);
                    }
                }
            }
        }
    }

    public void AddTask(TaskItem task)
    {
        Tasks.Add(task);

        using (var connection = new SQLiteConnection("Data Source=" + DatabaseFile))
        {
            connection.Open();

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO " + TableName +
                                      "(Name, Schedule, IsComplete) VALUES (@Name, @Schedule, @IsComplete)";
                command.Parameters.AddWithValue("@Name", task.Name);
                command.Parameters.AddWithValue("@Schedule", task.Schedule);
                command.Parameters.AddWithValue("@IsComplete", task.IsComplete);
                command.ExecuteNonQuery();
            }
        }
    }

    public void RemoveTask(TaskItem task)
    {
        Tasks.Remove(task);

        using (var connection = new SQLiteConnection("Data Source=" + DatabaseFile))
        {
            connection.Open();

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM " + TableName +
                                      " WHERE Name = @Name AND Schedule = @Schedule";
                command.Parameters.AddWithValue("@Name", task.Name);
                command.Parameters.AddWithValue("@Schedule", task.Schedule);
                command.ExecuteNonQuery();
            }
        }
    }
}
