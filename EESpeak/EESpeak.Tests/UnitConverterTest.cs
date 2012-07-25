using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EESpeak;

namespace EESpeak.Tests
{
    [TestFixture]
    public class UnitConverterTest
    {
        [Test]
        public void ConvertToEngineeringNotation()
        {
            Tuple<double, string>[] values =
            {
                new Tuple<double, string>(5.10, "5.1"),
                new Tuple<double, string>(510, "510"),
                new Tuple<double, string>(5100, "5.1k"),
                new Tuple<double, string>(51000, "51k"),
                new Tuple<double, string>(1e6, "1M"),
                new Tuple<double, string>(1e8, "100M"),
                new Tuple<double, string>(1e9, "1G"),
                new Tuple<double, string>(1e12, "1T"),
                new Tuple<double, string>(1e15, "1P"),
                new Tuple<double, string>(1e18, "1E"),
                new Tuple<double, string>(1e21, "1Z"),
                new Tuple<double, string>(1e24, "1Y")
            };

            foreach(Tuple<double, string> value in values)
                Assert.AreEqual(value.Item2, UnitConverter.ToEngineeringNotation(value.Item1));
            
        }
    }
}
