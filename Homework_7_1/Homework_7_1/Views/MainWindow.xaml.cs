using MahApps.Metro.Controls;
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

namespace Homework_7_1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Mammal mammal = new Mammal();
            Fish fish = new Fish();



            fish.Name = "RYBA";
            mammal.Name = "SSAK";

            fish.Swim();
            mammal.Run();
            
        }

    }

  public class Animal
{
	public string Name
    {
        get
        {
            return Name;
        }
        set
        {
            this.Name = value;
        }
    }

    public void Breathe()
    {
        Console.WriteLine($"{Name} - Oddycham!");
    }

}



 class Mammal : Animal
{
    public void Run()
    {
        Console.WriteLine($"{Name} - Biegnę!");
    }
}

class Fish : Animal
{
    public void Swim()
    {
        Console.WriteLine($"{Name} - Płynę!");
    }
}

}
