﻿using Homework_7_1.Commands;
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
    public class AddEditStudentViewModel : ViewModelBase
    {
        public AddEditStudentViewModel(Student student = null)
        {

            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            throw new Exception("BŁĄD");

            if (student == null)
            {
                Student = new Student();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set
            { 
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
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

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>()
            {
                new Group() {Id = 0, Name = "-- brak --"},
                new Group() {Id = 1, Name = "1A"},
                new Group() {Id = 2, Name = "2A"}
            };

            Student.Group.Id = 0;
        }

        private void Confirm(object obj)
        {
            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();


            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            //baza danych
        }

        private void AddStudent()
        {
            //baza danych
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

    }
}
