using System;
using System.Windows.Forms;
using GRIDs;

namespace Magn3D_Prof
{
    public partial class SurfaceVIewer : UserControl
    {

        private SurfacePlotControlManager _surfacePlotControlManager;
        public IGrid Surface
        {
            get => _surfacePlotControlManager?.OriginalSurface;
            set
            {
                if(_surfacePlotControlManager == null) return;
                _surfacePlotControlManager.OriginalSurface = value;
            }
        }

        public SurfaceVIewer()
        {
            InitializeComponent();
        }

        private void SurfaceVIewer_Load(object sender, EventArgs e)
        {
            _surfacePlotControlManager = new SurfacePlotControlManager(splitContainer1.Panel2)
            {
                PropertyGridParentControl = splitContainer1.Panel1,
                OriginalSurface = Surface
            };
        }
    }
}