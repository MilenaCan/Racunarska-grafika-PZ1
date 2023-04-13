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

namespace Racunarska_Grafika___PZ1
{
    /// <summary>
    /// Interaction logic for PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        
        public MainWindow mainWindow;
        List<Point> pointList;
        StackPanel stackPanel = new StackPanel();
        Polygon polygon = new Polygon();

        public PolygonWindow(MainWindow main, List<Point>points)
        {
            InitializeComponent();
            this.mainWindow = main;
            this.pointList = points;
            
        }

        Brush polygonColor;
        private void button_PolygonColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                polygonColor = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));

            }
        }

        private void button_DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_cThickness.Text))
            {
                System.Windows.MessageBox.Show("Please enter a value for Thickness.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double thicknessValue;
            if (!double.TryParse(tb_cThickness.Text, out thicknessValue))
            {
                System.Windows.MessageBox.Show("Invalid value for Thickness.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (thicknessValue < 0)
            {
                System.Windows.MessageBox.Show("Invalid value for Thickness.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = double.Parse(tb_cThickness.Text);
            foreach (var item in pointList)
            {

                polygon.Points.Add(item);

            }

            polygon.Fill = polygonColor;
            mainWindow.Canvas.Children.Add(polygon);
            mainWindow.PointsList.Clear();
            mainWindow.History.Add(polygon);
            mainWindow.UndoRedoPosition++;

            VisualBrush visualBrush = new VisualBrush();

            TextBlock myTextBlock = new TextBlock();
            myTextBlock.Text = tb_AddText.Text;

            myTextBlock.Foreground = textColor;
            myTextBlock.FontSize = (double)10;
            myTextBlock.Margin = new Thickness(10);


            stackPanel.Background = polygonColor;

            stackPanel.Children.Add(myTextBlock);
            visualBrush.Visual = stackPanel;
            polygon.Fill = visualBrush;

            polygon.MouseLeftButtonDown += new MouseButtonEventHandler(ChangeObject);

            if (tb_transparency.Text != "")
            {
                polygon.Opacity = 1 - (double.Parse(tb_transparency.Text) / 100);

            }
            
            this.Close();
        }

        Brush textColor;
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
            mainWindow.LastClickedObject = polygon;
            ChangeObject changeObject = new ChangeObject(mainWindow, stackPanel);
            changeObject.ShowDialog();
            return;
        }
    }
}
