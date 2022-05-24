using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public partial class MainViewModel : ObservableRecipient
    {
        public ObservableCollection<ComputerPart> ComputerParts { get; set; }
        [ObservableProperty]
        private ComputerPart selectedComputerPart;
        public ObservableCollection<ComputerPart> SelectedComputerParts { get; set; }
        [ObservableProperty]
        private ComputerPart selectedCPinComputer;

        public ObservableCollection<Computer> Computers { get; set; }
        [ObservableProperty]
        private Computer selectedComputer;

        //Commands
        [ICommand]
        private void AddToComputer()
        {
            if (selectedComputerPart != null)
            {
                SelectedComputerParts.Add(selectedComputerPart);
            }
        }
        [ICommand]
        private void RemoveFromComputer()
        {
            if (selectedCPinComputer != null)
            {
                SelectedComputerParts.Remove(selectedCPinComputer);
            }
        }
        [ICommand]
        private void SaveComputer()
        {
            if (SelectedComputerParts != null)
            {
                var PC = new Computer();
                PC.ListOfParts = SelectedComputerParts.ToList();
                PC.ListOfPartsString = ListToStringConverter(SelectedComputerParts.ToList());
                foreach (var item in SelectedComputerParts)
                {
                    PC.SumPrice += item.Price;
                }
                Computers.Add(PC);
            }
        }
        public string ListToStringConverter(List<ComputerPart> input)
        {
            string Result = "";
            foreach (var item in input)
            {
                Result += item.Identifier + ", ";
            }
            return Result;
        }
        [ICommand]
        private void EditComputerPart()
        {

        }
        public MainViewModel()
        {
            ComputerParts = new ObservableCollection<ComputerPart>();
            ReadComputerparts("Parts.txt");
            SelectedComputerParts = new ObservableCollection<ComputerPart>();
            Computers = new ObservableCollection<Computer>();
        }

        private void ReadComputerparts(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                string row = reader.ReadLine();
                string[] items = row.Split(',');
                ComputerPart ComputerPart = new();
                ComputerPart.Identifier = items[0];
                ComputerPart.Brand = items[1];
                ComputerPart.Price = double.Parse(items[2], System.Globalization.NumberStyles.AllowDecimalPoint,System.Globalization.NumberFormatInfo.InvariantInfo);
                ComputerPart.Category = items[3];
                ComputerParts.Add(ComputerPart);
            }
        }
    }
}