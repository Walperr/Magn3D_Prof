using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GRIDs;
using NUnit.Framework;

namespace GridUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var filenameDSRB = "DSRB.grd";
            var filenameDSBB = "DSBB.grd";
            var filenameDSAA = "DSAA.grd";
            
            GRD grdDSRB, grdDSBB, grdDSAA;
            
            using (GridParser parser = new GridParser(new BinaryReader(File.OpenRead(filenameDSRB),Encoding.Default)))
            { 
                grdDSRB = parser.ReadGRD();
            }
            
            using (GridParser parser = new GridParser(new BinaryReader(File.OpenRead(filenameDSBB),Encoding.Default)))
            {
                grdDSBB = parser.ReadGRD();
            }
            
            using (GridParser parser = new GridParser(new BinaryReader(File.OpenRead(filenameDSAA),Encoding.Default)))
            {
                grdDSAA = parser.ReadGRD();
            }

            Assert.AreEqual(grdDSRB.SNx, grdDSBB.SNx);
            Assert.AreEqual(grdDSRB.SNy, grdDSBB.SNy);
            Assert.AreEqual(grdDSRB.X, grdDSBB.X);
            Assert.AreEqual(grdDSRB.Y, grdDSBB.Y);
            Assert.AreEqual(grdDSRB.Z, grdDSBB.Z);
            Assert.AreEqual(grdDSRB.GetzMax(), grdDSBB.GetzMax());
            Assert.AreEqual(grdDSRB.GetzMin(), grdDSBB.GetzMin());
            Assert.AreEqual(grdDSRB.GetzMean(), grdDSBB.GetzMean());
            
            Assert.AreEqual(grdDSRB.SNx, grdDSAA.SNx);
            Assert.AreEqual(grdDSRB.SNy, grdDSAA.SNy);
            Assert.AreEqual(grdDSRB.X, grdDSAA.X);
            Assert.AreEqual(grdDSRB.Y, grdDSAA.Y);

            for (int i = 0; i < grdDSRB.Z.Count; i++)
                Assert.Less(Math.Abs(grdDSRB.Z[i] - grdDSAA.Z[i]), 0.0000000000000001d);
        }
    }
}