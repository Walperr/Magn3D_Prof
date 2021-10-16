
namespace ploting
{
    partial class plot
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.surfaceVIewer1 = new Magn3D_Prof.SurfaceVIewer();
            this.SuspendLayout();
            // 
            // surfaceVIewer1
            // 
            this.surfaceVIewer1.Location = new System.Drawing.Point(0, 0);
            this.surfaceVIewer1.Name = "surfaceVIewer1";
            this.surfaceVIewer1.Size = new System.Drawing.Size(155, 113);
            this.surfaceVIewer1.TabIndex = 0;
            // 
            // plot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.surfaceVIewer1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "plot";
            this.Size = new System.Drawing.Size(536, 224);
            this.Load += new System.EventHandler(this.plot_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plot_MouseMove);
            this.ResumeLayout(false);
        }

        private Magn3D_Prof.SurfaceVIewer surfaceVIewer1;

        #endregion
    }
}
