using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;

namespace Training_platfomt
{
    class CoursePanelsGenerator
    {
        public static IEnumerable<CoursePanel> GeneratePanels(DatabaseController database)
        {
            List<Course> courses = database.GetAllCourses().ToList();
            foreach(var course in courses)
            {
                CoursePanel coursePanel = new CoursePanel();
                coursePanel.CourseTitle.Text = course.title;
                coursePanel.ChooseCourseEvent += MainWindow.CoursePanel_ChooseCourseEvent;
                yield return coursePanel;
            }
        }
    }
}
