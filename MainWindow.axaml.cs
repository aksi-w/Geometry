using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace taskIT2
{
    public partial class MainWindow : Window
    {
        private readonly List<Shape> shapes = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnShapeSelected(object? sender, SelectionChangedEventArgs e)
        {
            if (ShapeSelector?.SelectedItem is not ComboBoxItem selectedItem) 
                return;

            var selected = selectedItem.Content?.ToString();
            
            if (LineInputs != null) 
                LineInputs.IsVisible = selected == "Линия";
            if (EllipseInputs != null) 
                EllipseInputs.IsVisible = selected == "Эллипс";
            if (PolygonInputs != null) 
                PolygonInputs.IsVisible = selected == "Многоугольник";
        }

        private void OnAddPointClick(object? sender, RoutedEventArgs e)
        {
            if (InputX == null || InputY == null || PolygonPointsPanel == null) 
                return;

            if (!double.TryParse(InputX.Text, out double x) || !double.TryParse(InputY.Text, out double y))
                return;

            var pointText = $"({x}, {y})";
            PolygonPointsPanel.Children.Add(new TextBlock 
            { 
                Text = pointText,
                Margin = new Thickness(5, 0, 0, 0)
            });
        }

        private void OnAddButtonClick(object? sender, RoutedEventArgs e)
        {
            if (InputX == null || InputY == null || ShapeContent == null || ShapeSelector == null)
                return;

            if (!double.TryParse(InputX.Text, out double x) || !double.TryParse(InputY.Text, out double y))
            {
                ShapeContent.Text = "Ошибка в координатах центра!";
                return;
            }

            var selectedShape = (ShapeSelector.SelectedItem as ComboBoxItem)?.Content?.ToString();
            Shape? shape = selectedShape switch
            {
                "Точка" => new PointShape(x, y),
                "Линия" when InputEndX != null && InputEndY != null &&
                            double.TryParse(InputEndX.Text, out double endX) && 
                            double.TryParse(InputEndY.Text, out double endY) =>
                    new LineShape(x, y, endX, endY),
                "Эллипс" when InputRadiusX != null && InputRadiusY != null &&
                             double.TryParse(InputRadiusX.Text, out double rx) && 
                             double.TryParse(InputRadiusY.Text, out double ry) =>
                    new EllipseShape(x, y, rx, ry),
                "Многоугольник" when PolygonPointsPanel != null && 
                                   PolygonPointsPanel.Children.Count > 2 =>
                    new PolygonShape(x, y, GetPolygonPoints()),
                _ => null
            };

            if (shape is not null)
            {
                shapes.Add(shape);
                ShapeContent.Text += $"{shape.GetType().Name}: Площадь = {shape.GetArea():F2}\n";
            }
            else
            {
                ShapeContent.Text = "Ошибка: не все параметры указаны корректно!";
            }
        }

        private (double X, double Y)[] GetPolygonPoints()
        {
            var points = new List<(double X, double Y)>();
            
            if (PolygonPointsPanel?.Children == null)
                return points.ToArray();

            foreach (var child in PolygonPointsPanel.Children)
            {
                if (child is TextBlock textBlock && !string.IsNullOrEmpty(textBlock.Text))
                {
                    var match = Regex.Match(textBlock.Text, @"\(([\d.]+),\s*([\d.]+)\)");
                    if (match.Success && 
                        double.TryParse(match.Groups[1].Value, out double x) &&
                        double.TryParse(match.Groups[2].Value, out double y))
                    {
                        points.Add((x, y));
                    }
                }
            }
            
            return points.ToArray();
        }
    }
}