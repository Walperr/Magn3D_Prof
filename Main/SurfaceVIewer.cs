using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using GRIDs;
using OpenControls.Wpf.SurfacePlot.Common;

namespace Magn3D_Prof.Main
{
    public partial class SurfaceVIewer : UserControl
    {

        private ISurfacePlotControlManager _surfacePlotControlManager;
        public IGrid Surface
        {
            get => _surfacePlotControlManager?.OriginalSurface as IGrid;
            set
            {
                if(_surfacePlotControlManager == null) return;
                _surfacePlotControlManager.OriginalSurface = value;
            }
        }

        public ObservableCollection<Prismbody> Bodies => ((SurfacePlotControlManager)_surfacePlotControlManager)?.Bodies;

        public SurfaceVIewer()
        {
            InitializeComponent();
        }

        private void SurfaceVIewer_Load(object sender, EventArgs e)
        {
            _surfacePlotControlManager = new SurfacePlotControlManager(splitContainer1.Panel2)
            {
                PropertyGridParentControl = splitContainer1.Panel1,
                OriginalSurface = Surface,
            };
        }

        private void showProperties_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;

            if(splitContainer1.Panel1Collapsed)
                showProperties.Text = ">";
            else
                showProperties.Text = "<";
        }
    }
}