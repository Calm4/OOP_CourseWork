using GameLibrary.Dirigible;
using GameLibrary.DirigibleDecorators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using System;

namespace GameTests
{
    [TestClass]
    public class MovementFuelTest
    {
        [TestMethod]
        public void MovementCheckTestMethod()
        {
            AbstractDirigible dirigible = new BasicDirigible(Vector2.Zero, 0);
            int expectedFuel = 1990;
            int actualFuel;


            dirigible.Fuel = 2000;
            for (int i = 0; i < 10; i++)
            {
                dirigible.Move(Vector2.Zero);
            }
            actualFuel = dirigible.Fuel;

            Assert.AreEqual(expectedFuel, actualFuel);
        }
        [TestMethod]
        public void MovementCheckWithBoostTest()
        {
            Random random = new Random();
            AbstractDirigible dirigible = new BasicDirigible(Vector2.Zero, 0);
            int actualFuel;
            int boost = random.Next(1,250);
            int expectedFuel = 1500 + boost;


            dirigible.Fuel = 2000;
            for (int i = 0; i < 500; i++)
            {
                dirigible.Move(Vector2.Zero);
            }
            dirigible = new FuelBoostDecorator(dirigible,boost);
            actualFuel = dirigible.Fuel;

            
            Assert.AreEqual(expectedFuel, actualFuel);
        }
    }
}
