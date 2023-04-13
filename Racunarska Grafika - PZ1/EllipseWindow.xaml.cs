using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;

namespace Racunarska_Grafika___PZ1
{
    public partial class EllipseWindow : Window
    {

        public Point point { get; set; }
        public MainWindow mainWindow { get; set; }
        StackPanel stackPanel = new StackPanel();
        VisualBrush visualBrush = new VisualBrush();

        Ellipse ellipse = new Ellipse();
        public Shape LastClickedObject { get; set; }

        public EllipseWindow(Point point, MainWindow mainWindow)
        {
            InitializeComponent();
            this.point = point;
            this.mainWindow = mainWindow;
        }

        Brush ellipseColor;

        private void button_EllipseColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ellipseColor = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
                
            }
        }

        Brush textColor;
        private void button_DrawEllipse_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(radiusX.Text) || string.IsNullOrWhiteSpace(radiusY.Text))
            {
                System.Windows.MessageBox.Show("Please enter a value for both X and Y radius.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double radiusXValue, radiusYValue;
            if (!double.TryParse(radiusX.Text, out radiusXValue) || !double.TryParse(radiusY.Text, out radiusYValue))
            {
                System.Windows.MessageBox.Show("Invalid value for X or Y radius. Please enter valid numbers.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(radiusXValue < 0 || radiusYValue < 0) 
            {
                System.Windows.MessageBox.Show("Invalid value for X or Y radius. Please enter valid numbers.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            if (string.IsNullOrWhiteSpace(Thicness.Text))
            {
                System.Windows.MessageBox.Show("Please enter a value for Thickness.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            double thicknessValue;
            if (!double.TryParse(Thicness.Text, out thicknessValue))
            {
                System.Windows.MessageBox.Show("Invalid value for Thickness.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(thicknessValue < 0)
            {
                System.Windows.MessageBox.Show("Invalid value for Thickness.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            ellipse.Width = double.Parse(radiusX.Text) * 2;
            ellipse.Height = double.Parse(radiusY.Text) * 2;
            ellipse.Fill = ellipseColor;
            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = double.Parse(Thicness.Text);
            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);
            mainWindow.Canvas.Children.Add(ellipse);
            mainWindow.History.Add(ellipse);
            mainWindow.UndoRedoPosition++;

            TextBlock myTextBlock = new TextBlock();


            myTextBlock.Text = tb_AddText.Text;
            myTextBlock.FontSize = (double)10;
            myTextBlock.Margin = new Thickness(10);

            myTextBlock.Foreground = textColor;

            stackPanel.Background = ellipseColor;
            stackPanel.Children.Add(myTextBlock);
            visualBrush.Visual = stackPanel;
            ellipse.Fill = visualBrush;

            if (tb_transparency.Text != "")
                ellipse.Opacity = 1 - (double.Parse(tb_transparency.Text) / 100);

            

            ellipse.MouseLeftButtonDown += new MouseButtonEventHandler(ChangeObject);

            Canvas.SetLeft(mainWindow, point.X);
            Canvas.SetTop(mainWindow, point.Y);
            //TextBlock textBlock = new TextBlock();
            //textBlock.Text = tb_AddText.Text;
            //textBlock.Foreground = textColor;
            //Canvas.SetLeft(textBlock, point.X + double.Parse(radiusX.Text));
            //Canvas.SetTop(textBlock, point.Y + double.Parse(radiusY.Text));
            //if (tb_transparency.Text != "")
            //ellipse.Opacity = 1 - (double.Parse(tb_transparency.Text) / 100);
            //mainWindow.Canvas.Children.Add(textBlock);
            this.Close();

        }

        
        private void button_TextColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textColor = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
            }
        }

        private void ChangeObject(object sender, RoutedEventArgs e)
        {
            mainWindow.LastClickedObject = ellipse;
            ChangeObject changeObject = new ChangeObject(mainWindow, stackPanel);
            changeObject.ShowDialog();
            return;
        }


    }
}
