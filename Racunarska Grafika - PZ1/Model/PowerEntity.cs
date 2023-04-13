using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racunarska_Grafika___PZ1.Model
{
    public class PowerEntity
    {
        private long id;
        private string name;
        private double x;
        private double y;
        private CanvasPoint canvasPoint;

        public PowerEntity()
        {

        }

        //public PowerEntity(long id, string name, double x, double y)
        //{
        //    Id = id;
        //    Name = name;


        //    double lat = 0;
        //    double lon = 0;

        //    ImportXML.ToLatLon(x, y, 34, out lat, out lon);

        //    X = Convert.ToInt64((x - 400131) / 17.587);
        //    Y = Convert.ToInt64((y - 5004702) / 17.587);
        //}
        public long Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public CanvasPoint CanvasPoint { get => canvasPoint; set => canvasPoint = value; }
    }
}
