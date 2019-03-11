using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Rectangle rect;
        private Rectangle myrect1;
        private Rectangle myrect2;
        private Rectangle myrect3;

        public MainWindow()
        {
            InitializeComponent();
        }

        double newheight;
        double newwidth;
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            newheight = canvas.ActualHeight;
            newwidth = canvas.ActualWidth;
            //DrawRect();
            Drawrect1();
            Drawrect2();
            Drawrect3();

            Point p = new Point() { X = 0, Y = 0 };
            System.Windows.Rect Winrect = new Rect()
            {
                Height = 100,
                Width = 150,
                Location = p
            };
            //canvas.Children.Add();
            //canvas.Arrange();
           

        }

        public void Drawrect1()
        {
            myrect1 = new Rectangle
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 1,
                //Height = newheight - 50,
                //Width = newwidth - 150,
                //Height = 75,
                Width = 100
            };

            Point point = new Point(75, 25);
            Canvas.SetLeft(myrect1, point.X);
            Canvas.SetTop(myrect1, point.Y);
            
           

            TextBlock port1 = new TextBlock();
            port1.Text = "Port 1";
            Point TextPoint1 = new Point(point.X + 2, point.Y + 2);
            Canvas.SetLeft(port1, TextPoint1.X);
            Canvas.SetTop(port1, TextPoint1.Y);
            canvas.Children.Add(port1);

            TextBlock port2 = new TextBlock();
            port2.Text = "Port 2";
            Point TextPoint2 = new Point(point.X + 2, point.Y + 2 + 15);
            Canvas.SetLeft(port2, TextPoint2.X);
            Canvas.SetTop(port2, TextPoint2.Y);
            canvas.Children.Add(port2);

            Point point2 = new Point(275, 125);

            TextBlock port3 = new TextBlock();
            port3.Text = "Port 3";
            Point TextPoint3 = new Point(point.X +100 - 32, point.Y + 2);
            Canvas.SetLeft(port3, TextPoint3.X);
            Canvas.SetTop(port3, TextPoint3.Y);
            canvas.Children.Add(port3);

            TextBlock port4 = new TextBlock();
            port4.Text = "Port 4";
            
            Point TextPoint4 = new Point(point2.X + 2, point2.Y + 2 );
            Canvas.SetLeft(port4, TextPoint4.X);
            Canvas.SetTop(port4, TextPoint4.Y);
            canvas.Children.Add(port4);

            
            Point[] points = new Point[4];
            points[0] = new Point(TextPoint3.X + 32 , TextPoint3.Y + 10);
            points[1] = new Point( (points[0].X + TextPoint4.X)/2 , points[0].Y);
            points[2] = new Point(points[1].X, TextPoint4.Y + 10);
            points[3] = new Point(TextPoint4.X  , points[2].Y);

            for(int i = 0; i < 3;i++)
            {
                Line line = new Line() { X1 = points[i].X, Y1 = points[i].Y, X2 = points[i+1].X, Y2 = points[i+1].Y};
                line.StrokeThickness = 1;
                line.Stroke = Brushes.Black;
                canvas.Children.Add(line);

            }

           

            myrect1.Height = 12+12 + 10;
            canvas.Children.Add(myrect1);
            //Line line = new Line();
            //line.Stroke = Brushes.Yellow;
            //line.StrokeThickness = 1;

            //line.X1 = TextPoint3.X + 32;
            //line.Y1 = TextPoint3.Y + 10;
            //line.X2 = TextPoint4.X;
            //line.Y2 = TextPoint4.Y + 10;
            //canvas.Children.Add(line);
        }

        public void Drawrect2()
        {
            myrect2 = new Rectangle
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 1,
                //Height = newheight - 50,
                //Width = newwidth - 150,
                Height = 12 + 10,
                Width = 100
            };

            Point point = new Point(275, 125);
            Canvas.SetLeft(myrect2, point.X);
            Canvas.SetTop(myrect2, point.Y);
            canvas.Children.Add(myrect2);
        }

        public void Drawrect3()
        {
            myrect3 = new Rectangle
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 1,
                //Height = newheight - 50,
                //Width = newwidth - 150,
                Height = canvas.ActualHeight - 30,
                Width = canvas.ActualWidth - 30
            };

            Point point = new Point(15, 15);
            Canvas.SetLeft(myrect3, point.X);
            Canvas.SetTop(myrect3, point.Y);
            canvas.Children.Add(myrect3);
        }

        #region
        //private void DrawRect()
        //{
        //    //var newheight = canvas.ActualHeight ;
        //    //var newwidth = canvas.ActualWidth ;

        //    #region Rectangle Creation
        //    myrect = new Rectangle
        //    {
        //        //Stroke = Brushes.Blue,
        //        //StrokeThickness = 2,
        //        Height = (int)newheight - 50,
        //        Width = (int)newwidth - 150,
        //    };

        //    Canvas.SetLeft(myrect, 75);
        //    Canvas.SetTop(myrect, 25);
        //    canvas.Children.Add(myrect);
        //    #endregion

        //    #region Text creation
        //    TextBlock text = new TextBlock();
        //    text.Text = "Port 1";

        //    var x = 78;
        //    var y = 30;
        //    Canvas.SetLeft(text, x);
        //    Canvas.SetTop(text, y);
        //    canvas.Children.Add(text);
        //    #endregion

        //    #region Text creation
        //    TextBlock text1 = new TextBlock();
        //    text1.Text = "Port 2";

        //    var x1 = 78;
        //    var y1 = 50;
        //    //Point point2 = new Point(x1, y1);
        //    Canvas.SetLeft(text1, x1);
        //    Canvas.SetTop(text1, y1);
        //    canvas.Children.Add(text1);
        //    #endregion

        //    #region Text creation
        //    TextBlock text2 = new TextBlock();
        //    text2.Text = "Port 3";

        //    var x2 = 78;
        //    var y2 = 30;
        //    //Point point3 = new Point(x2,y2);
        //    Canvas.SetRight(text2, x2);
        //    Canvas.SetTop(text2, y2);
        //    canvas.Children.Add(text2);
        //    #endregion


        //}
        #endregion

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canvas);

            rect = new Rectangle
            {
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2
            };
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.Y);
            canvas.Children.Add(rect);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || rect == null)
                return;

            var pos = e.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            rect.Width = w;
            rect.Height = h;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }
    }
}
