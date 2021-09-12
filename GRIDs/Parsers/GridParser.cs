using System.IO;
using System.Text;
using GRIDs.Parsers.Interfaces;

namespace GRIDs.Parsers
{
    public class GridParser : IGridParser
    {
        private readonly BinaryReader _reader;
        private IGridParser _parser;
        private const string DSRB = "DSRB";
        private const string DSBB = "DSBB";
        private const string DSAA = "DSAA";


        public GridParser(BinaryReader reader)
        {
            _reader = reader;
        }
        
        public void Dispose()
        {
            _reader?.Dispose();
            _parser?.Dispose();
        }

        public GRD ReadGRD()
        {
            var marker = _reader.ReadChars(4);

            var strMarker = "";

            foreach (var sign in marker)
                strMarker += sign;

            switch (strMarker)
            {
                case DSRB:
                    _parser = new DSRBGridParser(_reader);
                    break;
                case DSBB:
                    _parser = new DSBBGridParser(_reader);
                    break;
                case DSAA:
                    StreamReader sr = new StreamReader(_reader.BaseStream, Encoding.Default);
                    _parser = new DSAAGridParser(sr);
                    break;
                default:
                    throw new FileLoadException("Wrong file format");
            }

            return _parser.ReadGRD();
        }

        public void SkipFormat() => _reader.ReadChars(4);
    }
}