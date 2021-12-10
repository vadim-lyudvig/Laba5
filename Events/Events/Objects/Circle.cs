using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Objects
{
    internal class Circle : BaseObject
    {
        public Circle(float x, float y, float angle) : base(x, y, angle)
        {

        }
        // Метод для описания формы объекта
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        // Метод для отрисовки объекта
        public override void Render(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.Green),
                -15, -15,
                30, 30
                );
            g.DrawEllipse(
                new Pen(Color.DarkGreen, 2),
                -15, -15,
                30, 30
                );
        }
    }
}
