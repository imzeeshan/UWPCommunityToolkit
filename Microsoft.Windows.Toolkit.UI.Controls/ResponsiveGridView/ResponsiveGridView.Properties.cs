﻿using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Microsoft.Windows.Toolkit.UI.Controls
{
    public sealed partial class ResponsiveGridView 
    {
        /// <summary>
        /// Identifies the <see cref="ItemClickCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.Register(nameof(ItemClickCommand), typeof(ICommand), typeof(ResponsiveGridView), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="ItemsSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(ResponsiveGridView), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="ItemTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(ResponsiveGridView), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="ItemHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(ResponsiveGridView), new PropertyMetadata(0D));

        /// <summary>
        /// Identifies the <see cref="OneRowModeEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OneRowModeEnabledProperty =
            DependencyProperty.Register(nameof(OneRowModeEnabled), typeof(bool), typeof(ResponsiveGridView), new PropertyMetadata(false, ((o, e) => { OnOneRowModeEnabledChanged(o, e.NewValue); })));

        /// <summary>
        /// Identifies the <see cref="VerticalScroll"/> dependency property.
        /// </summary>
        private static readonly DependencyProperty VerticalScrollProperty =
            DependencyProperty.Register(nameof(VerticalScroll), typeof(ScrollMode), typeof(ResponsiveGridView), new PropertyMetadata(ScrollMode.Auto));

        /// <summary>
        /// Identifies the <see cref="ItemWidth"/> dependency property.
        /// </summary>
        private static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(ResponsiveGridView), new PropertyMetadata(0D));

        /// <summary>
        /// Identifies the <see cref="DesiredWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DesiredWidthProperty =
            DependencyProperty.Register(nameof(DesiredWidth), typeof(double), typeof(ResponsiveGridView), new PropertyMetadata(0D, DesiredWidthChanged));

        private static void OnOneRowModeEnabledChanged(DependencyObject d, object newValue)
        {
            var self = d as ResponsiveGridView;

            if ((bool)newValue)
            {
                if (self._isInitialized)
                {
                    var b = new Binding()
                    {
                        Source = self,
                        Path = new PropertyPath("ItemHeight")
                    };

                    self._listView.SetBinding(GridView.MaxHeightProperty, b);
                    self.VerticalScroll = ScrollMode.Disabled;
                }
            }
        }

        private static void DesiredWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as ResponsiveGridView;
            if (self._isInitialized)
            {
                self.RecalculateLayout(self._listView.ActualWidth);
            }
        }

        /// <summary>
        /// Gets or sets the desired width of each item
        /// </summary>
        /// <value>The width of the desired.</value>
        public double DesiredWidth
        {
            get { return (double)GetValue(DesiredWidthProperty); }
            set { SetValue(DesiredWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command to execute when an item is clicked.
        /// </summary>
        /// <value>The item click command.</value>
        public ICommand ItemClickCommand
        {
            get { return (ICommand)GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of each item in the grid.
        /// </summary>
        /// <value>The height of the item.</value>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets an object source used to generate the content of the grid.
        /// </summary>
        /// <value>The object that is used to generate the content of the ItemsControl. The default is null</value>
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the DataTemplate used to display each item.
        /// </summary>
        /// <value>The template that specifies the visualization of the data objects. The default is null.</value>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether only one row should be displayed.
        /// </summary>
        /// <value><c>true</c> if only one row is displayed; otherwise, <c>false</c>.</value>
        public bool OneRowModeEnabled
        {
            get { return (bool)GetValue(OneRowModeEnabledProperty); }
            set { SetValue(OneRowModeEnabledProperty, value); }
        }

        private ScrollMode VerticalScroll
        {
            get { return (ScrollMode)GetValue(VerticalScrollProperty); }
            set { SetValue(VerticalScrollProperty, value); }
        }

        private double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }        
    }
}