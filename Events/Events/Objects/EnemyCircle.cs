using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Objects
{
    internal class EnemyCircle : BaseObject
    {
        private float radius = 0;
        public EnemyCircle(float x, float y, float angle) : base(x, y, angle)
        {
        }
        // Метод для описания формы объекта
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15 - radius / 2, -15 - radius / 2,
               30 + radius, 30 + radius);
            return path;
        }
        // Метод для отрисовки объекта
        public override void Render(Graphics g)
        {
            radius+=0.6f;
            g.FillEllipse(
               new SolidBrush(Color.FromArgb(128, 255, 0, 0)),
               -15 - radius / 2, -15 - radius / 2,
               30 + radius, 30 + radius
               );
            g.DrawEllipse(
               new Pen(Color.DarkRed, 2),
               -15 - radius / 2, -15 - radius / 2,
               30 + radius, 30 + radius
               );
        }
    }
}
