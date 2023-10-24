using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using MovieDataAccess.Data;

namespace ListViewDemo;

public class MainWindowContext : INotifyPropertyChanged
{
    private string _prodName;
    public string ProdName
    {
        get
        {
            return _prodName;
        }
        set
        {
            _prodName = value;
            OnPropertyChanged();
        }
    }

    private string _prodPrice;
    public string ProdPrice
    {
        get
        {
            return _prodPrice;
        }
        set
        {
            _prodPrice = value; 
            OnPropertyChanged();
        }
    }

    // En ObservableCollection är en sorts lista som skickar OnPropertyChanged när innehåller förändras.
    // Vi behöver alltså inte hantera det själva med denna typen av lista
    public ObservableCollection<Product> Products { get; set; } = new();


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public void AddProduct()
    {
        Products.Add(new Product(){Id = 0, Name = ProdName, Price = double.Parse(ProdPrice)});
    }

    public void SaveToFile()
    { 
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DemoJson");
        Directory.CreateDirectory(directory);
        var filepath = Path.Combine(directory, "products.json");
        var json = JsonSerializer.Serialize(Products, new JsonSerializerOptions() {WriteIndented = true});
        using var sw = new StreamWriter(filepath);
        sw.WriteLine(json);
    }

    public void LoadFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DemoJson");
        Directory.CreateDirectory(directory);
        var filepath = Path.Combine(directory, "products.json");
        if (File.Exists(filepath))
        {
            var text = string.Empty;
            string? line = string.Empty;

            using StreamReader sr = new StreamReader(filepath);

            while ((line = sr.ReadLine()) != null)
            {
                text += line;
            }

            var products = JsonSerializer.Deserialize<List<Product>>(text);
        }
    }
}