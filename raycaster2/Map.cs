using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace raycaster2
{
    class Map
    {
        public int width;
        public int height;
        public bool[,] walls;
        public Player player;

        public Map(string[] walls)
        {
            this.width = walls[0].Length;
            this.height = walls.Length;
            this.player = new Player();
            this.walls = new bool[this.height, this.width];
            for(int row = 0; row < this.height; row++)
            {
                if (walls[row].Length == this.width)
                {
                    for(int col = 0; col < this.width; col++)
                    {
                        this.walls[row,col] = walls[row][col] != ' ';
                    }
                }
                else
                {
                    throw new Exception("Map initializer has invalid form (not all rows are equal width)");
                }
            }
        }

        public Map(int width,int height,bool[,] walls)
        {
            this.width = width;
            this.height = height;
            this.player = new Player();
            this.walls = walls;
        }

        public static Map ParseTimMap(string map, int width, int height)
        {
            bool[,] walls = new bool[width * 2 + 1, height * 2 + 1];
            for(int x = 0; x < width * 2 + 1; x++)
            {
                for(int y = 0; y < height * 2 + 1; y++)
                {
                    walls[y, x] = true;
                }
            }

            var doors = map.Split(' ');

            var p = new Printer();

            foreach (var item in doors)
            {
                int x = int.Parse(item[0].ToString()) * 2 + 1;
                int y = int.Parse(item[1].ToString()) * 2 + 1;
                walls[y, x] = false;
                if (item.Substring(2).Contains('1'))
                    walls[y - 1, x] = false;
                if (item.Substring(2).Contains('2'))
                    walls[y, x + 1] = false;
                if (item.Substring(2).Contains('3'))
                    walls[y + 1, x] = false;
                if (item.Substring(2).Contains('4'))
                    walls[y, x - 1] = false;
                //p.generateMiniMap(new Map(width * 2 + 1, height * 2 + 1, walls));
                //p.draw();
            }

            return new Map(width * 2 + 1, height * 2 + 1, walls);
        }

        public void update(TimeSpan DeltaTime)
        {
            Vector2 newPos = player.updatePos(DeltaTime);
            if (newPos.Y >= 0 && newPos.X >= 0 && newPos.Y < height && newPos.X < width && !walls[(int)newPos.Y, (int)newPos.X])
            {
                player.pos = newPos;
            }
        }
    }
}
