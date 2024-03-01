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
using System.Windows.Navigation;
using System.Windows.Shapes;
using To_Do_List_WIth_SQL.Connection;
using To_Do_List_WIth_SQL.Exceptions;
using To_Do_List_WIth_SQL.Model;

namespace To_Do_List_WIth_SQL
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IToDoRepository toDoRepository;
        public MainWindow()
        {
            InitializeComponent();
            toDoRepository = new ToDoRepository(ConnectionWithDB.OpenConnect());
            OpenAllList();
        }

        private void ToDoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ToDoListBox.SelectedItem != null)
            {
                ToDo todo = (ToDoListBox.SelectedItem as ToDo);
                descriptionTextBox.Text = todo.ToDescriptionString();
            }
            
        }

        private void OpenAllList()
        {
            try
            {
                List<ToDo> todos = toDoRepository.GetAll();
                foreach (ToDo toDo in todos)
                {
                    ToDoListBox.Items.Add(toDo);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"{ex.Message}", "ERROR");
            }
        }

        private void getByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var v = toDoRepository.GetById(Convert.ToInt32(idTextBox.Text));
                ToDoListBox.Items.Clear();
                ToDoListBox.Items.Add(v);
            }catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ERROR");
            }
        }

        private void deleteByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                toDoRepository.DeleteById(Convert.ToInt32(idTextBox.Text));
                ToDoListBox.Items.Clear();
                OpenAllList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ERROR");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToDo toDo;
            try
            {
                 toDo = new ToDo()
                {
                    Deadline = Convert.ToDateTime(DateExceptionProcessing(dateTextBox.Text)),
                    Description = discriptionTextBox.Text,
                    Name = nameTextBox.Text
                };
                toDoRepository.AddToDo(toDo);
                ToDoListBox.Items.Add(toDo);
            }
            catch ( DateException ex)
            {
                MessageBox.Show($"{ex.Message}", ex.DateValue, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TitleNullOrEmtyException ex)
            {
                MessageBox.Show($"{ex.Message}", "Пустое значение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ERROR");
            }
            
            ToDoListBox.Items.Clear ();
            OpenAllList();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                int id = 0;
                if (ToDoListBox.SelectedItem != null)
                {
                    ToDo todo = (ToDoListBox.SelectedItem as ToDo);
                    id = todo.Id;
                }
                ToDo toDo = new ToDo()
                {
                    Id = id,
                    Deadline = Convert.ToDateTime(dateTextBox.Text),
                    Description = discriptionTextBox.Text,
                    Name = nameTextBox.Text
                };
                try
                {
                    toDoRepository.UpdateToDo(toDo, id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "ERROR");
                }
            }
            catch(DateException ex)
            {
                MessageBox.Show($"{ex.Message}", ex.DateValue, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(TitleNullOrEmtyException ex)
            {
                MessageBox.Show($"{ex.Message}", "Пустое значение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            ToDoListBox.Items.Clear();
            OpenAllList();
        }

        private string DateExceptionProcessing(string message)
        {
            try
            {
                if(message == null || message.Length < 10 || message.Length > 10)
                {
                    throw new DateException("Некорректный ввод даты, введите дату правильно", message);
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Неизвестная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return message;
        }

        
    }
}
