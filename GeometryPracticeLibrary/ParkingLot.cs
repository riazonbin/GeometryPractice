using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PracticeLibrary
{
    public class ParkingLot
    {
        public int totalNumberOfPlaces;
        public Dictionary<int, Car> ParkedCars; // parking lot and car attached to it

        public ParkingLot(int totalNumberOfPlaces)
        {
            this.totalNumberOfPlaces = totalNumberOfPlaces;
            ParkedCars = new Dictionary<int, Car>(totalNumberOfPlaces);
        }

        public int GetAvailableSpacesCount()
        {
            return totalNumberOfPlaces - ParkedCars.Count;
        }

        public List<Car> GetParkedCars(List<Car> carsList)
        {
            return carsList.Where(x => x.IsParked == true).ToList();
        }

        public List<Car> GetUnparkedCars(List<Car> carsList)
        {
            return carsList.Where(x => x.IsParked == false).ToList();
        }

        public KeyValuePair<int, Car>? GetCarOnParkLotByLicensePlate(string licensePlate)
        {
            return ParkedCars.FirstOrDefault(x => x.Value.LicensePlate == licensePlate);
        }

        public Car GetCarByParkingSpaceNumber(int parkingSpaceNumber)
        {
            try
            {
                if (!CheckParkingPlaceCorrectness(parkingSpaceNumber))
                {
                    return null;
                }
                return ParkedCars[parkingSpaceNumber];
            }
            catch { return null; }
        }

        public void ParkCar(Car car)
        {
            if(GetAvailableSpacesCount() == 0)
            {
                return;
            }
            car.IsParked = true;
            if (GetAvailableSpacesCount() == totalNumberOfPlaces)
            {
                ParkedCars[1] = car;
                return;
            }
            int lastOcupuedSpace = ParkedCars.Last().Key;
            ParkedCars[lastOcupuedSpace + 1] = car;


        }

        public void ParkCar(Car car, int parkingSpaceNumber) 
        {
            if(!CheckParkingPlaceCorrectness(parkingSpaceNumber))
            {
                return;
            }

            if (GetAvailableSpacesCount() == 0)
            {
                return;
            }

            if (GetCarByParkingSpaceNumber(parkingSpaceNumber) != null)
            {
                return;
            }

            ParkedCars[parkingSpaceNumber] = car;
            car.IsParked= true;
        }

        public void UnparkCar(int parkingSpaceNumber)
        {
            Car car = GetCarByParkingSpaceNumber(parkingSpaceNumber);

            if (car == null)
            {
                return;
            }

            ParkedCars.Remove(parkingSpaceNumber);
            car.IsParked = false;
        }

        public void UnparkCar(string licensePlate)
        {
            KeyValuePair<int, Car>? car = GetCarOnParkLotByLicensePlate(licensePlate);

            if (car?.Value is null || car is null)
            {
                return;
            }

            ParkedCars.Remove(car.Value.Key);
            car.Value.Value.IsParked = false;
        }

        public bool CheckParkingPlaceCorrectness(int parkingSpaceNumber)
        {
            if (parkingSpaceNumber > totalNumberOfPlaces)
            {
                Console.WriteLine("Такого места не существует на парковке!");
                return false;
            }
            return true;
        }
    }
}
