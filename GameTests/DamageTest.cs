﻿using GameLibrary.Dirigible;
using GameLibrary.DirigibleDecorators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using System;

namespace GameTests
{
    [TestClass]
    public class DamageTest
    {
        [TestMethod]
        public void GetDamageTestMethod()
        {
            AbstractDirigible dirigible = new BasicDirigible(Vector2.Zero,0);
            dirigible.Health = 100;
            dirigible.Armor = 0;
            int expectedHealth = 60;
            int actualHealth;

            dirigible.GetDamage(40);
            actualHealth = dirigible.Health;

            Assert.AreEqual(expectedHealth, actualHealth);

        }
        [TestMethod]
        public void GetDamageWithArmorTestMethod()
        {
            AbstractDirigible dirigible = new BasicDirigible(Vector2.Zero, 0);
            dirigible.Health = 100;
            dirigible.Armor = 25;
            int expectedHealth = 85;
            int actualHealth;

            dirigible.GetDamage(40);
            actualHealth = dirigible.Health;

            Assert.AreEqual(expectedHealth, actualHealth);
        }
        [TestMethod]
        public void GetDamageWithArmorAndBoostTestMethod()
        {
            Random random = new Random();
            AbstractDirigible dirigible = new BasicDirigible(Vector2.Zero, 0);
            dirigible.Health = 100;
            dirigible.Armor = 25;
            int boostHealth = random.Next(1,16);
            int expectedHealth = 85 + boostHealth;
            int actualHealth;

            dirigible.GetDamage(40);
            dirigible = new HealthBoostDecorator(dirigible,boostHealth);
            actualHealth = dirigible.Health;

            Assert.AreEqual(expectedHealth, actualHealth);
        }
    }
}
