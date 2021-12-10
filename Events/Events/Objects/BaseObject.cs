using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Events.Objects
{
    internal class BaseObject
    {
        public Action<BaseObject, BaseObject> OnOverlap; // Делегат для отслеживания пересечения

        public float X;
        public float Y;
        public float Angle;
        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }
        // матрица трансформации 
        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        }
        //Метод отрисовки
        public virtual void Render(Graphics g)
        {

        }
        // Возвращает текущее положение объекта 
        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
        //Метод для реализации делегата
        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            var region = new Region(path1);
            region.Intersect(path2); 
            return !region.IsEmpty(g); 
        }
        public virtual void Overlap(BaseObject obj)
        {
            if(this.OnOverlap != null)
            {
                this.OnOverlap(this, obj);
            }
        }
    }
}
