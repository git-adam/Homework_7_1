﻿using Homework_7_1.Commands;
using Homework_7_1.Models;
using Homework_7_1.Models.Domains;
using Homework_7_1.Models.Wrappers;
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

        private Repository _repository = new Repository();
        public AddEditStudentViewModel(StudentWrapper student = null)
        {

            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            //throw new Exception("BŁĄD");

            if (student == null)
            {
                Student = new StudentWrapper();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }

        private StudentWrapper _student;

        public StudentWrapper Student
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
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group() { Id = 0, Name = "-- brak --" });


            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Student.Group.Id;
        }

        private void Confirm(object obj)
        {
            if (!Student.IsValid)
            {
                return;
            }

            if (!IsUpdate)
                AddStudent();
            else
                UpdateStudent();


            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            //baza danych
            _repository.UpdateStudent(Student);
        }

        private void AddStudent()
        {
            //baza danych
            _repository.AddStudent(Student);
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
