using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace Joukowsky_transform
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Complex[] source = new Complex[100];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        void drawline(Complex start, Complex end)
        {
            Line dline = new Line();
            dline.Stroke = Brushes.Blue;
            dline.StrokeThickness = 5;
            dline.X1 = start.Real * 500 + Math.Floor(MainPanel.ActualWidth / 2);
            dline.X2 = end.Real * 500 + Math.Floor(MainPanel.ActualWidth / 2);
            dline.Y1 = start.Imaginary * 500 - Math.Floor(MainPanel.ActualHeight / 2);
            dline.Y2 = end.Imaginary * 500 - Math.Floor(MainPanel.ActualHeight / 2);
            MainPanel.Children.Add(dline);
        }

        void Cln()
        {
            MainPanel.Children.Clear();
            Line vert = new Line();
            vert.X1 = 130;
            vert.X2 = 130;
            vert.Y1 = 0;
            vert.Y2 = Math.Floor(MainPanel.ActualHeight);
            vert.Stroke = Brushes.Black;
            MainPanel.Children.Add(vert);
            Line hor = new Line();
            hor.X1 = 0;
            hor.X2 = Math.Floor(MainPanel.ActualWidth);
            hor.Y1 = -Math.Floor(MainPanel.ActualHeight / 2);
            hor.Y2 = -Math.Floor(MainPanel.ActualHeight / 2);
            hor.Stroke = Brushes.Black;
            Line scls = new Line();
            scls.X1 = 1130;
            scls.X2 = 1130;
            scls.Y1 = -Math.Floor(MainPanel.ActualHeight / 2) + 10;
            scls.Y2 = -Math.Floor(MainPanel.ActualHeight / 2) - 10;
            scls.Stroke = Brushes.Black;
            MainPanel.Children.Add(scls);
            MainPanel.Children.Add(hor);
        }

        public Complex S1(Complex z)
        {
            return Complex.Divide(z-1,z+1);
        }
        public Complex S2(Complex z)
        {
            return Complex.Pow(z,2);
        }
        public Complex S3(Complex z)
        {
            return Complex.Divide(1 + z, 1 - z);
        }
        public Complex Jouktranform(Complex z)
        {
            
            return S3(S2(S1(z)));
        }

        void Update()
        {
            Cln();
            double phi = 0;
            double h = (2 * Math.PI) / source.Length;
            double c = Convert.ToDouble(centerX.Text);
            double k = Convert.ToDouble(centerY.Text);
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = Complex.FromPolarCoordinates(Convert.ToDouble(circleR.Text), phi);
                source[i] = new Complex(source[i].Real + c, source[i].Imaginary + k);
                phi += h;
            }
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = Jouktranform(source[i]);
            }
            for (int i = 0; i < source.Length - 1; i++)
            {
                drawline(source[i], source[i + 1]);
            }
            drawline(source[source.Length - 1], source[0]);
        }

        private void centerX_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double text = Convert.ToDouble(centerX.Text);
            if (e.Delta > 0)
            {
                text += 0.01;
            }
            else
            {
                text -= 0.01;
            }
            centerX.Text = text.ToString();
            Update();
        }

        private void centerY_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double text = Convert.ToDouble(centerY.Text);
            if (e.Delta > 0)
            {
                text += 0.01;
            }
            else
            {
                text -= 0.01;
            }
            centerY.Text = text.ToString();
            Update();
        }

        private void circleR_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double text = Convert.ToDouble(circleR.Text);
            if (e.Delta > 0)
            {
                text += 0.01;
            }
            else
            {
                text -= 0.01;
            }
            circleR.Text = text.ToString();
            Update();
        }
    }
}
