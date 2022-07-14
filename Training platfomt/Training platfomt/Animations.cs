using System;
using System.Windows.Media.Animation;

namespace Training_platfomt
{
    class Animations
    {
        public static DoubleAnimation menuBarEnable = new DoubleAnimation {
            From = 50,
            To = 150,
            Duration = TimeSpan.FromSeconds(1)
        };
    }
}
