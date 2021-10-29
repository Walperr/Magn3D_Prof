
namespace Magn3D_Prof.Main
{
    partial class Numeric
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Text
            // 
            this.Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text.Location = new System.Drawing.Point(0, 0);
            this.Text.Name = "Text";
            this.Text.ShortcutsEnabled = false;
            this.Text.Size = new System.Drawing.Size(152, 22);
            this.Text.TabIndex = 0;
            this.Text.Text = "0";
            this.Text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_KeyPress);
            this.Text.Leave += new System.EventHandler(this.Text_Leave);
            this.Text.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Text_PreviewKeyDown);
            // 
            // Numeric
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Text);
            this.Name = "Numeric";
            this.Size = new System.Drawing.Size(152, 21);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_KeyPress);
            this.Leave += new System.EventHandler(this.Text_Leave);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Text_PreviewKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private new System.Windows.Forms.TextBox Text;
    }
}
