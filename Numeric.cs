using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magn3D_Prof
{
    public partial class Numeric : UserControl
    {
        private double Value;
        public double Increment = 1;
        private int decimalplaces = 3;
        public double minimum = double.NegativeInfinity;
        public double maximum = double.PositiveInfinity;
        
        public int Decimalplaces { get => decimalplaces; set { decimalplaces = value; Text.Text = Value.ToString("F" + Decimalplaces); } }

        public event EventHandler OnValueChanged;

        public Numeric()
        {
            InitializeComponent();
            OnValueChanged = (object sender,EventArgs e) => { };
            TabStop = false;
        }
        
        private void Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | (e.KeyChar == 8) | (e.KeyChar == '-' & Text.Text.IndexOf('-') == -1) | (((e.KeyChar == '.') | (e.KeyChar == ',')) & ((Text.Text.IndexOf('.') == -1) & (Text.Text.IndexOf(',') == -1)))) return;

            if ((Char.IsControl(e.KeyChar) | e.KeyChar == 9))
            {
                if (Text.Text.Length != 0) {
                    ChangeValue(double.Parse(Text.Text.Replace('.', ','), System.Globalization.NumberStyles.Any));
                }
                else
                    ChangeValue(0);
                OnValueChanged(this, new EventArgs());
            }

            e.Handled = true;
        }

        private void Text_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (Value == maximum) return;
                if (Text.Text.Length != 0)
                    ChangeValue(double.Parse(Text.Text.Replace('.', ','), System.Globalization.NumberStyles.Any));
                else
                    ChangeValue(0);
                ChangeValue(Value + Increment);
                OnValueChanged(this, new EventArgs());
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                if (Value == minimum) return;
                if (Text.Text.Length != 0)
                    ChangeValue(double.Parse(Text.Text.Replace('.', ','), System.Globalization.NumberStyles.Any));
                else
                    ChangeValue(0);
                ChangeValue(Value - Increment);
                OnValueChanged(sender, new EventArgs());
                return;
            }
        }

        private void Text_Leave(object sender, EventArgs e)
        {
            if (Text.Text.Length != 0)
                ChangeValue(double.Parse(Text.Text.Replace('.', ','), System.Globalization.NumberStyles.Any));
            else
                ChangeValue(0);
            OnValueChanged(this, new EventArgs());
        }
        
        private void ChangeValue(double value)
        {
            this.Value = Math.Round(value,Decimalplaces,MidpointRounding.AwayFromZero);
            if (value > maximum) value = maximum;
            if (value < minimum) value = minimum;
            Text.Text = value.ToString("F" + Decimalplaces);
        }

        public void SetValue(double value)
        {
            if (value == Value)
                return;
            ChangeValue(value);
            OnValueChanged(this, new EventArgs());
        }

        public void SetValue(double value, object sender)
        {
            if (value == Value)
                return;
            ChangeValue(value);
            OnValueChanged(sender, new EventArgs());
        }


        public double GetValue()
        {
            return Value;
        }
    }
}
