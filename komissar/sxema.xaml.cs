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
using System.IO;

namespace komissar
{
    /// <summary>
    /// Логика взаимодействия для sxema.xaml
    /// </summary>
    public partial class sxema : Window
    {
      
        public sxema()
        {
            InitializeComponent();
            InitializeComponent();
            CarContainerGrid.Visibility = Visibility.Visible;
            TruckContainerGrid.Visibility = Visibility.Collapsed;
            MotoContainerGrid.Visibility = Visibility.Collapsed;
        }

        private void CarDamageCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddDamageMark(CarDamageCanvas, e.GetPosition(CarDamageCanvas));
        }

        private void TruckDamageCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddDamageMark(TruckDamageCanvas, e.GetPosition(TruckDamageCanvas));
        }

        private void MotoDamageCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddDamageMark(MotoDamageCanvas, e.GetPosition(MotoDamageCanvas));
        }

        private void AddDamageMark(Canvas canvas, Point position)
        {
            var selectedItem = DamageTypeComboBox.SelectedItem as ComboBoxItem;
            var colorName = selectedItem?.Tag.ToString() ?? "Orange";

            var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(colorName);

            Ellipse ellipse = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = brush,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(ellipse, position.X - 10);
            Canvas.SetTop(ellipse, position.Y - 10);
            canvas.Children.Add(ellipse);
        }

        private void SaveVisualAsJpg(FrameworkElement visual, string filename)
        {
            int width = (int)visual.ActualWidth;
            int height = (int)visual.ActualHeight;

            if (width == 0 || height == 0)
            {
                MessageBox.Show($"Ошибка: элемент {filename} имеет нулевой размер.");
                return;
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(visual);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string savedSchemasDirectory = System.IO.Path.Combine(projectDirectory, "SavedSchemas");

            if (!Directory.Exists(savedSchemasDirectory))
            {
                Directory.CreateDirectory(savedSchemasDirectory);
            }

            string fullPath = System.IO.Path.Combine(savedSchemasDirectory, filename);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                encoder.Save(fs);
            }

            MessageBox.Show($"Схема сохранена в: {fullPath}");
        }

        private void SaveSchema_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = VehicleTypeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedText = selectedItem.Content.ToString();

                if (selectedText == "Легковой автомобиль")
                {
                    SaveVisualAsJpg(CarContainerGrid, "car-schema.jpg");
                }
                else if (selectedText == "Грузовой автомобиль")
                {
                    SaveVisualAsJpg(TruckContainerGrid, "truck-schema.jpg");
                }
                else if (selectedText == "Мотоцикл")
                {
                    SaveVisualAsJpg(MotoContainerGrid, "moto-schema.jpg");
                }
            }
        }

        private void VehicleTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ComboBoxItem)VehicleTypeComboBox.SelectedItem;
            string tag = selected.Tag.ToString();

            CarContainerGrid.Visibility = Visibility.Collapsed;
            TruckContainerGrid.Visibility = Visibility.Collapsed;
            MotoContainerGrid.Visibility = Visibility.Collapsed;

            if (tag == "car")
            {
                CarContainerGrid.Visibility = Visibility.Visible;
            }
            else if (tag == "truck")
            {
                TruckContainerGrid.Visibility = Visibility.Visible;
            }
            else if (tag == "moto")
            {
                MotoContainerGrid.Visibility = Visibility.Visible;
            }
        }
    }

}





