
namespace Magn3D_Prof
{
    partial class Global
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Global));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.выбратьПапкуПроектаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рассчитатьПолеНаПлоскостиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьВСерфереToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.отменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вернутьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загруженныеСеткиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjectButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.создатьПрофильToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьПрофильToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьПолеНаПрофильToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьНормальноеПолеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьРельефToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfPlace = new System.Windows.Forms.TabControl();
            this.menuProfile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.отображатьАномальноеПолеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отображатьПокомпонентноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectProjectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.OpenProfile = new System.Windows.Forms.OpenFileDialog();
            this.OpenProfileCoords = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.menuProfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripDropDownButton1, this.toolStripDropDownButton2, this.ProjectButton });
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(946, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.выбратьПапкуПроектаToolStripMenuItem, this.сохранитьToolStripMenuItem, this.настройкиToolStripMenuItem, this.рассчитатьПолеНаПлоскостиToolStripMenuItem, this.открытьВСерфереToolStripMenuItem, this.выходToolStripMenuItem });
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(40, 20);
            this.toolStripDropDownButton1.Text = "Файл";
            // 
            // выбратьПапкуПроектаToolStripMenuItem
            // 
            this.выбратьПапкуПроектаToolStripMenuItem.Name = "выбратьПапкуПроектаToolStripMenuItem";
            this.выбратьПапкуПроектаToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.выбратьПапкуПроектаToolStripMenuItem.Text = "Выбрать папку проекта";
            this.выбратьПапкуПроектаToolStripMenuItem.Click += new System.EventHandler(this.выбратьПапкуПроектаToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Enabled = false;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Enabled = false;
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // рассчитатьПолеНаПлоскостиToolStripMenuItem
            // 
            this.рассчитатьПолеНаПлоскостиToolStripMenuItem.Enabled = false;
            this.рассчитатьПолеНаПлоскостиToolStripMenuItem.Name = "рассчитатьПолеНаПлоскостиToolStripMenuItem";
            this.рассчитатьПолеНаПлоскостиToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.рассчитатьПолеНаПлоскостиToolStripMenuItem.Text = "Рассчитать поле на плоскости";
            this.рассчитатьПолеНаПлоскостиToolStripMenuItem.Click += new System.EventHandler(this.рассчитатьПолеНаПлоскостиToolStripMenuItem_Click);
            // 
            // открытьВСерфереToolStripMenuItem
            // 
            this.открытьВСерфереToolStripMenuItem.Name = "открытьВСерфереToolStripMenuItem";
            this.открытьВСерфереToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.открытьВСерфереToolStripMenuItem.Text = "Открыть в серфере";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.отменитьToolStripMenuItem, this.вернутьToolStripMenuItem, this.загруженныеСеткиToolStripMenuItem });
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.ShowDropDownArrow = false;
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(51, 20);
            this.toolStripDropDownButton2.Text = "Правка";
            // 
            // отменитьToolStripMenuItem
            // 
            this.отменитьToolStripMenuItem.Enabled = false;
            this.отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            this.отменитьToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.отменитьToolStripMenuItem.Text = "Отменить";
            // 
            // вернутьToolStripMenuItem
            // 
            this.вернутьToolStripMenuItem.Enabled = false;
            this.вернутьToolStripMenuItem.Name = "вернутьToolStripMenuItem";
            this.вернутьToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.вернутьToolStripMenuItem.Text = "Вернуть";
            // 
            // загруженныеСеткиToolStripMenuItem
            // 
            this.загруженныеСеткиToolStripMenuItem.Name = "загруженныеСеткиToolStripMenuItem";
            this.загруженныеСеткиToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.загруженныеСеткиToolStripMenuItem.Text = "Загруженные сетки";
            this.загруженныеСеткиToolStripMenuItem.Click += new System.EventHandler(this.загруженныеСеткиToolStripMenuItem_Click);
            // 
            // ProjectButton
            // 
            this.ProjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ProjectButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.создатьПрофильToolStripMenuItem, this.загрузитьПрофильToolStripMenuItem, this.загрузитьПолеНаПрофильToolStripMenuItem, this.обновитьНормальноеПолеToolStripMenuItem, this.загрузитьРельефToolStripMenuItem, this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem });
            this.ProjectButton.Enabled = false;
            this.ProjectButton.Image = ((System.Drawing.Image)(resources.GetObject("ProjectButton.Image")));
            this.ProjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProjectButton.Name = "ProjectButton";
            this.ProjectButton.ShowDropDownArrow = false;
            this.ProjectButton.Size = new System.Drawing.Size(51, 20);
            this.ProjectButton.Text = "Проект";
            // 
            // создатьПрофильToolStripMenuItem
            // 
            this.создатьПрофильToolStripMenuItem.Name = "создатьПрофильToolStripMenuItem";
            this.создатьПрофильToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.создатьПрофильToolStripMenuItem.Text = "Создать профиль";
            this.создатьПрофильToolStripMenuItem.Click += new System.EventHandler(this.создатьПрофильToolStripMenuItem_Click);
            // 
            // загрузитьПрофильToolStripMenuItem
            // 
            this.загрузитьПрофильToolStripMenuItem.Name = "загрузитьПрофильToolStripMenuItem";
            this.загрузитьПрофильToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.загрузитьПрофильToolStripMenuItem.Text = "Загрузить профиль";
            this.загрузитьПрофильToolStripMenuItem.Click += new System.EventHandler(this.загрузитьПрофильToolStripMenuItem_Click);
            // 
            // загрузитьПолеНаПрофильToolStripMenuItem
            // 
            this.загрузитьПолеНаПрофильToolStripMenuItem.Name = "загрузитьПолеНаПрофильToolStripMenuItem";
            this.загрузитьПолеНаПрофильToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.загрузитьПолеНаПрофильToolStripMenuItem.Text = "Загрузить поле на профиль";
            this.загрузитьПолеНаПрофильToolStripMenuItem.Click += new System.EventHandler(this.загрузитьПолеНаПрофильToolStripMenuItem_Click);
            // 
            // обновитьНормальноеПолеToolStripMenuItem
            // 
            this.обновитьНормальноеПолеToolStripMenuItem.Name = "обновитьНормальноеПолеToolStripMenuItem";
            this.обновитьНормальноеПолеToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.обновитьНормальноеПолеToolStripMenuItem.Text = "Обновить нормальное поле";
            this.обновитьНормальноеПолеToolStripMenuItem.Click += new System.EventHandler(this.обновитьНормальноеПолеToolStripMenuItem_Click);
            // 
            // загрузитьРельефToolStripMenuItem
            // 
            this.загрузитьРельефToolStripMenuItem.Name = "загрузитьРельефToolStripMenuItem";
            this.загрузитьРельефToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.загрузитьРельефToolStripMenuItem.Text = "Загрузить рельеф";
            this.загрузитьРельефToolStripMenuItem.Click += new System.EventHandler(this.загрузитьРельефToolStripMenuItem_Click);
            // 
            // загрузитьСеткуИзмеренныхПолейToolStripMenuItem
            // 
            this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem.Name = "загрузитьСеткуИзмеренныхПолейToolStripMenuItem";
            this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem.Text = "Загрузить измеренные поля и высоты";
            this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem.Click += new System.EventHandler(this.загрузитьСеткуИзмеренныхПолейToolStripMenuItem_Click);
            // 
            // ProfPlace
            // 
            this.ProfPlace.ContextMenuStrip = this.menuProfile;
            this.ProfPlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProfPlace.Location = new System.Drawing.Point(0, 22);
            this.ProfPlace.Margin = new System.Windows.Forms.Padding(2);
            this.ProfPlace.Name = "ProfPlace";
            this.ProfPlace.SelectedIndex = 0;
            this.ProfPlace.Size = new System.Drawing.Size(946, 525);
            this.ProfPlace.TabIndex = 2;
            // 
            // menuProfile
            // 
            this.menuProfile.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuProfile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.отображатьАномальноеПолеToolStripMenuItem, this.отображатьПокомпонентноToolStripMenuItem, this.удалитьToolStripMenuItem });
            this.menuProfile.Name = "contextMenuStrip1";
            this.menuProfile.Size = new System.Drawing.Size(262, 70);
            this.menuProfile.Opening += new System.ComponentModel.CancelEventHandler(this.MenuProfile_Opening);
            // 
            // отображатьАномальноеПолеToolStripMenuItem
            // 
            this.отображатьАномальноеПолеToolStripMenuItem.Enabled = false;
            this.отображатьАномальноеПолеToolStripMenuItem.Name = "отображатьАномальноеПолеToolStripMenuItem";
            this.отображатьАномальноеПолеToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.отображатьАномальноеПолеToolStripMenuItem.Text = "отображать аномальное поле";
            this.отображатьАномальноеПолеToolStripMenuItem.Click += new System.EventHandler(this.ОтображатьАномальноеПолеToolStripMenuItem_Click);
            // 
            // отображатьПокомпонентноToolStripMenuItem
            // 
            this.отображатьПокомпонентноToolStripMenuItem.Name = "отображатьПокомпонентноToolStripMenuItem";
            this.отображатьПокомпонентноToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.отображатьПокомпонентноToolStripMenuItem.Text = "отображать поле покомпонентно";
            this.отображатьПокомпонентноToolStripMenuItem.Click += new System.EventHandler(this.ОтображатьПокомпонентноToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.удалитьToolStripMenuItem.Text = "закрыть";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "GRID файлы|*.grd";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "GRID файлы|*.grd";
            this.openFileDialog2.Multiselect = true;
            // 
            // OpenProfile
            // 
            this.OpenProfile.Filter = "dat Файлы|*.dat";
            // 
            // OpenProfileCoords
            // 
            this.OpenProfileCoords.Filter = "bln Файлы|*.bln";
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            this.openFileDialog3.Filter = "|.";
            this.openFileDialog3.RestoreDirectory = true;
            // 
            // Global
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 547);
            this.Controls.Add(this.ProfPlace);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Global";
            this.Text = "Магнитное поле. 0.1.2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Global_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Global_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuProfile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem выбратьПапкуПроектаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem отменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вернутьToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton ProjectButton;
        private System.Windows.Forms.ToolStripMenuItem создатьПрофильToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьПрофильToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьПолеНаПрофильToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьНормальноеПолеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьРельефToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьСеткуИзмеренныхПолейToolStripMenuItem;
        private System.Windows.Forms.TabControl ProfPlace;
        private System.Windows.Forms.ContextMenuStrip menuProfile;
        private System.Windows.Forms.ToolStripMenuItem отображатьАномальноеПолеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отображатьПокомпонентноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog SelectProjectFolder;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.OpenFileDialog OpenProfile;
        private System.Windows.Forms.OpenFileDialog OpenProfileCoords;
        private System.Windows.Forms.ToolStripMenuItem загруженныеСеткиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рассчитатьПолеНаПлоскостиToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.ToolStripMenuItem открытьВСерфереToolStripMenuItem;
    }
}

