﻿using System;
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
            Line vert = new Line();
            vert.X1 = Math.Floor(Panel.ActualWidth / 2);
            vert.X2 = Math.Floor(Panel.ActualWidth / 2);
            vert.Y1 = 0;
            vert.Y2 = Math.Floor(Panel.ActualHeight);
            vert.Stroke = Brushes.Black;
            Panel.Children.Add(vert);
            Line hor = new Line();
            hor.X1 = 0;
            hor.X2 = Math.Floor(Panel.ActualWidth);
            hor.Y1 = -Math.Floor(Panel.ActualHeight / 2);
            hor.Y2 = -Math.Floor(Panel.ActualHeight / 2);
            hor.Stroke = Brushes.Black;
            Panel.Children.Add(hor);
        }

        void drawline(Complex start, Complex end)
        {
            Line dline = new Line();
            dline.Stroke = Brushes.Blue;
            dline.StrokeThickness = 5;
            dline.X1 = start.Real * 500 + Math.Floor(Panel.ActualWidth / 2);
            dline.X2 = end.Real * 500 + Math.Floor(Panel.ActualWidth / 2);
            dline.Y1 = start.Imaginary * 500 - Math.Floor(Panel.ActualHeight / 2);
            dline.Y2 = end.Imaginary * 500 - Math.Floor(Panel.ActualHeight / 2);
            Panel.Children.Add(dline);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double phi = 0;
            double h = (2 * Math.PI) / source.Length;
            double c = -0.05;
            double k = -0.05;
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = Complex.FromPolarCoordinates(1.05, phi);
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
    }
}