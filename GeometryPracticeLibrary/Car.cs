using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeLibrary
{
    public class Car
    {
        public string LicensePlate { get; }
        public string Owner { get; }
        public string Color { get; }
        public bool IsParked { get; set; }

        public Car(string licensePlate, string owner, string color)
        {
            LicensePlate = licensePlate;
            Owner = owner;
            Color = color;
            IsParked = false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Car otherCar = (Car)obj;
            return LicensePlate == otherCar.LicensePlate;
        }

        public override int GetHashCode()
        {
            return LicensePlate.GetHashCode();
        }
    }
}
