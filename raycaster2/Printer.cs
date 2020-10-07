using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Threading;

namespace raycaster2
{
    class Printer
    {
        public float cornerWidth = 0.05f;
        private int winWidth = Console.WindowWidth;
        private int winHeight = Console.WindowHeight - 1;
        private char[,] buffer = new char[Console.WindowHeight - 1, Console.WindowWidth];
        private DateTime prevFpsTime = DateTime.Now;

        public void updateWinSize()
        {
            COORD winSize = ConsoleHelper.GetConsoleSize();
            if(winWidth != winSize.X || winHeight != winSize.Y - 1)
            {
                winWidth = winSize.X;
                winHeight = winSize.Y - 1;
                buffer = new char[winSize.Y - 1, winSize.X];
            }
        }

        public void jumpscare()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            string xd = @"……………▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄\n
…………▄▄█▓▓▓▒▒▒▒▒▒▒▒▒▒▓▓▓▓█▄▄\n
………▄▀▀▓▒░░░░░░░░░░░░░░░░▒▓▓▀▄\n
……▄▀▓▒▒░░░░░░░░░░░░░░░░░░░▒▒▓▀▄\n
…..█▓█▒░░░░░░░░░░░░░░░░░░░░░▒▓▒▓█\n
..▌▓▀▒░░░░░░░░░░░░░░░░░░░░░░░░▒▀▓█\n
..█▌▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░▒▓█\n
▐█▓▒░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓█▌\n
█▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓█\n
█▐▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒█▓\n
█▓█▒░░░░░░░░░░░░░░░░░░░░░░░░░░░▒█▌▓█\n
█▓▓█▒░░░░▒█▄▒▒░░░░░░░░░▒▒▄█▒░░░░▒█▓▓█\n
█▓█▒░▒▒▒▒░░▀▀█▄▄░░░░░▄▄█▀▀░░▒▒▒▒░▒█▓█\n
█▓▌▒▒▓▓▓▓▄▄▄▒▒▒▀█░░░░█▀▒▒▒▄▄▄▓▓▓▓▒▒▐▓█\n
██▌▒▓███▓█████▓▒▐▌░░▐▌▒▓████▓████▓▒▐██\n
.██▒▒▓███▓▓▓████▓▄░░░▄▓████▓▓▓███▓▒▒██\n
.█▓▒▒▓██████████▓▒░░░▒▓██████████▓▒▒▓█\n
..█▓▒░▒▓███████▓▓▄▀░░▀...▄▓▓███████▓▒▒▓█\n
....█▓▒░▒▒▓▓▓▓▄▄▄▀▒░░░░░▒▀▄▄▄▓▓▓▓▒▒░▓█\n
......█▓▒░▒▒▒▒░░░░░░▒▒▒▒░░░░░░▒▒▒▒░▒▓█\n
........█▓▓▒▒▒░░██░░▒▓██▓▒░░██░░▒▒▒▓▓█\n
........▀██▓▓▓▒░░▀░▒▓████▓▒░▀░░▒▓▓▓██▀\n
............░▀█▓▒▒░░░▓█▓▒▒▓█▓▒░░▒▒▓█▀░\n
...........█░░██▓▓▒░░▒▒▒░▒▒▒░░▒▓▓██░░█\n
............█▄░░▀█▓▒░░░░░░░░░░▒▓█▀░░▄█\n
.............█▓█░░█▓▒▒▒░░░░░▒▒▒▓█░░█▓█\n
.............▐█▓█░░█▀█▓▓▓▓▓▓█▀░░█░█▓█▌\n
...............█▓▓█░█░█░█▀▀▀█░█░▄▀░█▓█\n
.............. █▓▓█░░▀█▀█░█░█▄█▀░░█▓▓█\n
................█▓▓█░░░░▀▀▀▀░░░░░█▓▓█";
            string yeet = @"⢠⠣⡑⡕⡱⡸⡀⡢⡂⢨⠀⡌⠀⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⡕⢅⠕⢘⢜⠰⣱⢱⢱⢕⢵⠰⡱⡱⢘⡄⡎⠌⡀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠱⡸⡸⡨⢸⢸⢈⢮⡪⣣⣣⡣⡇⣫⡺⡸⡜⡎⡢⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⢱⢱⠵⢹⢸⢼⡐⡵⣝⢮⢖⢯⡪⡲⡝⠕⣝⢮⢪⢀⠀⠀⠀⠀ 
⠀⠀⠀⠀⢀⠂⡮⠁⠐⠀⡀⡀⠑⢝⢮⣳⣫⢳⡙⠐⠀⡠⡀⠀⠑⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⢠⠣⠐⠀ :o: ￼ ⠀⠀⢪⢺⣪⢣⠀⡀ :o:     .⠈⡈⠀⡀⠀⠀ 
⠀⠀⠀⠀⠐⡝⣕⢄⡀⠑⢙⠉⠁⡠⡣⢯⡪⣇⢇⢀⠀⠡⠁⠁⡠⡢⠡⠀⠀⠀ 
⠀⠀⠀⠀⠀⢑⢕⢧⣣⢐⡄⣄⡍⡎⡮⣳⢽⡸⡸⡊⣧⣢⠀⣕⠜⡌⠌⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠌⡪⡪⠳⣝⢞⡆⡇⡣⡯⣞⢜⡜⡄⡧⡗⡇⠣⡃⡂⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⠨⢊⢜⢜⣝⣪⢪⠌⢩⢪⢃⢱⣱⢹⢪⢪⠊⠀⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⠀⠐⠡⡑⠜⢎⢗⢕⢘⢜⢜⢜⠜⠕⠡⠡⡈⠀⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⡢⢀⠈⠨⣂⡐⢅⢕⢐⠁⠡⠡⢁⠀⠀⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢈⠢⠀⡀⡐⡍⢪⢘⠀⠀⠡⡑⡀⠀⠀⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠨⢂⠀⠌⠘⢜⠘⠀⢌⠰⡈⠀⠀⠀⠀⠀⠀⠀⠀ 
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢑⢸⢌⢖⢠⢀⠪⡂";
            var xdd = xd.Split("\n");
            for (int i = 0; i < xdd.Length; i++)
            {
                for (int y = 0; y < xdd[i].Length; y++)
                {
                    buffer[i * 2, y * 2] = xdd[i][y];
                    buffer[i * 2 + 1, y * 2] = xdd[i][y];
                    buffer[i * 2, y * 2 + 1] = xdd[i][y];
                    buffer[i * 2 + 1, y * 2 + 1] = xdd[i][y];
                }
            }
        }

        public void generateMiniMap(Map map, int xoff = 0, int yoff = 0)
        {
            xoff *= 2;
            for(int row = 0; row < map.height; row++)
            {
                for(int col = 0; col < map.width * 2; col+=2)
                {
                    if(map.walls[row, col/2])
                    {
                        buffer[row + yoff, col + xoff] = '█';
                        buffer[row + yoff, col + xoff + 1] = '█';
                    }
                    else
                    {
                        buffer[row + yoff, col + xoff] = ' ';
                        buffer[row + yoff, col + xoff + 1] = ' ';
                    }
                }
            }
            buffer[(int)map.player.pos.Y + yoff, (int)((map.player.pos.X + xoff) * 2)] = 'M';
            buffer[(int)map.player.pos.Y + yoff, (int)((map.player.pos.X + xoff) * 2) + 1] = 'e';
        }

        public void raycastMap(Map map, int fov = 70)
        {
            float startAngle = map.player.ang - fov / 2;
            for (int rayN = 0; rayN < winWidth; rayN++)
            {
                // float rayAngle = map.player.ang - fov / 2; rayAngle < map.player.ang + fov / 2; rayAngle += (float)winWidth / (fov * 2)
                float rayAngle = startAngle + rayN * fov / winWidth;
                float rayLen = 0.0f;
                bool wallHit = false;
                bool corner = false;
                float radians = rayAngle / 360 * (float)Math.PI * 2;
                Vector2 rayUnit = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
                Random rnd = new Random(rayN);
                while (!wallHit && rayLen < 20.0f)
                {
                    rayLen += 0.05f;
                    Vector2 testPoint;
                    try
                    {
                        testPoint = Vector2.Add(map.player.pos, Vector2.Multiply(rayUnit, rayLen));
                    }
                    catch (System.ArgumentException)
                    {
                        continue;
                    }
                    if(testPoint.Y < 0 || testPoint.X < 0 || testPoint.Y >= map.height || testPoint.X >= map.width)
                    {
                        continue;
                    }
                    if (map.walls[(int)testPoint.Y, (int)testPoint.X])
                    {
                        wallHit = true;

                        // Check if ray hits corner
                        Vector2 corner1 = new Vector2((int)testPoint.X, (int)testPoint.Y);
                        Vector2 corner2 = new Vector2((int)testPoint.X + 1, (int)testPoint.Y);
                        Vector2 corner3 = new Vector2((int)testPoint.X, (int)testPoint.Y + 1);
                        Vector2 corner4 = new Vector2((int)testPoint.X + 1, (int)testPoint.Y + 1);

                        if(Vector2.Distance(testPoint,corner1)< cornerWidth || Vector2.Distance(testPoint, corner2) < cornerWidth || Vector2.Distance(testPoint, corner3) < cornerWidth || Vector2.Distance(testPoint, corner4) < cornerWidth)
                        {
                            corner = true;
                        }
                    }
                }

                int distTop = (int)Math.Floor((winHeight / 2.0f) - (winHeight / (2.0f * rayLen)));
                if (distTop < 0)
                {
                    distTop = 0;
                }
                int distBot = winHeight - distTop;
                for (int y = 0; y < distTop; y++)
                {
                    buffer[y, rayN] = ' ';
                }
                for (int y = distTop; y < distBot; y++)
                {
                    if(corner)
                    {
                        buffer[y, rayN] = ' ';
                    }
                    else if (rayLen < 2)
                    {
                        buffer[y, rayN] = '█';
                    }
                    else if(rayLen < 5)
                    {
                        buffer[y, rayN] = '▓';
                    }
                    else if (rayLen <7)
                    {
                        buffer[y, rayN] = '▒';
                    }
                    else
                    {
                        buffer[y, rayN] = '░';
                    }
                }
                for(int y = distBot; y < winHeight; y++)
                {
                    float val = (float)y / winHeight;
                    if (val > 0.85f)
                    {
                        buffer[y, rayN] = ';';
                    }
                    else if (val > 0.7f)
                    {
                        buffer[y, rayN] = ':';
                    }
                    else if (val > 0.55f)
                    {
                        buffer[y, rayN] = '.';
                    }
                    else
                    {
                        buffer[y, rayN] = ' ';
                    }
                    //buffer[y, rayN] = y % (y/2) == 0 ? '.' : ' ';
                }
            }
        }

        public void displayFps()
        {
            TimeSpan frametime = DateTime.Now - prevFpsTime;
            writeToBuffer($"FPS: {Math.Floor(1 / frametime.TotalSeconds)}".PadLeft(20), winWidth - 20, 0);
            Debug.WriteLine($"FPS: {Math.Floor(1 / frametime.TotalSeconds)}");
            prevFpsTime = DateTime.Now;
        }

        public void draw()
        {
            char[] outp = new char[winWidth * winHeight];
            for (int row = 0; row < winHeight; row++)
            {
                for (int col = 0; col < winWidth; col++)
                {
                    outp[row * winWidth + col] = buffer[row, col];
                }
            }
            ConsoleHelper.Write(new string(outp), (uint)(winHeight * winWidth));
        }

        private void writeToBuffer(string str, int x, int y)
        {
            for(int i = 0; i < str.Length; i++)
            {
                buffer[y, x + i] = str[i];
            }
        }

        private string coloredChar(int r, int g, int b)
        {
            int colorCode;
            if (r == g && g == b)
            {
                // Greyscale
                colorCode = 232 + (int)(r / 10.626);
            }
            else
            {
                r = r / 43;
                g = g / 43;
                b = b / 43;
                colorCode = 16 + 36 * r + 6 * g + b;
            }
            return $"\x1b[48;5;{colorCode}m \x1b[0m";
        }

        
    }
}
