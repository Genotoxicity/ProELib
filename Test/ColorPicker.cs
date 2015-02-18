using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ProELib
{
    class ColorPicker
    {
        private Thickness singleThickness = new Thickness(1);
        private LinearGradientBrush rainbowBrush = GetRainbowBrush();
        private LinearGradientBrush popupBorderBrush;
        private LinearGradientBrush pressedBorderBrush;
        private SolidColorBrush dimGraySolidColorBrush = new SolidColorBrush(Colors.DimGray);
        private SolidColorBrush solidColorBrush = new SolidColorBrush();
        private Border gridBorder;
        private const double innerColorElementSide = 12;
        private const double radioButtonSide = 18;
        private const int elements = 16;
        private const int autoColorIndex = -1;
        private Dictionary<int, Color> colorByCode;
        private ToggleButton topButton;
        private RadioButton autoColorRadioButton;
        private Window colorPickerWindow;
        private const string radioButtonGroup = "ColorsRadioButton";
        private const string auto = "Auto";

        public event EventHandler<SelectedColorChangedEventArgs> SelectedColorChanged;

        public UIElement UIElement
        {
            get
            {
                return topButton;
            }
        }

        public int SelectedColorIndex { get; private set; }

        public Color SelectedColor
        {
            get
            {
                return colorByCode[SelectedColorIndex];
            }
        }

        public double Width
        {
            get
            {
                return topButton.Width;
            }
            set
            {
                topButton.Width = value;
            }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return topButton.HorizontalAlignment;
            }
            set
            {
                topButton.HorizontalAlignment = value;
            }
        }

        public VerticalAlignment VerticalAlignment
        {
            get
            {
                return topButton.VerticalAlignment;
            }
            set
            {
                topButton.VerticalAlignment = value;
            }
        }

        public ColorPicker(Dictionary<int, Color> colorByCode) : this(colorByCode, -1)
        { 
        }

        public ColorPicker(Dictionary<int, Color> colorByCode, int selectedColorIndex)
        {
            this.colorByCode = colorByCode;
            double colorPickerGridWidth = radioButtonSide * elements;
            ResourceDictionary dictionary = GetResourceDictionary();
            double autoColorRadioButtonHeight = 22;
            popupBorderBrush = GetEffectBorderBrush(colorPickerGridWidth -2,autoColorRadioButtonHeight-2, Colors.White, Colors.LightSlateGray);
            pressedBorderBrush = GetEffectBorderBrush(colorPickerGridWidth - 2, autoColorRadioButtonHeight-2, Colors.LightSlateGray, Colors.White);
            autoColorRadioButton = GetAutoColorRadioButton(dictionary["AutoColorElementTemplate"] as ControlTemplate, colorPickerGridWidth, autoColorRadioButtonHeight);
            Grid colorPickerGrid = GetColorPickerGrid(colorPickerGridWidth);
            Grid.SetRow(autoColorRadioButton, 0);
            Grid.SetColumn(autoColorRadioButton, 0);
            Grid.SetColumnSpan(autoColorRadioButton, elements);
            colorPickerGrid.Children.Add(autoColorRadioButton);
            ControlTemplate colorElementTemplate = dictionary["ColorElementTemplate"] as ControlTemplate;
            Color color;
            for (int colorIndex = 0; colorIndex <= colorByCode.Keys.Max<int>(); colorIndex++)
            {
                int rowIndex = (colorIndex / elements) + 1; // в первый ряд сетки уже добавлен элемент
                int columnIndex = colorIndex % elements;
                color = colorByCode[colorIndex];
                string tip = String.Format("{0} (#{1}{2}{3})", colorIndex, color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
                RadioButton radioButton = GetRadioButton(colorElementTemplate, ref color, colorIndex, tip);
                Grid.SetRow(radioButton, rowIndex);
                Grid.SetColumn(radioButton, columnIndex);
                colorPickerGrid.Children.Add(radioButton);
            }
            colorPickerGrid.Height = radioButtonSide * elements + autoColorRadioButton.Height + 2;
            gridBorder = GetGridBorder(colorPickerGrid);
            topButton = GetToggleButton(dictionary["ColorPickerTopButtonTemplate"] as ControlTemplate);
            colorPickerWindow = GetColorPickerWindow(gridBorder);
            SelectedColorIndex = selectedColorIndex;
            SetRadioButtonChecked(selectedColorIndex);
            Width = 150;
        }

        private static ResourceDictionary GetResourceDictionary()
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("/KSPE3Lib;component/Resources.xaml", UriKind.Relative);
            return dictionary;
        }

        private static LinearGradientBrush GetEffectBorderBrush(double borderWidth, double borderHeight, Color topLeftColor, Color bottomRightColor)
        {
            LinearGradientBrush popupBrush = new LinearGradientBrush();
            popupBrush.StartPoint = new Point(0, 0);
            popupBrush.EndPoint = new Point(0, 1);
            popupBrush.GradientStops.Add(new GradientStop(topLeftColor, 0));
            popupBrush.GradientStops.Add(new GradientStop(topLeftColor, 0.5));
            popupBrush.GradientStops.Add(new GradientStop(bottomRightColor, 0.5));
            popupBrush.GradientStops.Add(new GradientStop(bottomRightColor, 1));
            double angleInDegree = Math.Atan2(borderHeight, borderWidth) * 180 / Math.PI;
            popupBrush.Transform = new RotateTransform(-angleInDegree, borderWidth / 2, borderHeight / 2);
            return popupBrush;
        }

        private RadioButton GetAutoColorRadioButton(ControlTemplate template, double width, double height)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.Template = template;
            TextBlock textBlock = GetAutoColorTextBlock(width - 6, height - 6);
            radioButton.Content = textBlock;
            radioButton.Margin = new Thickness(0, 0, 0, 1);
            radioButton.GroupName = radioButtonGroup;
            radioButton.BorderBrush = popupBorderBrush;
            radioButton.Checked += radioButton_Checked;
            radioButton.Tag = autoColorIndex;
            radioButton.Width = width;
            radioButton.Height = height;
            radioButton.SnapsToDevicePixels = true;
            return radioButton;
        }

        private TextBlock GetAutoColorTextBlock(double width, double height)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = auto;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Background = rainbowBrush;
            textBlock.Width = width;
            textBlock.Height = height;
            return textBlock;
        }

        private static Grid GetColorPickerGrid(double width)
        {
            Grid grid = new Grid();
            grid.Width = width;
            GridLength autoLength = new GridLength(1, GridUnitType.Auto);
            for (int i = 0; i <= elements; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = autoLength;
                grid.ColumnDefinitions.Add(column);
                RowDefinition row = new RowDefinition();
                row.Height = autoLength;
                grid.RowDefinitions.Add(row);
            }
            RowDefinition additionalRow = new RowDefinition();
            additionalRow.Height = autoLength;
            grid.RowDefinitions.Add(additionalRow);
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            return grid;
        }

        private RadioButton GetRadioButton(ControlTemplate colorElementTemplate, ref Color color, int colorIndex, string tip)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.Width = radioButtonSide;
            radioButton.Height = radioButtonSide;
            radioButton.GroupName = radioButtonGroup;
            radioButton.Template = colorElementTemplate;
            radioButton.Content = GetInnerColorElement(color);
            radioButton.ToolTip = tip;
            radioButton.Tag = colorIndex;
            radioButton.SnapsToDevicePixels = true;
            radioButton.Checked += radioButton_Checked;
            return radioButton;
        }

        private Border GetInnerColorElement(Color color)
        {
            Border elementBorder = new Border();
            elementBorder.BorderThickness = singleThickness;
            elementBorder.BorderBrush = dimGraySolidColorBrush;
            elementBorder.Background = new SolidColorBrush(color);
            elementBorder.Width = innerColorElementSide;
            elementBorder.Height = innerColorElementSide;
            elementBorder.HorizontalAlignment = HorizontalAlignment.Center;
            elementBorder.VerticalAlignment = VerticalAlignment.Center;
            return elementBorder;
        }

        private Border GetGridBorder(Grid colorPickerGrid)
        {
            Border border = new Border();
            border.Width = colorPickerGrid.Width + 6;
            border.Height = colorPickerGrid.Height + 5;
            border.BorderBrush = new SolidColorBrush(Colors.Gray);
            border.Background = new SolidColorBrush(Colors.LightGray);
            border.BorderThickness = singleThickness;
            border.Padding = singleThickness;
            border.Child = colorPickerGrid;
            return border;
        }

        private ToggleButton GetToggleButton(ControlTemplate template)
        {
            ToggleButton toggleButton = new ToggleButton();
            toggleButton.Template = template;
            toggleButton.Background = rainbowBrush;
            toggleButton.Content = auto;
            toggleButton.ClickMode = ClickMode.Press;
            toggleButton.Click += toggleButton_Click;
            return toggleButton;
        }

        private static LinearGradientBrush GetRainbowBrush()
        {
            LinearGradientBrush rainbowBrush = new LinearGradientBrush();
            rainbowBrush.StartPoint = new Point(0, 0);
            rainbowBrush.EndPoint = new Point(1, 1);
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.03));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.06));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.08));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.11));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.13));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.16));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.18));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.LightBlue, 0.21));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.LightBlue, 0.23));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.26));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.28));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Violet, 0.31));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Violet, 0.33));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.White, 0.36));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.White, 0.7));
            rainbowBrush.GradientStops.Add(new GradientStop(Colors.Black, 1));
            return rainbowBrush;
        }

        private static Window GetColorPickerWindow(Border content)
        {
            Window window = new Window();
            window.Content = content;
            window.Width = content.Width;
            window.Height = content.Height;
            window.ResizeMode = ResizeMode.NoResize;
            window.WindowStyle = WindowStyle.None;
            window.ShowInTaskbar = false;
            return window;
        }

        private void ShowColorPickerWindow()
        {
            Point topButtonScreenPosition = GetTopButtonScreenPosition();
            colorPickerWindow.Left = topButtonScreenPosition.X;
            colorPickerWindow.Top = topButtonScreenPosition.Y + topButton.ActualHeight;
            colorPickerWindow.ShowDialog();
        }

        private void HideColorPickerWindow()
        {
            colorPickerWindow.Hide();
            topButton.IsChecked = false;
        }

        private Point GetTopButtonScreenPosition()
        {
            UIElement parent, current = topButton;
            Point relativeLocation, totalOffset = default(Point), elementScreenPosition = default(Point);
            while (current != null)
            {
                parent = VisualTreeHelper.GetParent(current) as UIElement;
                if (parent != null)
                {
                    relativeLocation = current.TranslatePoint(new Point(0, 0), parent);
                    totalOffset.X += relativeLocation.X;
                    totalOffset.Y += relativeLocation.Y;
                }
                else
                {
                    elementScreenPosition = current.PointToScreen(totalOffset);
                }
                current = parent;
            }
            return elementScreenPosition;
        }

        private void SetRadioButtonChecked(int colorIndex)
        {
            Grid grid = gridBorder.Child as Grid;
            foreach (RadioButton radioButton in grid.Children)
            {
                if (colorIndex == (int)radioButton.Tag)
                {
                    radioButton.IsChecked = true;
                    break;
                }
            }
        }

        private void SetSelectedColor(RadioButton radioButton)
        {
            int colorIndex = (int)radioButton.Tag;
            SelectedColorIndex = colorIndex;
            if (colorIndex >= 0)
            {
                solidColorBrush.Color = colorByCode[colorIndex];
                topButton.Background = solidColorBrush;
                topButton.Content = String.Empty;
                autoColorRadioButton.BorderBrush = popupBorderBrush;
            }
            else
            {
                if (autoColorRadioButton.IsChecked == true)
                {
                    autoColorRadioButton.BorderBrush = pressedBorderBrush;
                    topButton.Background = rainbowBrush;
                    topButton.Content = auto;
                }
            }
            if (SelectedColorChanged!=null)
                SelectedColorChanged(this, new SelectedColorChangedEventArgs(SelectedColorIndex, SelectedColor));
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            SetSelectedColor(sender as RadioButton);
            HideColorPickerWindow();
        }

        private void toggleButton_Click(object sender, RoutedEventArgs e)
        {
            ShowColorPickerWindow();
        }

    }
}
