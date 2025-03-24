﻿#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Windows;
using System.Windows.Controls;

namespace syncfusion.pdfviewerdemos.wpf
{
    public class PdfViewerEventAttachUtil
    {
        public static DependencyProperty WindowLoaded = DependencyProperty.RegisterAttached("WindowLoaded", typeof(bool), typeof(PdfViewerEventAttachUtil), new PropertyMetadata(new PropertyChangedCallback(WindowLoadedChanged)));

        public static void SetWindowLoaded(DependencyObject sender, bool command)
        {
            sender.SetValue(WindowLoaded, command);
        }

        public static void WindowLoadedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = sender as Grid;
            if (grid != null)
            {
                Window view = grid.Parent as Window;
                if (view != null)
                {

                    if (view.ToString().Contains("CustomToolBar"))
                    {
                        CustomToolbarViewModel viewModel = view.DataContext as CustomToolbarViewModel;
                        if (viewModel != null)
                        {
                            view.Loaded += new RoutedEventHandler(viewModel.Loaded);
                            view.Closed += new EventHandler(viewModel.Closed);
                        }
                    }
                }
            }
        }
    }
}
