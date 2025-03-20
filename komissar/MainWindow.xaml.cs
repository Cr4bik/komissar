using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel.Design;

namespace komissar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, List<string>> carData = new Dictionary<string, List<string>>()
        {
            { "Toyota", new List<string>() { "Camry", "RAV4", "Corolla", "Highlander" } },
            { "Honda", new List<string>() { "Civic", "CRV", "Accord", "Pilot" } },
            { "Ford", new List<string>() { "Focus", "Mustang", "Explorer", "F-150" } },
            { "Chevrolet", new List<string>() { "Silverado", "Camaro", "Equinox", "Malibu" } },
            { "BMW", new List<string>() { "3 Series", "5 Series", "X5", "X7" } },
            { "Mercedes-Benz", new List<string>() { "C-Class", "E-Class", "GLE", "GLS" } },
            { "Audi", new List<string>() { "A4", "A6", "Q5", "Q7" } },
            { "Volkswagen", new List<string>() { "Golf", "Jetta", "Tiguan", "Atlas" } },
            { "Nissan", new List<string>() { "Altima", "Rogue", "Titan", "Sentra" } },
            { "Hyundai", new List<string>() { "Sonata", "Tucson", "Santa Fe", "Elantra" } },
            { "Kia", new List<string>() { "Optima", "Sportage", "Sorento", "Telluride" } },
            { "Subaru", new List<string>() { "Outback", "Forester", "Legacy", "Impreza" } },
            { "Mazda", new List<string>() { "Mazda3", "CX-5", "CX-9", "Mazda6" } },
            { "Jeep", new List<string>() { "Grand Cherokee", "Wrangler", "Compass", "Cherokee" } },
            { "Tesla", new List<string>() { "Model 3", "Model S", "Model X", "Model Y" } }
        };
        public MainWindow()
        {
            InitializeComponent();
            cbBrand1.ItemsSource = carData.Keys;
            cbBrand2.ItemsSource = carData.Keys;
        }
        private void cbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedBrand1 = cbBrand1.SelectedItem as string;
            string selectedBrand2 = cbBrand2.SelectedItem as string;

            if (selectedBrand1 != null)
            {
                List<string> models = carData[selectedBrand1];

                cbModel1.ItemsSource = models;
                cbModel1.IsEnabled = true;
            }
            else
            {
                cbModel1.ItemsSource = null;
                cbModel1.IsEnabled = false;
            }
            if (selectedBrand2 != null)
            {
                List<string> models = carData[selectedBrand2];

                cbModel2.ItemsSource = models;
                cbModel2.IsEnabled = true;
            }
            else
            {
                cbModel2.ItemsSource = null;
                cbModel2.IsEnabled = false;
            }
        }

        private void BtnAddData_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=server15.hosting.reg.ru;user=u2923335_Kolesni;password=fjdg664356DFDgfhhgh;database=u2923335_Kolesnichonko_Maksim;charset=utf8mb4;";
            string марка = cbBrand1.SelectedItem.ToString();
            string модель = cbModel1.SelectedItem.ToString();
            string vin = TbVIN1.Text;
            string госЗнак = TbSign1.Text;
            string свидетельство = TbReg1.Text;
            string собственник = TbOwner1.Text;
            string адресСобственника = TbOwnAddress1.Text;
            string водительскоеУдостоверение = TbDrvYd1.Text;
            string категория = CbCategory.SelectedItem.ToString();
            string датаВыдачиВУ = TbGotDate1.Text;
            string документНаПраво = TbDoc1.Text;
            string страховщик = TbStrax1.Text;
            string страховойПолис = TbStraxPol1.Text;
            string действителенДо = TbValidDate1.Text;
            string застрахованоОтУщерба = CbTF1.SelectedItem.ToString(); 
            string характерПовреждений = TbDiscription1.Text;
            string замечания = TbComment1.Text;
            string местоДТП = TbLocation.Text;
            string датаДТП = TbDate.Text;
            string свидетелиДТП = TbWatchers.Text; // объявление переменных
            if (cbBrand1.SelectedValue == null || string.IsNullOrWhiteSpace(cbBrand1.SelectedItem.ToString()))
            {
                MessageBox.Show("Поле \"Марка\" должно быть выбрано.");
                return;
            }
            if (cbModel1.SelectedValue == null || string.IsNullOrWhiteSpace(cbModel1.SelectedItem.ToString()))
            {
                MessageBox.Show("Поле \"Модель\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(vin))
            {
                MessageBox.Show("Поле \"VIN\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(госЗнак))
            {
                MessageBox.Show("Поле \"Гос. регистрационный знак ТС\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(свидетельство))  //проверка ну пустые поля
            {
                MessageBox.Show("Поле \"Свидетельство о регистрации ТС\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(собственник))
            {
                MessageBox.Show("Поле \"ФИО собственника\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(адресСобственника))
            {
                MessageBox.Show("Поле \"Адрес собственника\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(водительскоеУдостоверение))
            {
                MessageBox.Show("Поле \"Водительское удостоверение\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(датаВыдачиВУ))
            {
                MessageBox.Show("Поле \"Дата выдачи водительского удостоверения\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(документНаПраво))
            {
                MessageBox.Show("Поле \"Документ на право владения, пользования, распоряжения ТС\" не может быть пустым.");
                return;
            }
            if (CbCategory.SelectedValue == null)
            { 
                MessageBox.Show("Поле \"Категория\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(страховщик))
            {
                MessageBox.Show("Поле \"Страховщик\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(страховойПолис))
            {
                MessageBox.Show("Поле \"Страховой полис\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(действителенДо))
            {
                MessageBox.Show("Поле \"Действителен до\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(характерПовреждений))
            {
                MessageBox.Show("Поле \"Характер и перечень видимых поврежденных деталей и элементов\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(замечания))
            {
                MessageBox.Show("Поле \"Замечания\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(застрахованоОтУщерба))
            {
                MessageBox.Show("Поле \"Застраховано от ущерба\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(местоДТП))
            {
                MessageBox.Show("Поле \"Место ДТП\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(датаДТП))
            {
                MessageBox.Show("Поле \"Дата ДТП\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(свидетелиДТП))
            {
                MessageBox.Show("Поле \"Свидетели ДТП\" не может быть пустым.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    string queryТС = @"INSERT INTO ТС (Модель, Марка, VIN, Гос_рег_знак, Свид_о_рег, Собственник, Адрес_собс, Вод_удостов, Категория, Дата_Выдачи, Док_на_право_вл,
                страховщик, Страховой_полис, Действ_до, ТС_застраховано, Перечинь_вид_повр, Замечания)
                VALUES (@Модель, @Марка, @VIN, @Гос_рег_знак, @Свид_о_рег, @Собственник, @Адрес_собс, @Вод_удостов, @Категория, @Дата_выдачи, @Док_на_право_вл,
                @страховщик, @Страховой_полис, @Действ_до, @ТС_застраховано, @Перечинь_вид_повр, @Замечания);";
                    connection.Open();
                    using (MySqlCommand commandТС = new MySqlCommand(queryТС, connection))
                    {
                        commandТС.Parameters.AddWithValue("@Модель", модель);
                        commandТС.Parameters.AddWithValue("@Марка", марка);
                        commandТС.Parameters.AddWithValue("@VIN", vin);
                        commandТС.Parameters.AddWithValue("@Гос_рег_знак", госЗнак);
                        commandТС.Parameters.AddWithValue("@Свид_о_рег", свидетельство);
                        commandТС.Parameters.AddWithValue("@Собственник", собственник);
                        commandТС.Parameters.AddWithValue("@Адрес_собс", адресСобственника);
                        commandТС.Parameters.AddWithValue("@Вод_удостов", водительскоеУдостоверение);
                        commandТС.Parameters.AddWithValue("@Категория", категория);
                        commandТС.Parameters.AddWithValue("@Дата_выдачи", датаВыдачиВУ);
                        commandТС.Parameters.AddWithValue("@Док_на_право_вл", документНаПраво);
                        commandТС.Parameters.AddWithValue("@страховщик", страховщик);
                        commandТС.Parameters.AddWithValue("@Страховой_полис", страховойПолис);
                        commandТС.Parameters.AddWithValue("@Действ_до", действителенДо);
                        commandТС.Parameters.AddWithValue("@ТС_застраховано", застрахованоОтУщерба);
                        commandТС.Parameters.AddWithValue("@Перечинь_вид_повр", характерПовреждений);
                        commandТС.Parameters.AddWithValue("@Замечания", замечания);
                        // ...  добавить параметры для остальных полей ...

                        int rowsAffected = commandТС.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно сохранены!");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при сохранении данных.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }
    }
}
