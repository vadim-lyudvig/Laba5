using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Objects
{
    internal class Player : BaseObject
    {
        public int point = 0;
        public Action<Marker> OnMarkerOverlap; // Делегат для отслеживания пересечения игрока с маркером
        public Action<Circle> OnCircleOverlap; // Делегат для отслеживания пересечения игрока с кругом
        public Action<EnemyCircle> OnEnemyCircleOverlap; // Делегат для отслеживания пересечения игрока с вражеским кругом
        public float vX, vY;

        public Player(float x, float y, float angle) : base(x, y, angle)
        {
        }
        // Возвращает текущее положение объекта
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        //Метод для реализации делегата
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if(obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
            if(obj is Circle)
            {
                OnCircleOverlap(obj as Circle);
            }
            if(obj is EnemyCircle)
            {
                OnEnemyCircleOverlap(obj as EnemyCircle);
            }
        }

        // отрисовка объекта
        public override void Render(Graphics g)
        {
            //элипс
            g.FillEllipse(
                new SolidBrush(Color.DeepSkyBlue),
                -15, -15,
                30, 30
                );
            //контур элипса
            g.DrawEllipse(
                new Pen(Color.Black, 2),
                -15, -15,
                30, 30
                );
            //стрелка
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
        }
    }
}
