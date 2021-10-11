using System;
using System.IO;
using System.Text;
using GRIDs.Exporters.Interfaces;

namespace GRIDs.Exporters
{
    public class GridExporter : IGridExporter
    {
        private IGridExporter _exporter;
        private const string DSRB = "DSRB";
        private const string DSBB = "DSBB";
        private const string DSAA = "DSAA";

        public GridExporter(BinaryWriter writer, IGrid grid, string format = DSRB)
        {
            switch (format)
            {
                case DSRB :
                    _exporter = new DSRBGridExporter(writer, grid);
                    break;
                case DSBB :
                    _exporter = new DSBBGridExporter(writer, grid);
                    break;
                case DSAA :
                    _exporter = new DSAAGridExporter(new StreamWriter(writer.BaseStream, Encoding.Default), grid);
                    break;
                default:
                    throw new ArgumentException("Invalid format");
            }
        }
        
        public void Dispose()
        {
            _exporter?.Dispose();
        }

        public void SaveGrid()
        {
            _exporter.SaveGrid();
        }
    }
}