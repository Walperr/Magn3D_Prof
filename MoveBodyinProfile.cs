using System;
using System.Windows.Forms;
using static Magn3D_Prof.Global;

namespace Magn3D_Prof
{
    public partial class Profile
    {
        public enum Quater
        {
            XgreaterZero = 0b1101,
            XlessZero = 0b1110,
            YgreaterZero = 0b0111,
            YlessZero = 0b1011,
            First = 0b0101,
            Second = 0b0110,
            Third = 0b1010,
            Fourth = 0b1001
        }
        public enum MovingEdge
        {
            Upper,
            Lower,
            Left,
            Right,
            Front,
            Back,
            All
        }

        private MovingEdge _edgeToMove;
        private enum ShowType
        {
            Slit,
            Top,
            None
        }

        private ShowType _showType;

        public MovingEdge EdgeToMove
        {
            get => _edgeToMove;
            set => _edgeToMove = value;
        }

        public void MoveXY(double dx, double dy)
        {
            switch (EdgeToMove)
            {
                case MovingEdge.All:
                    MoveBodyXY(dx,dy);
                    break;
                case MovingEdge.Upper:
                    MoveUpXY(dx,dy);
                    break;
                case MovingEdge.Lower:
                    MoveDownXY(dx,dy);
                    break;
                case MovingEdge.Left:
                    MoveLeftXY(dx,dy);
                    break;
                case MovingEdge.Right:
                    MoveRightXY(dx,dy);
                    break;
                case MovingEdge.Front:
                    MoveFrontXY(dx,dy);
                    break;
                case MovingEdge.Back:
                    MoveBackXY(dx,dy);
                    break;
            }
        }
        public void MoveUpXY(double dx, double dy)
        {
            var x = bodyControls[SelectedBodyIndex].X.GetValue();
            var y = bodyControls[SelectedBodyIndex].Y.GetValue();
            
            var L = bodyControls[SelectedBodyIndex].L.GetValue();
            var fi = bodyControls[SelectedBodyIndex].fi.GetValue() * Math.PI / 180;
            var beta = bodyControls[SelectedBodyIndex].beta.GetValue() * Math.PI / 180;

            var sinfi = Math.Sin(fi);
            var cosfi = Math.Cos(fi);

            var sinbeta = Math.Sin(beta);
            var cosbeta = Math.Cos(beta);

            var a = cosfi * sinbeta;
            var b = cosfi * cosbeta;

            var Lb = L * b;

            var A = Lb * Lb + dx * dx - 2 * Lb * dx + L * L * a * a;

            var L1 = Math.Sqrt(A + L * L * sinfi * sinfi);

            var fi1 = Math.Asin(L * sinfi / L1);

            var sinbeta1 = L*a / (L1 * Math.Cos(fi1));
            
            var beta1 = Math.Asin(sinbeta1);

            GetQuater(Lb-dx,sinbeta1, ref beta1);

            var a1 = Math.Cos(fi1) * Math.Sin(beta1);
            var b1 = Math.Cos(fi1) * Math.Cos(beta1);

            var A1 = L1 * L1 * a1 * a1 + dy * dy - 2 * L1 * a1 * dy + L1 * L1 * b1 * b1;

            var L2 = Math.Sqrt(A1 + L1 * L1 * Math.Pow(Math.Sin(fi1), 2));
            
            var fi2 = Math.Asin(L1 * Math.Sin(fi1) / L2);

            var sinbeta2 = (L1 * a1 - dy) / (L2 * Math.Cos(fi2));
            
            var beta2 = Math.Asin(sinbeta2);
            
            GetQuater(Lb-dx,sinbeta2, ref beta2);
            
            bodyControls[SelectedBodyIndex].X.SetValue(x+dx);
            bodyControls[SelectedBodyIndex].Y.SetValue(y+dy);
            bodyControls[SelectedBodyIndex].L.SetValue(L2);
            bodyControls[SelectedBodyIndex].fi.SetValue(fi2 * 180 / Math.PI);
            bodyControls[SelectedBodyIndex].beta.SetValue(beta2 * 180 / Math.PI);
        }

        public void MoveDownXY(double dx, double dy)
        {
            var L = bodyControls[SelectedBodyIndex].L.GetValue();
            var fi = bodyControls[SelectedBodyIndex].fi.GetValue() * Math.PI / 180;
            var beta = bodyControls[SelectedBodyIndex].beta.GetValue() * Math.PI / 180;

            var cosfi = Math.Cos(fi);

            var sinbeta = Math.Sin(beta);
            var cosbeta = Math.Cos(beta);

            var a = cosfi * sinbeta;
            var b = cosfi * cosbeta;

            var Lb = L * b;

            var A = L * L * cosfi * cosfi + 2 * Lb * dx + dx * dx;

            var L1 = Math.Sqrt(A + L * L * Math.Pow(Math.Sin(fi), 2) );

            var fi1 = Math.Asin(L * Math.Sin(fi) / L1);

            var sinbeta1 = L * a / (L1 * Math.Cos(fi1));
           
            var beta1 = Math.Asin(sinbeta1);
           
            GetQuater(Lb+dx,sinbeta1, ref beta1);

            var cosfi1 = Math.Cos(fi1);
            sinbeta1 = Math.Sin(beta1);

            var a1 = cosfi1 * sinbeta1;
            var b1 = cosfi1 * Math.Cos(beta1);
            
            var La1 = L1 * a1;

            var A1 = La1 * La1 + dy * dy + 2 * La1 * dy + L1 * L1 * b1 * b1;

            var L2 = Math.Sqrt(A1 + L1 * L1 * Math.Pow(Math.Sin(fi1), 2) );

            var fi2 = Math.Asin(L1 * Math.Sin(fi1) / L2);

            var sinbeta2 = (L1 * a1 + dy) / (L2 * Math.Cos(fi2));
           
            var beta2 = Math.Asin(sinbeta2);

            GetQuater(L1 * b1, L1 * a1 + dy, ref beta2);
            
            bodyControls[SelectedBodyIndex].L.SetValue(L2);
            bodyControls[SelectedBodyIndex].fi.SetValue(fi2 * 180 / Math.PI);
            bodyControls[SelectedBodyIndex].beta.SetValue(beta2 * 180 / Math.PI);
        }

        public void MoveLeftXY(double dx, double dy)
        {
            // TODO: implement this method
            MessageBox.Show("Move LEFT in xy " + dx + " ; " + dy );
        }

        public void MoveRightXY(double dx, double dy)
        {
            //TODO: implement this method
            MessageBox.Show("Move RIGHT in xy " + dx + " ; " + dy );
        }

        public void MoveFrontXY(double dx, double dy)
        {
            //TODO: implement this method
            MessageBox.Show("Move FRONT in xy " + dx + " ; " + dy );
        }

        public void MoveBackXY(double dx, double dy)
        {
            //TODO: implement this method
            MessageBox.Show("Move BACK in xy " + dx + " ; " + dy );
        }

        public void MoveBodyXY(double dx, double dy)
        {
            bodyControls[SelectedBodyIndex].X.SetValue(bodyControls[SelectedBodyIndex].X.GetValue() + dx);
            bodyControls[SelectedBodyIndex].Y.SetValue(bodyControls[SelectedBodyIndex].Y.GetValue() + dy);
        }
      
        public void MoveXZ(double dx, double dz)
        {
            switch (EdgeToMove)
            {
                case MovingEdge.All:
                    MoveBodyXZ(dx,dz);
                    break;
                case MovingEdge.Upper:
                    MoveUpXZ(dx,dz);
                    break;
                case MovingEdge.Lower:
                    MoveDownXZ(dx,dz);
                    break;
                case MovingEdge.Left:
                    MoveLeftXZ(dx,dz);
                    break;
                case MovingEdge.Right:
                    MoveRightXZ(dx,dz);
                    break;
                case MovingEdge.Front:
                    MoveFrontXZ(dx,dz);
                    break;
                case MovingEdge.Back:
                    MoveBackXZ(dx,dz);
                    break;
            }
        }
        public void MoveUpXZ(double dx, double dz)
        {
            var x = bodyControls[SelectedBodyIndex].X.GetValue();
            
            var L = bodyControls[SelectedBodyIndex].L.GetValue();
            var fi = bodyControls[SelectedBodyIndex].fi.GetValue() * Math.PI / 180;
            var beta = bodyControls[SelectedBodyIndex].beta.GetValue() * Math.PI / 180;

            var sinfi = Math.Sin(fi);
            var cosfi = Math.Cos(fi);

            var sinbeta = Math.Sin(beta);
            var cosbeta = Math.Cos(beta);

            var a = cosfi * sinbeta;
            var b = cosfi * cosbeta;

            var Lb = L * b;

            var A = Lb * Lb + dx * dx - 2 * Lb * dx + L * L * a * a;

            var L1 = Math.Sqrt(A + L * L * sinfi * sinfi);

            var fi1 = Math.Asin(L * sinfi / L1);

            var sinbeta1 = L*a / (L1 * Math.Cos(fi1));
            
            var beta1 = Math.Asin(sinbeta1);

            GetQuater(Lb-dx,sinbeta1, ref beta1);

            L = Math.Sqrt(L1 * L1 + dz * dz - 2 * L1 * Math.Sin(fi1) * dz);

            fi1 = Math.Asin((L1 * Math.Sin(fi1) - dz) / L);

            L1 = L;
            
            bodyControls[SelectedBodyIndex].X.SetValue(x+dx);
            bodyControls[SelectedBodyIndex].L.SetValue(L1);
            bodyControls[SelectedBodyIndex].fi.SetValue(fi1 * 180 / Math.PI);
            bodyControls[SelectedBodyIndex].beta.SetValue(beta1 * 180 / Math.PI);
            bodyControls[SelectedBodyIndex].h1.SetValue(bodyControls[SelectedBodyIndex].h1.GetValue() + dz);
            if (!bodyControls[SelectedBodyIndex].ConnectDepth.Checked)
            {
                bodyControls[SelectedBodyIndex].h2.SetValue(bodyControls[SelectedBodyIndex].h2.GetValue() + dz);
                bodyControls[SelectedBodyIndex].h3.SetValue(bodyControls[SelectedBodyIndex].h3.GetValue() + dz);
            }
        }

        public void MoveDownXZ(double dx, double dz)
        {
           var L = bodyControls[SelectedBodyIndex].L.GetValue();
           var fi = bodyControls[SelectedBodyIndex].fi.GetValue() * Math.PI / 180;
           var beta = bodyControls[SelectedBodyIndex].beta.GetValue() * Math.PI / 180;

           var cosfi = Math.Cos(fi);

           var sinbeta = Math.Sin(beta);
           var cosbeta = Math.Cos(beta);

           var a = cosfi * sinbeta;
           var b = cosfi * cosbeta;

           var Lb = L * b;

           var A = L * L * cosfi * cosfi + 2 * Lb * dx + dx * dx;

           var L1 = Math.Sqrt(A + L * L * Math.Pow(Math.Sin(fi), 2) );

           var fi1 = Math.Asin(L * Math.Sin(fi) / L1);

           var sinbeta1 = L * a / (L1 * Math.Cos(fi1));
           
           var beta1 = Math.Asin(sinbeta1);

           GetQuater(Lb + dx, sinbeta1, ref beta1);

           L = Math.Sqrt(L1 * L1 + dz * dz + 2 * L1 * dz * Math.Sin(fi1));
         
           fi1 = Math.Asin((L1 * Math.Sin(fi1) + dz) / L);

           L1 = L;
           
           bodyControls[SelectedBodyIndex].L.SetValue(L1);
           bodyControls[SelectedBodyIndex].fi.SetValue(fi1 * 180 / Math.PI);
           bodyControls[SelectedBodyIndex].beta.SetValue(beta1 * 180 / Math.PI);
        }

        public void MoveLeftXZ(double dx, double dz)
        {
            // TODO: implement this method
            MessageBox.Show("Move LEFT in xz " + dx + " ; " + dz );
        }

        public void MoveRightXZ(double dx, double dz)
        {
            //TODO: implement this method
            MessageBox.Show("Move RIGHT in xz " + dx + " ; " + dz );
        }

        public void MoveFrontXZ(double dx, double dz)
        {
            //TODO: implement this method
            MessageBox.Show("Move FRONT in xz " + dx + " ; " + dz );
        }

        public void MoveBackXZ(double dx, double dz)
        {
            //TODO: implement this method
            MessageBox.Show("Move BACK in xz " + dx + " ; " + dz );
        }

        public void MoveBodyXZ(double dx, double dz)
        {
            //TODO: implement this method
            //MessageBox.Show("Move ALL BODY in xz " + dx + " ; " + dz );
            bodyControls[SelectedBodyIndex].X.SetValue(bodyControls[SelectedBodyIndex].X.GetValue() + dx);
            bodyControls[SelectedBodyIndex].h1.SetValue(bodyControls[SelectedBodyIndex].h1.GetValue() + dz);
            if (!bodyControls[SelectedBodyIndex].ConnectDepth.Checked)
            {
                bodyControls[SelectedBodyIndex].h2.SetValue(bodyControls[SelectedBodyIndex].h2.GetValue() + dz);
                bodyControls[SelectedBodyIndex].h3.SetValue(bodyControls[SelectedBodyIndex].h3.GetValue() + dz);
            }
        }

        private void Top_Enter(object sender, EventArgs e)
        {
            _showType = ShowType.Top;
        }

        private void Slit_Enter(object sender, EventArgs e)
        {
            _showType = ShowType.Slit;
        }

        private void Slit_Leave(object sender, EventArgs e)
        {
            _showType = ShowType.None;
        }

        private void Top_Leave(object sender, EventArgs e)
        {
            _showType = ShowType.None;
        }

        public void Profile_KeyDown(object sender, KeyEventArgs e)
        {
            double d1 = SettingsForm.incr;

            double dx = 0, dy = 0;
            
            switch (e.KeyCode)
            {
                case Keys.NumPad4:
                    dx = -d1;
                    break;
                case Keys.NumPad6:
                    dx = d1;
                    break;
                case Keys.NumPad8:
                    dy = -d1;
                    break;
                case  Keys.NumPad2:
                    dy = d1;
                    break;
                default:
                    dx = 0;
                    dy = 0;
                    break;
            }

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        if(bodies.Count >= 1)
                        SelectedBodyIndex = 0;
                        break;
                    case Keys.D2:
                        if(bodies.Count >= 2)
                            SelectedBodyIndex = 1;
                        break;
                    case Keys.D3:
                        if(bodies.Count >= 3)
                            SelectedBodyIndex = 2;
                        break;
                    case Keys.D4:
                        if(bodies.Count >= 4)
                            SelectedBodyIndex = 3;
                        break;
                    case Keys.D5:
                        if(bodies.Count >= 5)
                            SelectedBodyIndex = 4;
                        break;
                    case Keys.D6:
                        if(bodies.Count >= 6)
                            SelectedBodyIndex = 5;
                        break;
                    case Keys.D7:
                        if(bodies.Count >= 7)
                            SelectedBodyIndex = 6;
                        break;
                    case Keys.D8:
                        if(bodies.Count >= 8)
                            SelectedBodyIndex = 7;
                        break;
                    case Keys.D9:
                        if(bodies.Count >= 9)
                            SelectedBodyIndex = 8;
                        break;
                }
            }

            if (e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        EdgeToMove = MovingEdge.All;
                        break;
                    case Keys.D2:
                        EdgeToMove = MovingEdge.Upper;
                        break;
                    case Keys.D3:
                        EdgeToMove = MovingEdge.Lower;
                        break;
                    case Keys.D4:
                        EdgeToMove = MovingEdge.Left;
                        break;
                    case Keys.D5:
                        EdgeToMove = MovingEdge.Right;
                        break;
                    case Keys.D6:
                        EdgeToMove = MovingEdge.Front;
                        break;
                    case Keys.D7:
                        EdgeToMove = MovingEdge.Back;
                        break;
                }
            }

            if ((dx != 0 || dy != 0) && Global.SelectedBodyIndex != -1)
            {
                switch (_showType)
                {
                    case ShowType.None:
                        return;
                    case ShowType.Slit:
                        MoveXZ(dx, dy);
                        break;
                    case ShowType.Top:
                        MoveXY(dx, dy);
                        break;
                }
            }

            Draw(sender,e);
        }

        private Quater GetQuater(double x, double y, ref double angle)
        {
            Quater quater = (x < 0 ? Quater.XlessZero : Quater.XgreaterZero) &
                            (y < 0 ? Quater.YlessZero : Quater.YgreaterZero);
            
            switch (quater)
            {
                case Quater.First:
                    break;
                case Quater.Second:
                    angle = Math.PI - angle;
                    break;
                case Quater.Third:
                    angle = Math.PI - angle;
                    break;
                case Quater.Fourth:
                    angle = 2 * Math.PI + angle;
                    break;
            }
            
            return quater;
        }
    }
}