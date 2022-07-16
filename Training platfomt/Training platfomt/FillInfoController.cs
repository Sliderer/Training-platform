using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Controls;

namespace Training_platfomt
{
    class FillInfoController
    {
        public static void FillUserInfo(User currentUser, MainWindow mainWindow)
        {
            mainWindow.LoginUserInfo.Text = currentUser.login;
            mainWindow.FullNameUserInfo.Text = currentUser.name + " " + currentUser.surname;
            mainWindow.EmailUserInfo.Text = currentUser.email;
        }

        public static void FillCoursesWrapPanel(DatabaseController database, MainWindow mainWindow)
        {
            List<CoursePanel> coursePanels = CoursePanelsGenerator.GeneratePanels(database).ToList();
            foreach (var panel in coursePanels)
            {
                mainWindow.CoursesWrapPanel.Children.Add(panel);
            }
        }

        public static void FillCourseGrid(Course course, List<Video> videos, Grid CurrentCourseGrid, MainWindow mainWindow)
        {
            mainWindow.ChangeGridinAccount(CurrentCourseGrid);
            mainWindow.CourseTitle.Text = course.title;
            mainWindow.CourseDiscription.Text = course.discription;
            foreach (var video in videos)
            {
                VideoViewer videoViewer = new VideoViewer();
                videoViewer.CourseVideo.Source = new Uri(video.link);
                videoViewer.VideoTitle.Text = video.title;
                mainWindow.CurrentCourseVideoPanel.Children.Add(videoViewer);
            }
        }
    }
}
