using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace raycaster2
{
    class Helpers
    {
        static public Vector2 rotateVector2(Vector2 v, float angle)
        {
            angle = angle * (float)Math.PI / 180;
            float ca = (float)Math.Cos(angle);
            float sa = (float)Math.Sin(angle);
            return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}
