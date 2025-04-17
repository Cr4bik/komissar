using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace komissar
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        string connectionString = "server=server15.hosting.reg.ru;user=u2923335_Kolesni;password=fjdg664356DFDgfhhgh;database=u2923335_Kolesnichonko_Maksim;charset=utf8mb4;";
        public ReportWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var dtpCmd = new MySqlCommand("SELECT * FROM ДТП ORDER BY ID DESC LIMIT 1", connection);
                    var reader = dtpCmd.ExecuteReader();
                    if (!reader.Read()) return;

                    int id_tc_a = Convert.ToInt32(reader["ID_ТС_А"]);
                    int id_tc_b = Convert.ToInt32(reader["ID_ТС_B"]);

                    PlaceText.Text = reader["Место_ДТП"].ToString();
                    DateText.Text = reader["Дата_ДТП"].ToString();
                    WitnessesText.Text = reader["Свидетели_ДТП"].ToString();
                    reader.Close();

                    // ТС A
                    var tcACmd = new MySqlCommand($"SELECT * FROM ТС WHERE ID = {id_tc_a}", connection);
                    reader = tcACmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string cat = reader["Категория"].ToString();
                        string imgA = cat == "C" ? "truck-schema.jpg"
                                     : cat == "B" ? "car-schema.jpg"
                                     : cat == "A" ? "moto-schema.jpg" : null;

                        string info =
        $"Марка: {reader["Марка"]}\nМодель: {reader["Модель"]}\nVIN: {reader["VIN"]}\nГос. знак: {reader["Гос_рег_знак"]}\nСвид. о регистрации: {reader["Свид_о_рег"]}\nСобственник: {reader["Собственник"]}\nАдрес: {reader["Адрес_собс"]}\nВодитель: {reader["Водитель"]}\nТел.: {reader["Телефон_вод"]}\nАдрес вод.: {reader["Адрес_вод"]}\nВУ: {reader["Вод_удостов"]}\nКатегория: {cat}\nСтраховщик: {reader["страховщик"]}\nПолис: {reader["Страховой_полис"]}";
                        VehicleAInfo.Text = info;
                        DamageA.Text = reader["Перечинь_вид_повр"].ToString();

                        if (imgA != null)
                            LoadImage(VehicleASchema, System.IO.Path.Combine("SavedSchemas", imgA));
                    }
                    reader.Close();

                    // ТС B
                    var tcBCmd = new MySqlCommand($"SELECT * FROM ТС WHERE ID = {id_tc_b}", connection);
                    reader = tcBCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string cat = reader["Категория"].ToString();
                        string imgB = cat == "C" ? "truck-schema1.jpg"
                                     : cat == "B" ? "car-schema1.jpg"
                                     : cat == "A" ? "moto-schema1.jpg" : null;

                        string info =
        $"Марка: {reader["Марка"]}\nМодель: {reader["Модель"]}\nVIN: {reader["VIN"]}\nГос. знак: {reader["Гос_рег_знак"]}\nСвид. о регистрации: {reader["Свид_о_рег"]}\nСобственник: {reader["Собственник"]}\nАдрес: {reader["Адрес_собс"]}\nВодитель: {reader["Водитель"]}\nТел.: {reader["Телефон_вод"]}\nАдрес вод.: {reader["Адрес_вод"]}\nВУ: {reader["Вод_удостов"]}\nКатегория: {cat}\nСтраховщик: {reader["страховщик"]}\nПолис: {reader["Страховой_полис"]}";
                        VehicleBInfo.Text = info;
                        DamageB.Text = reader["Перечинь_вид_повр"].ToString();
                        NotesText.Text = reader["Замечания"].ToString();

                        if (imgB != null)
                            LoadImage(VehicleBSchema, System.IO.Path.Combine("SavedSchemas", imgB));
                    }
                    reader.Close();

                    LoadImage(DTPImage, "DTP.jpg");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        private void LoadImage(System.Windows.Controls.Image imageControl, string fileName)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(path))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(path);
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
                imageControl.Source = bmp;
            }
        }

        private void SaveToPdf_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Формирование PDF в процессе реализации...");
        }
    }
}
