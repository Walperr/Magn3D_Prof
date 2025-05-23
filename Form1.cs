﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using GRIDs;
using Vectors;

namespace Magn3D_Prof
{
    public partial class Global : Form
    {
        public static IGrid Relief;

        public static List<IGrid> HeigthMaps = new List<IGrid>();

        public static List<IGrid> MeasuredField = new List<IGrid>();
        
        public static Vector3 T0 = new Vector3(3000,2000,700);

        public static double I,D;

        public static List<Prismbody> bodies = new List<Prismbody>();

        public static List<Profile> Profiles = new List<Profile>();

        public static SettingsForm Setti;

        public static string ProjectPath = "";

        public static bool ReliefLoaded = false, FieldHeigthLoad = false;

        public  static  bool Lock = false;
        public static bool UpEdit = false;
        public static bool DownEdit = false;
        public static bool RightEdit = false;
        public static bool LeftEdit = false;

        private static string reliefName = "";
        public static List<string> MeasFieldNames = new List<string>();
        public static List<string> HeightNames = new List<string>();

        public static int SelectedBodyIndex = -1;

        public Global()
        {
            InitializeComponent();
        }

        private void выбратьПапкуПроектаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            выбратьПапкуПроектаToolStripMenuItem.Enabled = false;
            ProjectButton.Enabled = true;
           //if (SelectProjectFolder.ShowDialog() == DialogResult.Cancel) return;

            FolderSelect.FolderSelectDialog folderSelect = new FolderSelect.FolderSelectDialog();

            if (File.Exists("path"))
            {

                StreamReader sr = new StreamReader("path");

                folderSelect.InitialDirectory = sr.ReadLine();
                sr.Close();
            }

            if (!folderSelect.ShowDialog()) return;

            ProjectPath = folderSelect.FileName;

            StreamWriter sw = new StreamWriter("path");
            sw.WriteLine(ProjectPath);

            sw.Close();

            if(File.Exists(ProjectPath+ "\\ProjectSettings.xml"))
            {
                SettingsForm.LoadSettings();
            }
            if(File.Exists(ProjectPath + "\\GridsInfo.xml"))
            {
                LoadGrids();
            }
            CreateGrids();
                    
            if (File.Exists(ProjectPath + "\\Bodies.xml"))
            {
                LoadBodies();
            }
            if(File.Exists(ProjectPath + "\\Profiles.xml"))
            {
                LoadProfiles();
            }

            UpdateT0();
            сохранитьToolStripMenuItem.Enabled = true;
            настройкиToolStripMenuItem.Enabled = true;
            рассчитатьПолеНаПлоскостиToolStripMenuItem.Enabled = true;
        }

        public void UpdateT0()
        {

            string filename = ProjectPath + "\\" + SettingsForm.T0name + ".dat";

            double T0x, T0y, T0z; // Компоненты вектора нормального поля

            try // Пытаемся
            {
                string s; int j; // Вспомогательные переменные

                // Открываем файл в папке проекта с именем Main_Field.dat
                using (StreamReader sr = new StreamReader(filename))
                {
                    sr.ReadLine(); // Пропустили строку
                    s = sr.ReadLine(); // Считали вторую строку
                }

                for (int i = 1; i <= 4; i++) // Заводим цикл на 4 итерации
                {
                    j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                    s = s.Remove(0, j + 1); // Удаляем значения, которые нас не интересуют
                }

                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                // Переводим строку в число и записываем значение по x
                T0x = double.Parse(s.Substring(0, j).Replace(".", ","), System.Globalization.NumberStyles.Any);
                s = s.Remove(0, j + 1); // Удаляем считанное значение

                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                // Переводим строку в число и записываем значение по y
                T0y = double.Parse(s.Substring(0, j).Replace(".", ","), System.Globalization.NumberStyles.Any);
                s = s.Remove(0, j + 1); // Удаляем считанное значение

                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                // Переводим строку в число и записываем значение по z
                T0z = double.Parse(s.Substring(0, j).Replace(".", ","), System.Globalization.NumberStyles.Any);
                s = s.Remove(0, j + 1); // Удаляем считанное значение
            }
            catch (Exception ex) // Если получили исключение
            {
                T0x = 17400.0d; // Устанавливаем стандартные значения поля
                T0y = 2424.0d;
                T0z = 56728.0d;
                // Выводим сообщение об ошибке
                MessageBox.Show(ex.Message + "Будут установлены значения (17400,2424,56728)", "Ошибка");
            }
            T0 = new Vector3(T0x, T0y, T0z); // Собираем вектор нормального поля

            if (T0.Getlength() == 0) // Если вектор нулевой
            {
                I = 0; // Устанавливаем склонение и наклонение равными нулю
                D = 0;
                return;
            }
            // Считаем склонение и наклонение поля в градусах
            I = Math.Asin(T0.Z / T0.Getlength()) * 180 / Math.PI;

            D = Math.Atan2(T0.Y, T0.X) * 180 / Math.PI;

            foreach(var prof in Profiles)
            {
                prof.UpdateT0vals();
            }
        }

        public static void CreateGrids()
        {
            if (ReliefLoaded && FieldHeigthLoad) return;

            if(!ReliefLoaded)
                Relief = new GRD(SettingsForm.minX,SettingsForm.maxX,SettingsForm.minY,SettingsForm.maxY,SettingsForm.stepX,SettingsForm.stepY,0);

            if (!FieldHeigthLoad)
            {

                HeigthMaps = new List<IGrid>();
                MeasuredField = new List<IGrid>();

                for (int i = 0; i < SettingsForm.Hcount; i++)
                {
                    HeigthMaps.Add(new GRD(SettingsForm.minX, SettingsForm.maxX, SettingsForm.minY, SettingsForm.maxY, SettingsForm.stepX, SettingsForm.stepY, SettingsForm.Hs[i]));
                    MeasuredField.Add(new GRD(SettingsForm.minX, SettingsForm.maxX, SettingsForm.minY, SettingsForm.maxY, SettingsForm.stepX, SettingsForm.stepY, 0));
                }
            }
            foreach(var prof in Profiles)
            {
                prof.RecalculatePoints();
            }
            foreach(var body in bodies)
            {
                body.UpdateFieldList();
            }
    }

        private void LoadGrids()
        {
            XmlTextReader reader = new XmlTextReader(ProjectPath + "\\GridsInfo.xml");
            
            string relName = "";
            List<string> FielNames = new List<string>();
            List<string> HeigNames = new List<string>();

            while (reader.Read())
            {
                if (reader.IsStartElement("relief"))
                {
                    relName = ProjectPath + reader.ReadString();
                    continue;
                }
                if (reader.IsStartElement("fields"))
                {
                    FielNames.Add(ProjectPath + reader.ReadString());
                    continue;
                }
                if (reader.IsStartElement("heights"))
                {
                    HeigNames.Add(ProjectPath + reader.ReadString());
                    continue;
                }
            }

            if (File.Exists(relName))
                LoadRelief(relName);
            else if (relName != "\r\n  ")
            {
                Relief = new GRD(SettingsForm.minX,SettingsForm.maxX,SettingsForm.minY,SettingsForm.maxY,SettingsForm.stepX,SettingsForm.stepY,0);
                MessageBox.Show(relName + " не найден", "Ошибка!");
            }

            MeasFieldNames.Clear();
            HeightNames.Clear();
            MeasuredField.Clear();
            HeigthMaps.Clear();

            for (int i = 0; i < HeigNames.Count; i++)
            {
                if (!File.Exists(FielNames[i]))
                {
                    MessageBox.Show(FielNames[i] +  " не найден", "Ошибка");
                    continue;
                }

                IGrid field = GRD.ReadGRD(FielNames[i]);

                // if (field.Xmax < Relief.Xmax || field.Ymax < Relief.Ymax || field.Xmin > Relief.Xmin || field.Ymin > Relief.Ymin)
                // {
                //     MessageBox.Show("файл " + FielNames[i] + " имеет неверный размер", "Невозможно загрузить сетку");
                //     continue;
                // }
                MeasuredField.Add(field);
                MeasFieldNames.Add(FielNames[i]);

                try
                {
                    HeigthMaps.Add(GRD.ReadGRD(HeigNames[i]));
                    HeightNames.Add(HeigNames[i]);
                }
                catch (Exception)
                {
                    MessageBox.Show(HeigNames[i] + " Невозможно загрузить", "Ошибка");
                    HeigthMaps.Add(new GRD(Relief.Xmin, Relief.Xmax, Relief.Ymin, Relief.Ymax, SettingsForm.stepX, SettingsForm.stepY, 0));
                    HeightNames.Add("");
                }
            }
            if(HeigthMaps.Count > 0)
                FieldHeigthLoad = true;

            reader.Close();
        }

        private void LoadBodies()
        {
            XmlTextReader reader = new XmlTextReader(ProjectPath + "\\Bodies.xml");

            while (reader.Read())
            {
                double X, Y, b, d, L, h1, h2, h3, fi, alpha, beta, kappa, I, D;
                bool hle;
                
                if (reader.IsStartElement("Body"))
                {
                    double.TryParse(reader.GetAttribute("X")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out X);

                    double.TryParse(reader.GetAttribute("Y")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out Y);

                    double.TryParse(reader.GetAttribute("b")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out b);

                    double.TryParse(reader.GetAttribute("d")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out d);

                    double.TryParse(reader.GetAttribute("L")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out L);

                    double.TryParse(reader.GetAttribute("h1")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out h1);

                    double.TryParse(reader.GetAttribute("h2")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out h2);

                    double.TryParse(reader.GetAttribute("h3")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out h3);

                    double.TryParse(reader.GetAttribute("fi")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out fi);

                    double.TryParse(reader.GetAttribute("alpha")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out alpha);

                    double.TryParse(reader.GetAttribute("beta")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out beta);

                    double.TryParse(reader.GetAttribute("kappa")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out kappa);

                    double.TryParse(reader.GetAttribute("I")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out I);

                    double.TryParse(reader.GetAttribute("D")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out D);
                    
                    hle = Convert.ToBoolean(reader.GetAttribute("hle"));

                    bodies.Add(new Prismbody(X, Y, b, d, L, h1, h2, h3, fi, alpha, beta, kappa, I, D, hle));
                }
            }
            reader.Close();
        }

        private void LoadProfiles()
        {
            XmlTextReader reader = new XmlTextReader(ProjectPath + "\\Profiles.xml");

            while (reader.Read())
            {
                string name;
                if (reader.IsStartElement("profile"))
                {
                    double.TryParse(reader.GetAttribute("X0")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var X0);
                    
                    double.TryParse(reader.GetAttribute("Y0")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var Y0);

                    double.TryParse(reader.GetAttribute("X1")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var X1);
                    
                    double.TryParse(reader.GetAttribute("Y1")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var Y1);
                    
                    double.TryParse(reader.GetAttribute("PointsDensity")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var p);
                    
                    double.TryParse(reader.GetAttribute("H1")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var H1);
                    
                    double.TryParse(reader.GetAttribute("H2")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var H2);

                    double.TryParse(reader.GetAttribute("FieldOffset")?.Replace(',', '.'), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var fieldOffset);

                    name = reader.ReadString();

                    ProfPlace.TabPages.Add(name); // Добавляем вкладку с именем

                    // Создаем элемент управления профиля
                    Profile element = new Profile(X0, X1, Y0, Y1)
                    {
                        Dock = DockStyle.Fill
                    };
                    ProfPlace.TabPages[ProfPlace.TabPages.Count - 1].Controls.Add(element); // Добавляем его в последнюю созданную вкладку

                    ProfPlace.SelectedIndex = ProfPlace.TabPages.Count - 1; // Делаем последнюю вкладку активной

                    element.PointsCount.SetValue(p);
                    element.Hi1.SetValue(H1);
                    element.Hi2.SetValue(H2);
                    element.numeric1.SetValue(fieldOffset);
                }
            }
            
            reader.Close();
        }

        private void создатьПрофильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfPlace.TabPages.Add("Profile " + ProfPlace.TabPages.Count); // Добавляем вкладку с именем "Profile [index]"

            // Создаем элемент управления профиля
            Profile element = new Profile
            {
                Dock = DockStyle.Fill // Заполняем всю вкладку
            };

            ProfPlace.TabPages[ProfPlace.TabPages.Count - 1].Controls.Add(element); // Добавляем его в последнюю созданную вкладку

            ProfPlace.SelectedIndex = ProfPlace.TabPages.Count - 1; // Делаем последнюю вкладку активной
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Выводим сообщение с предупреждением, если нажата кнопка да
            if (MessageBox.Show("Внимание текущий профиль будет закрыт \n продолжить?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var body in bodies)
                    body.RemoveProfileFields(ProfPlace.SelectedIndex);

                Profiles.RemoveAt(ProfPlace.SelectedIndex);

                ProfPlace.TabPages.RemoveAt(ProfPlace.SelectedIndex); // Удаляем текущую вкладку
            }
        }

        // Событие, вызываемое при нажатии на кнопку отображения аномального поля
        private void ОтображатьАномальноеПолеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем элемент управления текущего профиля из открытой вкладки
            Profile profile = ProfPlace.TabPages[ProfPlace.SelectedIndex].Controls[0] as Profile;

            profile.TurnToAnomalField(); // Переключили отображение на аномальное поле

            отображатьАномальноеПолеToolStripMenuItem.Enabled = false;
            отображатьПокомпонентноToolStripMenuItem.Enabled = true;
        }

        // Событие, вызываемое при нажатии на отображение компонент поля
        private void ОтображатьПокомпонентноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получаем элемент управления текущего профиля из открытой вкладки
            Profile profile = ProfPlace.TabPages[ProfPlace.SelectedIndex].Controls[0] as Profile;

            profile.TurnToModelField(); // Переключили отображение на компоненты поля

            отображатьАномальноеПолеToolStripMenuItem.Enabled = true;
            отображатьПокомпонентноToolStripMenuItem.Enabled = false;
        }
        // При открытии меню профиля блокируем кнопки аномального или покомпонентного отображения поля
        private void MenuProfile_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Получаем элемент управления текущего профиля из открытой вкладки
            Profile profile = ProfPlace.TabPages[ProfPlace.SelectedIndex].Controls[0] as Profile;

            if (profile.GetFieldMode())
            {
                отображатьАномальноеПолеToolStripMenuItem.Enabled = false;
                отображатьПокомпонентноToolStripMenuItem.Enabled = true;
            }
            else
            {
                отображатьАномальноеПолеToolStripMenuItem.Enabled = true;
                отображатьПокомпонентноToolStripMenuItem.Enabled = false;
            }
        }

        private void Global_Load(object sender, EventArgs e)
        {
            Setti = new SettingsForm();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setti.Show();
        }

        private void загрузитьРельефToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = ProjectPath;
                if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            LoadRelief(openFileDialog1.FileName);
        }

        private void LoadRelief(string filename)
        {
            Relief = GRD.ReadGRD(filename);

            SettingsForm.maxX = Relief.Xmax;
            SettingsForm.maxY = Relief.Ymax;

            SettingsForm.minX = Relief.Xmin;
            SettingsForm.minY = Relief.Ymin;

            foreach (var prof in Profiles)
                prof.UpdateNumerics(this, new EventArgs());

            filename = ProjectPath + filename.Substring(filename.LastIndexOf("\\"));

            if(!File.Exists(filename))
                ((GRD)Relief).SaveGRD(filename);

            //загрузитьСеткуИзмеренныхПолейToolStripMenuItem.Enabled = true;
            ReliefLoaded = true;
            reliefName = filename;
        }

        private void загрузитьПолеНаПрофильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProfile.InitialDirectory = ProjectPath;

            if (OpenProfile.ShowDialog() == DialogResult.Cancel) // Открываем диалоговое окно, если нажата кнопка отмены
                return; // Выходим

            // Получаем элемент управления профиля
            var profile_ = ProfPlace.TabPages[ProfPlace.SelectedIndex].Controls[0] as Profile;

            profile_.LoadField(OpenProfile.FileName); // Загружаем поле по указанному файлу в диалоговом окне
        }

        private void загрузитьПрофильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProfileCoords.InitialDirectory = ProjectPath;

            if (OpenProfileCoords.ShowDialog() != DialogResult.OK) return;

            string s; int j; // Вспомагательные переменные
            using (StreamReader sr = new StreamReader(OpenProfileCoords.FileName)) // Открываем файл filename
            {
                // Создаем новый путь к файлу с таким же названием, но в папке проекта
                string newpath = ProjectPath + "\\" + OpenProfileCoords.FileName.Remove(0, OpenProfileCoords.FileName.LastIndexOf("\\"));
                StreamWriter sw = new StreamWriter("j"); // Создаем пустой файл для записи
                if (!File.Exists(newpath)) // Если файл в проекте не существует
                {
                    sw.Close();
                    sw = new StreamWriter(newpath); // Создаем его
                    sw.WriteLine(sr.ReadLine()); // Переписываем первую строку
                }
                else sr.ReadLine();
                s = sr.ReadLine(); // Считаем строку
                sw.WriteLine(s);
                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)

                double P0X = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
                s = s.Remove(0, j + 1);
                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                double P0Y;
                if (j != -1)
                    P0Y = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
                else
                    P0Y = double.Parse(s.Replace('.', ','), System.Globalization.NumberStyles.Any);

                s = sr.ReadLine(); // Считаем строку
                sw.WriteLine(s);
                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)

                double P1X = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
                s = s.Remove(0, j + 1);
                j = s.IndexOf(','); // Получаем индекс разделителя (запятая)
                double P1Y;
                if (j != -1)
                    P1Y = double.Parse(s.Substring(0, j).Replace('.', ','), System.Globalization.NumberStyles.Any);
                else
                    P1Y = double.Parse(s.Replace('.', ','), System.Globalization.NumberStyles.Any);

                sr.Close(); // Закрываем файл чтения
                sw.Close();



                ProfPlace.TabPages.Add(OpenProfileCoords.FileName.Remove(0, OpenProfileCoords.FileName.LastIndexOf("\\"))); // Добавляем вкладку с именем

                // Создаем элемент управления профиля
                Profile element = new Profile(P0X, P1X, P0Y, P1Y)
                {
                    Dock = DockStyle.Fill
                };

                ProfPlace.TabPages[ProfPlace.TabPages.Count - 1].Controls.Add(element); // Добавляем его в последнюю созданную вкладку

                ProfPlace.SelectedIndex = ProfPlace.TabPages.Count - 1; // Делаем последнюю вкладку активной
            }
        }

        private void SaveGridsInfo()
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode root = xmlDoc.CreateElement("Grids");
            xmlDoc.AppendChild(root);
            XmlNode node;
            XmlAttribute attribute;

            node = xmlDoc.CreateElement("relief");
            node.InnerText = reliefName.Replace(ProjectPath,"");

            root.AppendChild(node);

            for (int i = 0; i < MeasFieldNames.Count; i++)
            {
                node = xmlDoc.CreateElement("fields");
                attribute = xmlDoc.CreateAttribute("index");
                attribute.Value = i.ToString();
                node.Attributes.Append(attribute);
                node.InnerText = MeasFieldNames[i].Replace(ProjectPath, "");
                root.AppendChild(node);

                node = xmlDoc.CreateElement("heights");
                attribute = xmlDoc.CreateAttribute("index");
                attribute.Value = i.ToString();
                node.Attributes.Append(attribute);
                node.InnerText = HeightNames[i].Replace(ProjectPath, "");
                root.AppendChild(node);
            }

            xmlDoc.Save(ProjectPath + "\\GridsInfo.xml");
        }

        private void SaveBodies()
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlNode root = XmlDoc.CreateElement("Bodies");
            XmlDoc.AppendChild(root);

            XmlNode node; XmlAttribute attribute;

            foreach(var body in bodies)
            {
                node = XmlDoc.CreateElement("Body");
                attribute = XmlDoc.CreateAttribute("X");
                attribute.Value = body.X.ToString(CultureInfo.InvariantCulture);
                
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("Y");
                attribute.Value = body.Y.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("b");
                attribute.Value = body.b.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("d");
                attribute.Value = body.d.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("L");
                attribute.Value = body.L.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("h1");
                attribute.Value = body.h1.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("h2");
                attribute.Value = body.h2.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("h3");
                attribute.Value = body.h3.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("fi");
                attribute.Value = body.alpha.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("alpha");
                attribute.Value = body.beta.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("beta");
                attribute.Value = body.fi.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("kappa");
                attribute.Value = body.kappa.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("I");
                attribute.Value = body.I.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("D");
                attribute.Value = body.D.ToString(CultureInfo.InvariantCulture);

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("hle");
                attribute.Value = body.hle.ToString();

                node.Attributes.Append(attribute);
                root.AppendChild(node);
            }

            XmlDoc.Save(ProjectPath + "\\Bodies.xml");
        }
        
        private void SaveProfiles()
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlNode root = XmlDoc.CreateElement("profiles");
            XmlDoc.AppendChild(root);

            XmlNode node; XmlAttribute attribute;

            foreach(var profile in Profiles)
            {
                node = XmlDoc.CreateElement("profile");
                attribute = XmlDoc.CreateAttribute("X0");
                attribute.Value = profile.Point0X.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("Y0");
                attribute.Value = profile.Point0Y.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("X1");
                attribute.Value = profile.Point1X.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("Y1");
                attribute.Value = profile.Point1Y.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("PointsDensity");
                attribute.Value = profile.PointsCount.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("H1");
                attribute.Value = profile.Hi1.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("H2");
                attribute.Value = profile.Hi2.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("FieldOffset");
                attribute.Value = profile.numeric1.GetValue().ToString(CultureInfo.InvariantCulture);
                node.Attributes.Append(attribute);

                node.InnerText = ProfPlace.TabPages[Profiles.IndexOf(profile)].Text;

                root.AppendChild(node);
            }
            XmlDoc.Save(ProjectPath + "\\Profiles.xml");
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGridsInfo();
            SaveBodies();
            SaveProfiles();
        }

        private void загруженныеСеткиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = "";

            output += "Рельеф - " + reliefName + "\nСреднее значение = " + Relief.Zmean.ToString("F2") + "\nСетки измеренных полей:\n";


            for(int i = 0; i < MeasFieldNames.Count; i++)
                output += i + " - " + MeasFieldNames[i] + "\nСреднее значение = " + MeasuredField[i].Zmean.ToString("F2") + "\n";
            
            output += "Сетки высот точек наблюдения:\n";

            for (int i = 0; i < HeightNames.Count; i++)
                output += i + " - " + HeightNames[i] + "\nСреднее значение = " + HeigthMaps[i].Zmean.ToString("F2") + "\n";

            MessageBox.Show(output, "Загруженные сетки");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Все несохраненные данные будут утеряны. \n Сохранить их?", "Внимание!", MessageBoxButtons.YesNoCancel);

            if (res == DialogResult.Cancel) return;

            if (res == DialogResult.Yes)
            {
                сохранитьToolStripMenuItem_Click(sender, e);
                Application.Exit();
            }
            if(res == DialogResult.No)
                Application.Exit();

        }

        private void рассчитатьПолеНаПлоскостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaneField pf = new PlaneField();

            pf.Show();
        }

        private void СерферToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string CmndTxt;
            CmndTxt = "/C \"D:/ Program Files / Golden Software / Surfer 15 / Scripter.exe\" -x \"C: \\Users\\vladb\\Desktop\\Scripts / Visualize_data_DV.bas\" C:\\Users\\vladb\\Desktop\\Pr/test1.csv";
            System.Diagnostics.Process.Start("CMD.exe", CmndTxt);
        }

        private void загрузитьСеткуИзмеренныхПолейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = ProjectPath;

            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;

            MeasuredField = new List<IGrid>();
            HeigthMaps = new List<IGrid>();
            MeasFieldNames.Clear();
            HeightNames.Clear();

            string temp = "";

            foreach (var filename in openFileDialog2.FileNames)
            {
                string filenameH = filename.Insert(filename.IndexOf('.'), "H");

                
                IGrid field = GRD.ReadGRD(filename);

                MeasuredField.Add(field);
                if(!File.Exists(ProjectPath + filename.Substring(filename.LastIndexOf("\\"))))
                    ((GRD)field).SaveGRD(ProjectPath + filename.Substring(filename.LastIndexOf("\\")));

                MeasFieldNames.Add(ProjectPath + filename.Substring(filename.LastIndexOf("\\")));

                temp += "\n" + filename.Substring(filename.LastIndexOf("\\") + 1);
                try
                {
                    HeigthMaps.Add(GRD.ReadGRD(filenameH));
                    temp += " - " + filenameH.Substring(filename.LastIndexOf("\\") + 1);
                    filenameH = ProjectPath + filenameH.Substring(filename.LastIndexOf("\\"));
                    HeightNames.Add(filenameH);
                    if(!File.Exists(ProjectPath + filenameH.Substring(filename.LastIndexOf("\\"))))
                        ((GRD)HeigthMaps.Last()).SaveGRD(filenameH);
                }
                catch (Exception)
                {
                    HeigthMaps.Add(new GRD(Relief.Xmin, Relief.Xmax, Relief.Ymin, Relief.Ymax, SettingsForm.stepX, SettingsForm.stepY, 0));
                    temp += " - " + "None";
                    HeightNames.Add("");
                }
            }


            MessageBox.Show(temp, "Загруженные файлы");

            if (HeigthMaps.Count > 0)
                FieldHeigthLoad = true;
            else
            {
                FieldHeigthLoad = false;
                CreateGrids();
            }
            foreach (var prof in Profiles)
                prof.UpdateNumerics(sender, e);

        }

        private void обновитьНормальноеПолеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateT0();
        }

        private void Global_KeyDown(object sender, KeyEventArgs e)
        {
            if(ProfPlace.SelectedIndex < 0) return;
             Profiles[ProfPlace.SelectedIndex]?.Profile_KeyDown(sender,e);
        }
    }
}