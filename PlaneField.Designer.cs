
namespace Magn3D_Prof
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.H = new Magn3D_Prof.Numeric();
            this.label7 = new System.Windows.Forms.Label();
            this.dY = new Magn3D_Prof.Numeric();
            this.label4 = new System.Windows.Forms.Label();
            this.Ymaximum = new Magn3D_Prof.Numeric();
            this.label5 = new System.Windows.Forms.Label();
            this.Yminimum = new Magn3D_Prof.Numeric();
            this.label6 = new System.Windows.Forms.Label();
            this.dX = new Magn3D_Prof.Numeric();
            this.label3 = new System.Windows.Forms.Label();
            this.Xmaximum = new Magn3D_Prof.Numeric();
            this.label2 = new System.Windows.Forms.Label();
            this.Xminimum = new Magn3D_Prof.Numeric();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.numeric1 = new Magn3D_Prof.Numeric();
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
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(472, 177);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.numeric1);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(464, 151);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "По заданной сетке";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 23);
            this.label9.TabIndex = 19;
            this.label9.Text = "Смещение поля, нТл";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 15);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Расчитать поле по сетке: ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 119);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(450, 25);
            this.button2.TabIndex = 16;
            this.button2.Text = "Рассчитать и сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(8, 31);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(451, 56);
            this.listBox1.TabIndex = 0;
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
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(464, 151);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "По произвольной сетке";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 99);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(450, 25);
            this.button1.TabIndex = 15;
            this.button1.Text = "Рассчитать и сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(160, 73);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(298, 21);
            this.progressBar1.TabIndex = 14;
            // 
            // H
            // 
            this.H.Decimalplaces = 3;
            this.H.Location = new System.Drawing.Point(40, 73);
            this.H.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.H.Name = "H";
            this.H.Size = new System.Drawing.Size(114, 21);
            this.H.TabIndex = 13;
            this.H.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 73);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "H";
            // 
            // dY
            // 
            this.dY.Decimalplaces = 3;
            this.dY.Location = new System.Drawing.Point(344, 47);
            this.dY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dY.Name = "dY";
            this.dY.Size = new System.Drawing.Size(114, 21);
            this.dY.TabIndex = 11;
            this.dY.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Шаг Y";
            // 
            // Ymaximum
            // 
            this.Ymaximum.Decimalplaces = 3;
            this.Ymaximum.Location = new System.Drawing.Point(192, 47);
            this.Ymaximum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ymaximum.Name = "Ymaximum";
            this.Ymaximum.Size = new System.Drawing.Size(114, 21);
            this.Ymaximum.TabIndex = 9;
            this.Ymaximum.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 47);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ymax";
            // 
            // Yminimum
            // 
            this.Yminimum.Decimalplaces = 3;
            this.Yminimum.Location = new System.Drawing.Point(40, 47);
            this.Yminimum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Yminimum.Name = "Yminimum";
            this.Yminimum.Size = new System.Drawing.Size(114, 21);
            this.Yminimum.TabIndex = 7;
            this.Yminimum.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Ymin";
            // 
            // dX
            // 
            this.dX.Decimalplaces = 3;
            this.dX.Location = new System.Drawing.Point(344, 21);
            this.dX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dX.Name = "dX";
            this.dX.Size = new System.Drawing.Size(114, 21);
            this.dX.TabIndex = 5;
            this.dX.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(310, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Шаг X";
            // 
            // Xmaximum
            // 
            this.Xmaximum.Decimalplaces = 3;
            this.Xmaximum.Location = new System.Drawing.Point(192, 21);
            this.Xmaximum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Xmaximum.Name = "Xmaximum";
            this.Xmaximum.Size = new System.Drawing.Size(114, 21);
            this.Xmaximum.TabIndex = 3;
            this.Xmaximum.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Xmax";
            // 
            // Xminimum
            // 
            this.Xminimum.Decimalplaces = 3;
            this.Xminimum.Location = new System.Drawing.Point(40, 21);
            this.Xminimum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Xminimum.Name = "Xminimum";
            this.Xminimum.Size = new System.Drawing.Size(114, 21);
            this.Xminimum.TabIndex = 1;
            this.Xminimum.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xmin";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "GRID файлы|*.grd|Dat файлы|*.dat";
            // 
            // numeric1
            // 
            this.numeric1.Decimalplaces = 3;
            this.numeric1.Location = new System.Drawing.Point(221, 91);
            this.numeric1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numeric1.Name = "numeric1";
            this.numeric1.Size = new System.Drawing.Size(236, 24);
            this.numeric1.TabIndex = 20;
            this.numeric1.TabStop = false;
            // 
            // PlaneField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 177);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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

        private Magn3D_Prof.Numeric numeric1;

        private System.Windows.Forms.Label label9;

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