using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Magn3D_Prof
{
    public partial class Global : Form
    {
        public static GRD Relief;

        public static List<GRD> HeigthMaps = new List<GRD>();

        public static List<GRD> MeasuredField = new List<GRD>();
        
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

                HeigthMaps = new List<GRD>();
                MeasuredField = new List<GRD>();

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
            else if(relName != "\r\n  ")
                MessageBox.Show(relName + " не найден", "Ошибка!");

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

                GRD field = GRD.ReadGRD(FielNames[i]);

                if (field.GetxMax() < Relief.GetxMax() || field.GetyMax() < Relief.GetyMax() || field.GetxMin() > Relief.GetxMin() || field.GetyMin() > Relief.GetyMin())
                {
                    MessageBox.Show("файл " + FielNames[i] + " имеет неверный размер", "Невозможно загрузить сетку");
                    continue;
                }
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
                    HeigthMaps.Add(new GRD(Relief.GetxMin(), Relief.GetxMax(), Relief.GetyMin(), Relief.GetyMax(), SettingsForm.stepX, SettingsForm.stepY, 0));
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
                    X = double.Parse(reader.GetAttribute("X"));
                    Y = double.Parse(reader.GetAttribute("Y"));
                    b = double.Parse(reader.GetAttribute("b"));
                    d = double.Parse(reader.GetAttribute("d"));
                    L = double.Parse(reader.GetAttribute("L"));
                    h1 = double.Parse(reader.GetAttribute("h1"));
                    h2 = double.Parse(reader.GetAttribute("h2"));
                    h3 = double.Parse(reader.GetAttribute("h3"));
                    fi = double.Parse(reader.GetAttribute("fi"));
                    alpha = double.Parse(reader.GetAttribute("alpha"));
                    beta = double.Parse(reader.GetAttribute("beta"));
                    kappa = double.Parse(reader.GetAttribute("kappa"));
                    I = double.Parse(reader.GetAttribute("I"));
                    D = double.Parse(reader.GetAttribute("D"));
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
                double X0, Y0, X1, Y1, p, H1, H2;
                string name;
                if (reader.IsStartElement("profile"))
                {
                    X0 = double.Parse(reader.GetAttribute("X0"));
                    Y0 = double.Parse(reader.GetAttribute("Y0"));

                    X1 = double.Parse(reader.GetAttribute("X1"));
                    Y1 = double.Parse(reader.GetAttribute("Y1"));

                    p = double.Parse(reader.GetAttribute("PointsDensity"));
                    
                    H1 = double.Parse(reader.GetAttribute("H1"));
                    H2 = double.Parse(reader.GetAttribute("H2"));

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

            SettingsForm.maxX = Relief.GetxMax();
            SettingsForm.maxY = Relief.GetyMax();

            SettingsForm.minX = Relief.GetxMin();
            SettingsForm.minY = Relief.GetyMin();

            foreach (var prof in Profiles)
                prof.UpdateNumerics(this, new EventArgs());

            filename = ProjectPath + filename.Substring(filename.LastIndexOf("\\"));

            if(!File.Exists(filename))
                Relief.SaveGRD(filename);

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
                attribute.Value = body.X.ToString().Replace(".", ",");
                
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("Y");
                attribute.Value = body.Y.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("b");
                attribute.Value = body.b.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("d");
                attribute.Value = body.d.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("L");
                attribute.Value = body.L.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("h1");
                attribute.Value = body.h1.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("h2");
                attribute.Value = body.h2.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("h3");
                attribute.Value = body.h3.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("fi");
                attribute.Value = body.alpha.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("alpha");
                attribute.Value = body.alpha.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("beta");
                attribute.Value = body.fi.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("kappa");
                attribute.Value = body.kappa.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("I");
                attribute.Value = body.I.ToString().Replace(".", ",");

                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("D");
                attribute.Value = body.D.ToString().Replace(".", ",");

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
                attribute.Value = profile.Point0X.GetValue().ToString().Replace(".", ",");
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("Y0");
                attribute.Value = profile.Point0Y.GetValue().ToString().Replace(".", ",");
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("X1");
                attribute.Value = profile.Point1X.GetValue().ToString().Replace(".", ",");
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("Y1");
                attribute.Value = profile.Point1Y.GetValue().ToString().Replace(".", ",");
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("PointsDensity");
                attribute.Value = profile.PointsCount.GetValue().ToString().Replace(".", ",");
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("H1");
                attribute.Value = profile.Hi1.GetValue().ToString().Replace(".", ",");
                node.Attributes.Append(attribute);

                attribute = XmlDoc.CreateAttribute("H2");
                attribute.Value = profile.Hi2.GetValue().ToString().Replace(".", ",");
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

            output += "Рельеф - " + reliefName + "\nСреднее значение = " + Relief.GetzMean().ToString("F2") + "\nСетки измеренных полей:\n";


            for(int i = 0; i < MeasFieldNames.Count; i++)
                output += i + " - " + MeasFieldNames[i] + "\nСреднее значение = " + MeasuredField[i].GetzMean().ToString("F2") + "\n";
            
            output += "Сетки высот точек наблюдения:\n";

            for (int i = 0; i < HeightNames.Count; i++)
                output += i + " - " + HeightNames[i] + "\nСреднее значение = " + HeigthMaps[i].GetzMean().ToString("F2") + "\n";

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

            MeasuredField = new List<GRD>();
            HeigthMaps = new List<GRD>();
            MeasFieldNames.Clear();
            HeightNames.Clear();

            string temp = "";

            foreach (var filename in openFileDialog2.FileNames)
            {
                string filenameH = filename.Insert(filename.IndexOf('.'), "H");

                
                GRD field = GRD.ReadGRD(filename);

                if (field.GetxMax() < Relief.GetxMax() || field.GetyMax() < Relief.GetyMax() || field.GetxMin() > Relief.GetxMin() || field.GetyMin() > Relief.GetyMin())
                {
                    MessageBox.Show("файл " + filename.Substring(filename.LastIndexOf("\\")+1) + " имеет неверный размер", "Невозможно загрузить сетку");
                    continue;
                }

                MeasuredField.Add(field);
                if(!File.Exists(ProjectPath + filename.Substring(filename.LastIndexOf("\\"))))
                    field.SaveGRD(ProjectPath + filename.Substring(filename.LastIndexOf("\\")));

                MeasFieldNames.Add(ProjectPath + filename.Substring(filename.LastIndexOf("\\")));

                temp += "\n" + filename.Substring(filename.LastIndexOf("\\") + 1);
                try
                {
                    HeigthMaps.Add(GRD.ReadGRD(filenameH));
                    temp += " - " + filenameH.Substring(filename.LastIndexOf("\\") + 1);
                    filenameH = ProjectPath + filenameH.Substring(filename.LastIndexOf("\\"));
                    HeightNames.Add(filenameH);
                    if(!File.Exists(ProjectPath + filenameH.Substring(filename.LastIndexOf("\\"))))
                        HeigthMaps.Last().SaveGRD(filenameH);
                }
                catch (Exception)
                {
                    HeigthMaps.Add(new GRD(Relief.GetxMin(), Relief.GetxMax(), Relief.GetyMin(), Relief.GetyMax(), SettingsForm.stepX, SettingsForm.stepY, 0));
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
        
        private void Global_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        { 
            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {

            return;
            if (e.KeyCode == Keys.CapsLock)
            {
                Lock = !Lock;

                for (int k = 0; k < Profiles[ProfPlace.SelectedIndex].Controls.Count; k++)
                    Profiles[ProfPlace.SelectedIndex].Controls[k].Enabled =
                       !Profiles[ProfPlace.SelectedIndex].Controls[k].Enabled;
            }
            if (!Lock) return;

            if (e.KeyCode == Keys.U)
            {
                UpEdit = !UpEdit;
                DownEdit = false;
                RightEdit = false;
                LeftEdit = false;
            }

            if (e.KeyCode == Keys.J)
            {
                DownEdit = !DownEdit;
                UpEdit = false;
                RightEdit = false;
                LeftEdit = false;
            }

            if (e.KeyCode == Keys.I)
            {
                RightEdit = !RightEdit;
                UpEdit = false;
                DownEdit = false;
                LeftEdit = false;
            }

            if (e.KeyCode == Keys.Y)
            {
                LeftEdit = !LeftEdit;
                UpEdit = false;
                DownEdit = false;
                RightEdit = false;
            }

            if (e.KeyCode == Keys.R)
            {
                LeftEdit = false;
                UpEdit = false;
                DownEdit = false;
                RightEdit = false;
            }

            int i = 0, j = 0;

            if (e.KeyCode == Keys.NumPad4) { i = -1; j = 0; }

            if (e.KeyCode == Keys.NumPad6) { i = 1; j = 0; }

            if (e.KeyCode == Keys.NumPad8) { i = 0; j = -1; }

            if (e.KeyCode == Keys.NumPad2) { i = 0; j = 1; }

            if (UpEdit) MoveUpperEdge(i, j);

            if (DownEdit) MoveLowerEdge(i, j);

            if (RightEdit) MoveRightEdge(i, j);

            if (LeftEdit) MoveLeftEdge(i, j);

            if (!UpEdit && !DownEdit && !RightEdit && !LeftEdit) MoveBody(i, j);

            Profiles[ProfPlace.SelectedIndex].Draw(new object(), new EventArgs());
        }

        private void MoveBody(int x, int y)
        {
            if (SelectedBodyIndex == -1) return;
            var body = bodies[SelectedBodyIndex];

            body.X += Math.Sign(x) * SettingsForm.incr;
            body.h1 += Math.Sign(y) * SettingsForm.incr;
            body.h2 += Math.Sign(y) * SettingsForm.incr;
            body.h3 += Math.Sign(y) * SettingsForm.incr;

            body.UpdateBody();
        }

        private void MoveUpperEdge(int x, int y)
        {
            if (SelectedBodyIndex == -1) return;
            var body = bodies[SelectedBodyIndex];

            var profile = Profiles[ProfPlace.SelectedIndex];
            
            var bias = Math.Sign(x) * SettingsForm.incr * profile.GetAxis()[0] +
                       Math.Sign(y) * SettingsForm.incr * new Vector3(0, 0, 1);


            body.Verticles[0] += bias;
            body.Verticles[1] += bias;
            body.Verticles[2] += bias;
            body.Verticles[3] += bias;

            body.UpdateParameters();
            body.UpdateBody();
        }

        private void MoveLowerEdge(int x, int y)
        {
            if (SelectedBodyIndex == -1) return;
            var body = bodies[SelectedBodyIndex];

            var profile = Profiles[ProfPlace.SelectedIndex];

            var bias = Math.Sign(x) * SettingsForm.incr * profile.GetAxis()[0] +
                       Math.Sign(y) * SettingsForm.incr * new Vector3(0, 0, 1);


            body.Verticles[0] += bias;
            body.Verticles[1] += bias;
            body.Verticles[2] += bias;
            body.Verticles[3] += bias;

            body.UpdateParameters();
            body.UpdateBody();

        }

        private void открытьВСерфереToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MoveRightEdge(int x, int y)
        {
            if (SelectedBodyIndex == -1) return;
            var body = bodies[SelectedBodyIndex];

            int[] i = new[] { 1, 3, 5, 7 };

            var profile = Profiles[ProfPlace.SelectedIndex];
            var alpha = profile.bodyControls[SelectedBodyIndex].alpha.GetValue();

            if (alpha < 0) alpha += 360;

            if (alpha <= 45 || alpha > 315)
                i =  new []{1,3,5,7 };
            if (alpha > 45 && alpha <= 135)
                i = new[] { 2, 3, 6, 7 };
            if (alpha > 135 && alpha <= 225)
                i = new[] { 0, 2, 4, 6 };
            if (alpha > 225 && alpha <= 315)
                i = new[] { 0, 1, 4, 5 };


            var bias = ((body.Verticles[i[1]] - body.Verticles[i[2]]) & (body.Verticles[i[0]] - body.Verticles[i[3]]))
                .Normalized();

            if (bias * Profiles[ProfPlace.SelectedIndex].GetAxis()[0] < 0)
                bias = bias * (-1);

            bias = bias * Math.Sign(x) * SettingsForm.incr + new Vector3(0, 0, Math.Sign(y) * SettingsForm.incr);

            body.Verticles[i[0]] += bias;
            body.Verticles[i[1]] += bias;
            body.Verticles[i[2]] += bias;
            body.Verticles[i[3]] += bias;
            
            body.UpdateParameters();
        }

        private void MoveLeftEdge(int x, int y)
        {
            if (SelectedBodyIndex == -1) return;
            var body = bodies[SelectedBodyIndex];

            int[] i = new[] { 1, 3, 5, 7 };

            var profile = Profiles[ProfPlace.SelectedIndex];
            var alpha = profile.bodyControls[SelectedBodyIndex].alpha.GetValue();

            if (alpha < 0) alpha += 360;

            if (alpha <= 45 || alpha > 315)
                i = new[] { 0, 2, 4, 6 };
            if (alpha > 45 && alpha <= 135)
                i = new[] { 2, 3, 6, 7 };
            if (alpha > 135 && alpha <= 225)
                i = new[] { 1, 3, 5, 7 };
            if (alpha > 225 && alpha <= 315)
                i = new[] { 0, 1, 4, 5 };

            var bias = ((body.Verticles[i[1]] - body.Verticles[i[2]]) & (body.Verticles[i[0]] - body.Verticles[i[3]]))
                .Normalized();

            if (bias * Profiles[ProfPlace.SelectedIndex].GetAxis()[0] < 0)
                bias = bias * (-1);

            bias = bias * Math.Sign(x) * SettingsForm.incr + new Vector3(0, 0, Math.Sign(y) * SettingsForm.incr);

            body.Verticles[i[0]] += bias;
            body.Verticles[i[1]] += bias;
            body.Verticles[i[2]] += bias;
            body.Verticles[i[3]] += bias;

            body.UpdateParameters();
        }

    }
}