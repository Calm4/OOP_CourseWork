﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace DirigibleBattle
{
    internal class StartGame
    {
        static void Main()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Run(60);
        }
    }
}
