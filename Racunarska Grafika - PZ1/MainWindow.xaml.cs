using Racunarska_Grafika___PZ1.Model;
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



namespace Racunarska_Grafika___PZ1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        Data data;
        List<object> history;
        private List<object> ClearHistory = new List<object>();
        PowerEntity[,] matrix = new PowerEntity[160, 160];
        List<Line> allLines = new List<Line>();
        private List<System.Windows.Point> pointsList;
        int undoRedoPosition;

        public Shape LastClickedObject;

        public List<object> History { get => history; set => history = value; }
        public int UndoRedoPosition { get => undoRedoPosition; set => undoRedoPosition = value; }
        public List<System.Windows.Point> PointsList { get => pointsList; set => pointsList = value; }


        public MainWindow()
        {
            InitializeComponent();
            data = new Data();
            DrawData();
            DrawLines();
            PointsList = new List<System.Windows.Point>();
            History = new List<object>();
            UndoRedoPosition = -1;

        }

        public void DrawData()
        {
            foreach (NodeEntity node in data.Nodes.Values)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Fill = Brushes.DarkBlue;
                ellipse.Stroke = Brushes.Black;
                ellipse.StrokeThickness = 0.1;
                ellipse.ToolTip = "Id: " + node.Id + Environment.NewLine + "Name: " + node.Name;
                ellipse.Uid = node.Id.ToString();

                MapAndDraw(ellipse, node);
            }




            foreach (SubstationEntity sub in data.Substations.Values)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Fill = Brushes.DarkRed;
                ellipse.Stroke = Brushes.Yellow;
                ellipse.StrokeThickness = 0.1;
                ellipse.ToolTip = "Id: " + sub.Id + Environment.NewLine + "Name: " + sub.Name;
                ellipse.Uid = sub.Id.ToString();

                MapAndDraw(ellipse, sub);
            }

            foreach (SwitchEntity sw in data.Switches.Values)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 2;
                ellipse.Height = 2;
                ellipse.Fill = Brushes.DarkGreen;
                ellipse.Stroke = Brushes.MediumTurquoise;
                ellipse.StrokeThickness = 0.1;
                ellipse.ToolTip = "Id: " + sw.Id + Environment.NewLine + "Name: " + sw.Name + Environment.NewLine + "State: " + sw.Status;
                ellipse.Uid = sw.Id.ToString();

                MapAndDraw(ellipse, sw);
            }
        }

        private void DrawLines()
        {


            foreach (LineEntity line in data.Lines.Values)
            {

                CanvasPoint canvasPoint = null;
                CanvasPoint canvasPoint2 = null;

                if (data.Nodes.ContainsKey(line.FirstEnd))
                    canvasPoint = data.Nodes[line.FirstEnd].CanvasPoint;
                if (data.Nodes.ContainsKey(line.SecondEnd))
                    canvasPoint2 = data.Nodes[line.SecondEnd].CanvasPoint;

                if (data.Substations.ContainsKey(line.FirstEnd))
                    canvasPoint = data.Substations[line.FirstEnd].CanvasPoint;
                if (data.Substations.ContainsKey(line.SecondEnd))
                    canvasPoint2 = data.Substations[line.SecondEnd].CanvasPoint;

                if (data.Switches.ContainsKey(line.FirstEnd))
                    canvasPoint = data.Switches[line.FirstEnd].CanvasPoint;
                if (data.Switches.ContainsKey(line.SecondEnd))
                    canvasPoint2 = data.Switches[line.SecondEnd].CanvasPoint;

                if (canvasPoint == null || canvasPoint2 == null) //ako tacke za tu liniju nisu pronadjenje
                                                                 //prelazi se na sledecu
                    continue;

                Line drawingLine = new Line();

                Line drawingLine2 = new Line();

                drawingLine.Stroke = Brushes.Black;
                drawingLine.StrokeThickness = 1;
                drawingLine.ToolTip = $"ID:{line.Id}  Name:{line.Name}";
                drawingLine.Uid = line.Id.ToString() + ":" + line.FirstEnd.ToString() + ":" + line.SecondEnd.ToString();



                drawingLine2.Stroke = Brushes.Black;
                drawingLine2.StrokeThickness = 1;
                drawingLine2.ToolTip = $"ID:{line.Id}  Name:{line.Name}";
                drawingLine2.Uid = line.Id.ToString() + ":" + line.FirstEnd.ToString() + ":" + line.SecondEnd.ToString();

                if (canvasPoint.GridX == canvasPoint2.GridX) // da li su horizontalne
                {

                    drawingLine.X1 = canvasPoint.GridX + 1; // +1 da bi se linija pomjerila za 1 u desno i dole
                                                            // pa se crta gdje se nalaze cvorovi u mrezi
                    drawingLine.Y1 = canvasPoint.GridY + 1;
                    drawingLine.X2 = canvasPoint.GridX + 1;
                    drawingLine.Y2 = canvasPoint.GridY + 1;

                    allLines.Add(drawingLine);
                    Canvas.Children.Add(drawingLine);

                }
                else if (canvasPoint.GridY == canvasPoint2.GridY) //da li su vertikalne
                {

                    drawingLine.X1 = canvasPoint.GridX + 1;
                    drawingLine.Y1 = canvasPoint.GridY + 1;
                    drawingLine.X2 = canvasPoint2.GridX + 1;
                    drawingLine.Y2 = canvasPoint.GridY + 1;

                    allLines.Add(drawingLine);
                    Canvas.Children.Add(drawingLine);

                }
                else 
                {
                    drawingLine.X1 = canvasPoint.GridX + 1;
                    drawingLine.Y1 = canvasPoint.GridY + 1;
                    drawingLine.X2 = canvasPoint2.GridX + 1;
                    drawingLine.Y2 = canvasPoint.GridY + 1;

                    allLines.Add(drawingLine);
                    Canvas.Children.Add(drawingLine);

                    drawingLine2.X1 = canvasPoint2.GridX + 1;
                    drawingLine2.Y1 = canvasPoint.GridY + 1;
                    drawingLine2.X2 = canvasPoint2.GridX + 1;
                    drawingLine2.Y2 = canvasPoint2.GridY + 1;

                    allLines.Add(drawingLine2);
                    Canvas.Children.Add(drawingLine2);

                }
            }
        }
        


        public void MapAndDraw(Ellipse ellipse, PowerEntity entity)
        {

            int heightCanvas = (int)Canvas.Height;
            int widthCanvas = (int)Canvas.Width;
            int distancePoint = 5;


            int i = (int)(entity.CanvasPoint.CanvasX / distancePoint); //polozaj u matrici
            int j = (int)(entity.CanvasPoint.CanvasY / distancePoint);

            if (i == 160)
                i = 159;
            if (j == 160)
                j = 159;

            int distance = 0;


            //Trazimo sledece slobodno mjesto

            if (matrix[i, j] != null)
            {
                distance = 1; //najkraca distanca
                while (distance != 161) //u matrici 160x160
                {
                    if (i + distance < 160 && matrix[i + distance, j] == null)
                    {
                        matrix[i + distance, j] = entity;

                        i = i + distance;
                        break;
                    }
                    else if (i - distance > 0 && matrix[i - distance, j] == null)
                    {
                        matrix[i - distance, j] = entity;

                        i = i - distance;
                        break;
                    }
                    else if (j + distance < 160 && matrix[i, j + distance] == null)
                    {
                        matrix[i, j + distance] = entity;

                        j = j + distance;
                        break;
                    }
                    else if (j - distance > 0 && matrix[i, j - distance] == null)
                    {
                        matrix[i, j - distance] = entity;

                        j = j - distance;
                        break;
                    }
                    else if (j - distance > 0 && i - distance > 0 && matrix[i - distance, j - distance] == null)
                    {
                        matrix[i - distance, j - distance] = entity;

                        i = i - distance;
                        j = j - distance;
                        break;
                    }
                    else if (j + distance < 160 && i + distance < 160 && matrix[i + distance, j + distance] == null)
                    {
                        matrix[i + distance, j + distance] = entity;

                        i = i + distance;
                        j = j + distance;
                        break;
                    }
                    else if (i + distance < 160 && j - distance > 0 && matrix[i + distance, j - distance] == null)
                    {
                        matrix[i + distance, j - distance] = entity;

                        i = i + distance;
                        j = j - distance;
                        break;
                    }
                    else if (i - distance > 0 && j + distance < 160 && matrix[i - distance, j + distance] == null)
                    {
                        matrix[i - distance, j + distance] = entity;

                        i = i - distance;
                        j = j + distance;
                        break;
                    }
                    distance++;
                }


            }

            else
            {
                matrix[i, j] = entity;  //stavi na slobodno
            }

            Canvas.SetLeft(ellipse, i * distancePoint); //polozaj na platnu
            Canvas.SetTop(ellipse, j * distancePoint);

            Canvas.Children.Add(ellipse);

            entity.CanvasPoint.GridX = i * distancePoint; //koo entiteta u gridu, vrijednost na gridu
            entity.CanvasPoint.GridY = j * distancePoint;

        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point point = e.GetPosition((IInputElement)sender);
            if ((bool)Ellipse.IsChecked)
            {
                EllipseWindow newEllipse = new EllipseWindow(point, this);
                newEllipse.ShowDialog();
            }
            else if ((bool)Text.IsChecked)
            {
                TextWindow newText = new TextWindow(point, this);
                newText.ShowDialog();
            }
            else if ((bool)Polygon.IsChecked)
            {
                PointsList.Add(point);
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point point = e.GetPosition((IInputElement)sender);
            if ((bool)Polygon.IsChecked)
            {
                if (this.pointsList.Count >= 3)
                {
                    PolygonWindow newPolygon = new PolygonWindow(this, PointsList);
                    newPolygon.Show();
                }
                else if (this.pointsList.Count == 0)
                {
                    System.Windows.MessageBox.Show("You have to choose at least 3 points for polygon!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (UndoRedoPosition > -1)
            {
                if (History[UndoRedoPosition] is Ellipse)
                {
                    Ellipse ell = (Ellipse)History[UndoRedoPosition];
                    this.Canvas.Children.Remove(ell);
                    UndoRedoPosition--;
                }
                else if (History[UndoRedoPosition] is Polygon)
                {
                    Polygon pol = (Polygon)History[UndoRedoPosition];
                    this.Canvas.Children.Remove(pol);
                    UndoRedoPosition--;
                }
                else if (History[UndoRedoPosition] is TextBlock)
                {
                    TextBlock tb = (TextBlock)History[UndoRedoPosition];
                    this.Canvas.Children.Remove(tb);
                    UndoRedoPosition--;
                }
            }
            else //vraca posle clear element
            {
                foreach(var item in ClearHistory)
                {
                    if (item is Ellipse)
                    {
                        Ellipse ell = (Ellipse)item;
                        this.Canvas.Children.Add(ell);
                        UndoRedoPosition++;
                    }
                    else if(item is Polygon)
                    {
                        Polygon pol = (Polygon)item;
                        this.Canvas.Children.Add(pol);
                        UndoRedoPosition++;
                    }
                    else if (item is TextBlock)
                    {
                        TextBlock tb = (TextBlock)item;
                        this.Canvas.Children.Add(tb);
                        UndoRedoPosition++;
                    }
                }
                
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (History.Count > UndoRedoPosition + 1)
            {
                if (History[UndoRedoPosition + 1] is Ellipse)
                {
                    Ellipse ell = (Ellipse)History[UndoRedoPosition + 1];
                    this.Canvas.Children.Add(ell);
                    UndoRedoPosition++;
                }
                else if (History[UndoRedoPosition + 1] is Polygon)
                {
                    Polygon pol = (Polygon)History[UndoRedoPosition + 1];
                    this.Canvas.Children.Add(pol);
                    UndoRedoPosition++;
                }
                else if (History[UndoRedoPosition + 1] is TextBlock)
                {
                    TextBlock tb = (TextBlock)History[UndoRedoPosition + 1];
                    this.Canvas.Children.Add(tb);
                    UndoRedoPosition++;
                }
            }
            

            
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in History)
            {
                if (item is Ellipse)
                {
                    Ellipse ell = (Ellipse)item;
                    ClearHistory.Add(ell);
                    this.Canvas.Children.Remove(ell);
                    
                }
                else if (item is Polygon)
                {
                    Polygon pol = (Polygon)item;
                    ClearHistory.Add(pol);
                    this.Canvas.Children.Remove(pol);
                }
                else if (item is TextBlock)
                {
                    TextBlock tb = (TextBlock)item;
                    ClearHistory.Add(tb);
                    this.Canvas.Children.Remove(tb);
                }
            }
            History.Clear();
            UndoRedoPosition = -1;
        }

        
    }
}
