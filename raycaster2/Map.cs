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

        public void fixedUpdate()
        {
            Vector2 newPos = player.updatePos();
            if (!walls[(int)newPos.Y, (int)newPos.X])
            {
                player.pos = newPos;
            }
        }
    }
}
