using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace raycaster2
{
    class Program
    {
        static Printer printer;
        static Map map1;
        static DateTime lastUpdate;

        static void Main(string[] args)
        {
            ConsoleHelper.SetCurrentFont("Consolas", 8);
            Console.CursorVisible = false;
            Console.Title = "xd";
            Console.SetWindowSize(200, 80);
            ConsoleHelper.InitConsoleOutput();


            printer = new Printer();
            map1 = new Map(new string[]{
                "#########       #########       ",
                "#               #               ",
                "#       #########       ########",
                "#              ##              #",
                "#      ##      ##      ##      #",
                "#      ##              ##      #",
                "#              ##              #",
                "###            ####            #",
                "##             ###             #",
                "#            ####            ###",
                "#                              #",
                "#              ##              #",
                "#              ##              #",
                "#           #####           ####",
                "#                              #",
                "###  ####    ########    #######",
                "#### ####       ######          ",
                "#               #               ",
                "#       #########       ##  ####",
                "#              ##              #",
                "#      ##      ##       #      #",
                "#      ##      ##      ##      #",
                "#              ##              #",
                "###            ####            #",
                "##             ###             #",
                "#            ####            ###",
                "#                              #",
                "#                              #",
                "#              ##              #",
                "#           ##              ####",
                "#              ##              #",
                "################################",
        });
            map1.player.pos.X = 5;

            Thread mainLoopThread = new Thread(mainLoop);
            Thread fixedLoopThread = new Thread(fixedLoop);

            mainLoopThread.Start();
            fixedLoopThread.Start();
        }

        static void mainLoop()
        {
            while (true)
            {
                printer.updateWinSize();
                printer.raycastMap(map1);
                printer.generateMiniMap(map1);
                printer.displayFps();
                printer.draw();
            }
        }

        static void fixedLoop()
        {
            while (true)
            {
                map1.fixedUpdate();

                // Timings
                int sleepTime = (1000 / 60) - (int)(DateTime.Now - lastUpdate).TotalMilliseconds;
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                lastUpdate = DateTime.Now;
            }
        }
    }
}
