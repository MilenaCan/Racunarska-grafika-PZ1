using Racunarska_Grafika___PZ1.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Racunarska_Grafika___PZ1
{
    public class ImportXML
    {
        public static void LoadXml(Dictionary<long, SubstationEntity> substations, Dictionary<long, NodeEntity> nodes, Dictionary<long, SwitchEntity> switches, Dictionary<long, LineEntity> lines)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Geographic.xml");

            XmlNodeList nodeList;

            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");

            foreach (XmlNode node in nodeList)
            {

                double x, y;
                x = double.Parse(node.SelectSingleNode("X").InnerText, CultureInfo.InvariantCulture);
                y = double.Parse(node.SelectSingleNode("Y").InnerText, CultureInfo.InvariantCulture);


                long id = long.Parse(node.SelectSingleNode("Id").InnerText);
                substations.Add(id, new SubstationEntity
                {
                    Id = id,
                    Name = node.SelectSingleNode("Name").InnerText,
                    X = x,
                    Y = y
                });
            }


            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");

            foreach (XmlNode node in nodeList)
            {

                double x, y;
                x = double.Parse(node.SelectSingleNode("X").InnerText, CultureInfo.InvariantCulture);
                y = double.Parse(node.SelectSingleNode("Y").InnerText, CultureInfo.InvariantCulture);


                long id = long.Parse(node.SelectSingleNode("Id").InnerText);
                nodes.Add(id, new NodeEntity
                {
                    Id = id,
                    Name = node.SelectSingleNode("Name").InnerText,
                    X = x,
                    Y = y
                });
            }

            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");

            foreach (XmlNode node in nodeList)
            {
                double x, y;
                x = double.Parse(node.SelectSingleNode("X").InnerText, CultureInfo.InvariantCulture);
                y = double.Parse(node.SelectSingleNode("Y").InnerText, CultureInfo.InvariantCulture);


                long id = long.Parse(node.SelectSingleNode("Id").InnerText);
                switches.Add(id, new SwitchEntity
                {
                    Id = id,
                    Name = node.SelectSingleNode("Name").InnerText,
                    Status = node.SelectSingleNode("Status").InnerText,
                    X = x,
                    Y = y
                });
            }

            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

            List<Point> points = new List<Point>();
            LineEntity l = new LineEntity();

            foreach (XmlNode node in nodeList)
            {
                bool isUnderground;
                if (node.SelectSingleNode("IsUnderground").InnerText.Equals("true"))
                {
                    isUnderground = true;
                }
                else
                {
                    isUnderground = false;
                }

                long id = long.Parse(node.SelectSingleNode("Id").InnerText);
                lines.Add(id, new LineEntity
                {
                    Id = id,
                    Name = node.SelectSingleNode("Name").InnerText,
                    IsUnderground = isUnderground,
                    R = float.Parse(node.SelectSingleNode("R").InnerText),
                    ConductorMaterial = node.SelectSingleNode("ConductorMaterial").InnerText,
                    LineType = node.SelectSingleNode("LineType").InnerText,
                    ThermalConstantHeat = long.Parse(node.SelectSingleNode("ThermalConstantHeat").InnerText),
                    FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText),
                    SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText),
                });


            }
        }
       
        public static void AddingCanvasPoints(Dictionary<long, SubstationEntity> substations, Dictionary<long, NodeEntity> nodes, Dictionary<long, SwitchEntity> switches)
        {
            List<double> allLatLongsX = new List<double>();
            List<double> allLatLongsY = new List<double>();

            //da dobijem lat i lon i min i maks
            foreach (var node in nodes)
            {
                double LatLonX, LatLonY;
                ToLatLon(node.Value.X, node.Value.Y, 34, out LatLonX, out LatLonY);

                allLatLongsX.Add(LatLonX);
                allLatLongsY.Add(LatLonY);
            }

            foreach (var sub in substations)
            {
                double LatLonX, LatLonY;
                ToLatLon(sub.Value.X, sub.Value.Y, 34, out LatLonX, out LatLonY);

                allLatLongsX.Add(LatLonX);
                allLatLongsY.Add(LatLonY);
            }

            foreach (var switche in switches)
            {
                double LatLonX, LatLonY;
                ToLatLon(switche.Value.X, switche.Value.Y, 34, out LatLonX, out LatLonY);

                allLatLongsX.Add(LatLonX);
                allLatLongsY.Add(LatLonY);
            }

            double maximumLatLonX, maximumLatLonY, minimumLatLonX, minimumLatLonY;
            maximumLatLonX = allLatLongsX.Max();  //da bi izracunali relativnu poziciju svakog objekta na mapi
            maximumLatLonY = allLatLongsY.Max();  //min koo na osnovu kojih se vrsi skalirannje
            minimumLatLonX = allLatLongsX.Min();
            minimumLatLonY = allLatLongsY.Min();

            //opet prolazim kroz entitete da dobijem vrijednost na canvasu

            foreach (var sub in substations)
            {
                sub.Value.CanvasPoint = new CanvasPoint();
                double LatLonX, LatLonY;

                ToLatLon(sub.Value.X, sub.Value.Y, 34, out LatLonX, out LatLonY);

                sub.Value.CanvasPoint = LatLonToCanvasPoint(LatLonX, LatLonY, maximumLatLonX, maximumLatLonY, minimumLatLonX, minimumLatLonY);
            }

            foreach (var node in nodes)
            {
                node.Value.CanvasPoint = new CanvasPoint();
                double LatLonX, LatLonY;

                ToLatLon(node.Value.X, node.Value.Y, 34, out LatLonX, out LatLonY);

                node.Value.CanvasPoint = LatLonToCanvasPoint(LatLonX, LatLonY, maximumLatLonX, maximumLatLonY, minimumLatLonX, minimumLatLonY);
            }

            foreach (var switchh in switches)
            {
                switchh.Value.CanvasPoint = new CanvasPoint();
                double LatLonX, LatLonY;

                ToLatLon(switchh.Value.X, switchh.Value.Y, 34, out LatLonX, out LatLonY);

                switchh.Value.CanvasPoint = LatLonToCanvasPoint(LatLonX, LatLonY, maximumLatLonX, maximumLatLonY, minimumLatLonX, minimumLatLonY);
            }


        }

        //Tacke na Canvasu 
        public static CanvasPoint LatLonToCanvasPoint(double LatLonX, double LatLonY, double maximumLatLonX, double maximumLatLonY, double minimumLatLonX, double minimumLatLonY)
        {

            CanvasPoint canvasPoint = new CanvasPoint();
            canvasPoint.LatLonX = LatLonX;
            canvasPoint.LatLonY = LatLonY;

            canvasPoint.CanvasX = 800 * ((LatLonX - minimumLatLonX) / (maximumLatLonX - minimumLatLonX));
            canvasPoint.CanvasY = 800 * ((LatLonY - minimumLatLonY) / (maximumLatLonY - minimumLatLonY));

            return canvasPoint;
        }


        //From UTM to Latitude and longitude in decimal
        public static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = true;

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = zoneUTM;
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }
    }
}
