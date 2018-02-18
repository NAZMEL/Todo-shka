using System.Windows;
using Wpf_Todo_shka.Resource;

namespace Wpf_Todo_shka
{
    public partial class AddExcercise : Window
    {
        public string ExcerciseContent { private set; get; }
        public AddExcercise()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        public void Add_Excercise(object sender, RoutedEventArgs e)
        { 
            ExcerciseContent = BlockContent.Text.ToString();
            Exercises exc = new Exercises();
            exc.AddRow(ExcerciseContent);
            exc.DBClose();
            this.Close();
        }

        public void Cancel_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
