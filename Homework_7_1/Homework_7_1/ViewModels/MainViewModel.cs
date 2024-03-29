﻿using Homework_7_1.Commands;
using Homework_7_1.Models;
using Homework_7_1.Models.Domains;
using Homework_7_1.Models.Wrappers;
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
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            SetConnectionStringCommand = new RelayCommand(SetConnectionString);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            LoadedWindow(null);
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand SetConnectionStringCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
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
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper); //Nie jest to dobra praktyka, stosuje się mechanizm DEPENDENCY INJECTION
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
            _repository.DeleteStudent(SelectedStudent.Id);

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

        private void SetConnectionString(object obj)
        {
            var setConnectionStringWindow = new SetConnectionStringView(true); //Nie jest to dobra praktyka, stosuje się mechanizm DEPENDENCY INJECTION
            setConnectionStringWindow.Closed += SetConnectionStringWindow_Closed;
            setConnectionStringWindow.ShowDialog();
        }

        private void SetConnectionStringWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary(); // Czy to wszystko?
        }

        private async void LoadedWindow(object obj)
        {
            if (!IsDatabaseConnectionPossible())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync(
                    "Błąd połączenia",
                    $"Nie można połączyć się z bazą danych. Czy chcesz zmienić ustawienia?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (dialog != MessageDialogResult.Affirmative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var setConnectionStringWindow = new SetConnectionStringView(false);
                    setConnectionStringWindow.ShowDialog();
                }
            }
            else
            {
                RefreshDiary();
                InitGroups();
            }
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group() { Id = 0, Name = "Wszystkie" });


            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));
        }

        public bool IsDatabaseConnectionPossible()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
        }
    }
}
