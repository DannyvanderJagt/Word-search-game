﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Word_search_game.Classes
{
    class Colors
    {
        /* 
         * Some colors that are used in this application.
         */
        static public SolidColorBrush green = new SolidColorBrush(Color.FromArgb(255, 215, 208, 94)),
               red = new SolidColorBrush(Color.FromArgb(255, 238, 129, 72)),
               blue = new SolidColorBrush(Color.FromArgb(255, 31, 164, 205)),
               yellow = new SolidColorBrush(Color.FromArgb(255, 247, 204, 57)),
               white = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
    }
}
