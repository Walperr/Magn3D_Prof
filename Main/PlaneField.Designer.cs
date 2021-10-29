
namespace Magn3D_Prof.Main
{
    partial class PlaneField
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaneField));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.H = new Numeric();
            this.dY = new Numeric();
            this.Ymaximum = new Numeric();
            this.Yminimum = new Numeric();
            this.dX = new Numeric();
            this.Xmaximum = new Numeric();
            this.Xminimum = new Numeric();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(630, 189);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(622, 160);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "По заданной сетке";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.progressBar1);
            this.tabPage2.Controls.Add(this.H);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.dY);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.Ymaximum);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.Yminimum);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.dX);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.Xmaximum);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.Xminimum);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(622, 160);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "По произвольной сетке";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xmin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Xmax";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Шаг X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Шаг Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ymax";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Ymin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "H";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(214, 90);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(397, 26);
            this.progressBar1.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(600, 31);
            this.button1.TabIndex = 15;
            this.button1.Text = "Рассчитать и сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "GRID файлы|*.grd|Dat файлы|*.dat";
            // 
            // H
            // 
            this.H.Decimalplaces = 3;
            this.H.Location = new System.Drawing.Point(53, 90);
            this.H.Name = "H";
            this.H.Size = new System.Drawing.Size(152, 26);
            this.H.TabIndex = 13;
            // 
            // dY
            // 
            this.dY.Decimalplaces = 3;
            this.dY.Location = new System.Drawing.Point(459, 58);
            this.dY.Name = "dY";
            this.dY.Size = new System.Drawing.Size(152, 26);
            this.dY.TabIndex = 11;
            // 
            // Ymaximum
            // 
            this.Ymaximum.Decimalplaces = 3;
            this.Ymaximum.Location = new System.Drawing.Point(256, 58);
            this.Ymaximum.Name = "Ymaximum";
            this.Ymaximum.Size = new System.Drawing.Size(152, 26);
            this.Ymaximum.TabIndex = 9;
            // 
            // Yminimum
            // 
            this.Yminimum.Decimalplaces = 3;
            this.Yminimum.Location = new System.Drawing.Point(53, 58);
            this.Yminimum.Name = "Yminimum";
            this.Yminimum.Size = new System.Drawing.Size(152, 26);
            this.Yminimum.TabIndex = 7;
            // 
            // dX
            // 
            this.dX.Decimalplaces = 3;
            this.dX.Location = new System.Drawing.Point(459, 26);
            this.dX.Name = "dX";
            this.dX.Size = new System.Drawing.Size(152, 26);
            this.dX.TabIndex = 5;
            // 
            // Xmaximum
            // 
            this.Xmaximum.Decimalplaces = 3;
            this.Xmaximum.Location = new System.Drawing.Point(256, 26);
            this.Xmaximum.Name = "Xmaximum";
            this.Xmaximum.Size = new System.Drawing.Size(152, 26);
            this.Xmaximum.TabIndex = 3;
            // 
            // Xminimum
            // 
            this.Xminimum.Decimalplaces = 3;
            this.Xminimum.Location = new System.Drawing.Point(53, 26);
            this.Xminimum.Name = "Xminimum";
            this.Xminimum.Size = new System.Drawing.Size(152, 26);
            this.Xminimum.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(11, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(600, 68);
            this.listBox1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(600, 31);
            this.button2.TabIndex = 16;
            this.button2.Text = "Рассчитать и сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "Расчитать поле по сетке: ";
            // 
            // PlaneField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 189);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlaneField";
            this.Text = "Рассчитать поле";
            this.Load += new System.EventHandler(this.PlaneField_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Numeric dX;
        private System.Windows.Forms.Label label3;
        private Numeric Xmaximum;
        private System.Windows.Forms.Label label2;
        private Numeric Xminimum;
        private System.Windows.Forms.Label label1;
        private Numeric dY;
        private System.Windows.Forms.Label label4;
        private Numeric Ymaximum;
        private System.Windows.Forms.Label label5;
        private Numeric Yminimum;
        private System.Windows.Forms.Label label6;
        private Numeric H;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
    }
}