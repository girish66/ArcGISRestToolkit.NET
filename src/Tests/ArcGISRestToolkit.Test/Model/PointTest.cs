namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class PointTest
	{
		[Test]
		public void PointShouldBeInvalidIfSpatialReferenceIsNull()
		{
			(new Action(() => new Point(10, 10, null))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void PointShouldBeInvalidIfSpatialReferenceIsEqualOrLessThanZero()
		{
			(new Action(() => new Point(10, 10, 0))).Should().Throw<ArgumentException>();
			(new Action(() => new Point(10, 10, -102100))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldThrowExceptionWhenCreatingASimplePointWithInvalidParameters()
		{
			(new Action(() => new Point(double.MinValue, 10, 102100))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void PointShouldBeValidWithCorrectValues()
		{
			var newPoint = new Point(10, 10, 102100);

			newPoint.Should().Not.Be.Null();
			newPoint.GetType().Should().Be<Point>();
			newPoint.X.Should().Be(10);
			newPoint.Y.Should().Be(10);
			newPoint.SpatialReference.WKID.Should().Be(102100);
		}

		[Test]
		public void PointsWithSameValuesShouldBeConsideredEquals()
		{
			var newPoint1 = new Point(10, 10, 102100);
			var newPoint2 = new Point(10, 10, 102100);

			newPoint1.Equals(newPoint2).Should().Be(true);
		}

		[Test]
		public void PointsWithDifferentSpatialReferenceShouldBeConsideredNotEquals()
		{
			var newPoint1 = new Point(10, 10, 102100);
			var newPoint2 = new Point(10, 10, 4928);

			newPoint1.Equals(newPoint2).Should().Be(false);
		}

		[Test]
		public void PointsWithDifferentValuesShouldBeConsideredNotEquals()
		{
			var newPoint1 = new Point(10, 10, 102100);
			var newPoint2 = new Point(23, 14, 102100);

			newPoint1.Equals(newPoint2).Should().Be(false);
		}
	}
}
