﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Filesystem.Ntfs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFS.Models;
using FFS.Services;
using FFS.Services.FileSystemScanner;
using FFS.Services.FileSystemScanner.NTFS;
using Microsoft.Toolkit.Mvvm.Input;
using Serilog;

namespace FFS.ViewModels
{
    public class DiskScanViewModel : ViewModelBase
    {
        public AsyncRelayCommand QueryCommand { get; }

        public bool IsExecutingQuery
        {
            get => _isExecutingQuery;
            set
            {
                SetProperty(ref _isExecutingQuery, value);
            }
        }
        private bool _isExecutingQuery;

        public ObservableCollection<ScannableDrive> ScannableDrives
        {
            get => _scannableDrives;
            private set
            {
                SetProperty(ref _scannableDrives, value);
            }
        }
        private ObservableCollection<ScannableDrive> _scannableDrives;

        private ScanResult _model;
        private readonly IFSScanner _fsScanner;

        public DiskScanViewModel(IFSScanner fsScanner)
        {
            _model = new ScanResult();

            IsExecutingQuery = false;
            _fsScanner = fsScanner;

            QueryCommand = new AsyncRelayCommand(ExecuteQuery);

            BuildScannableDrivesList();
        }

        private void BuildScannableDrivesList()
        {
            _scannableDrives = new ObservableCollection<ScannableDrive>();

            DriveInfo[] ntfsDrives = SystemDrivesRetriever.RetrieveFixed(new List<string>() { FileSystemDb.NTFS });
            foreach (DriveInfo drive in ntfsDrives)
            {
                _scannableDrives.Add(new ScannableDrive(drive));
            }
        }

        private async Task ExecuteQuery()
        {
            if (null == _fsScanner)
            {
                Log.Error("Cannot execute query, IFSScanner is NULL");
                return;
            }

            IsExecutingQuery = true;

            DriveInfo[] drives = SystemDrivesRetriever.RetrieveFixed(new List<string>() { FileSystemDb.NTFS });

            _model.ScannedDrives = new List<DriveInfo>(drives);

            List<INode> results = await _fsScanner.Scan(drives[2]) as List<INode> ?? new List<INode>();
            _model.Files = results;

            for (int i = 0; i < drives.Length; ++i)
            {
                Log.Information($"Inspected drive: {drives[i].Name}");
            }
            Log.Information($"Retrieved: {results.Count}");

            results.Clear();

            IsExecutingQuery = false;
            GC.Collect();
        }
    }
}