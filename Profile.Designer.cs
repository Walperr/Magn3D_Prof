
using System.Windows.Forms;

namespace Magn3D_Prof
{
    partial class Profile
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Profile));
            this.ProfileSplit = new System.Windows.Forms.SplitContainer();
            this.LeftSplit = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Hi2 = new Magn3D_Prof.Numeric();
            this.Hi1 = new Magn3D_Prof.Numeric();
            this.label5 = new System.Windows.Forms.Label();
            this.PointsCount = new Magn3D_Prof.Numeric();
            this.label4 = new System.Windows.Forms.Label();
            this.addbody = new System.Windows.Forms.Button();
            this.rememberField = new System.Windows.Forms.Button();
            this.Point1Y = new Magn3D_Prof.Numeric();
            this.Point1X = new Magn3D_Prof.Numeric();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Point0Y = new Magn3D_Prof.Numeric();
            this.Point0X = new Magn3D_Prof.Numeric();
            this.Point0YLabel = new System.Windows.Forms.Label();
            this.Point0XLabel = new System.Windows.Forms.Label();
            this.Point0Label = new System.Windows.Forms.Label();
            this.T0z = new Magn3D_Prof.Numeric();
            this.T0y = new Magn3D_Prof.Numeric();
            this.T0zLabel = new System.Windows.Forms.Label();
            this.T0yLabel = new System.Windows.Forms.Label();
            this.T0x = new Magn3D_Prof.Numeric();
            this.T0xLabel = new System.Windows.Forms.Label();
            this.ViewLabel = new System.Windows.Forms.Label();
            this.BodiesLabel = new System.Windows.Forms.Label();
            this.RigthSplit = new System.Windows.Forms.SplitContainer();
            this.SKOlabel = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Hmin = new Magn3D_Prof.Numeric();
            this.Hmax = new Magn3D_Prof.Numeric();
            this.Slit = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resetView = new System.Windows.Forms.Button();
            this.chooseGrid = new System.Windows.Forms.ComboBox();
            this.Top = new ploting.plot();
            ((System.ComponentModel.ISupportInitialize) (this.ProfileSplit)).BeginInit();
            this.ProfileSplit.Panel1.SuspendLayout();
            this.ProfileSplit.Panel2.SuspendLayout();
            this.ProfileSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.LeftSplit)).BeginInit();
            this.LeftSplit.Panel1.SuspendLayout();
            this.LeftSplit.Panel2.SuspendLayout();
            this.LeftSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.RigthSplit)).BeginInit();
            this.RigthSplit.Panel1.SuspendLayout();
            this.RigthSplit.Panel2.SuspendLayout();
            this.RigthSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.chart1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.Slit)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProfileSplit
            // 
            this.ProfileSplit.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfileSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProfileSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.ProfileSplit.IsSplitterFixed = true;
            this.ProfileSplit.Location = new System.Drawing.Point(0, 0);
            this.ProfileSplit.Name = "ProfileSplit";
            // 
            // ProfileSplit.Panel1
            // 
            this.ProfileSplit.Panel1.Controls.Add(this.LeftSplit);
            // 
            // ProfileSplit.Panel2
            // 
            this.ProfileSplit.Panel2.Controls.Add(this.RigthSplit);
            this.ProfileSplit.Size = new System.Drawing.Size(1280, 720);
            this.ProfileSplit.SplitterDistance = 382;
            this.ProfileSplit.TabIndex = 0;
            this.ProfileSplit.TabStop = false;
            // 
            // LeftSplit
            // 
            this.LeftSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.LeftSplit.IsSplitterFixed = true;
            this.LeftSplit.Location = new System.Drawing.Point(0, 0);
            this.LeftSplit.Name = "LeftSplit";
            this.LeftSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // LeftSplit.Panel1
            // 
            this.LeftSplit.Panel1.Controls.Add(this.label7);
            this.LeftSplit.Panel1.Controls.Add(this.label6);
            this.LeftSplit.Panel1.Controls.Add(this.Hi2);
            this.LeftSplit.Panel1.Controls.Add(this.Hi1);
            this.LeftSplit.Panel1.Controls.Add(this.label5);
            this.LeftSplit.Panel1.Controls.Add(this.PointsCount);
            this.LeftSplit.Panel1.Controls.Add(this.label4);
            this.LeftSplit.Panel1.Controls.Add(this.addbody);
            this.LeftSplit.Panel1.Controls.Add(this.rememberField);
            this.LeftSplit.Panel1.Controls.Add(this.Point1Y);
            this.LeftSplit.Panel1.Controls.Add(this.Point1X);
            this.LeftSplit.Panel1.Controls.Add(this.label1);
            this.LeftSplit.Panel1.Controls.Add(this.label2);
            this.LeftSplit.Panel1.Controls.Add(this.label3);
            this.LeftSplit.Panel1.Controls.Add(this.Point0Y);
            this.LeftSplit.Panel1.Controls.Add(this.Point0X);
            this.LeftSplit.Panel1.Controls.Add(this.Point0YLabel);
            this.LeftSplit.Panel1.Controls.Add(this.Point0XLabel);
            this.LeftSplit.Panel1.Controls.Add(this.Point0Label);
            this.LeftSplit.Panel1.Controls.Add(this.T0z);
            this.LeftSplit.Panel1.Controls.Add(this.T0y);
            this.LeftSplit.Panel1.Controls.Add(this.T0zLabel);
            this.LeftSplit.Panel1.Controls.Add(this.T0yLabel);
            this.LeftSplit.Panel1.Controls.Add(this.T0x);
            this.LeftSplit.Panel1.Controls.Add(this.T0xLabel);
            this.LeftSplit.Panel1.Controls.Add(this.ViewLabel);
            // 
            // LeftSplit.Panel2
            // 
            this.LeftSplit.Panel2.AutoScroll = true;
            this.LeftSplit.Panel2.Controls.Add(this.BodiesLabel);
            this.LeftSplit.Size = new System.Drawing.Size(382, 720);
            this.LeftSplit.SplitterDistance = 169;
            this.LeftSplit.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(440, 173);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 17);
            this.label7.TabIndex = 47;
            this.label7.Text = "Z2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 173);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 17);
            this.label6.TabIndex = 46;
            this.label6.Text = "Z1";
            // 
            // Hi2
            // 
            this.Hi2.Decimalplaces = 3;
            this.Hi2.Location = new System.Drawing.Point(472, 173);
            this.Hi2.Name = "Hi2";
            this.Hi2.Size = new System.Drawing.Size(29, 26);
            this.Hi2.TabIndex = 45;
            this.Hi2.TabStop = false;
            // 
            // Hi1
            // 
            this.Hi1.Decimalplaces = 3;
            this.Hi1.Location = new System.Drawing.Point(388, 173);
            this.Hi1.Name = "Hi1";
            this.Hi1.Size = new System.Drawing.Size(29, 26);
            this.Hi1.TabIndex = 44;
            this.Hi1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 173);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(287, 17);
            this.label5.TabIndex = 43;
            this.label5.Text = "Отображаемые эшелоны высот (индексы)";
            // 
            // PointsCount
            // 
            this.PointsCount.Decimalplaces = 3;
            this.PointsCount.Location = new System.Drawing.Point(224, 141);
            this.PointsCount.Name = "PointsCount";
            this.PointsCount.Size = new System.Drawing.Size(64, 26);
            this.PointsCount.TabIndex = 42;
            this.PointsCount.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 141);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 17);
            this.label4.TabIndex = 41;
            this.label4.Text = "Количество точек на профиле";
            // 
            // addbody
            // 
            this.addbody.Location = new System.Drawing.Point(359, 111);
            this.addbody.Margin = new System.Windows.Forms.Padding(4);
            this.addbody.Name = "addbody";
            this.addbody.Size = new System.Drawing.Size(142, 27);
            this.addbody.TabIndex = 39;
            this.addbody.Text = "Новое тело";
            this.addbody.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addbody.UseVisualStyleBackColor = true;
            this.addbody.Click += new System.EventHandler(this.addbody_Click);
            // 
            // rememberField
            // 
            this.rememberField.Location = new System.Drawing.Point(359, 75);
            this.rememberField.Margin = new System.Windows.Forms.Padding(4);
            this.rememberField.Name = "rememberField";
            this.rememberField.Size = new System.Drawing.Size(142, 28);
            this.rememberField.TabIndex = 34;
            this.rememberField.Text = "Запомнить поле";
            this.rememberField.UseVisualStyleBackColor = true;
            this.rememberField.Click += new System.EventHandler(this.rememberField_Click);
            // 
            // Point1Y
            // 
            this.Point1Y.Decimalplaces = 3;
            this.Point1Y.Location = new System.Drawing.Point(224, 112);
            this.Point1Y.Name = "Point1Y";
            this.Point1Y.Size = new System.Drawing.Size(107, 26);
            this.Point1Y.TabIndex = 38;
            this.Point1Y.TabStop = false;
            // 
            // Point1X
            // 
            this.Point1X.Decimalplaces = 3;
            this.Point1X.Location = new System.Drawing.Point(45, 112);
            this.Point1X.Name = "Point1X";
            this.Point1X.Size = new System.Drawing.Size(107, 26);
            this.Point1X.TabIndex = 37;
            this.Point1X.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 36;
            this.label1.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 35;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "End";
            // 
            // Point0Y
            // 
            this.Point0Y.Decimalplaces = 3;
            this.Point0Y.Location = new System.Drawing.Point(224, 78);
            this.Point0Y.Name = "Point0Y";
            this.Point0Y.Size = new System.Drawing.Size(107, 28);
            this.Point0Y.TabIndex = 33;
            this.Point0Y.TabStop = false;
            // 
            // Point0X
            // 
            this.Point0X.Decimalplaces = 3;
            this.Point0X.Location = new System.Drawing.Point(45, 78);
            this.Point0X.Name = "Point0X";
            this.Point0X.Size = new System.Drawing.Size(107, 25);
            this.Point0X.TabIndex = 32;
            this.Point0X.TabStop = false;
            // 
            // Point0YLabel
            // 
            this.Point0YLabel.AutoSize = true;
            this.Point0YLabel.Location = new System.Drawing.Point(201, 81);
            this.Point0YLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Point0YLabel.Name = "Point0YLabel";
            this.Point0YLabel.Size = new System.Drawing.Size(17, 17);
            this.Point0YLabel.TabIndex = 30;
            this.Point0YLabel.Text = "Y";
            // 
            // Point0XLabel
            // 
            this.Point0XLabel.AutoSize = true;
            this.Point0XLabel.Location = new System.Drawing.Point(21, 81);
            this.Point0XLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Point0XLabel.Name = "Point0XLabel";
            this.Point0XLabel.Size = new System.Drawing.Size(17, 17);
            this.Point0XLabel.TabIndex = 28;
            this.Point0XLabel.Text = "X";
            // 
            // Point0Label
            // 
            this.Point0Label.AutoSize = true;
            this.Point0Label.Location = new System.Drawing.Point(155, 64);
            this.Point0Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Point0Label.Name = "Point0Label";
            this.Point0Label.Size = new System.Drawing.Size(38, 17);
            this.Point0Label.TabIndex = 27;
            this.Point0Label.Text = "Start";
            // 
            // T0z
            // 
            this.T0z.Decimalplaces = 3;
            this.T0z.Enabled = false;
            this.T0z.Location = new System.Drawing.Point(394, 38);
            this.T0z.Name = "T0z";
            this.T0z.Size = new System.Drawing.Size(107, 30);
            this.T0z.TabIndex = 26;
            this.T0z.TabStop = false;
            // 
            // T0y
            // 
            this.T0y.Decimalplaces = 3;
            this.T0y.Enabled = false;
            this.T0y.Location = new System.Drawing.Point(224, 38);
            this.T0y.Name = "T0y";
            this.T0y.Size = new System.Drawing.Size(107, 34);
            this.T0y.TabIndex = 25;
            this.T0y.TabStop = false;
            // 
            // T0zLabel
            // 
            this.T0zLabel.AutoSize = true;
            this.T0zLabel.Location = new System.Drawing.Point(356, 38);
            this.T0zLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.T0zLabel.Name = "T0zLabel";
            this.T0zLabel.Size = new System.Drawing.Size(32, 17);
            this.T0zLabel.TabIndex = 23;
            this.T0zLabel.Text = "T0z";
            // 
            // T0yLabel
            // 
            this.T0yLabel.AutoSize = true;
            this.T0yLabel.Location = new System.Drawing.Point(186, 38);
            this.T0yLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.T0yLabel.Name = "T0yLabel";
            this.T0yLabel.Size = new System.Drawing.Size(32, 17);
            this.T0yLabel.TabIndex = 21;
            this.T0yLabel.Text = "T0y";
            // 
            // T0x
            // 
            this.T0x.Decimalplaces = 3;
            this.T0x.Enabled = false;
            this.T0x.Location = new System.Drawing.Point(45, 38);
            this.T0x.Name = "T0x";
            this.T0x.Size = new System.Drawing.Size(107, 34);
            this.T0x.TabIndex = 20;
            this.T0x.TabStop = false;
            // 
            // T0xLabel
            // 
            this.T0xLabel.AutoSize = true;
            this.T0xLabel.Location = new System.Drawing.Point(7, 38);
            this.T0xLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.T0xLabel.Name = "T0xLabel";
            this.T0xLabel.Size = new System.Drawing.Size(31, 17);
            this.T0xLabel.TabIndex = 17;
            this.T0xLabel.Text = "T0x";
            // 
            // ViewLabel
            // 
            this.ViewLabel.AutoSize = true;
            this.ViewLabel.Location = new System.Drawing.Point(234, 5);
            this.ViewLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ViewLabel.Name = "ViewLabel";
            this.ViewLabel.Size = new System.Drawing.Size(54, 17);
            this.ViewLabel.TabIndex = 1;
            this.ViewLabel.Text = "Общие";
            // 
            // BodiesLabel
            // 
            this.BodiesLabel.AutoSize = true;
            this.BodiesLabel.Location = new System.Drawing.Point(237, 5);
            this.BodiesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BodiesLabel.Name = "BodiesLabel";
            this.BodiesLabel.Size = new System.Drawing.Size(39, 17);
            this.BodiesLabel.TabIndex = 2;
            this.BodiesLabel.Text = "тела";
            // 
            // RigthSplit
            // 
            this.RigthSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RigthSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RigthSplit.Location = new System.Drawing.Point(0, 0);
            this.RigthSplit.Name = "RigthSplit";
            this.RigthSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // RigthSplit.Panel1
            // 
            this.RigthSplit.Panel1.Controls.Add(this.SKOlabel);
            this.RigthSplit.Panel1.Controls.Add(this.chart1);
            // 
            // RigthSplit.Panel2
            // 
            this.RigthSplit.Panel2.Controls.Add(this.tabControl1);
            this.RigthSplit.Size = new System.Drawing.Size(894, 720);
            this.RigthSplit.SplitterDistance = 343;
            this.RigthSplit.TabIndex = 0;
            this.RigthSplit.TabStop = false;
            // 
            // SKOlabel
            // 
            this.SKOlabel.AutoSize = true;
            this.SKOlabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.SKOlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.SKOlabel.Location = new System.Drawing.Point(801, 0);
            this.SKOlabel.Name = "SKOlabel";
            this.SKOlabel.Size = new System.Drawing.Size(91, 29);
            this.SKOlabel.TabIndex = 2;
            this.SKOlabel.Text = "СКО = ";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chart1.PaletteCustomColors = new System.Drawing.Color[] {System.Drawing.Color.Blue, System.Drawing.Color.Yellow, System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Cyan, System.Drawing.Color.Green, System.Drawing.Color.Olive, System.Drawing.Color.Gray};
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Аномальное поле 1";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Аномальное поле 2";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Enabled = false;
            series3.Legend = "Legend1";
            series3.Name = "FieldX";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Enabled = false;
            series4.Legend = "Legend1";
            series4.Name = "FieldY";
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Enabled = false;
            series5.Legend = "Legend1";
            series5.Name = "FieldZ";
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "Измеренное поле 1";
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.Name = "Измеренное поле 2";
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Legend = "Legend1";
            series8.Name = "Буфер";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Series.Add(series8);
            this.chart1.Size = new System.Drawing.Size(892, 341);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "FieldChart";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            this.chart1.Enter += new System.EventHandler(this.Slit_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(892, 371);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Hmin);
            this.tabPage1.Controls.Add(this.Hmax);
            this.tabPage1.Controls.Add(this.Slit);
            this.tabPage1.Location = new System.Drawing.Point(25, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(863, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Разрез";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Hmin
            // 
            this.Hmin.Decimalplaces = 3;
            this.Hmin.Location = new System.Drawing.Point(32, 36);
            this.Hmin.Name = "Hmin";
            this.Hmin.Size = new System.Drawing.Size(100, 30);
            this.Hmin.TabIndex = 501;
            this.Hmin.TabStop = false;
            this.Hmin.Enter += new System.EventHandler(this.Slit_Enter);
            this.Hmin.Leave += new System.EventHandler(this.Slit_Leave);
            // 
            // Hmax
            // 
            this.Hmax.Decimalplaces = 3;
            this.Hmax.Location = new System.Drawing.Point(32, 314);
            this.Hmax.Name = "Hmax";
            this.Hmax.Size = new System.Drawing.Size(100, 30);
            this.Hmax.TabIndex = 53;
            this.Hmax.TabStop = false;
            this.Hmax.Enter += new System.EventHandler(this.Slit_Enter);
            this.Hmax.Leave += new System.EventHandler(this.Slit_Leave);
            // 
            // Slit
            // 
            this.Slit.AllowDrop = true;
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.LabelStyle.Format = "F2";
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.IsReversed = true;
            chartArea2.AxisY.LabelStyle.Format = "F0";
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.Slit.ChartAreas.Add(chartArea2);
            this.Slit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Slit.Location = new System.Drawing.Point(3, 3);
            this.Slit.Name = "Slit";
            this.Slit.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.Slit.PaletteCustomColors = new System.Drawing.Color[] {System.Drawing.Color.Black};
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series9.Name = "Series1";
            this.Slit.Series.Add(series9);
            this.Slit.Size = new System.Drawing.Size(857, 357);
            this.Slit.TabIndex = 0;
            this.Slit.Click += new System.EventHandler(this.Slit_Click);
            this.Slit.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintA);
            this.Slit.Enter += new System.EventHandler(this.Slit_Enter);
            this.Slit.Leave += new System.EventHandler(this.Slit_Leave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(25, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(863, 363);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сверху";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.resetView);
            this.splitContainer1.Panel1.Controls.Add(this.chooseGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Top);
            this.splitContainer1.Size = new System.Drawing.Size(857, 357);
            this.splitContainer1.SplitterDistance = 91;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 108);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5, 10, 60, 5);
            this.panel1.Size = new System.Drawing.Size(91, 249);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // resetView
            // 
            this.resetView.AutoSize = true;
            this.resetView.Image = ((System.Drawing.Image) (resources.GetObject("resetView.Image")));
            this.resetView.Location = new System.Drawing.Point(6, 30);
            this.resetView.Name = "resetView";
            this.resetView.Size = new System.Drawing.Size(81, 72);
            this.resetView.TabIndex = 0;
            this.resetView.TabStop = false;
            this.resetView.UseVisualStyleBackColor = true;
            this.resetView.Click += new System.EventHandler(this.resetView_Click);
            // 
            // chooseGrid
            // 
            this.chooseGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chooseGrid.FormattingEnabled = true;
            this.chooseGrid.Items.AddRange(new object[] {"рельеф"});
            this.chooseGrid.Location = new System.Drawing.Point(0, 0);
            this.chooseGrid.Name = "chooseGrid";
            this.chooseGrid.Size = new System.Drawing.Size(91, 24);
            this.chooseGrid.TabIndex = 3;
            this.chooseGrid.Text = "рельеф";
            this.chooseGrid.SelectedIndexChanged += new System.EventHandler(this.chooseGrid_SelectedIndexChanged);
            // 
            // Top
            // 
            this.Top.ContoursCount = 15;
            this.Top.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Top.DeferUpdate = false;
            this.Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Top.Grid = null;
            this.Top.LabelsCountX = 5;
            this.Top.LabelsCountY = 5;
            this.Top.Location = new System.Drawing.Point(0, 0);
            this.Top.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Top.Name = "Top";
            this.Top.Padding = new System.Windows.Forms.Padding(5);
            this.Top.Size = new System.Drawing.Size(762, 357);
            this.Top.TabIndex = 0;
            this.Top.ViewHeight = 0;
            this.Top.ViewXmin = 0;
            this.Top.ViewYmin = 0;
            this.Top.Enter += new System.EventHandler(this.Top_Enter);
            this.Top.Leave += new System.EventHandler(this.Top_Leave);
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ProfileSplit);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Profile";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Load += new System.EventHandler(this.Profile_Load);
            this.ProfileSplit.Panel1.ResumeLayout(false);
            this.ProfileSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.ProfileSplit)).EndInit();
            this.ProfileSplit.ResumeLayout(false);
            this.LeftSplit.Panel1.ResumeLayout(false);
            this.LeftSplit.Panel1.PerformLayout();
            this.LeftSplit.Panel2.ResumeLayout(false);
            this.LeftSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.LeftSplit)).EndInit();
            this.LeftSplit.ResumeLayout(false);
            this.RigthSplit.Panel1.ResumeLayout(false);
            this.RigthSplit.Panel1.PerformLayout();
            this.RigthSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.RigthSplit)).EndInit();
            this.RigthSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.chart1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.Slit)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.ComboBox chooseGrid;

        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.SplitContainer splitContainer1;

        #endregion

        private System.Windows.Forms.SplitContainer ProfileSplit;
        private System.Windows.Forms.SplitContainer LeftSplit;
        private System.Windows.Forms.SplitContainer RigthSplit;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label ViewLabel;
        private System.Windows.Forms.Label BodiesLabel;
        private System.Windows.Forms.Label T0xLabel;
        private Magn3D_Prof.Numeric T0z;
        private Magn3D_Prof.Numeric T0y;
        private System.Windows.Forms.Label T0zLabel;
        private System.Windows.Forms.Label T0yLabel;
        private Magn3D_Prof.Numeric T0x;
        private System.Windows.Forms.Label Point0YLabel;
        private System.Windows.Forms.Label Point0XLabel;
        private System.Windows.Forms.Label Point0Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addbody;
        private System.Windows.Forms.Button rememberField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public Magn3D_Prof.Numeric Point0Y;
        public Magn3D_Prof.Numeric Point0X;
        public Magn3D_Prof.Numeric Point1Y;
        public Magn3D_Prof.Numeric Point1X;
        public Magn3D_Prof.Numeric PointsCount;
        public Magn3D_Prof.Numeric Hi2;
        public Magn3D_Prof.Numeric Hi1;
        private System.Windows.Forms.Label SKOlabel;
        public Magn3D_Prof.Numeric Hmin;
        private Magn3D_Prof.Numeric Hmax;
        private System.Windows.Forms.DataVisualization.Charting.Chart Slit;
        private ploting.plot Top;
        private System.Windows.Forms.Button resetView;
    }
}
