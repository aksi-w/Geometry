using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace taskIT2
{
    public partial class MainWindow : Window
    {
        private List<Shape> shapes = new List<Shape>();

        public MainWindow()
        {
            InitializeComponent();
            ShapeSelector = this.FindControl<ComboBox>("ShapeSelector");
            InputX = this.FindControl<TextBox>("InputX");
            InputY = this.FindControl<TextBox>("InputY");
            ShapeContent = this.FindControl<TextBlock>("ShapeContent");
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            if (ShapeSelector == null || InputX == null || InputY == null || ShapeContent == null)
                return;

            if (!double.TryParse(InputX.Text, out double x) || !double.TryParse(InputY.Text, out double y))
            {
                ShapeContent.Text = "Ошибка: Введите числа!";
                return;
            }

            string selectedShape = (ShapeSelector.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";

            Shape? shape = selectedShape switch
            {
                "Точка" => new PointShape(x, y),
                "Линия" => new LineShape(x, y, x + 10, y + 10),
                "Многоугольник" => new PolygonShape(x, y, new[] { (x, y), (x + 10, y + 5), (x + 5, y + 10) }),
                "Эллипс" => new EllipseShape(x, y, 10, 5),
                _ => null
            };

            if (shape != null)
            {
                shapes.Add(shape);
                UpdateShapeContent();
            }
        }

        private void UpdateShapeContent()
        {
            if (ShapeContent == null) return;
            ShapeContent.Text = shapes.Count == 0
                ? "Нет фигур."
                : string.Join("\n", shapes.ConvertAll(s => $"{s.GetType().Name} - Площадь: {s.GetArea()}"));
        }
    }
}
