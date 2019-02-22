using System;
using System.Linq;
using NUnit.Framework;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace PSOTester
{
    [TestFixture]
    public class CoordsTester
    {
        [Test]
        public void TestInitWithCoords()
        {
            var coords = new Coords(5, 0, 10, true);
            Assert.True(coords.CoordinateArray.All(value => value == 0));
            coords.Add(1.0);
            Assert.True(coords.CoordinateArray.All(value => value == 1));
            
            var coords2 = new Coords(coords);
            Assert.True(coords2.LowerBound == 0);
            Assert.True(coords2.UpperBound == 10);
            Assert.True(coords2.CoordinateArray.All(value => value == 1));
        }
        
        [Test]
        public void TestAddScalar()
        {
            var coords = new Coords(5, 0, 10, true);
            Assert.True(coords.CoordinateArray.All(value => value == 0));
            coords.Add(1.0);
            Assert.True(coords.CoordinateArray.All(value => value == 1));
        }
        
        [Test]
        public void TestMinusScalar()
        {
            var coords = new Coords(5, 0, 10, true);
            Assert.True(coords.CoordinateArray.All(value => value == 0));
            coords.Minus(1.0);
            Assert.True(coords.CoordinateArray.All(value => value == -1));
        }
        
        [Test]
        public void TestMultiplyScalar()
        {
            var coords = new Coords(5, 0, 10, true);
            Assert.True(coords.CoordinateArray.All(value => value == 0));
            coords.Add(1.0);
            Assert.True(coords.CoordinateArray.All(value => value == 1));
            coords.Multiply(6.0);
            Assert.True(coords.CoordinateArray.All(value => value == 6));
        }
        
        [Test]
        public void TestDivideScalar()
        {
            var coords = new Coords(5, 0, 10, true);
            Assert.True(coords.CoordinateArray.All(value => value == 0));
            coords.Add(6.0);
            Assert.True(coords.CoordinateArray.All(value => value == 6));
            coords.Divide(2.0);
            Assert.True(coords.CoordinateArray.All(value => value == 3));
        }
        
        [Test]
        public void TestAddVector()
        {
            var coords1 = new Coords(5, 0, 10, true);
            Assert.True(coords1.CoordinateArray.All(value => value == 0));
            var coords2 = new Coords(5, 0, 10, true);
            Assert.True(coords2.CoordinateArray.All(value => value == 0));
            coords2.Add(2);
            Assert.True(coords2.CoordinateArray.All(value => value == 2));
            coords1.Add(coords2);
            Assert.True(coords1.CoordinateArray.All(value => value == 2));
        }
        
        [Test]
        public void TestMinusVector()
        {
            var coords1 = new Coords(5, 0, 10, true);
            Assert.True(coords1.CoordinateArray.All(value => value == 0));
            var coords2 = new Coords(5, 0, 10, true);
            Assert.True(coords2.CoordinateArray.All(value => value == 0));
            coords2.Add(2);
            Assert.True(coords2.CoordinateArray.All(value => value == 2));
            coords1.Minus(coords2);
            Assert.True(coords1.CoordinateArray.All(value => value == -2));
        }
        
        [Test]
        public void TestMultiplyVector()
        {
            var coords1 = new Coords(5, 0, 10, true);
            Assert.True(coords1.CoordinateArray.All(value => value == 0));
            coords1.Add(3);
            Assert.True(coords1.CoordinateArray.All(value => value == 3));
            var coords2 = new Coords(5, 0, 10, true);
            Assert.True(coords2.CoordinateArray.All(value => value == 0));
            coords2.Add(2);
            Assert.True(coords2.CoordinateArray.All(value => value == 2));
            coords1.Multiply(coords2);
            Assert.True(coords1.CoordinateArray.All(value => value == 6));
        }
        
        [Test]
        public void TestDivideVector()
        {
            var coords1 = new Coords(5, 0, 10, true);
            Assert.True(coords1.CoordinateArray.All(value => value == 0));
            coords1.Add(6);
            Assert.True(coords1.CoordinateArray.All(value => value == 6));
            var coords2 = new Coords(5, 0, 10, true);
            Assert.True(coords2.CoordinateArray.All(value => value == 0));
            coords2.Add(2);
            Assert.True(coords2.CoordinateArray.All(value => value == 2));
            coords1.Divide(coords2);
            Assert.True(coords1.CoordinateArray.All(value => value == 3));
        }
        
        [Test]
        public void TestMove()
        {
            var coords1 = new Coords(5, 0, 10, true);
            Assert.True(coords1.CoordinateArray.All(value => value == 0));
            coords1.Add(2);
            Assert.True(coords1.CoordinateArray.All(value => value == 2));
            
            var coords2 = new Coords(5, 0, 10, true);
            Assert.True(coords2.CoordinateArray.All(value => value == 0));
            coords2.Add(3);
            Assert.True(coords2.CoordinateArray.All(value => value == 3));
            
            var coords3 = new Coords(7, 0, 10, true);
            Assert.True(coords3.CoordinateArray.All(value => value == 0));

            var err = coords1.Move(coords3);
            Assert.Null(err);

            coords1.Move(coords2);
            Assert.True(coords1.CoordinateArray.All(value => value == 5));
            
            coords2.Add(3);
            Assert.True(coords2.CoordinateArray.All(value => value == 6));
            
            coords1.Move(coords2);
            Assert.True(coords1.CoordinateArray.All(value => value == 5));
        }
    }
}