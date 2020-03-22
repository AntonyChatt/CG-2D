using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D
{
    public partial class Form1 : Form
    {
        public Form1()// Also, you can change Form background-color
        {
            InitializeComponent();
        }

        void drawAxises(Graphics g, Pen myPen, Brush myBrush)
        {
            g.DrawLine(myPen, ClientSize.Width / 2, 0, ClientSize.Width / 2, ClientSize.Height);
            g.DrawLine(myPen, 0, ClientSize.Height / 2, ClientSize.Width, ClientSize.Height / 2);

            PointF[] Y = new PointF[]
            {
                 new PointF(ClientSize.Width / 2, 0),
                 new PointF((ClientSize.Width / 2) - 5, 5),
                 new PointF((ClientSize.Width / 2) + 5, 5)
            };

            PointF[] X = new PointF[]
            {
                 new PointF(ClientSize.Width, ClientSize.Height / 2),
                 new PointF((ClientSize.Width) - 5,(ClientSize.Height / 2) - 5),
                 new PointF((ClientSize.Width ) - 5,(ClientSize.Height / 2) + 5)
            };

            string y = "y";
            string x = "x";
            string zero = "0";

            PointF forY = new PointF((ClientSize.Width / 2) - 20, 0);
            PointF forX = new PointF(ClientSize.Width - 15, (ClientSize.Height / 2) + 5);
            PointF forZero = new PointF((ClientSize.Width / 2) - 15, (ClientSize.Height / 2) + 5);

            Font f = new Font(this.Font, FontStyle.Bold);

            g.DrawString(y, f, myBrush, forY);
            g.DrawString(x, f, myBrush, forX);
            g.DrawString(zero, f, myBrush, forZero);

            g.FillPolygon(myBrush, Y);
            g.FillPolygon(myBrush, X);
        }

        void drawPolygon(Graphics g, Pen polygonPen)
        {
            int interval = ClientSize.Width / 20;

            PointF[] polygon = new PointF[]//in this massive you must to enter your points for polygon
            {
                new PointF((ClientSize.Width / 2) - 2 * interval, (ClientSize.Height / 2) - 2 * interval),
                new PointF((ClientSize.Width / 2) - 2 * interval, (ClientSize.Height / 2) + 2 * interval),
                new PointF((ClientSize.Width / 2) + 2 * interval, (ClientSize.Height / 2) + 2 * interval),
                new PointF((ClientSize.Width / 2) + 3 * interval, (ClientSize.Height / 2) + 3 * interval),
                new PointF((ClientSize.Width / 2) + 4 * interval, (ClientSize.Height / 2)),
                new PointF((ClientSize.Width / 2) + 2 * interval, (ClientSize.Height / 2) - 2 * interval)
            };

            g.DrawPolygon(polygonPen, polygon);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Refresh();
            Graphics g = this.CreateGraphics();
            Pen myPen = new Pen(Color.MidnightBlue, 2);  //Change color
            Brush myBrush = new SolidBrush(Color.MidnightBlue); //Change color
            Pen polygonPen = new Pen(Color.SlateBlue, 2);  //Change color
            drawAxises(g, myPen, myBrush);
            drawPolygon(g, polygonPen);
        }


    }
}
