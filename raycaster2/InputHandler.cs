using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace raycaster2
{
    class InputHandler
    {
        static public List<ConsoleKeyInfo> inputBuffer = new List<ConsoleKeyInfo>();

        static public void init()
        {
            Thread thread = new Thread(inputThread);
            thread.Start();
        }

        static private void inputThread()
        {
            while (true)
            {
                inputBuffer.Add(Console.ReadKey());
            }
        }
    }
}
