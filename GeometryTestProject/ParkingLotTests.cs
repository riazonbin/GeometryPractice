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
    }
}
