namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class SimplePointTest
	{
		[Test]
		public void ShouldCreateASimplePointSucessfullyWithValidParameters()
		{
			var simplePoint = new SimplePoint(10, 10);

			simplePoint.Should().Not.Be.Null();
			simplePoint.GetType().Should().Be<SimplePoint>();
			simplePoint.X.Should().Be(10);
			simplePoint.Y.Should().Be(10);
		}

		[Test]
		public void ShouldThrowExceptionWhenCreatingASimplePointWithInvalidParameters()
		{
			(new Action(() => new SimplePoint(double.MinValue, 10))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void TwoSimplePointsWithTheSameCoordinatesShouldBeConsideredTheSame()
		{
			var simplePoint1 = new SimplePoint(10, 10);
			var simplePoint2 = new SimplePoint(10, 10);

			simplePoint1.Equals(simplePoint2).Should().Be(true);
		}

		[Test]
		public void ShouldCreateAProperArrayWithCoordinates()
		{
			var simplePoint = new SimplePoint(15, 30);

			var simplePointArray = simplePoint.ToArray();
			simplePointArray.Should().Not.Be.Null();
			simplePointArray.Length.Should().Be(2);
			simplePointArray[0].Should().Be(15);
			simplePointArray[1].Should().Be(30);
		}
	}
}
