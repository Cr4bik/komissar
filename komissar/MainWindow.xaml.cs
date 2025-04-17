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
using System.IO;

namespace komissar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Shape tempShape = null;
        private TextBlock tempText = null;
        private bool isDrawing = false;
        private UIElement selectedElement = null;
        private bool isDragging = false;
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
            string марка = cbBrand1.Text;
            string модель = cbModel1.Text;
            string vin = TbVIN1.Text;
            string госЗнак = TbSign1.Text;
            string свидетельство = TbReg1.Text;
            string собственник = TbOwner1.Text;
            string адресСобственника = TbOwnAddress1.Text;
            string водитель = TbDriver1.Text;
            string датарожвод = TbDrvDate1.Text;
            string адресвод = TbDrvAddress1.Text;
            string телефонвод = TbDrvPhone1.Text;
            string водительскоеУдостоверение = TbDrvYd1.Text;
            string категория = CbCategory.Text;
            string датаВыдачиВУ = TbGotDate1.Text;
            string документНаПраво = TbDoc1.Text;
            string страховщик = TbStrax1.Text;
            string страховойПолис = TbStraxPol1.Text;
            string действителенДо = TbValidDate1.Text;
            string застрахованоОтУщерба = CbTF1.Text;
            string характерПовреждений = TbDiscription1.Text;
            string замечания = TbComment1.Text;
            string марка1 = cbBrand2.Text;
            string модель1 = cbModel2.Text;
            string vin1 = TbVIN2.Text;
            string госЗнак1 = TbSign2.Text;
            string свидетельство1 = TbReg2.Text;
            string собственник1 = TbOwner2.Text;
            string адресСобственника1 = TbOwnAddress2.Text;
            string водитель1 = TbDriver2.Text;
            string датарожвод1 = TbDrvDate2.Text;
            string адресвод1 = TbDrvAddress2.Text;
            string телефонвод1 = TbDrvPhone2.Text;
            string водительскоеУдостоверение1 = TbDrvYd2.Text;
            string категория1 = CbCategory2.Text;
            string датаВыдачиВУ1 = TbGotDate2.Text;
            string документНаПраво1 = TbDoc2.Text;
            string страховщик1 = TbStrax2.Text;
            string страховойПолис1 = TbStraxPol2.Text;
            string действителенДо1 = TbValidDate2.Text;
            string застрахованоОтУщерба1 = CbTF2.Text;
            string характерПовреждений1 = TbDiscription2.Text;
            string замечания1 = TbComment2.Text;
            string местоДТП = TbLocation.Text;
            string датаДТП = TbDate.Text;
            string свидетелиДТП = TbWatchers.Text; // объявление переменных
            if (string.IsNullOrWhiteSpace(марка))
            {
                MessageBox.Show("Поле \"Марка\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(модель))
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
            if (string.IsNullOrWhiteSpace(водитель))
            {
                MessageBox.Show("Поле \"Водитель(ФИО)\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(датарожвод))
            {
                MessageBox.Show("Поле \"Дата рождения водителя\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(адресвод))
            {
                MessageBox.Show("Поле \"Адрес водителя\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(телефонвод))
            {
                MessageBox.Show("Поле \"Номер телефона водителя\" не может быть пустым.");
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

            if (string.IsNullOrWhiteSpace(марка1))
            {
                MessageBox.Show("Поле \"Марка\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(модель1))
            {
                MessageBox.Show("Поле \"Модель\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(vin1))
            {
                MessageBox.Show("Поле \"VIN\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(госЗнак1))
            {
                MessageBox.Show("Поле \"Гос. регистрационный знак ТС\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(свидетельство1))  //проверка ну пустые поля
            {
                MessageBox.Show("Поле \"Свидетельство о регистрации ТС\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(собственник1))
            {
                MessageBox.Show("Поле \"ФИО собственника\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(адресСобственника1))
            {
                MessageBox.Show("Поле \"Адрес собственника\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(водитель1))
            {
                MessageBox.Show("Поле \"Водитель(ФИО)\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(датарожвод1))
            {
                MessageBox.Show("Поле \"Дата рождения водителя\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(адресвод1))
            {
                MessageBox.Show("Поле \"Адрес водителя\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(телефонвод1))
            {
                MessageBox.Show("Поле \"Номер телефона водителя\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(водительскоеУдостоверение1))
            {
                MessageBox.Show("Поле \"Водительское удостоверение\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(датаВыдачиВУ1))
            {
                MessageBox.Show("Поле \"Дата выдачи водительского удостоверения\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(документНаПраво1))
            {
                MessageBox.Show("Поле \"Документ на право владения, пользования, распоряжения ТС\" не может быть пустым.");
                return;
            }
            if (CbCategory2.SelectedValue == null)
            {
                MessageBox.Show("Поле \"Категория\" должно быть выбрано.");
                return;
            }
            if (string.IsNullOrWhiteSpace(страховщик1))
            {
                MessageBox.Show("Поле \"Страховщик\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(страховойПолис1))
            {
                MessageBox.Show("Поле \"Страховой полис\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(действителенДо1))
            {
                MessageBox.Show("Поле \"Действителен до\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(характерПовреждений1))
            {
                MessageBox.Show("Поле \"Характер и перечень видимых поврежденных деталей и элементов\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(замечания1))
            {
                MessageBox.Show("Поле \"Замечания\" не может быть пустым.");
                return;
            }
            if (string.IsNullOrWhiteSpace(застрахованоОтУщерба1))
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
                    string queryТС = @"INSERT INTO ТС (Модель, Марка, VIN, Гос_рег_знак, Свид_о_рег, Собственник, Адрес_собс, Водитель, Дата_рож_вод, Адрес_вод, Телефон_вод, Вод_удостов, Категория, Дата_Выдачи, Док_на_право_вл,
                    страховщик, Страховой_полис, Действ_до, ТС_застраховано, Перечинь_вид_повр, Замечания)
                    VALUES (@Модель, @Марка, @VIN, @Гос_рег_знак, @Свид_о_рег, @Собственник, @Адрес_собс, @Водитель, @Дата_рож_вод, @Дата_рож_вод, @Телефон_вод, @Вод_удостов, @Категория, @Дата_выдачи, @Док_на_право_вл,
                     @страховщик, @Страховой_полис, @Действ_до, @ТС_застраховано, @Перечинь_вид_повр, @Замечания);";
                    string queryТС1 = @"INSERT INTO ТС (Модель, Марка, VIN, Гос_рег_знак, Свид_о_рег, Собственник, Адрес_собс, Водитель, Дата_рож_вод, Адрес_вод, Телефон_вод, Вод_удостов, Категория, Дата_Выдачи, Док_на_право_вл,
                    страховщик, Страховой_полис, Действ_до, ТС_застраховано, Перечинь_вид_повр, Замечания)
                    VALUES (@Модель, @Марка, @VIN, @Гос_рег_знак, @Свид_о_рег, @Собственник, @Адрес_собс, @Водитель, @Дата_рож_вод, @Дата_рож_вод, @Телефон_вод, @Вод_удостов, @Категория, @Дата_выдачи, @Док_на_право_вл,
                     @страховщик, @Страховой_полис, @Действ_до, @ТС_застраховано, @Перечинь_вид_повр, @Замечания);";
                    string queryДТП = @"INSERT INTO ДТП (Место_ДТП, Дата_ДТП, Свидетели_ДТП, ID_ТС_А, ID_ТС_B) 
                    VALUES (@Место_ДТП, @Дата_ДТП, @Свидетели_ДТП, @ID_ТС_А, @ID_ТС_B);";
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand commandТС = new MySqlCommand(queryТС, connection, transaction))
                            {
                                commandТС.Parameters.AddWithValue("@Модель", модель);
                                commandТС.Parameters.AddWithValue("@Марка", марка);
                                commandТС.Parameters.AddWithValue("@VIN", vin);
                                commandТС.Parameters.AddWithValue("@Гос_рег_знак", госЗнак);
                                commandТС.Parameters.AddWithValue("@Свид_о_рег", свидетельство);
                                commandТС.Parameters.AddWithValue("@Собственник", собственник);
                                commandТС.Parameters.AddWithValue("@Адрес_собс", адресСобственника);
                                commandТС.Parameters.AddWithValue("@Водитель", водитель);
                                commandТС.Parameters.AddWithValue("@Дата_рож_вод", датарожвод);
                                commandТС.Parameters.AddWithValue("@Адрес_вод", адресвод);
                                commandТС.Parameters.AddWithValue("@Телефон_вод", телефонвод);
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
                                commandТС.ExecuteNonQuery();
                            }
                            using (MySqlCommand commandТС = new MySqlCommand(queryТС1, connection, transaction))
                            {
                                commandТС.Parameters.AddWithValue("@Модель", модель1);
                                commandТС.Parameters.AddWithValue("@Марка", марка1);
                                commandТС.Parameters.AddWithValue("@VIN", vin1);
                                commandТС.Parameters.AddWithValue("@Гос_рег_знак", госЗнак1);
                                commandТС.Parameters.AddWithValue("@Свид_о_рег", свидетельство1);
                                commandТС.Parameters.AddWithValue("@Собственник", собственник1);
                                commandТС.Parameters.AddWithValue("@Адрес_собс", адресСобственника1);
                                commandТС.Parameters.AddWithValue("@Водитель", водитель1);
                                commandТС.Parameters.AddWithValue("@Дата_рож_вод", датарожвод1);
                                commandТС.Parameters.AddWithValue("@Адрес_вод", адресвод1);
                                commandТС.Parameters.AddWithValue("@Телефон_вод", телефонвод1);
                                commandТС.Parameters.AddWithValue("@Вод_удостов", водительскоеУдостоверение1);
                                commandТС.Parameters.AddWithValue("@Категория", категория1);
                                commandТС.Parameters.AddWithValue("@Дата_выдачи", датаВыдачиВУ1);
                                commandТС.Parameters.AddWithValue("@Док_на_право_вл", документНаПраво1);
                                commandТС.Parameters.AddWithValue("@страховщик", страховщик1);
                                commandТС.Parameters.AddWithValue("@Страховой_полис", страховойПолис1);
                                commandТС.Parameters.AddWithValue("@Действ_до", действителенДо1);
                                commandТС.Parameters.AddWithValue("@ТС_застраховано", застрахованоОтУщерба1);
                                commandТС.Parameters.AddWithValue("@Перечинь_вид_повр", характерПовреждений1);
                                commandТС.Parameters.AddWithValue("@Замечания", замечания1);
                                commandТС.ExecuteNonQuery();
                            }
                            using (MySqlCommand cmdGetUserId = new MySqlCommand("SELECT LAST_INSERT_ID();", connection, transaction))
                            {
                                int userId = Convert.ToInt32(cmdGetUserId.ExecuteScalar());
                                using (MySqlCommand cmdGetUser = new MySqlCommand("SELECT LAST_INSERT_ID() - 1;", connection, transaction))
                                {
                                    int userId1 = Convert.ToInt32(cmdGetUser.ExecuteScalar());
                                    using (MySqlCommand commandДТП = new MySqlCommand(queryДТП, connection, transaction))
                                    {
                                        commandДТП.Parameters.AddWithValue("@Место_ДТП", местоДТП);
                                        commandДТП.Parameters.AddWithValue("@Дата_ДТП", датаДТП);
                                        commandДТП.Parameters.AddWithValue("@Свидетели_ДТП", свидетелиДТП);
                                        commandДТП.Parameters.AddWithValue("@ID_ТС_А", userId1);
                                        commandДТП.Parameters.AddWithValue("@ID_ТС_B", userId);
                                        commandДТП.ExecuteNonQuery();
                                    }
                                }

                            }


                            transaction.Commit(); // Подтверждение транзакции
                            MessageBox.Show("Данные успешно добавлены!");
                            ReportWindow reportWindow = new ReportWindow();
                            reportWindow.Show();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Отмена транзакции при ошибке
                            MessageBox.Show("Ошибка: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(DrawingCanvas);
            string shape = ((ComboBoxItem)ShapeSelector.SelectedItem)?.Content.ToString();

            if (shape == "Перемещение")
            {
                selectedElement = e.Source as UIElement;
                if (selectedElement != null && DrawingCanvas.Children.Contains(selectedElement))
                {
                    isDragging = true;
                    DrawingCanvas.CaptureMouse();
                }
                return;
            }

            isDrawing = true;

            if (shape == "Линия")
            {
                tempShape = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = startPoint.X,
                    Y2 = startPoint.Y
                };
                DrawingCanvas.Children.Add(tempShape);
            }
            else if (shape == "Прямоугольник")
            {
                tempShape = new Rectangle
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                Canvas.SetLeft(tempShape, startPoint.X);
                Canvas.SetTop(tempShape, startPoint.Y);
                DrawingCanvas.Children.Add(tempShape);
            }
            else if (shape == "Стрелка")
            {
                tempShape = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = startPoint.X,
                    Y2 = startPoint.Y,
                    StrokeEndLineCap = PenLineCap.Triangle
                };
                DrawingCanvas.Children.Add(tempShape);
            }
            else if (shape == "Буква A" || shape == "Буква B")
            {
                tempText = new TextBlock
                {
                    Text = shape == "Буква A" ? "A" : "B",
                    FontSize = 24,
                    Foreground = Brushes.Black
                };
                Canvas.SetLeft(tempText, startPoint.X);
                Canvas.SetTop(tempText, startPoint.Y);
                DrawingCanvas.Children.Add(tempText);
                isDrawing = false;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(DrawingCanvas);
            string shape = ((ComboBoxItem)ShapeSelector.SelectedItem)?.Content.ToString();

            if (shape == "Перемещение" && isDragging && selectedElement != null)
            {
                double offsetX = pos.X - startPoint.X;
                double offsetY = pos.Y - startPoint.Y;
                startPoint = pos;

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    RotateTransform rotate = selectedElement.RenderTransform as RotateTransform;
                    if (rotate == null)
                    {
                        rotate = new RotateTransform(0);
                        selectedElement.RenderTransform = rotate;
                        selectedElement.RenderTransformOrigin = new Point(0.5, 0.5);
                    }
                    rotate.Angle += 5;
                }
                else
                {
                    double left = Canvas.GetLeft(selectedElement);
                    double top = Canvas.GetTop(selectedElement);

                    if (double.IsNaN(left)) left = 0;
                    if (double.IsNaN(top)) top = 0;

                    Canvas.SetLeft(selectedElement, left + offsetX);
                    Canvas.SetTop(selectedElement, top + offsetY);
                }
                return;
            }

            if (!isDrawing || tempShape == null) return;

            if (tempShape is Line line)
            {
                line.X2 = pos.X;
                line.Y2 = pos.Y;
            }
            else if (tempShape is Rectangle rect)
            {
                double x = Math.Min(pos.X, startPoint.X);
                double y = Math.Min(pos.Y, startPoint.Y);
                double w = Math.Abs(pos.X - startPoint.X);
                double h = Math.Abs(pos.Y - startPoint.Y);
                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                rect.Width = w;
                rect.Height = h;
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            tempShape = null;
            tempText = null;

            if (isDragging)
            {
                isDragging = false;
                DrawingCanvas.ReleaseMouseCapture();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && selectedElement != null)
            {
                DrawingCanvas.Children.Remove(selectedElement);
                selectedElement = null;
            }
        }

        private void SaveCanvasAsJpg(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)DrawingCanvas.ActualWidth, (int)DrawingCanvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            rtb.Render(DrawingCanvas);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DTP.jpg");
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                encoder.Save(fs);
            }

            MessageBox.Show("Схема сохранена!");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sxema sxema = new sxema();
            sxema.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sxema1 sxema1 = new sxema1();
            sxema1.Show();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow();
            reportWindow.Show();
        }
    }
}
