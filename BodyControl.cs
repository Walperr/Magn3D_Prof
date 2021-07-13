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
    public partial class BodyControl : UserControl
    {

        private Profile CurrentProfile;
        private double depth = 2;

        public BodyControl()
        {
            InitializeComponent();
        }

        public void UpdateParameters()
        {
            int index = CurrentProfile.bodyControls.IndexOf(this);

            if (index == -1) index = CurrentProfile.bodyControls.Count;

            var ang = CurrentProfile.GetAngle() * Math.PI / 180;

            double nx = (Global.bodies[index].X - CurrentProfile.GetStartPoint().x) * Math.Cos(ang) -   (Global.bodies[index].Y - CurrentProfile.GetStartPoint().y) * Math.Sin(ang);
            double ny = (Global.bodies[index].X - CurrentProfile.GetStartPoint().x) * Math.Sin(ang) + (Global.bodies[index].Y - CurrentProfile.GetStartPoint().y) * Math.Cos(ang);

            X.SetValue(nx,this);
            Y.SetValue(ny, this);
            b.SetValue(Global.bodies[index].b, this);
            d.SetValue(Global.bodies[index].d, this);
            L.SetValue(Global.bodies[index].L, this);
            h1.SetValue(Global.bodies[index].h1, this);
            h2.SetValue(Global.bodies[index].h2, this);
            h3.SetValue(Global.bodies[index].h3, this);
            alpha.SetValue(Global.bodies[index].alpha+ang*180/Math.PI, this);
            beta.SetValue(Global.bodies[index].beta + ang * 180 / Math.PI, this);
            fi.SetValue(Global.bodies[index].fi, this);
            kappa.SetValue(Global.bodies[index].kappa, this);
            Declin.SetValue(Global.bodies[index].D, this);
            Inclin.SetValue(Global.bodies[index].I, this);

        }

        private void Body_Load(object sender, EventArgs e)
        {
            Global.Setti.UpdateAll += UpdateNumerics;

            UpdateNumerics(sender, e);

            CurrentProfile = Parent.Parent.Parent.Parent.Parent as Profile; // Определяем профиль на котором расположено тело

            var index = CurrentProfile.bodyControls.IndexOf(this);

            if (index == -1) index = CurrentProfile.bodyControls.Count;

            Global.bodies[index].OnChangeParametrs += OnChangeBody;

            UpdateParameters();

            X.OnValueChanged += OnChangeParams;
            Y.OnValueChanged += OnChangeParams;
            b.OnValueChanged += OnChangeParams;
            d.OnValueChanged += OnChangeParams;
            L.OnValueChanged += OnChangeParams;
            h1.OnValueChanged += OnChangeH1;
            h2.OnValueChanged += OnChangeParams;
            h3.OnValueChanged += OnChangeParams;
            alpha.OnValueChanged += OnChangeParams;
            beta.OnValueChanged += OnChangeParams;
            fi.OnValueChanged += OnChangeParams;
            kappa.OnValueChanged += OnChangeParams;
            Declin.OnValueChanged += OnChangeParams;
            Inclin.OnValueChanged += OnChangeParams;

            
            b.minimum = 0;
            d.minimum = 0;
            L.minimum = 0;
            h1.minimum = 0;
            h2.minimum = 0;
            h3.minimum = 0;
            alpha.minimum = -360;
            alpha.maximum = 360;
            alpha.minimum = -360;
            alpha.maximum = 360;
            fi.minimum = 0;
            fi.maximum = 180;

            Declin.minimum = -360;
            Declin.maximum = 360;

            Inclin.minimum = -360;
            Inclin.maximum = 360;
        }

        public void OnChangeBody(Prismbody sender, EventArgs e)
        {
            UpdateParameters();
        }
        private void OnChangeParams(object sender, EventArgs e)
        {
            var index = CurrentProfile.bodyControls.IndexOf(this);

            if (sender == this) return;

            var Ox = CurrentProfile.GetAxis()[0];
            var Oy = CurrentProfile.GetAxis()[1];

            var pos = X.GetValue() * Ox + Y.GetValue() * Oy + new Vector3(CurrentProfile.GetStartPoint().x, CurrentProfile.GetStartPoint().y,0);

            var ang = CurrentProfile.GetAngle();

            Global.bodies[index].X = pos.X;
            Global.bodies[index].Y = pos.Y;
            Global.bodies[index].b = b.GetValue();
            Global.bodies[index].d = d.GetValue();
            Global.bodies[index].L = L.GetValue();
            Global.bodies[index].h1 = h1.GetValue();
            Global.bodies[index].h2 = h2.GetValue();
            Global.bodies[index].h3 = h3.GetValue();
            Global.bodies[index].alpha = alpha.GetValue() - ang;
            Global.bodies[index].beta = beta.GetValue() - ang;
            Global.bodies[index].fi = fi.GetValue();
            Global.bodies[index].kappa = kappa.GetValue();
            Global.bodies[index].D = Declin.GetValue();
            Global.bodies[index].I = Inclin.GetValue();
            Global.bodies[index].hle = HorizontalLowEdge.Checked;

            Global.bodies[index].UpdateBody();

            CurrentProfile.ShowField();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить тело?", "ВНИМАНИЕ!", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var index = CurrentProfile.bodyControls.IndexOf(this);

            Global.bodies.RemoveAt(index);

            foreach (var profile in Global.Profiles)
                profile.DeleteControls(index);
            CurrentProfile.ShowField();
        }

        private void HorizontalLowEdge_CheckedChanged(object sender, EventArgs e)
        {
            var index = CurrentProfile.bodyControls.IndexOf(this);
            Global.bodies[index].hle = HorizontalLowEdge.Checked;

            Global.bodies[index].UpdateBody();

            CurrentProfile.ShowField();
        }

        private void ConnectDepth_CheckedChanged(object sender, EventArgs e)
        {
            h2.Enabled = !h2.Enabled;
            h3.Enabled = !h3.Enabled;
        }
        private void OnChangeH1(object sender, EventArgs e)
        {
            var index = CurrentProfile.bodyControls.IndexOf(this);

            if (sender == this) return;
            
            if (ConnectDepth.Checked)
            {
                h2.SetValue(h2.GetValue() + h1.GetValue() - depth);
                h3.SetValue(h3.GetValue() + h1.GetValue() - depth);
            }
            depth = h1.GetValue();
            Global.bodies[index].h1 = h1.GetValue();
            Global.bodies[index].h2 = h2.GetValue();
            Global.bodies[index].h3 = h3.GetValue();

            Global.bodies[index].UpdateBody();

            CurrentProfile.ShowField();
        }

        private void UpdateNumerics(object sender, EventArgs e)
        {
            X.Decimalplaces = (int)SettingsForm.decimals;
            X.Increment = SettingsForm.incr;

            Y.Decimalplaces = (int)SettingsForm.decimals;
            Y.Increment = SettingsForm.incr;

            b.Decimalplaces = (int)SettingsForm.decimals;
            b.Increment = SettingsForm.incr;

            d.Decimalplaces = (int)SettingsForm.decimals;
            d.Increment = SettingsForm.incr;

            L.Decimalplaces = (int)SettingsForm.decimals;
            L.Increment = SettingsForm.incr;

            h1.Decimalplaces = (int)SettingsForm.decimals;
            h1.Increment = SettingsForm.incr;

            h2.Decimalplaces = (int)SettingsForm.decimals;
            h2.Increment = SettingsForm.incr;

            h3.Decimalplaces = (int)SettingsForm.decimals;
            h3.Increment = SettingsForm.incr;

            alpha.Decimalplaces = (int)SettingsForm.decimals;
            alpha.Increment = SettingsForm.incr;

            alpha.Decimalplaces = (int)SettingsForm.decimals;
            alpha.Increment = SettingsForm.incr;

            fi.Decimalplaces = (int)SettingsForm.decimals;
            fi.Increment = SettingsForm.incr;

            kappa.Decimalplaces = (int)SettingsForm.decimals;
            kappa.Increment = SettingsForm.incr;

            Declin.Decimalplaces = (int)SettingsForm.decimals;
            Declin.Increment = SettingsForm.incr;

            Inclin.Decimalplaces = (int)SettingsForm.decimals;
            Inclin.Increment = SettingsForm.incr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inclin.SetValue(Global.I);
            Declin.SetValue(Global.D);
        }

        private void KappaAuto_Click(object sender, EventArgs e)
        {
            var index = CurrentProfile.bodyControls.IndexOf(this);
            var Field1 = Global.bodies[index].GetField(Global.Profiles.IndexOf(CurrentProfile), (int)CurrentProfile.Hi1.GetValue());
            var Field2 = Global.bodies[index].GetField(Global.Profiles.IndexOf(CurrentProfile), (int)CurrentProfile.Hi2.GetValue());

            var FieldMeas = CurrentProfile.MeasureFields;

            List<double> dT = new List<double>(); // Массив аномальных полей
            List<double> Tobs = new List<double>(); // Массив интерпретируемых полей

            double dTmean = 0, Tobsmean = 0;

            for (int i = 0; i < Field1.Count; i++)
            {
                double field1x = Field1[i].X / kappa.GetValue();
                double field1y = Field1[i].Y / kappa.GetValue();
                double field1z = Field1[i].Z / kappa.GetValue();

                double field2x = Field2[i].X / kappa.GetValue();
                double field2y = Field2[i].Y / kappa.GetValue();
                double field2z = Field2[i].Z / kappa.GetValue();

                // Считаем аномальное поле
                double T = new Vector3(field1x + Global.T0.X, field1y + Global.T0.Y, field1z + Global.T0.Z).Getlength() - Global.T0.Getlength();

                dTmean += T / (2 *Field1.Count);
                dT.Add(T);

                T = new Vector3(field2x + Global.T0.X, field2y + Global.T0.Y, field2z + Global.T0.Z).Getlength() - Global.T0.Getlength();

                dTmean += T / (2 * Field1.Count);
                dT.Add(T);
            }

            for(int i = 0; i < FieldMeas.Count; i++)
            {
                double temp1 = 0, temp2 = 0;
                foreach (var body in Global.bodies)
                {
                    if(body != Global.bodies[index])
                    {
                        var BField1 = body.GetField(Global.Profiles.IndexOf(CurrentProfile), (int)CurrentProfile.Hi1.GetValue());
                        var BField2 = body.GetField(Global.Profiles.IndexOf(CurrentProfile), (int)CurrentProfile.Hi2.GetValue());

                        temp1 += new Vector3(BField1[i].X + Global.T0.X, BField1[i].Y + Global.T0.Y, BField1[i].Z + Global.T0.Z).Getlength() - Global.T0.Getlength();
                        temp2 += new Vector3(BField2[i].X + Global.T0.X, BField2[i].Y + Global.T0.Y, BField2[i].Z + Global.T0.Z).Getlength() - Global.T0.Getlength();
                    }
                }
                temp1 = FieldMeas[i].Y - temp1;
                temp2 = FieldMeas[i].Z - temp2;

                Tobsmean += temp1 / (2 * FieldMeas.Count);
                Tobsmean += temp2 / (2 * FieldMeas.Count);

                Tobs.Add(temp1);
                Tobs.Add(temp2);
            }

            double s1 = 0, s2 = 0; // Определяем 2 переменные для суммы

            for (int i = 0; i < dT.Count; i++) // Проходим по всем i 
            {
                s1 += (Tobs[i] - Tobsmean) * (dT[i] - dTmean); // Считаем сумму в числителе
                s2 += (dT[i] - dTmean) * (dT[i] - dTmean);  // Считаем сумму в знаменателе
            }

            kappa.SetValue(s1 / s2);

        }

        private void BodyControl_Enter(object sender, EventArgs e)
        {
            var index = CurrentProfile.bodyControls.IndexOf(this);

            Global.SelectedBodyIndex = index;
            CurrentProfile.Draw(sender,e);
        }

        private void BodyControl_Leave(object sender, EventArgs e)
        {
            if(Global.Lock) return;

            Global.SelectedBodyIndex = -1;
        }
    }
}