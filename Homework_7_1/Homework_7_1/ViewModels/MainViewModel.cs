using Homework_7_1.Commands;
using System;
using System.Collections.Generic;
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
        }
        public ICommand RefreshStudentsCommand { get; set; }

        private string _test = "XXX";
            
        public string Test
        {
            get { return _test; }
            set 
            {
                _test = value;
                OnPropertyChanged();
            }
        }


        private void RefreshStudents(object obj)
        {
            //MessageBox.Show("RefreshStudnets");
            //Test = "sss";
            MessageBox.Show(Test);
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }
    }
}
