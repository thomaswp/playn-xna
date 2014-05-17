using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNAPath : Path
    {
        public readonly List<Segment> segments = new List<Segment>();

        public bool contains(float x, float y)
        {
            int intersections = 0;
            int count = segments.Count;
            float lastX = 0, lastY = 0;
            for (int i = 0; i < count; i++)
            {
                Segment seg = segments[i];
                intersections += seg.intersections(-1, -1, x, y, lastX, lastY);
                lastX = seg.x;
                lastY = seg.y;
            }
            return intersections % 2 == 1;
        }

        public XNAPath()
        {
            segments.Add(new Move(0, 0));
        }

        public Path bezierTo(float c1x, float c1y, float c2x, float c2y, float x, float y)
        {
            segments.Add(new Bezier(c1x, c1y, c2x, c2y, x, y));
            return this;
        }

        public Path close()
        {
            int i = 0;
            while (i < segments.Count && segments[i] is Move) i++;
            Segment start = segments[i - 1];
            lineTo(start.x, start.y);
            return this;
        }

        public Path lineTo(float x, float y)
        {
            segments.Add(new Line(x, y));
            return this;
        }

        public Path moveTo(float x, float y)
        {
            if (segments[segments.Count - 1] is Move)
            {
                segments.RemoveAt(segments.Count - 1);
            }
            segments.Add(new Move(x, y));
            return this;
        }

        public Path quadraticCurveTo(float cpx, float cpy, float x, float y)
        {
            segments.Add(new Quadratic(cpx, cpy, x, y));
            return this;
        }

        public Path reset()
        {
            segments.Clear();
            segments.Add(new Move(0, 0));
            return this;
        }

        public abstract class Segment
        {
            public readonly float x, y;
            public Segment(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public abstract int intersections(float x1, float y1, float x2, float y2, float sx, float sy);
        }

        public class Bezier : Segment
        {
            public readonly float c1x, c1y, c2x, c2y;
            public Bezier(float c1x, float c1y, float c2x, float c2y, float x, float y) : base(x, y)
            {
                this.c1x = c1x;
                this.c1y = c1y;
                this.c2x = c2x;
                this.c2y = c2y;
            }

            public override int intersections(float x1, float y1, float x2, float y2, float sx, float sy)
            {
                return 0;
            }
        }

        public class Move : Segment
        {
            public Move(float x, float y) : base(x, y) { }

            public override int intersections(float x1, float y1, float x2, float y2, float sx, float sy)
            {
                return 0;
            }
        }

        public class Line : Segment
        {
            public Line(float x, float y) : base(x, y) { }

            public override int intersections(float x1, float y1, float x2, float y2, float sx, float sy)
            {
                float x21 = x2 - x1, x43 = x - sx, x31 = sx - x1;
                float y21 = y2 - y1, y43 = y - sy, y31 = sy - y1;
                float ds = x43 * y21 - x21 * y43;
                if (ds == 0) return 0;
                float dt = x43 * y21 - x21 * y43;
                if (dt == 0) return 0;
                float s = (x43 * y31 - x31 * y43) / ds;
                if (s <= 0 || s > 1) return 0;
                float t = (x21 * y31 - x31 * y21) / dt;
                if (t <= 0 || t > 1) return 0;
                return 1;
            }
        }

        public class Quadratic : Segment
        {
            public readonly float cpx, cpy;

            public Quadratic(float cpx, float cpy, float x, float y) : base(x, y)
            {
                this.cpx = cpx;
                this.cpy = cpy;
            }

            public override int intersections(float x1, float y1, float x2, float y2, float sx, float sy)
            {
                return 0;
            }
        }
    }
}
