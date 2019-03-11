using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VHDLGenerator.Models;


namespace VHDLGenerator.Models
{
    class DrawingFile
    {
        //canvas does not exist within this file

        //private void RenderDatapath(DataPathModel _data)
        //{

        //    if (_data.Name != null)
        //    {
        //        Rectangle dprect = new Rectangle
        //        {
        //            Stroke = Brushes.Blue,
        //            StrokeThickness = 1,
        //            Height = canvas.ActualHeight - 100,
        //            Width = canvas.ActualWidth - 100
        //        };

        //        Point spoint = new Point(50, 50);
        //        Canvas.SetLeft(dprect, spoint.X);
        //        Canvas.SetTop(dprect, spoint.Y);
        //        canvas.Children.Add(dprect);

        //        if (_data.Ports != null)
        //        {
        //            int incount = 1;
        //            int outcount = 1;

        //            foreach (PortModel port in _data.Ports)
        //            {
        //                TextBlock textBlock = new TextBlock() { Text = port.Name, FontSize = 10 };
        //                Point point = new Point();
        //                if (port.Direction == "in")
        //                {
        //                    point.X = spoint.X + 5;
        //                    point.Y = spoint.Y + (incount * 10);
        //                    incount++;
        //                }
        //                else
        //                {

        //                    point.X = spoint.X + dprect.Width - (textBlock.Text.Length * 5) - 5;
        //                    point.Y = spoint.Y + (outcount * 10);
        //                    outcount++;
        //                }
        //                Canvas.SetLeft(textBlock, point.X);
        //                Canvas.SetTop(textBlock, point.Y);
        //                canvas.Children.Add(textBlock);
        //            }
        //        }
        //    }
        //}

        //private void RenderComponent(DataPathModel _data)
        //{
        //    if (_data.Components != null)
        //    {
        //        Point point = new Point(50, 50);
        //        Point StartPoint = new Point() { X = 100, Y = 100 };
        //        int count1 = 0;
        //        int count2 = 0;
        //        int height = 0;

        //        foreach (PortModel port in _data.Components.Last().Ports)
        //        {
        //            if (port.Direction == "in")
        //                count1++;
        //            else
        //                count2++;
        //        }

        //        if (count1 > count2)
        //            height = count1;
        //        else
        //            height = count2;

        //        Rectangle rectComp = new Rectangle()
        //        {
        //            Stroke = Brushes.Black,
        //            StrokeThickness = 1,
        //            Height = (height * 10) + 20,
        //            Width = 100
        //        };
        //        Canvas.SetLeft(rectComp, StartPoint.X);
        //        Canvas.SetTop(rectComp, StartPoint.Y);
        //        canvas.Children.Add(rectComp);

        //        RenderPortText(_data.Components.Last().Ports, StartPoint);
        //    }
        //}

        //private void RenderPortText(List<PortModel> _data, Point _point)
        //{
        //    int incount = 1;
        //    int outcount = 1;
        //    foreach (PortModel port in _data)
        //    {
        //        TextBlock textBlock = new TextBlock() { Text = port.Name, FontSize = 10 };
        //        Point point = new Point();
        //        if (port.Direction == "in")
        //        {
        //            point.X = _point.X + 5;
        //            point.Y = _point.Y + (incount * 10);
        //            incount++;
        //        }
        //        else
        //        {
        //            point.X = _point.X + rect.Width - (textBlock.Text.Length * 5) - 5;
        //            point.Y = _point.Y + (outcount * 10);
        //            outcount++;
        //        }
        //        Canvas.SetLeft(textBlock, point.X);
        //        Canvas.SetTop(textBlock, point.Y);
        //        canvas.Children.Add(textBlock);
        //    }

        //}
    }
}
