using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace ProELib
{
    public class FontSettingsWindow : Window
    {

        private SolidColorBrush textBlockForeGroundBrush;
        private TextBlock textBlock;

        public FontSettingsWindow(int processId)
        {
            Test(processId);
        }

        public void Test(int processId)
        {
            IEnumerable<string> fontFamilies = System.Drawing.FontFamily.Families.Select(f=>f.Name);
            ListBox listBox = new ListBox();
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("/KSPE3Lib;component/Resources.xaml", UriKind.Relative);
            listBox.ItemTemplate = dictionary["FontItemTemplate"] as DataTemplate;
            listBox.ItemsSource = fontFamilies;
            listBox.Width = 250;
            listBox.Height = 300;
            listBox.SelectionChanged += listBox_SelectionChanged;
            Dictionary<int, Color> colorByCode = E3ColorTable.GetColorByCode(processId);
            ColorPicker colorPicker = new ColorPicker(colorByCode);
            colorPicker.VerticalAlignment = VerticalAlignment.Center;
            colorPicker.SelectedColorChanged += colorPicker_SelectedColorChanged;
            textBlockForeGroundBrush = new SolidColorBrush();
            textBlockForeGroundBrush.Color = colorPicker.SelectedColor;
            textBlock = new TextBlock();
            textBlock.Text = "Example";
            textBlock.Foreground = textBlockForeGroundBrush;
            StackPanel panel = new StackPanel();
            panel.Background = new SolidColorBrush(Colors.LightGray);
            panel.Children.Add(textBlock);
            panel.Children.Add(colorPicker.UIElement);
            panel.Children.Add(listBox);
            panel.Orientation = Orientation.Horizontal;
            Content = panel;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            FontFamily font = new FontFamily();
        }

        private void colorPicker_SelectedColorChanged(object sender, SelectedColorChangedEventArgs e)
        {
            textBlockForeGroundBrush.Color = e.color;
        }


    }
}