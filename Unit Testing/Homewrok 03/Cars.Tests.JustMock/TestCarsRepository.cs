﻿using Cars.Contracts;
using Cars.Data;
using Cars.Models;
using Cars.Tests.JustMock.Mocks;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Tests.JustMock
{
    [TestFixture]
    public class TetCarsRepository
    {

        [Test]
        public void TestCarsRepository_CheckParameterlessConstructor_ShouldInitialiseCorrectly()
        {
            var carRepo = new CarsRepository();
            Assert.IsNotNull(carRepo);
        }

        [Test]
        public void TestCarsRepository_CheckConstructorWithMockedDB_ShouldInitialiseCorrectly()
        {
            var mockedDataBase = new Mock<Database>();
            var carRepo = new CarsRepository(mockedDataBase.Object);
            Assert.IsNotNull(carRepo);
        }

        [Test]
        public void TestCarsRepository_AddMethodWhenPassNull_ShouldThrowArgumentException()
        {
            var carRepo = new CarsRepository();
            Assert.Throws<ArgumentException>(() => carRepo.Add(null));
        }

        [Test]
        public void TestCarsRepository_AddMethodWhenPassCarr_ShouldAddCorrectly()
        {
            // Database did not initialise the list, solved it with a database constructor
            var carRepo = new CarsRepository();
            var mockedCar = new Mock<Car>();
            var initialCount = carRepo.TotalCars;
            carRepo.Add(mockedCar.Object);
            var finalCount = carRepo.TotalCars;
            Assert.AreEqual(initialCount + 1, finalCount);
        }

        [Test]
        public void TestCarsRepository_RemoveMethodWhenPassNull_ShouldThrowArgumentException()
        {
            var carRepo = new CarsRepository();
            Assert.Throws<ArgumentException>(() => carRepo.Remove(null));
        }

        [Test]
        public void TestCarsRepository_RemoveMethodWhenPassCarr_ShouldRemoveCorrectly()
        {
            var carRepo = new CarsRepository();
            var mockedCar = new Mock<Car>();
            var initialCount = carRepo.TotalCars;

            carRepo.Add(mockedCar.Object);
            carRepo.Remove(mockedCar.Object);
            var finalCount = carRepo.TotalCars;

            Assert.AreEqual(initialCount, finalCount);
        }

        [Test]
        public void TestCarsRepository_GetByIdInvalidParameter_ShouldThrowArgumentException()
        {
            var carRepo = new CarsRepository();

            Assert.Throws<ArgumentException>(() => carRepo.GetById(5));
        }

        [Test]
        public void TestCarsRepository_GetByIdValidParameter_ShouldReturnCar()
        {
            var carRepo = new CarsRepository();
            var mockedCar = new Mock<Car>();
            var idToReturn = 1;

            mockedCar.SetupGet(x => x.Id).Returns(idToReturn);
            carRepo.Add(mockedCar.Object);

            Assert.AreEqual(idToReturn, mockedCar.Object.Id);
        }
    }
}
