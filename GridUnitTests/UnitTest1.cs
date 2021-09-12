using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GRIDs;
using GRIDs.Exporters;
using GRIDs.Exporters.Interfaces;
using GRIDs.Parsers;
using GRIDs.Parsers.Interfaces;
using NUnit.Framework;

namespace GridUnitTests
{
    public class Tests
    {
        private GRD _DSRBGrid;
        private GRD _DSBBGrid;
        private GRD _DSAAGrid;

        private GRD _DSRBExportedGrid;
        private GRD _DSBBExportedGrid;
        private GRD _DSAAExportedGrid;
        
        [SetUp]
        public void Setup()
        {
            _DSRBGrid = LoadDSRB("DSRB.grd");
            _DSBBGrid = LoadDSBB("DSBB.grd");
            _DSAAGrid = LoadDSAA("DSAA.grd");
            
            SaveDSRB("DSRBNew.grd");
            SaveDSBB("DSBBNew.grd");
            SaveDSAA("DSAANew.grd");

            _DSRBExportedGrid = LoadDSRB("DSRBNew.grd");
            _DSBBExportedGrid = LoadDSBB("DSBBNew.grd");
//            _DSAAExportedGrid = LoadDSAA("DSAANew.grd");
        }

        private GRD LoadDSRB(string filename)
        {
            GRD grid;
            
            using (IGridParser parser = new DSRBGridParser(new BinaryReader(File.OpenRead(filename),Encoding.Default)))
            { 
                parser.SkipFormat();
                grid = parser.ReadGRD();
            }

            return grid;
        }
        
        private GRD LoadDSBB(string filename)
        {
            GRD grid;
            
            using (IGridParser parser = new DSBBGridParser(new BinaryReader(File.OpenRead(filename),Encoding.Default)))
            { 
                parser.SkipFormat();
                grid = parser.ReadGRD();
            }

            return grid;
        }
        
        private GRD LoadDSAA(string filename)
        {
            GRD grid;
            
            using (IGridParser parser = new DSAAGridParser(new StreamReader(File.OpenRead(filename),Encoding.Default)))
            { 
                grid = parser.ReadGRD();
            }

            return grid;
        }

        private void SaveDSRB(string filename)
        {
            using (IGridExporter exporter = new DSRBGridExporter(new BinaryWriter(File.OpenWrite(filename)),_DSRBGrid))
            {
                exporter.SaveGrid();
            }
        }

        private void SaveDSBB(string filename)
        {
            using (IGridExporter exporter = new DSBBGridExporter(new BinaryWriter(File.OpenWrite(filename)),_DSBBGrid))
            {
                exporter.SaveGrid();
            }
        }
        
        private void SaveDSAA(string filename)
        {
            using (IGridExporter exporter = new DSAAGridExporter(new StreamWriter(File.OpenWrite(filename)),_DSAAGrid))
            {
                exporter.SaveGrid();
            }
        }

        #region DSRB and DSBB
       [Test]
        public void DsrbAndDsbbEquivalenceOfX() => Assert.AreEqual(_DSRBGrid.X,_DSBBGrid.X);

        [Test]
        public void DsrbAndDsbbEquivalenceOfXMax() => Assert.AreEqual(_DSRBGrid.Xmax,_DSBBGrid.Xmax);

        [Test]
        public void DsrbAndDsbbEquivalenceOfXMin() => Assert.AreEqual(_DSRBGrid.Xmin,_DSBBGrid.Xmin);

        [Test]
        public void DsrbAndDsbbEquivalenceOfY() => Assert.AreEqual(_DSRBGrid.Y,_DSBBGrid.Y);

        [Test]
        public void DsrbAndDsbbEquivalenceOfYMax() => Assert.AreEqual(_DSRBGrid.Ymax,_DSBBGrid.Ymax);

        [Test]
        public void DsrbAndDsbbEquivalenceOfYMin() => Assert.AreEqual(_DSRBGrid.Ymin,_DSBBGrid.Ymin);

        [Test]
        public void DsrbAndDsbbEquivalenceOfZ() => Assert.AreEqual(_DSRBGrid.Z,_DSBBGrid.Z);

        [Test]
        public void DsrbAndDsbbEquivalenceOfZMax() => Assert.AreEqual(_DSRBGrid.Zmax,_DSBBGrid.Zmax);

        [Test]
        public void DsrbAndDsbbEquivalenceOfZMin() => Assert.AreEqual(_DSRBGrid.Zmin,_DSBBGrid.Zmin);

        [Test]
        public void DsrbAndDsbbEquivalenceOfZMean() => Assert.AreEqual(_DSRBGrid.Zmean,_DSBBGrid.Zmean);
        
        #endregion

        #region DSRB and DSAA
       /* [Test]
        public void DsrbAndDsaaEquivalenceOfX() => Assert.AreEqual(_DSRBGrid.X,_DSAAGrid.X);

        [Test]
        public void DsrbAndDsaaEquivalenceOfXMax() => Assert.AreEqual(_DSRBGrid.Xmax,_DSAAGrid.Xmax);

        [Test]
        public void DsrbAndDsaaEquivalenceOfXMin() => Assert.AreEqual(_DSRBGrid.Xmin,_DSAAGrid.Xmin);

        [Test]
        public void DsrbAndDsaaEquivalenceOfY() => Assert.AreEqual(_DSRBGrid.Y,_DSAAGrid.Y);

        [Test]
        public void DsrbAndDsaaEquivalenceOfYMax() => Assert.AreEqual(_DSRBGrid.Ymax,_DSAAGrid.Ymax);

        [Test]
        public void DsrbAndDsaaEquivalenceOfYMin() => Assert.AreEqual(_DSRBGrid.Ymin,_DSAAGrid.Ymin);

        [Test]
        public void DsrbAndDsaaEquivalenceOfZ() => Assert.AreEqual(_DSRBGrid.Z,_DSAAGrid.Z);

        [Test]
        public void DsrbAndDsaaEquivalenceOfZMax() => Assert.AreEqual(_DSRBGrid.Zmax,_DSAAGrid.Zmax);

        [Test]
        public void DsrbAndDsaaEquivalenceOfZMin() => Assert.LessOrEqual(Math.Abs(_DSRBGrid.Zmin -_DSAAGrid.Zmin), 0.00000000001);

        [Test]
        public void DsrbAndDsaaEquivalenceOfZMean() => Assert.AreEqual(_DSRBGrid.Zmean,_DSAAGrid.Zmean);
        */
        #endregion

        #region DSRB ImportExport
        [Test]
        public void DsrbEquivalenceOfX() => Assert.AreEqual(_DSRBGrid.X,_DSRBExportedGrid.X);

        [Test]
        public void DsrbEquivalenceOfXMax() => Assert.AreEqual(_DSRBGrid.Xmax,_DSRBExportedGrid.Xmax);

        [Test]
        public void DsrbEquivalenceOfXMin() => Assert.AreEqual(_DSRBGrid.Xmin,_DSRBExportedGrid.Xmin);

        [Test]
        public void DsrbEquivalenceOfY() => Assert.AreEqual(_DSRBGrid.Y,_DSRBExportedGrid.Y);

        [Test]
        public void DsrbEquivalenceOfYMax() => Assert.AreEqual(_DSRBGrid.Ymax,_DSRBExportedGrid.Ymax);

        [Test]
        public void DsrbEquivalenceOfYMin() => Assert.AreEqual(_DSRBGrid.Ymin,_DSRBExportedGrid.Ymin);

        [Test]
        public void DsrbEquivalenceOfZ() => Assert.AreEqual(_DSRBGrid.Z,_DSRBExportedGrid.Z);

        [Test]
        public void DsrbEquivalenceOfZMax() => Assert.AreEqual(_DSRBGrid.Zmax,_DSRBExportedGrid.Zmax);

        [Test]
        public void DsrbEquivalenceOfZMin() => Assert.AreEqual(_DSRBGrid.Zmin,_DSRBExportedGrid.Zmin);

        [Test]
        public void DsrbEquivalenceOfZMean() => Assert.AreEqual(_DSRBGrid.Zmean,_DSRBExportedGrid.Zmean);

        #endregion
        
        #region DSBB ImportExport
        [Test]
        public void DsbbEquivalenceOfX() => Assert.AreEqual(_DSBBGrid.X,_DSBBExportedGrid.X);

        [Test]
        public void DsbbEquivalenceOfXMax() => Assert.AreEqual(_DSBBGrid.Xmax,_DSBBExportedGrid.Xmax);

        [Test]
        public void DsbbEquivalenceOfXMin() => Assert.AreEqual(_DSBBGrid.Xmin,_DSBBExportedGrid.Xmin);

        [Test]
        public void DsbbEquivalenceOfY() => Assert.AreEqual(_DSBBGrid.Y,_DSBBExportedGrid.Y);

        [Test]
        public void DsbbEquivalenceOfYMax() => Assert.AreEqual(_DSBBGrid.Ymax,_DSBBExportedGrid.Ymax);

        [Test]
        public void DsbbEquivalenceOfYMin() => Assert.AreEqual(_DSBBGrid.Ymin,_DSBBExportedGrid.Ymin);

        [Test]
        public void DsbbEquivalenceOfZ() => Assert.AreEqual(_DSBBGrid.Z,_DSBBExportedGrid.Z);

        [Test]
        public void DsbbEquivalenceOfZMax() => Assert.AreEqual(_DSBBGrid.Zmax,_DSBBExportedGrid.Zmax);

        [Test]
        public void DsbbEquivalenceOfZMin() => Assert.AreEqual(_DSBBGrid.Zmin,_DSBBExportedGrid.Zmin);

        [Test]
        public void DsbbEquivalenceOfZMean() => Assert.AreEqual(_DSBBGrid.Zmean,_DSBBExportedGrid.Zmean);

        #endregion
        
        #region DSAA ImportExport
     /*   [Test]
        public void DsaaEquivalenceOfX() => Assert.AreEqual(_DSAAGrid.X,_DSAAExportedGrid.X);

        [Test]
        public void DsaaEquivalenceOfXMax() => Assert.AreEqual(_DSAAGrid.Xmax,_DSAAExportedGrid.Xmax);

        [Test]
        public void DsaaEquivalenceOfXMin() => Assert.AreEqual(_DSAAGrid.Xmin,_DSAAExportedGrid.Xmin);

        [Test]
        public void DsaaEquivalenceOfY() => Assert.AreEqual(_DSAAGrid.Y,_DSAAExportedGrid.Y);

        [Test]
        public void DsaaEquivalenceOfYMax() => Assert.AreEqual(_DSAAGrid.Ymax,_DSAAExportedGrid.Ymax);

        [Test]
        public void DsaaEquivalenceOfYMin() => Assert.AreEqual(_DSAAGrid.Ymin,_DSAAExportedGrid.Ymin);

        [Test]
        public void DsaaEquivalenceOfZ() => Assert.AreEqual(_DSAAGrid.Z,_DSAAExportedGrid.Z);

        [Test]
        public void DsaaEquivalenceOfZMax() => Assert.AreEqual(_DSAAGrid.Zmax,_DSAAExportedGrid.Zmax);

        [Test]
        public void DsaaEquivalenceOfZMin() => Assert.AreEqual(_DSAAGrid.Zmin,_DSAAExportedGrid.Zmin);

        [Test]
        public void DsaaEquivalenceOfZMean() => Assert.AreEqual(_DSAAGrid.Zmean,_DSAAExportedGrid.Zmean);
*/
        #endregion
    }
}