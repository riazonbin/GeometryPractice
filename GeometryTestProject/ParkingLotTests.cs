using PracticeLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProject
{
    public class ParkingLotTests
    {
        [Fact]
        public void ParkCar_WhenSpaceAvailable_ShouldParkCar()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car("A123ПР", "Иванов", "черный");

            parkingLot.ParkCar(car);

            Assert.Single(parkingLot.ParkedCars);
            Assert.True(parkingLot.ParkedCars.ContainsValue(car));
            Assert.True(car.IsParked);
        }

        [Fact]
        public void ParkCar_WhenNoSpaceAvailable_ShouldNotParkCar()
        {
            var parkingLot = new ParkingLot(0);
            var car = new Car("A123ПР", "Иванов", "черный");

            parkingLot.ParkCar(car);

            Assert.Empty(parkingLot.ParkedCars);
            Assert.False(car.IsParked);
        }

        [Fact]
        public void ParkCar_WhenSpecificSpaceAvailable_ShouldParkCar()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car("A123ПР", "Иванов", "черный");

            parkingLot.ParkCar(car, 3);

            Assert.Single(parkingLot.ParkedCars);
            Assert.Equal(car, parkingLot.GetCarByParkingSpaceNumber(3));
            Assert.True(car.IsParked);
        }

        [Fact]
        public void ParkCar_WhenSpecificSpaceUnavailable_ShouldNotParkCar()
        {
            var parkingLot = new ParkingLot(10);
            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("П345РМ", "Петров", "белый");
            parkingLot.ParkCar(car1, 3);

            parkingLot.ParkCar(car2, 3);

            Assert.Single(parkingLot.ParkedCars);
            Assert.NotEqual(car2, parkingLot.GetCarByParkingSpaceNumber(3));
            Assert.False(car2.IsParked);
        }

        [Fact]
        public void UnparkCar_WhenCarParked_ShouldUnparkCar()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car("A123ПР", "Иванов", "черный");
            parkingLot.ParkCar(car);

            parkingLot.UnparkCar(1);

            Assert.Empty(parkingLot.ParkedCars);
            Assert.False(car.IsParked);
        }

        [Fact]
        public void UnparkCar_WhenCarNotParked_ShouldNotUnparkCar()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car("A123ПР", "Иванов", "черный");

            parkingLot.UnparkCar(1);

            Assert.Empty(parkingLot.ParkedCars);
            Assert.False(car.IsParked);
        }

        [Fact]
        public void UnparkCar_WhenCarParkedAndUnparkedUsingLicensePlate_ShouldUnparkCar()
        {
            var parkingLot = new ParkingLot(10);
            var car = new Car("A123ПР", "Иванов", "черный");
            parkingLot.ParkCar(car);

            parkingLot.UnparkCar("A123ПР");

            Assert.Empty(parkingLot.ParkedCars);
            Assert.False(car.IsParked);
        }

        [Fact]
        public void CheckParkingPlaceCorrectness_30placesOnParkLotAndTryingToGet45thPlace_ShouldReturnFalse()
        {
            var parkingLot = new ParkingLot(30);

            Assert.False(parkingLot.CheckParkingPlaceCorrectness(45));
        }

        [Fact]
        public void CheckParkingPlaceCorrectness_30placesOnParkLotAndTryingToGet20thPlace_ShouldReturnTrue()
        {
            var parkingLot = new ParkingLot(30);

            Assert.True(parkingLot.CheckParkingPlaceCorrectness(20));
        }

        [Fact]
        public void GetParkedCars_ListWithParkedAndUnparkedCars_ShouldReturnListOfParkedCars()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("К422АЗ", "Петров", "белый");
            var car3 = new Car("Е219АП", "Баширов", "серый");
            var car4 = new Car("А001АА", "Богатов", "синий");

            parkingLot.ParkCar(car1);
            parkingLot.ParkCar(car2);

            List<Car> cars = new List<Car>() { car1, car2, car3, car4 };

            var expectedList = new List<Car>() { car1, car2 };

            Assert.Equal(expectedList, parkingLot.GetParkedCars(cars));
        }

        [Fact]
        public void GetUnparkedCars_ListWithParkedAndUnparkedCars_ShouldReturnListOfUnparkedCars()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("К422АЗ", "Петров", "белый");
            var car3 = new Car("Е219АП", "Баширов", "серый");
            var car4 = new Car("А001АА", "Богатов", "синий");

            parkingLot.ParkCar(car1);
            parkingLot.ParkCar(car2);

            List<Car> cars = new List<Car>() { car1, car2, car3, car4 };

            var expectedList = new List<Car>() { car3, car4 };

            Assert.Equal(expectedList, parkingLot.GetUnparkedCars(cars));
        }

        [Fact]
        public void GetCarOnParkLotByLicensePlate_ListOfParkedCars_ShouldReturnCar()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("К422АЗ", "Петров", "белый");
            var car3 = new Car("Е219АП", "Баширов", "серый");
            var car4 = new Car("А001АА", "Богатов", "синий");

            parkingLot.ParkCar(car1);
            parkingLot.ParkCar(car2);
            parkingLot.ParkCar(car3);
            parkingLot.ParkCar(car4);

            var expectedCar = car3;
            var expectedCarPlace = 3;

            Assert.Equal(expectedCar, parkingLot.GetCarOnParkLotByLicensePlate("Е219АП").Value.Value);
            Assert.Equal(expectedCarPlace, parkingLot.GetCarOnParkLotByLicensePlate("Е219АП").Value.Key);
        }

        [Fact]
        public void GetCarOnParkLotByLicensePlate_ListOfParkedAndUnparkedCars_ShouldReturnNull()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("К422АЗ", "Петров", "белый");
            var car3 = new Car("Е219АП", "Баширов", "серый");
            var car4 = new Car("А001АА", "Богатов", "синий");

            parkingLot.ParkCar(car1);
            parkingLot.ParkCar(car2);

            Assert.Null(parkingLot.GetCarOnParkLotByLicensePlate("Е219АП").Value.Value);
            Assert.Equal(0, parkingLot.GetCarOnParkLotByLicensePlate("Е219АП").Value.Key);
        }

        [Fact]
        public void GetCarByParkingSpaceNumber_ListOfParkedAndUnparkedCars_ShouldReturnNull()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("К422АЗ", "Петров", "белый");
            var car3 = new Car("Е219АП", "Баширов", "серый");
            var car4 = new Car("А001АА", "Богатов", "синий");

            parkingLot.ParkCar(car1);
            parkingLot.ParkCar(car2);

            Assert.Null(parkingLot.GetCarByParkingSpaceNumber(3));
        }

        [Fact]
        public void GetCarByParkingSpaceNumber_ListOfParkedAndUnparkedCars_ShouldReturnCar()
        {
            var parkingLot = new ParkingLot(10);

            var car1 = new Car("A123ПР", "Иванов", "черный");
            var car2 = new Car("К422АЗ", "Петров", "белый");
            var car3 = new Car("Е219АП", "Баширов", "серый");
            var car4 = new Car("А001АА", "Богатов", "синий");

            parkingLot.ParkCar(car1);
            parkingLot.ParkCar(car2);

            var expectedCar = car2;

            Assert.Equal(expectedCar, parkingLot.GetCarByParkingSpaceNumber(2));
        }
    }
}
