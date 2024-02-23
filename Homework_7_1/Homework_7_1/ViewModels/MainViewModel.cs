using Homework_7_1.Commands;
using Homework_7_1.Models;
using Homework_7_1.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework_7_1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            RefreshDiary();

            InitGroups();

        }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }

        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }
        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as Student); //Nie jest to dobra praktyka, stosuje się mechanizm DEPENDENCY INJECTION
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia",
                $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            //usuwanie ucznia z bazy danych

            RefreshDiary();
        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }


        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>()
            {
                new Group() {Id = 0, Name = "Wszystkie"},
                new Group() {Id = 1, Name = "1A"},
                new Group() {Id = 2, Name = "2A"}
            };

            SelectedGroupId = 0;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<Student>
            {
                new Student()
                {
                    FirstName = "Kazimierz",
                    LastName = "Nowak",
                    Group = new Group() { Id = 1 },
                },

                new Student()
                {
                    FirstName = "Marek",
                    LastName = "Gaweł",
                    Group = new Group() { Id = 2 },
                },

                new Student()
                {
                    FirstName = "Jacek",
                    LastName = "Sztaba",
                    Group = new Group() { Id = 1 },
                },

            };
        }


    }
}
