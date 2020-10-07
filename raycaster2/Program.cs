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
        static Map map2;
        static DateTime lastUpdate = DateTime.Now;

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

            map2 = Map.ParseTimMap("002 102 2023 214 1134 013 023 032 132 2312 33 222 321 311 30 12", 4, 4);
            map2.player.pos.X = 3;
            map2.player.pos.Y = 5;


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
                printer.raycastMap(map2);
                printer.generateMiniMap(map2);
                printer.displayFps();
                if (map2.player.pos.X > 7 && map2.player.pos.Y > 7)
                {
                    printer.jumpscare();
                }
                printer.draw();
            }
        }

        static void fixedLoop()
        {
            while (true)
            {
                map2.update((DateTime.Now - lastUpdate));
                lastUpdate = DateTime.Now;
            }
        }
    }
}
