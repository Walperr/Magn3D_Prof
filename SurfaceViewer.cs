using System;
using System.Windows.Forms;
using GRIDs;
using test;

namespace Magn3D_Prof
{
    public partial class SurfaceViewer : UserControl
    {
        private SurfacePlotControlManager _surfacePlotControlManager;
        
        public IGrid Surface
        {
            get => _surfacePlotControlManager.OriginalSurface;
            set => _surfacePlotControlManager.OriginalSurface = value;
        }

        public SurfaceViewer()
        {
            InitializeComponent();
            
            Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _surfacePlotControlManager = new SurfacePlotControlManager(splitContainer1.Panel2)
            {
                PropertyGridParentControl = splitContainer1.Panel1,
                OriginalSurface = Surface
            };
        }
    }
}