
namespace Magn3D_Prof
{
    partial class BodyControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Body = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.KappaAuto = new System.Windows.Forms.Button();
            this.Inclin = new Magn3D_Prof.Numeric();
            this.Declin = new Magn3D_Prof.Numeric();
            this.kappa = new Magn3D_Prof.Numeric();
            this.alpha = new Magn3D_Prof.Numeric();
            this.beta = new Magn3D_Prof.Numeric();
            this.fi = new Magn3D_Prof.Numeric();
            this.h1 = new Magn3D_Prof.Numeric();
            this.h2 = new Magn3D_Prof.Numeric();
            this.h3 = new Magn3D_Prof.Numeric();
            this.L = new Magn3D_Prof.Numeric();
            this.d = new Magn3D_Prof.Numeric();
            this.b = new Magn3D_Prof.Numeric();
            this.Y = new Magn3D_Prof.Numeric();
            this.X = new Magn3D_Prof.Numeric();
            this.ConnectDepth = new System.Windows.Forms.CheckBox();
            this.HorizontalLowEdge = new System.Windows.Forms.CheckBox();
            this.Remove = new System.Windows.Forms.Button();
            this.DbigLabel = new System.Windows.Forms.Label();
            this.IBigLabel = new System.Windows.Forms.Label();
            this.Kappalabel = new System.Windows.Forms.Label();
            this.Fi_label = new System.Windows.Forms.Label();
            this.Beta_label = new System.Windows.Forms.Label();
            this.Alpha_label = new System.Windows.Forms.Label();
            this.h3label = new System.Windows.Forms.Label();
            this.h2label = new System.Windows.Forms.Label();
            this.h1label = new System.Windows.Forms.Label();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.dlabel = new System.Windows.Forms.Label();
            this.blabel = new System.Windows.Forms.Label();
            this.YcentreLabel = new System.Windows.Forms.Label();
            this.XcentreLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Body
            // 
            this.Body.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.Body.BackColor = System.Drawing.SystemColors.Control;
            this.Body.Controls.Add(this.button1);
            this.Body.Controls.Add(this.KappaAuto);
            this.Body.Controls.Add(this.Inclin);
            this.Body.Controls.Add(this.Declin);
            this.Body.Controls.Add(this.kappa);
            this.Body.Controls.Add(this.alpha);
            this.Body.Controls.Add(this.beta);
            this.Body.Controls.Add(this.fi);
            this.Body.Controls.Add(this.h1);
            this.Body.Controls.Add(this.h2);
            this.Body.Controls.Add(this.h3);
            this.Body.Controls.Add(this.L);
            this.Body.Controls.Add(this.d);
            this.Body.Controls.Add(this.b);
            this.Body.Controls.Add(this.Y);
            this.Body.Controls.Add(this.X);
            this.Body.Controls.Add(this.ConnectDepth);
            this.Body.Controls.Add(this.HorizontalLowEdge);
            this.Body.Controls.Add(this.Remove);
            this.Body.Controls.Add(this.DbigLabel);
            this.Body.Controls.Add(this.IBigLabel);
            this.Body.Controls.Add(this.Kappalabel);
            this.Body.Controls.Add(this.Fi_label);
            this.Body.Controls.Add(this.Beta_label);
            this.Body.Controls.Add(this.Alpha_label);
            this.Body.Controls.Add(this.h3label);
            this.Body.Controls.Add(this.h2label);
            this.Body.Controls.Add(this.h1label);
            this.Body.Controls.Add(this.LengthLabel);
            this.Body.Controls.Add(this.dlabel);
            this.Body.Controls.Add(this.blabel);
            this.Body.Controls.Add(this.YcentreLabel);
            this.Body.Controls.Add(this.XcentreLabel);
            this.Body.Location = new System.Drawing.Point(0, 0);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(384, 176);
            this.Body.TabIndex = 5;
            this.Body.TabStop = false;
            this.Body.Text = "Новое тело";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(315, 136);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 29);
            this.button1.TabIndex = 54;
            this.button1.Text = "reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // KappaAuto
            // 
            this.KappaAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.KappaAuto.Location = new System.Drawing.Point(92, 136);
            this.KappaAuto.Margin = new System.Windows.Forms.Padding(2);
            this.KappaAuto.Name = "KappaAuto";
            this.KappaAuto.Size = new System.Drawing.Size(27, 29);
            this.KappaAuto.TabIndex = 50;
            this.KappaAuto.Text = "кA";
            this.toolTip1.SetToolTip(this.KappaAuto, "1E-5 ед. СИ");
            this.KappaAuto.UseVisualStyleBackColor = true;
            this.KappaAuto.Click += new System.EventHandler(this.KappaAuto_Click);
            // 
            // Inclin
            // 
            this.Inclin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Inclin.Decimalplaces = 3;
            this.Inclin.Location = new System.Drawing.Point(243, 141);
            this.Inclin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Inclin.Name = "Inclin";
            this.Inclin.Size = new System.Drawing.Size(67, 29);
            this.Inclin.TabIndex = 53;
            this.Inclin.TabStop = false;
            // 
            // Declin
            // 
            this.Declin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Declin.Decimalplaces = 3;
            this.Declin.Location = new System.Drawing.Point(148, 141);
            this.Declin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Declin.Name = "Declin";
            this.Declin.Size = new System.Drawing.Size(73, 29);
            this.Declin.TabIndex = 52;
            this.Declin.TabStop = false;
            // 
            // kappa
            // 
            this.kappa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.kappa.Decimalplaces = 3;
            this.kappa.Location = new System.Drawing.Point(26, 141);
            this.kappa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kappa.Name = "kappa";
            this.kappa.Size = new System.Drawing.Size(62, 29);
            this.kappa.TabIndex = 49;
            this.kappa.TabStop = false;
            this.toolTip1.SetToolTip(this.kappa, "1E-5 ед. СИ");
            // 
            // alpha
            // 
            this.alpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.alpha.Decimalplaces = 3;
            this.alpha.Location = new System.Drawing.Point(26, 115);
            this.alpha.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.alpha.Name = "alpha";
            this.alpha.Size = new System.Drawing.Size(93, 29);
            this.alpha.TabIndex = 46;
            this.alpha.TabStop = false;
            // 
            // beta
            // 
            this.beta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.beta.Decimalplaces = 3;
            this.beta.Location = new System.Drawing.Point(148, 115);
            this.beta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.beta.Name = "beta";
            this.beta.Size = new System.Drawing.Size(93, 29);
            this.beta.TabIndex = 47;
            this.beta.TabStop = false;
            // 
            // fi
            // 
            this.fi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.fi.Decimalplaces = 3;
            this.fi.Location = new System.Drawing.Point(272, 115);
            this.fi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fi.Name = "fi";
            this.fi.Size = new System.Drawing.Size(92, 29);
            this.fi.TabIndex = 48;
            this.fi.TabStop = false;
            // 
            // h1
            // 
            this.h1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.h1.Decimalplaces = 3;
            this.h1.Location = new System.Drawing.Point(26, 89);
            this.h1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.h1.Name = "h1";
            this.h1.Size = new System.Drawing.Size(93, 29);
            this.h1.TabIndex = 43;
            this.h1.TabStop = false;
            // 
            // h2
            // 
            this.h2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.h2.Decimalplaces = 3;
            this.h2.Location = new System.Drawing.Point(148, 89);
            this.h2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.h2.Name = "h2";
            this.h2.Size = new System.Drawing.Size(93, 29);
            this.h2.TabIndex = 44;
            this.h2.TabStop = false;
            // 
            // h3
            // 
            this.h3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.h3.Decimalplaces = 3;
            this.h3.Location = new System.Drawing.Point(272, 89);
            this.h3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.h3.Name = "h3";
            this.h3.Size = new System.Drawing.Size(92, 29);
            this.h3.TabIndex = 45;
            this.h3.TabStop = false;
            // 
            // L
            // 
            this.L.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.L.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.L.Decimalplaces = 3;
            this.L.Location = new System.Drawing.Point(315, 27);
            this.L.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.L.Name = "L";
            this.L.Size = new System.Drawing.Size(50, 29);
            this.L.TabIndex = 38;
            this.L.TabStop = false;
            // 
            // d
            // 
            this.d.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.d.Decimalplaces = 3;
            this.d.Location = new System.Drawing.Point(243, 27);
            this.d.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.d.Name = "d";
            this.d.Size = new System.Drawing.Size(50, 29);
            this.d.TabIndex = 37;
            this.d.TabStop = false;
            // 
            // b
            // 
            this.b.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.b.Decimalplaces = 3;
            this.b.Location = new System.Drawing.Point(171, 27);
            this.b.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(50, 29);
            this.b.TabIndex = 36;
            this.b.TabStop = false;
            // 
            // Y
            // 
            this.Y.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Y.Decimalplaces = 3;
            this.Y.Location = new System.Drawing.Point(99, 27);
            this.Y.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(50, 29);
            this.Y.TabIndex = 35;
            this.Y.TabStop = false;
            // 
            // X
            // 
            this.X.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.X.Decimalplaces = 3;
            this.X.Location = new System.Drawing.Point(26, 27);
            this.X.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(50, 29);
            this.X.TabIndex = 34;
            this.X.TabStop = false;
            // 
            // ConnectDepth
            // 
            this.ConnectDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectDepth.BackColor = System.Drawing.SystemColors.Control;
            this.ConnectDepth.Location = new System.Drawing.Point(228, 58);
            this.ConnectDepth.Name = "ConnectDepth";
            this.ConnectDepth.Size = new System.Drawing.Size(136, 28);
            this.ConnectDepth.TabIndex = 40;
            this.ConnectDepth.Text = "Связать глубины";
            this.ConnectDepth.UseVisualStyleBackColor = false;
            this.ConnectDepth.CheckedChanged += new System.EventHandler(this.ConnectDepth_CheckedChanged);
            // 
            // HorizontalLowEdge
            // 
            this.HorizontalLowEdge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.HorizontalLowEdge.BackColor = System.Drawing.SystemColors.Control;
            this.HorizontalLowEdge.Location = new System.Drawing.Point(10, 56);
            this.HorizontalLowEdge.Name = "HorizontalLowEdge";
            this.HorizontalLowEdge.Size = new System.Drawing.Size(210, 32);
            this.HorizontalLowEdge.TabIndex = 39;
            this.HorizontalLowEdge.Text = "горизонтальное нижнее основание";
            this.HorizontalLowEdge.UseVisualStyleBackColor = false;
            this.HorizontalLowEdge.CheckedChanged += new System.EventHandler(this.HorizontalLowEdge_CheckedChanged);
            // 
            // Remove
            // 
            this.Remove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Remove.BackColor = System.Drawing.SystemColors.Control;
            this.Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Remove.Location = new System.Drawing.Point(364, 0);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(20, 25);
            this.Remove.TabIndex = 31;
            this.Remove.Text = "X";
            this.Remove.UseVisualStyleBackColor = false;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // DbigLabel
            // 
            this.DbigLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.DbigLabel.BackColor = System.Drawing.SystemColors.Control;
            this.DbigLabel.Location = new System.Drawing.Point(124, 144);
            this.DbigLabel.Name = "DbigLabel";
            this.DbigLabel.Size = new System.Drawing.Size(14, 29);
            this.DbigLabel.TabIndex = 27;
            this.DbigLabel.Text = "D";
            // 
            // IBigLabel
            // 
            this.IBigLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.IBigLabel.BackColor = System.Drawing.SystemColors.Control;
            this.IBigLabel.Location = new System.Drawing.Point(226, 141);
            this.IBigLabel.Name = "IBigLabel";
            this.IBigLabel.Size = new System.Drawing.Size(8, 29);
            this.IBigLabel.TabIndex = 25;
            this.IBigLabel.Text = "I";
            // 
            // Kappalabel
            // 
            this.Kappalabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Kappalabel.BackColor = System.Drawing.SystemColors.Control;
            this.Kappalabel.Location = new System.Drawing.Point(10, 141);
            this.Kappalabel.Name = "Kappalabel";
            this.Kappalabel.Size = new System.Drawing.Size(11, 29);
            this.Kappalabel.TabIndex = 22;
            this.Kappalabel.Text = "κ";
            // 
            // Fi_label
            // 
            this.Fi_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Fi_label.BackColor = System.Drawing.SystemColors.Control;
            this.Fi_label.Location = new System.Drawing.Point(249, 115);
            this.Fi_label.Name = "Fi_label";
            this.Fi_label.Size = new System.Drawing.Size(12, 29);
            this.Fi_label.TabIndex = 20;
            this.Fi_label.Text = "φ";
            // 
            // Beta_label
            // 
            this.Beta_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Beta_label.BackColor = System.Drawing.SystemColors.Control;
            this.Beta_label.Location = new System.Drawing.Point(124, 115);
            this.Beta_label.Name = "Beta_label";
            this.Beta_label.Size = new System.Drawing.Size(12, 29);
            this.Beta_label.TabIndex = 18;
            this.Beta_label.Text = "β";
            // 
            // Alpha_label
            // 
            this.Alpha_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.Alpha_label.BackColor = System.Drawing.SystemColors.Control;
            this.Alpha_label.Location = new System.Drawing.Point(8, 115);
            this.Alpha_label.Name = "Alpha_label";
            this.Alpha_label.Size = new System.Drawing.Size(13, 29);
            this.Alpha_label.TabIndex = 16;
            this.Alpha_label.Text = "α";
            // 
            // h3label
            // 
            this.h3label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.h3label.BackColor = System.Drawing.SystemColors.Control;
            this.h3label.Location = new System.Drawing.Point(249, 89);
            this.h3label.Name = "h3label";
            this.h3label.Size = new System.Drawing.Size(18, 29);
            this.h3label.TabIndex = 14;
            this.h3label.Text = "h3";
            // 
            // h2label
            // 
            this.h2label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.h2label.BackColor = System.Drawing.SystemColors.Control;
            this.h2label.Location = new System.Drawing.Point(124, 89);
            this.h2label.Name = "h2label";
            this.h2label.Size = new System.Drawing.Size(18, 29);
            this.h2label.TabIndex = 12;
            this.h2label.Text = "h2";
            // 
            // h1label
            // 
            this.h1label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.h1label.BackColor = System.Drawing.SystemColors.Control;
            this.h1label.Location = new System.Drawing.Point(8, 89);
            this.h1label.Name = "h1label";
            this.h1label.Size = new System.Drawing.Size(18, 29);
            this.h1label.TabIndex = 33;
            this.h1label.Text = "h1";
            // 
            // LengthLabel
            // 
            this.LengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.LengthLabel.BackColor = System.Drawing.SystemColors.Control;
            this.LengthLabel.Location = new System.Drawing.Point(298, 27);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(12, 29);
            this.LengthLabel.TabIndex = 8;
            this.LengthLabel.Text = "L";
            // 
            // dlabel
            // 
            this.dlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.dlabel.BackColor = System.Drawing.SystemColors.Control;
            this.dlabel.Location = new System.Drawing.Point(226, 27);
            this.dlabel.Name = "dlabel";
            this.dlabel.Size = new System.Drawing.Size(12, 29);
            this.dlabel.TabIndex = 6;
            this.dlabel.Text = "d";
            // 
            // blabel
            // 
            this.blabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.blabel.BackColor = System.Drawing.SystemColors.Control;
            this.blabel.Location = new System.Drawing.Point(154, 27);
            this.blabel.Name = "blabel";
            this.blabel.Size = new System.Drawing.Size(12, 29);
            this.blabel.TabIndex = 4;
            this.blabel.Text = "b";
            // 
            // YcentreLabel
            // 
            this.YcentreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.YcentreLabel.BackColor = System.Drawing.SystemColors.Control;
            this.YcentreLabel.Location = new System.Drawing.Point(81, 27);
            this.YcentreLabel.Name = "YcentreLabel";
            this.YcentreLabel.Size = new System.Drawing.Size(13, 29);
            this.YcentreLabel.TabIndex = 2;
            this.YcentreLabel.Text = "Y";
            // 
            // XcentreLabel
            // 
            this.XcentreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Right)));
            this.XcentreLabel.BackColor = System.Drawing.SystemColors.Control;
            this.XcentreLabel.Location = new System.Drawing.Point(8, 27);
            this.XcentreLabel.Name = "XcentreLabel";
            this.XcentreLabel.Size = new System.Drawing.Size(13, 29);
            this.XcentreLabel.TabIndex = 0;
            this.XcentreLabel.Text = "X";
            // 
            // BodyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Body);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BodyControl";
            this.Size = new System.Drawing.Size(384, 176);
            this.Load += new System.EventHandler(this.Body_Load);
            this.Enter += new System.EventHandler(this.BodyControl_Enter);
            this.Leave += new System.EventHandler(this.BodyControl_Leave);
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolTip toolTip1;

        #endregion

        private System.Windows.Forms.GroupBox Body;
        public Magn3D_Prof.Numeric L;
        public Magn3D_Prof.Numeric d;
        public Magn3D_Prof.Numeric b;
        public Magn3D_Prof.Numeric Y;
        public Magn3D_Prof.Numeric X;
        public System.Windows.Forms.CheckBox ConnectDepth;
        private System.Windows.Forms.CheckBox HorizontalLowEdge;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Label DbigLabel;
        private System.Windows.Forms.Label IBigLabel;
        private System.Windows.Forms.Label Kappalabel;
        private System.Windows.Forms.Label Fi_label;
        private System.Windows.Forms.Label Beta_label;
        private System.Windows.Forms.Label Alpha_label;
        private System.Windows.Forms.Label h3label;
        private System.Windows.Forms.Label h2label;
        private System.Windows.Forms.Label h1label;
        private System.Windows.Forms.Label LengthLabel;
        private System.Windows.Forms.Label dlabel;
        private System.Windows.Forms.Label blabel;
        private System.Windows.Forms.Label YcentreLabel;
        private System.Windows.Forms.Label XcentreLabel;
        private System.Windows.Forms.Button KappaAuto;
        private Numeric Inclin;
        private Numeric Declin;
        private Magn3D_Prof.Numeric kappa;
        public Magn3D_Prof.Numeric fi;
        public Magn3D_Prof.Numeric h1;
        public Magn3D_Prof.Numeric h2;
        public Magn3D_Prof.Numeric h3;
        private System.Windows.Forms.Button button1;
        public Magn3D_Prof.Numeric beta;
        public Numeric alpha;
    }
}
