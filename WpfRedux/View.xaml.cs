using System.Windows;

namespace WpfRedux
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
            var store = new Store(new Reducer(), new State(""));
            DataContext = new ViewModel(store);
        }
    }
}
