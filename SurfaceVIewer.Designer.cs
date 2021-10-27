using System.ComponentModel;

namespace Magn3D_Prof
{
    partial class SurfaceVIewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.showProperties = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.showProperties);
            this.splitContainer1.Size = new System.Drawing.Size(928, 525);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 0;
            // 
            // showProperties
            // 
            this.showProperties.BackColor = System.Drawing.Color.Transparent;
            this.showProperties.Location = new System.Drawing.Point(3, 3);
            this.showProperties.Name = "showProperties";
            this.showProperties.Size = new System.Drawing.Size(20, 27);
            this.showProperties.TabIndex = 0;
            this.showProperties.Text = ">";
            this.showProperties.UseVisualStyleBackColor = false;
            this.showProperties.Click += new System.EventHandler(this.showProperties_Click);
            // 
            // SurfaceVIewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SurfaceVIewer";
            this.Size = new System.Drawing.Size(928, 525);
            this.Load += new System.EventHandler(this.SurfaceVIewer_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button showProperties;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.SplitContainer splitContainer1;

        #endregion
    }
}