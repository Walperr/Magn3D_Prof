using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Magn3D_Prof.Main
{
    public partial class SettingsForm : Form
    {
        public static double minX;
        public static double maxX;
        public static double minY;
        public static double maxY;

        public static double stepX;
        public static double stepY;

        public static double decimals;
        public static double distk;
        public static double incr;

        public static int Hcount;

        public static List<double> Hs;

        public static string T0name;

        public EventHandler UpdateAll;

        static SettingsForm()
        {
            minX = -100;
            minY = -100;
            maxX = 100;
            maxY = 100;
            stepX = 2;
            stepY = 2;

            decimals = 3;
            distk = 1;
            incr = 1;

            Hcount = 1;

            Hs = new List<double>();

            Hs.Add(0);

            T0name = "Main_Field";
        }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UpdateAll += (object s, EventArgs e_) => { };

            if(Global.ReliefLoaded)
            {
                Xmax.Enabled = false;
                Xmin.Enabled = false;

                Ymax.Enabled = false;
                Ymin.Enabled = false;
            }

            Xmin.SetValue(minX);
            Ymin.SetValue(minY);

            Xmax.SetValue(maxX);
            Ymax.SetValue(maxY);

            dX.SetValue(stepX);
            dY.SetValue(stepY);

            decimalCount.SetValue(decimals);
            decimalCount.minimum = 0;
            decimalCount.Decimalplaces = 0;

            distmultipy.SetValue(distk);
            distmultipy.minimum = 1;
            distmultipy.Increment = 10;
            distmultipy.Decimalplaces = 0;

            increment.SetValue(incr);
            increment.minimum = 0;

            if (Global.FieldHeigthLoad)
            {
                HeightsCount.Enabled = false;
                foreach (var con in Heights.Controls)
                    ((Numeric)con).Enabled = false;
            }

            HeightsCount.OnValueChanged += UpdateHcounts;

            HeightsCount.SetValue(Hcount);
            HeightsCount.Decimalplaces = 0;
            HeightsCount.minimum = 1;

            while(Heights.Controls.Count < Hs.Count)
            {
                var n = new Numeric() { Dock = DockStyle.Top };
                n.SetValue(Hs[Heights.Controls.Count]);
                n.OnValueChanged += UpdateHeights;
                Heights.Controls.Add(n);
            }
        }

        private void UpdateHcounts(object sender, EventArgs e)
        {
            int delta = (int)HeightsCount.GetValue() - Hcount;

            if (delta == 0) return;
            if (delta > 0)
            {
                for (int i = 0; i < delta; i++)
                {
                    Hs.Add(0);
                    var n = new Numeric() { Dock = DockStyle.Top };
                    n.OnValueChanged += UpdateHeights;
                    Heights.Controls.Add(n);
                }
            }
            if (delta < 0)
            {
                for (int i = Hcount-1; i >= HeightsCount.GetValue(); i--)
                {
                    Hs.RemoveAt(i);
                    Heights.Controls.RemoveAt(i + 1 - Heights.Controls.Count);
                }
            }
            Hcount = (int)HeightsCount.GetValue();

        }
        private void UpdateHeights(object sender, EventArgs e)
        {
            for (int i = 0; i < Hcount; i++)
            {
                Hs[i] = ((Numeric)Heights.Controls[(int)Hcount - 1 - i]).GetValue();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            minX = Xmin.GetValue();
            maxX = Xmax.GetValue();
            minY = Ymin.GetValue();
            maxY = Ymax.GetValue();
            stepX = dX.GetValue();
            stepY = dY.GetValue();
            decimals = decimalCount.GetValue();
            incr = increment.GetValue();
            distk = distmultipy.GetValue();

            Global.CreateGrids();

            UpdateAll(sender,e);
            T0name = T0filename.Text;

            SaveXMLsettings();

            Hide();
        }

        private void SaveXMLsettings()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode rootNode = xmlDocument.CreateElement("Settings");
            xmlDocument.AppendChild(rootNode);

            XmlNode GridNode = xmlDocument.CreateElement("Grid");
            rootNode.AppendChild(GridNode);

            XmlNode node; XmlAttribute attribute;

            node = xmlDocument.CreateElement("points");
            attribute = xmlDocument.CreateAttribute("Importance");
            attribute.Value = "start";

            node.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("Component");
            attribute.Value = "X";

            node.Attributes.Append(attribute);

            node.InnerText = minX.ToString().Replace(",",".");

            GridNode.AppendChild(node);

            node = xmlDocument.CreateElement("points");
            attribute = xmlDocument.CreateAttribute("Importance");
            attribute.Value = "start";

            node.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("Component");
            attribute.Value = "Y";

            node.Attributes.Append(attribute);

            node.InnerText = minY.ToString().Replace(",",".");

            GridNode.AppendChild(node);

            node = xmlDocument.CreateElement("points");
            attribute = xmlDocument.CreateAttribute("Importance");
            attribute.Value = "end";

            node.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("Component");
            attribute.Value = "X";

            node.Attributes.Append(attribute);

            node.InnerText = maxX.ToString().Replace(",",".");

            GridNode.AppendChild(node);

            node = xmlDocument.CreateElement("points");
            attribute = xmlDocument.CreateAttribute("Importance");
            attribute.Value = "end";

            node.Attributes.Append(attribute);

            attribute = xmlDocument.CreateAttribute("Component");
            attribute.Value = "Y";

            node.Attributes.Append(attribute);

            node.InnerText = maxY.ToString().Replace(",",".");

            GridNode.AppendChild(node);

            node = xmlDocument.CreateElement("step");
            
            attribute = xmlDocument.CreateAttribute("Component");
            attribute.Value = "X";

            node.Attributes.Append(attribute);

            node.InnerText = stepX.ToString().Replace(",",".");

            GridNode.AppendChild(node);

            node = xmlDocument.CreateElement("step");

            attribute = xmlDocument.CreateAttribute("Component");
            attribute.Value = "Y";

            node.Attributes.Append(attribute);

            node.InnerText = stepY.ToString().Replace(",",".");

            GridNode.AppendChild(node);

            foreach(var h in Hs)
            {
                node = xmlDocument.CreateElement("heights");
                
                attribute = xmlDocument.CreateAttribute("Index");
                attribute.Value = Hs.IndexOf(h).ToString().Replace(",",".");

                node.Attributes.Append(attribute);

                node.InnerText = h.ToString().Replace(",",".");

                rootNode.AppendChild(node);
            }

            node = xmlDocument.CreateElement("T0Name");

            node.InnerText = T0name;

            rootNode.AppendChild(node);

            node = xmlDocument.CreateElement("Decimals");

            node.InnerText = decimals.ToString().Replace(",",".");

            rootNode.AppendChild(node);

            node = xmlDocument.CreateElement("distance_multiply");

            node.InnerText = distk.ToString().Replace(",",".");

            rootNode.AppendChild(node);

            node = xmlDocument.CreateElement("increment");

            node.InnerText = incr.ToString().Replace(",",".");

            rootNode.AppendChild(node);

            xmlDocument.Save(Global.ProjectPath + "\\ProjectSettings.xml");
        }

        public static void LoadSettings()
        {
            XmlTextReader reader = new XmlTextReader(Global.ProjectPath + "\\ProjectSettings.xml");
            Hs = new List<double>();
            while (reader.Read())
            {
                if (reader.IsStartElement("points") && reader.GetAttribute("Importance") == "start" && reader.GetAttribute("Component") == "X")
                {
                    minX = reader.ReadElementContentAsDouble();
                    continue;
                }
                if (reader.IsStartElement("points") && reader.GetAttribute("Importance") == "start" && reader.GetAttribute("Component") == "Y")
                {
                    minY = reader.ReadElementContentAsDouble();
                    continue;
                }
                if (reader.IsStartElement("points") && reader.GetAttribute("Importance") == "end" && reader.GetAttribute("Component") == "X")
                {
                    maxX = reader.ReadElementContentAsDouble();
                    continue;
                }
                if (reader.IsStartElement("points") && reader.GetAttribute("Importance") == "end" && reader.GetAttribute("Component") == "Y")
                {
                    maxY = reader.ReadElementContentAsDouble();
                    continue;
                }
                if (reader.IsStartElement("step") && reader.GetAttribute("Component") == "X")
                {
                    stepX = reader.ReadElementContentAsDouble();
                    continue;
                }
                if (reader.IsStartElement("step") && reader.GetAttribute("Component") == "Y")
                {
                    stepY = reader.ReadElementContentAsDouble();
                    continue;
                }
                
                if (reader.IsStartElement("heights"))
                {
                    Hs.Add(reader.ReadElementContentAsDouble());
                    continue;
                }
                if (reader.IsStartElement("T0Name"))
                {
                    T0name = reader.ReadString();
                    continue;
                }
                if (reader.IsStartElement("Decimals"))
                {
                    decimals = reader.ReadElementContentAsDouble();
                    continue;
                }
                if (reader.IsStartElement("increment"))
                {
                    incr = reader.ReadElementContentAsDouble();
                    continue;
                }
            }

            Hcount = Hs.Count;

            reader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide(); 
        }
    }
}