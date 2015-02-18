using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProELib
{
    internal class ApplicationSelectingWindow : Window
    {
        private int undefinedSelectionIndex = -1;
        private int selectedIndex;
        private Action<int> passSelectedIndex;

        private Grid grid;
        private ListBox listBox;
        
        private SolidColorBrush selectedBrush = new SolidColorBrush(Colors.White);
        private SolidColorBrush mainBrush = new SolidColorBrush(Colors.LightGray);
        private SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
        private Thickness borderThickness = new Thickness(0.5);
        private CornerRadius borderCornerRadius = new CornerRadius(5);
        
        public ApplicationSelectingWindow(IEnumerable<string> projects, Action<int> passSelectedIndex)
        {
            selectedIndex = undefinedSelectionIndex;
            this.passSelectedIndex = passSelectedIndex;
            grid = GetGrid();
            listBox = GetListBox(projects);
            SetElementInGrid(listBox, 0, 0, 0, 3);
            Button acceptButton = GetButton("Выбрать");
            Button cancelButton = GetButton("Отмена");
            acceptButton.Click += acceptButton_Click;
            cancelButton.Click += cancelButton_Click;
            SetElementInGrid(acceptButton, 1, 0);
            SetElementInGrid(cancelButton, 1, 2);
            Content = grid;
            MinWidth = grid.MinWidth+10;
            MinHeight = grid.MinWidth + 10;
            Width = 400;
            Height = 300;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            FocusManager.SetFocusedElement(grid, listBox);  // установка фокуса в listBox
            Closing += ProjectSelectingWindow_Closing;
        }

        private Grid GetGrid()
        {
            Grid returningGrid = new Grid();
            RowDefinition listBoxRow = new RowDefinition();
            RowDefinition buttonsRow = new RowDefinition();
            ColumnDefinition leftColumn = new ColumnDefinition();
            ColumnDefinition centerColumn = new ColumnDefinition();
            ColumnDefinition rightColumn = new ColumnDefinition();
            listBoxRow.Height = new GridLength(1, GridUnitType.Star);
            buttonsRow.Height = new GridLength(45, GridUnitType.Pixel);
            leftColumn.Width = new GridLength(80, GridUnitType.Pixel);
            centerColumn.Width = new GridLength(1, GridUnitType.Star);
            rightColumn.Width = new GridLength(80, GridUnitType.Pixel);
            returningGrid.MinWidth = 200;
            returningGrid.RowDefinitions.Add(listBoxRow);
            returningGrid.RowDefinitions.Add(buttonsRow);
            returningGrid.ColumnDefinitions.Add(leftColumn);
            returningGrid.ColumnDefinitions.Add(centerColumn);
            returningGrid.ColumnDefinitions.Add(rightColumn);
            returningGrid.Background = mainBrush;
            return returningGrid;
        }

        private ListBox GetListBox(IEnumerable<string> projects)
        {
            listBox = new ListBox();
            listBox.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            listBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            listBox.ItemContainerStyle = GetItemContainerStyle();
            listBox.SnapsToDevicePixels = true;
            listBox.SelectionChanged += listBox_SelectionChanged;
            listBox.Background = mainBrush;
            listBox.KeyUp += listBox_KeyUp;
            listBox.MouseDoubleClick += listBox_MouseDoubleClick;
            ScrollViewer.SetHorizontalScrollBarVisibility(listBox, ScrollBarVisibility.Disabled);
            foreach (string project in projects)
                listBox.Items.Add(GetBorder(project));
            listBox.SelectedIndex = 0;
            return listBox;
        }

        private static Style GetItemContainerStyle()
        {
            Style listBoxItemStyle = new Style(typeof(ListBoxItem));
            listBoxItemStyle.Setters.Add(new Setter(ListBox.FocusVisualStyleProperty, null));   // убираем рамку из точек вокруг выделенного элемента
            listBoxItemStyle.Setters.Add(new Setter(ListBox.PaddingProperty, new Thickness(0)));    // убираем отступы от края родительского элемента
            listBoxItemStyle.Setters.Add(new Setter(ListBoxItem.MarginProperty, new Thickness(0, 0, 0, 1)));    // добавляем отступ снизу каждого элемента
            ResourceDictionary resources = new ResourceDictionary();
            resources.Add(SystemColors.HighlightBrushKey, new SolidColorBrush(Colors.Transparent));
            listBoxItemStyle.Resources = resources;
            return listBoxItemStyle;
        }

        private Border GetBorder(string text)
        {
            Border border = new Border();
            border.Child = GetLabel(text);
            border.BorderThickness = borderThickness;
            border.BorderBrush = blackBrush;
            border.CornerRadius = borderCornerRadius;
            border.Background = mainBrush;
            return border;
        }

        private static Label GetLabel(string text)
        {
            TextBlock block = new TextBlock();
            block.Text = text;
            block.TextTrimming = TextTrimming.CharacterEllipsis;
            Label label = new Label();
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.Content = block;
            return label;
        }

        private static Button GetButton(string text)
        {
            Button button = new Button();
            button.Content = text;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Width = 60;
            button.Height = 25;
            return button;
        }

        private void SetElementInGrid(UIElement element, int row, int column, int rowSpan = 0, int columnSpan = 0)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            if (rowSpan > 0)
                Grid.SetRowSpan(element, rowSpan);
            if (columnSpan > 0)
                Grid.SetColumnSpan(element, columnSpan);
            grid.Children.Add(element);
        }

        private void ListBoxItemSelected()
        {
            if (selectedIndex >= 0)
            {
                SetItemState(listBox.Items[selectedIndex], mainBrush);
            }
            SetItemState(listBox.SelectedItem, selectedBrush);
            selectedIndex = listBox.SelectedIndex;
        }

        private static void SetItemState(object item, SolidColorBrush backgroundBrush)
        {
            Border border = item as Border;
            border.Background = backgroundBrush;
        }

        private void CancelSelectionAndClose()
        {
            selectedIndex = undefinedSelectionIndex;
            Close();
        }

        private void CloseIfSelected()
        {
            if (selectedIndex != undefinedSelectionIndex)
                Close();
        }
        
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItemSelected();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelSelectionAndClose();
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            CloseIfSelected();
        }

        private void listBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CloseIfSelected();
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CloseIfSelected();
        }

        private void ProjectSelectingWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            passSelectedIndex(selectedIndex);
        }
    }

}
