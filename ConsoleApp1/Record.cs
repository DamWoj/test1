using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConsoleApp1
{
    public class Record
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public int Beds { get => _beds; set => _beds = value; }
        private int _beds;
        public int Baths { get => _baths; set => _baths = value; }
        private int _baths;
        public double SqFt { get => _sqFt; set => _sqFt = value; }
        private double _sqFt;
        public DateTime SaleDate { get => _saleDate; set => _saleDate = value; }
        private DateTime _saleDate;
        public decimal Price { get => _price; set => _price = value; }
        private decimal _price;
        public double Latitude { get => _latitude; set => _latitude = value; }
        private double _latitude;
        public double Longitude { get => _longitude; set => _longitude = value; }
        private double _longitude;
        public Record(string line)
        {
            var columns = line.Split(',');
            Street = columns[0] ?? string.Empty;
            City = columns[1] ?? string.Empty;
            Zip = columns[2] ?? string.Empty;
            State = columns[3] ?? string.Empty;
            Type = columns[7] ?? string.Empty;
            int.TryParse(columns[4], out _beds);
            int.TryParse(columns[5], out _baths);
            double.TryParse(columns[6], out _sqFt);
            DateTime.TryParse(columns[8], out _saleDate);
            decimal.TryParse(columns[9],out _price);
            double.TryParse(columns[10], out _latitude);
            double.TryParse(columns[11], out _longitude);
        }

        public override string ToString()
        {
            return $"street: {Street} city: {City} zip: {Zip} state: {State} type: {Type} beds: {Beds} baths: {Baths} sq__ft: {SqFt} sale_date: {SaleDate} price: {Price} latitude: {Latitude} longitude: {Longitude}";
        }


    }
}
