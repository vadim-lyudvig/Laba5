using Events.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Events
{
    public partial class Form1 : Form
    {
        static Random rnd = new Random();
        Player player;
        List<BaseObject> objects = new List<BaseObject>();
        Marker marker;

        public Form1()
        {
            InitializeComponent();
            player = new Player(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            objects.Add(player);
            marker = new Marker(pictureBox1.Width / 2 + 1, pictureBox1.Height / 2 + 1, 0);
            objects.Add(marker);
            this.objects.Add(new Circle(rnd.Next(30, pictureBox1.Width - 30), rnd.Next(30, pictureBox1.Height - 30), 0));
            this.objects.Add(new Circle(rnd.Next(30, pictureBox1.Width - 30), rnd.Next(30, pictureBox1.Height - 30), 0));
            this.objects.Add(new EnemyCircle(rnd.Next(30, pictureBox1.Width - 30), rnd.Next(30, pictureBox1.Height - 30), 0));
            //реакция на пересечение
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            // пересечение маркера
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            // пересечение круга
            player.OnCircleOverlap += (c) =>
            {
                objects.Remove(c);
                player.point++;
                this.objects.Add(new Circle(rnd.Next(30, pictureBox1.Width - 30), rnd.Next(30, pictureBox1.Height - 30), 0));
            };
            // пересечение вражеского круга
            player.OnEnemyCircleOverlap += (ec) =>
            {
                objects.Remove(ec);
                player.point--;
                this.objects.Add(new EnemyCircle(rnd.Next(30, pictureBox1.Width - 30), rnd.Next(30, pictureBox1.Height - 30), 0));
            };
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }
        // Обновление положение игрока
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float lenght = (float)Math.Sqrt(dx * dx + dy * dy);
                dx /= lenght;
                dy /= lenght;
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;
                player.Angle = (float)(90 - Math.Atan2(player.vX, player.vY) * 180 / Math.PI);
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            points.Text = $"Кол-во очков игрока: {player.point}";
            pictureBox1.Invalidate();

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
