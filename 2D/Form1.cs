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
        Pen myPen = new Pen(Color.MidnightBlue, 2);
        Brush myBrush = new SolidBrush(Color.MidnightBlue);
        Pen polygonPen = new Pen(Color.SlateBlue, 2);
        Pen reflectPen = new Pen(Color.HotPink, 2);
        Pen turnPen = new Pen(Color.Tomato, 2);
        Pen scalePen = new Pen(Color.Plum, 2);
        Pen shiftPen = new Pen(Color.DarkTurquoise, 2);

        float Fi = (float)(0.5 * 180 / Math.PI);

        PointF[] global = new PointF[] { };
        float[,] globalMatrix = new float[3, 6];
        float sum = 0;
        float[,] buff = new float[3, 6];

        float x0;
        float y0;
        float interval;

        float[,] reflection =  
        {
            { 1, 0, 0 },
            { 0, -1, 0 },
            { 0, 0, 1 }
        };

        float[,] scale =
        {
             { -(float)0.5, 0, 0 },
             { 0, -(float)0.5, 0 },
             { 0, 0, 1 }
        };

        float[,] shift = 
        {
            {-1,0,0 },
            {0,-1,0 },
            {50,40,1 }
        };

        void multiplier(float[,] A)
        {         
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        sum += A[k, i] * globalMatrix[k, j];
                    }

                    buff[i, j] = sum;
                    sum = 0;
                }              
            }            
        }     

        void drawAxises(Graphics g, float x0, float y0)
        {
            g.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, x0, y0);
            
            g.DrawLine(myPen, -x0, 0, x0, 0);
            g.DrawLine(myPen, 0, -y0, 0, y0);

            PointF[] Y = new PointF[]
            {
                 new PointF(0, -y0),
                 new PointF(5, -y0 + 5),
                 new PointF(-5, -y0 + 5)
            };

            PointF[] X = new PointF[]
            {
                 new PointF(x0, 0),
                 new PointF(x0 - 5, -5),
                 new PointF(x0 - 5, 5)
            };

            string y = "y";
            string x = "x";
            string zero = "0";

            PointF forY = new PointF(-20, -y0);
            PointF forX = new PointF(x0 - 15, 5);
            PointF forZero = new PointF(-15, 5);

            Font f = new Font(this.Font, FontStyle.Bold);

            g.DrawString(y, f, myBrush, forY);
            g.DrawString(x, f, myBrush, forX);
            g.DrawString(zero, f, myBrush, forZero);

            g.FillPolygon(myBrush, Y);
            g.FillPolygon(myBrush, X);
        }

        void drawPolygon(Graphics g)
        {
           float interval = ClientSize.Width / 20;

            PointF[] global = 
            {
                new PointF(-2 * interval, -2 * interval),
                new PointF(-2 * interval, 2 * interval),
                new PointF(2 * interval, 2 * interval),
                new PointF(3 * interval, 3 * interval),
                new PointF(4 * interval, 0),
                new PointF(2 * interval,-2 * interval)
            };

            for ( int i = 0; i < 3; i++ )
            {
                for ( int j = 0; j < 6; j++ )
                {
                    switch (i)
                    {
                        case 0:
                            globalMatrix[i, j] = -global[j].X;
                            break;
                        case 1:
                            globalMatrix[i, j] = -global[j].Y;
                            break;
                        case 2:
                            globalMatrix[i, j] = 1;
                            break;
                        default:
                            break;
                    }                    
                }
            }

            g.DrawPolygon(polygonPen, global);
        }

        void reflectPolygon(Graphics g)
        {

            multiplier( reflection );

            PointF[] global =
            {
                new PointF(buff[0,0], buff[1,0]),
                new PointF(buff[0,1], buff[1,1]),
                new PointF(buff[0,2], buff[1,2]),
                new PointF(buff[0,3], buff[1,3]),
                new PointF(buff[0,4], buff[1,4]),
                new PointF(buff[0,5], buff[1,5]),
            };

            g.DrawPolygon(reflectPen, global);
        }
  
        void turnPolygon(Graphics g) 
        {
            float[,] turn = {
                { (float)(Math.Cos(Fi)), (float)(-Math.Sin(Fi)), 0 },
                { (float)(Math.Sin(Fi)), (float)(Math.Cos(Fi)), 0 },
                { 0, 0, 1 }
            };

            multiplier( turn );

           PointF[] global =
           {
                new PointF(buff[0,0], buff[1,0]),
                new PointF(buff[0,1], buff[1,1]),
                new PointF(buff[0,2], buff[1,2]),
                new PointF(buff[0,3], buff[1,3]),
                new PointF(buff[0,4], buff[1,4]),
                new PointF(buff[0,5], buff[1,5]),
            };

            g.DrawPolygon(turnPen, global);
        } 

        void scalePolygon(Graphics g) 
        {
            multiplier( scale );

            PointF[] global =
            {
                new PointF(buff[0,0], buff[1,0]),
                new PointF(buff[0,1], buff[1,1]),
                new PointF(buff[0,2], buff[1,2]),
                new PointF(buff[0,3], buff[1,3]),
                new PointF(buff[0,4], buff[1,4]),
                new PointF(buff[0,5], buff[1,5]),
            };

            g.DrawPolygon(scalePen, global);
        }

        void shiftPolygon(Graphics g)
        {
            multiplier( shift );

            PointF[] global =
            {
                new PointF(buff[0,0], buff[1,0]),
                new PointF(buff[0,1], buff[1,1]),
                new PointF(buff[0,2], buff[1,2]),
                new PointF(buff[0,3], buff[1,3]),
                new PointF(buff[0,4], buff[1,4]),
                new PointF(buff[0,5], buff[1,5]),
            };

            g.DrawPolygon(shiftPen, global);
        }        
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)  // Draw axises and polygon

        {
            Refresh();
            Graphics g = this.CreateGraphics();

            x0 = ClientSize.Width / 2;
            y0 = ClientSize.Height / 2;

            interval = ClientSize.Width / 20;

            drawAxises(g, x0, y0);
            drawPolygon(g);

            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;          
        }

         private void Button2_Click(object sender, EventArgs e) // turn it for 0.5 radians
         {
            Graphics g = this.CreateGraphics();

            drawAxises(g, x0, y0);
            turnPolygon(g);
         }
        
         private void Button3_Click(object sender, EventArgs e) //scale for 1/2
         {
            Graphics g = this.CreateGraphics();

            drawAxises(g, x0, y0);
            scalePolygon(g);
        }
    
        private void Button4_Click(object sender, EventArgs e) // reflect polygon
        {
            Graphics g = this.CreateGraphics();
            
            drawAxises( g,x0, y0);
            reflectPolygon(g);
        }

        private void Button5_Click(object sender, EventArgs e) // Shift polygon for 50 & 40 px
        {
            Graphics g = this.CreateGraphics();

            drawAxises(g, x0, y0);
            shiftPolygon(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
}
    }
   