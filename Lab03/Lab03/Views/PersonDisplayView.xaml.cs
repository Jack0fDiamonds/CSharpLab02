using System.Windows.Controls;
using Lab03.Tools.Navigation;
using Lab03.VIewModels;

namespace Lab03.Views
{
    /// <summary>
    /// Interaction logic for PersonListView.xaml
    /// </summary>
    public partial class PersonDisplayView : UserControl, INavigatable
    {
        public PersonDisplayView()
        {
            InitializeComponent();
            DataContext = new PersonDisplayViewModel();
        }
    }
}
