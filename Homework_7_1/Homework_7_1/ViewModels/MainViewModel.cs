using Homework_7_1.Commands;
using Homework_7_1.Models;
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
            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);

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

            InitGroups();

        }

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

        private void RefreshStudents(object obj)
        {
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }
    }
}
