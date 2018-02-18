using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;        // Colors
using System.Windows.Documents;   // For Run

namespace Wpf_Todo_shka.Resource
{
    // Current Tab
    class CurrentTabInterface
    {
        Border border = new Border { CornerRadius = new CornerRadius(8, 8, 8, 8), Margin = new Thickness(3, 3, 3, 5), Background = (Brush)new BrushConverter().ConvertFrom("#FF90F3F3"), BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF000000") };
        CheckBox CurrentExcer;
        TextBlock CurrentContent;

        public CurrentTabInterface(string content)
        {
            CurrentExcer = new CheckBox { Margin = new Thickness(5), Name = "Check" };

            /// <example>
            ///     For Check_Excercise
            ///     <code>
            ///     CurrentExcer.Checked += MainWindow.Check_Excercise;
            ///     </code>
            /// </example>


            CurrentContent = new TextBlock { Width = 975, FontSize = 15 };
            CurrentContent.TextWrapping = TextWrapping.Wrap;

            CurrentContent.Inlines.Add(new Run { Text = content, Name = "run" });
            //CurrentContent.Inlines.Add(content);

            CurrentExcer.Content = CurrentContent;

        }

        public Border GetCheckBox()
        {
            border.Child = CurrentExcer;
            return border;
        }
    }

    // Done and Deleted Tab
    class DoneAndDeletedInterface
    {
        Border border = new Border { CornerRadius = new CornerRadius(8, 8, 8, 8), Margin = new Thickness(3, 3, 3, 5), Background = (Brush)new BrushConverter().ConvertFrom("#FF90F3F3"), BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF000000") };
        TextBlock _content;
        public DoneAndDeletedInterface(string content)
        {
            _content = new TextBlock { Width = 975, FontSize = 15 };
            _content.TextWrapping = TextWrapping.Wrap;
            _content.Margin = new Thickness(3, 3, 3, 5);
            _content.Text = content;
        }

        public Border GetTextBlock()
        {
            border.Child = _content;
            return border;
        }
    }
}
