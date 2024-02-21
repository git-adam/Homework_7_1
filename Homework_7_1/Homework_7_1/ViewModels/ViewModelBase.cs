using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_1.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string popertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(popertyName));
        }
    }
}
