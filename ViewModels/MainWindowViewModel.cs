﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Filesystem.Ntfs;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows;
using AdonisUI;
using FFS.Models;
using FFS.Services;
using FFS.Services.FileSystemScanner;
using FFS.Services.FileSystemScanner.NTFS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;

namespace FFS.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }
        private ViewModelBase _currentPage;

        public MainWindowViewModel()
        {
            DiskScanViewModel discScanVM = App.DI.GetService<DiskScanViewModel>();
            if (null != discScanVM)
            {
                discScanVM.ScanCompleted += DiscScanVMOnScanCompleted;
                CurrentPage = discScanVM;
            }
        }

        [SupportedOSPlatform("windows7.0")]
        public bool IsLightThemeActive
        {
            get => _isLightThemeActive;
            set
            {
                SetProperty(ref _isLightThemeActive, value);
                UpdateActiveTheme();
            }
        }
        private bool _isLightThemeActive;

        [SupportedOSPlatform("windows7.0")]
        private void UpdateActiveTheme()
        {
            Uri theme = IsLightThemeActive ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme;
            ResourceLocator.SetColorScheme(Application.Current.Resources, theme);
        }

        private void DiscScanVMOnScanCompleted(ScanResult res)
        {
            QueryPanelViewModel queryPanel = App.DI.GetService<QueryPanelViewModel>();
            if (null != queryPanel)
            {
                queryPanel.SetData(res);
                CurrentPage = queryPanel;
            }
        }
    }
}
