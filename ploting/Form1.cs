using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ploting
{
    public partial class Form1 : Form
    {
        private GRD testGrid;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            testGrid = GRD.ReadGRD("test.grd");
        }

        private void plot1_Click(object sender, EventArgs e)
        {
            plot1.DeferUpdate = true;
            plot1.ViewHeight = (int)(testGrid.GetyMax()-testGrid.GetyMin());
            plot1.ViewXmin = (int)testGrid.GetxMin();
            plot1.ViewYmin = (int)testGrid.GetyMin();
            plot1.DeferUpdate = false;
            plot1.Grid = testGrid;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            plot1.ViewHeight -= (int)(e.Delta * 0.001 * (testGrid.GetyMax() - testGrid.GetyMin()));
            base.OnMouseWheel(e);
        }
    }
}
