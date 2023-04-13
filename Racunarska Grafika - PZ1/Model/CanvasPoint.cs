using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racunarska_Grafika___PZ1.Model
{
    public class CanvasPoint
    {
        private double latLonX;
        private double latLonY;
        private double canvasX;  //za polozaj u matrici
        private double canvasY;
        private int gridX;  //za polozaj na platnu
        private int gridY;

        public double LatLonX { get => latLonX; set => latLonX = value; }
        public double LatLonY { get => latLonY; set => latLonY = value; }
        public double CanvasX { get => canvasX; set => canvasX = value; }
        public double CanvasY { get => canvasY; set => canvasY = value; }
        public int GridX { get => gridX; set => gridX = value; }
        public int GridY { get => gridY; set => gridY = value; }
    }
}
