using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace raycaster2
{
    class Player
    {
        public Vector2 pos;
        public float ang = 0.0f;
        public float walkSpeed = 0.1f;

        public Player()
        {
            this.pos = new Vector2(2, 2);
        }

        public Player(ref Map map, float x, float y)
        {
            this.pos = new Vector2(x, y);
        }

        public Player(ref Map map, Vector2 pos)
        {
            this.pos = pos;
        }

        public Vector2 updatePos()
        {
            List<ConsoleKeyInfo> inputBufferBuffer = new List<ConsoleKeyInfo>(InputHandler.inputBuffer);
            int walkX = 0;
            int walkY = 0;
            foreach (ConsoleKeyInfo input in inputBufferBuffer)
            {
                switch (input.Key)
                {
                    case ConsoleKey.RightArrow:
                        ang += 5;
                        break;

                    case ConsoleKey.LeftArrow:
                        ang -= 5;
                        break;

                    case ConsoleKey.W:
                        walkY = 1;
                        break;

                    case ConsoleKey.A:
                        walkX = -1;
                        break;

                    case ConsoleKey.S:
                        walkY = -1;
                        break;

                    case ConsoleKey.D:
                        walkX = 1;
                        break;

                    default:
                        break;
                }
            }
            InputHandler.inputBuffer.Clear();
            return Vector2.Add(pos, Vector2.Multiply(Helpers.rotateVector2(new Vector2(walkY, walkX), ang), walkSpeed));
        }
    }
}
