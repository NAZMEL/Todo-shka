using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Wpf_Todo_shka.Resource;

namespace Wpf_Todo_shka
{
    public partial class MainWindow : Window
    {
       
        public static Exercises excer = new Exercises();
       
        public MainWindow()
        {
            InitializeComponent();
            // Window's size don't changes
            this.ResizeMode = ResizeMode.NoResize ;

            // Update Content
            this.Get_Changes(new object(), new EventArgs());
        }

        public void Get_Changes(object sender, EventArgs e)
        {
            GetExecuting(); GetDone(); GetDeleted();
            Current.Focus();                          // Focus on CurrentTab
        }

        /// <examples>
        ///     <code>
        ///         // Checking and change on done
        ///         public static void Check_Excercise(object sender, EventArgs e)
        ///           {
        ///              // From CheckBox take TextBlock.content
        ///              TextBlock tb = (TextBlock)((CheckBox)sender).Content;
        ///              
        ///             // From TextBlock.content get Run.Text
        ///             string run_text = ((Run)tb.Inlines.FirstInline).Text.ToString();
        ///
        ///             excer.DoneExcercise(run_text);
        ///          }
        ///     </code>
        /// </examples>

        // From Done_Button to database changes on status done
        void Check_Done(object sender, RoutedEventArgs e)
        {
            var stackPanel = CurrentExcercises.Children;
            foreach (Border border in stackPanel)
            {
                CheckBox check_tmp = border.Child as CheckBox;
                if (check_tmp.IsChecked ?? true)
                {
                    TextBlock tmp = (TextBlock)check_tmp.Content;
                    string run_text = ((Run)tmp.Inlines.FirstInline).Text.ToString();
                    excer.DoneExcercise(run_text);
                }
            }
            Get_Changes(new object(), new EventArgs());
        }

        // From Delete_Button to database on status delete
        void Check_Delete(object sender, RoutedEventArgs e)
        {
            var stackPanel = CurrentExcercises.Children;
            foreach(Border border in stackPanel)
            {
                CheckBox check_tmp = border.Child as CheckBox;
                if (check_tmp.IsChecked ?? true)
                {
                    TextBlock tmp = (TextBlock)check_tmp.Content;
                    string run_text = ((Run)tmp.Inlines.FirstInline).Text.ToString();
                    excer.DeleteExcercise(run_text);
                }
            }
            Get_Changes(new object(), new EventArgs());
        }
        
        // Get current excercises
        void GetExecuting()
        {
            var Executing = excer.Select_Executing();
            CurrentExcercises.Children.Clear();         // Clear content Tab
            foreach (var item in Executing)
            {
                CurrentExcercises.Children.Add(new CurrentTabInterface(item.content).GetCheckBox());
            }
        }

        /// <summary>
        ///  Get done excercises
        /// </summary>
        void GetDone()
        {
            var Done = excer.Select_Done();
            DoneExcercises.Children.Clear();
            foreach (var item in Done)
            {
                DoneExcercises.Children.Add(new DoneAndDeletedInterface(item.content).GetTextBlock());
            }
        }

        /// <summary>
        /// Get deleted Excercises
        /// </summary>
        void GetDeleted()
        {
            var Deleted = excer.Select_Deleted();
            DeletedExcercises.Children.Clear();
            foreach (var item in Deleted)
            {
                DeletedExcercises.Children.Add(new DoneAndDeletedInterface(item.content).GetTextBlock());
            }
        }

        /// <summary>
        ///  Add a new excercise
        /// </summary>
        void New_Excercise(object sender, EventArgs s)
        {
            // AddExcercise Window
            AddExcercise addWindow = new AddExcercise();
            addWindow.Owner = this;                          // MainWidow is main for AddExcercise, MainWindow activities after closing AddExcercise
            addWindow.Show();
        }
    }

 
}
