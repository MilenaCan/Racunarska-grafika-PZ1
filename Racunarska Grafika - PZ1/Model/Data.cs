using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Racunarska_Grafika___PZ1.Model
{
    public class Data
    {
        public Dictionary<long, LineEntity> lines = new Dictionary<long, LineEntity>();
        public Dictionary<long, NodeEntity> nodes = new Dictionary<long, NodeEntity>();
        public Dictionary<long, SubstationEntity> substations = new Dictionary<long, SubstationEntity>();
        public Dictionary<long, SwitchEntity> switches = new Dictionary<long, SwitchEntity>();
        public List<List<UIElement>> canvasObjects = new List<List<UIElement>>();

        public Data()
        {
            ImportXML.LoadXml(substations, nodes, switches, lines);
            ImportXML.AddingCanvasPoints(substations, nodes, switches);
        }

        public Dictionary<long, LineEntity> Lines { get => lines; set => lines = value; }
        public Dictionary<long, NodeEntity> Nodes { get => nodes; set => nodes = value; }
        public Dictionary<long, SubstationEntity> Substations { get => substations; set => substations = value; }
        public Dictionary<long, SwitchEntity> Switches { get => switches; set => switches = value; }
    }
}
