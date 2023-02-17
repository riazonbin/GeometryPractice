using GeometryPracticeLibrary;
using System;
using Xunit;

namespace GeometryTestProject
{
    public class GeometryTest
    {
        [Fact]
        public void RectangleArea_3and5_15returned()
        {
            int a = 3;
            int b = 5;
            int expected = 15;
            Geometry geometry = new Geometry();
            int actual = geometry.RectangleArea(a, b);


            Assert.Equal(expected, actual);
        }
    }
}
