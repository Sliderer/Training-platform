using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Training_platfomt
{
    public partial class CoursePanel : UserControl
    {
        public event RoutedEventHandler ChooseCourseEvent;

        public CoursePanel()
        {
            InitializeComponent();
        }

        private void ChooseCourse(object sender, RoutedEventArgs e)
        {
            ChooseCourseEvent?.Invoke(sender, e);
        }
    }
}
