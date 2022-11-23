using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WPFPait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Color BackColor { get; set; }
        public Color BorderColor { get; set; }


        Ellipse ellipse;
        Rectangle rectangle;
        Point point1, point2;

        Polygon polygon;
        public List<double> PointY { get; set; } = new();
        public List<double> PointX { get; set; } = new();

        Point PolyPoint = new();
        PointCollection points = new PointCollection();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 8; i <=62; i++)
            {
                if (i % 2 == 0)
                    borderSizeComboB.Items.Add(i);
            }

            

           
            
        }

        private void myPointMouseDown(object sender, MouseButtonEventArgs e)
        {
            point1.X = e.GetPosition(this).X;
            point1.Y= e.GetPosition(this).Y;

            if (polygonRb.IsChecked==true)
            {
                PolyPoint=e.GetPosition(this);

                PolyPoint.X = e.GetPosition(this).X;
                PolyPoint.Y = e.GetPosition(this).Y - ((window.Height - 10) / 5);
                points.Add(PolyPoint);
                PointX.Add(PolyPoint.X);
                PointY.Add(PolyPoint.Y);
            }
        }


        private void ClickRadioButton(object sender, RoutedEventArgs e)
        {

        }

        private void backGroundComboB_SelectionChanged(object sender, SelectionChangedEventArgs e)
            =>BackColor=(Color)(backGroundComboB.SelectedItem as PropertyInfo)!.GetValue(null, null)!;
       

        private void combo_border_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => BorderColor = (Color)(borderColorComboB.SelectedItem as PropertyInfo)!.GetValue(null, null)!;


        private void myPointMouseUp(object sender, MouseButtonEventArgs e)
        {
            point2.X= e.GetPosition(this).X;
            point2.Y=e.GetPosition(this).Y;

            var point = new Point(point1.X > point2.X ? point2.X : point1.X, point1.Y < point2.Y ? point1.Y : point2.Y);

            if (rectangleRb.IsChecked == true)
            {
                var type = borderColorComboB.Items.GetType();

                rectangle = new()
                {
                    Height=Math.Abs(point1.Y-point2.Y),
                    Width=Math.Abs(point1.X-point2.X),
                    Fill= new SolidColorBrush(BackColor),
                    Stroke=new SolidColorBrush(BorderColor),
                    StrokeThickness=double.Parse(borderSizeComboB.Text),
                };

                mypoint.Children.Add(rectangle);
                Canvas.SetTop(rectangle,point.Y-(window.Height-10)/5);
                Canvas.SetLeft(rectangle, point.X);
            }

            else if (ellipseRb.IsChecked == true)
            {
                ellipse = new()
                {
                    Height = Math.Abs(point1.Y - point2.Y),
                    Width = Math.Abs(point1.X - point2.X),
                    Fill = new SolidColorBrush(BackColor),
                    Stroke = new SolidColorBrush(BorderColor),
                    StrokeThickness = double.Parse(borderSizeComboB.Text)
                };

                mypoint.Children.Add(ellipse);
                Canvas.SetTop(ellipse, point.Y - (window.Height - 10) / 5);
                Canvas.SetLeft(ellipse, point.X);
            }
            else if (polygonRb.IsChecked == true)
            {
                polygon = new()
                {
                    Points = points,
                    Height = PointX.Max(),
                    Width = PointY.Max(),
                    Fill = new SolidColorBrush(BackColor),
                    Stroke = new SolidColorBrush(BorderColor),
                    StrokeThickness = double.Parse(borderSizeComboB.Text)
                };

                mypoint.Children.Add(polygon);
                Canvas.SetTop(polygon, PointX.Min());
                Canvas.SetLeft(polygon, PointY.Min());
            }
        }
    }
}
