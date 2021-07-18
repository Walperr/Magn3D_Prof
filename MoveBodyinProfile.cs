using System;
using System.Windows.Forms;
using static Magn3D_Prof.Global;

namespace Magn3D_Prof
{
    public partial class Profile
    {
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
            //TODO: implement this method
            MessageBox.Show("Move UP in xy " + dx + " ; " + dy);
        }

        public void MoveDownXY(double dx, double dy)
        {
            //TODO: implement this method
            MessageBox.Show("Move DOWN in xy " + dx + " ; " + dy );
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
            //TODO: implement this method
            //MessageBox.Show("Move ALL BODY in xy " + dx + " ; " + dy );
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
            //TODO: implement this method
            MessageBox.Show("Move UP in xz " + dx + " ; " + dz );
        }

        public void MoveDownXZ(double dx, double dz)
        {
            //TODO: implement this method
            MessageBox.Show("Move DOWN in xz " + dx + " ; " + dz );
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
    }
}