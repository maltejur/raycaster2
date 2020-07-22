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
        public float walkSpeed = 50;
        public float turnSpeed = 2;
        public float sprintFactor = 2;

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

        public Vector2 updatePos(TimeSpan DeltaTime)
        {
            float walkX = 0;
            float walkY = 0;
            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.RIGHT))
            {
                ang += turnSpeed * (float)DeltaTime.TotalMilliseconds;
            }

            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.LEFT))
            {
                ang -= turnSpeed * (float)DeltaTime.TotalMilliseconds;
            }

            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.KEY_W))
            {
                walkY += walkSpeed * (float)DeltaTime.TotalSeconds;
            }

            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.KEY_A))
            {
                walkX -= walkSpeed * (float)DeltaTime.TotalSeconds;
            }

            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.KEY_S))
            {
                walkY -= walkSpeed * (float)DeltaTime.TotalSeconds;
            }

            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.KEY_D))
            {
                walkX += walkSpeed * (float)DeltaTime.TotalSeconds;
            }

            if (ConsoleHelper.IsKeyPressed(ConsoleHelper.VirtualKeyStates.SHIFT))
            {
                walkX *= sprintFactor;
                walkY *= sprintFactor;
            }

            return Vector2.Add(pos, Helpers.rotateVector2(new Vector2(walkY, walkX), ang));
        }
    }
}
