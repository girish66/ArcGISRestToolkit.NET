namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class RingTest
	{
		[Test]
		public void RingShouldNotAcceptNullPointList()
		{
			(new Action(() => new Ring(null))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void RingShouldNotAcceptPointListWithANullObject()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(5, 5),
				new SimplePoint(5, 10),
				null,
				new SimplePoint(5, 5)
			};

			(new Action(() => new Ring(listPoints))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void RingShouldNotAcceptPointListWithLessThanThreePoints()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20)
			};

			(new Action(() => new Ring(listPoints))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void RingShouldNotAcceptPointListWhereTheFirstAndLastPointsAreNotTheSame()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(15, 20)
			};

			(new Action(() => new Ring(listPoints))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldCreateAProperRingWithTheCorrectArguments()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			(new Action(() => new Ring(listPoints))).Should().NotThrow();

			var testRing = new Ring(listPoints);
			testRing.Should().Not.Be.Null();
			testRing.Points.Should().Not.Be.Null();
			testRing.Points.Count.Should().Be(4);
		}

		[Test]
		public void TwoRingsWithTheSamePointsAndInTheSameOrderShouldBeEquals()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 18),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			var testRing1 = new Ring(listPoints);
			var testRing2 = new Ring(listPoints);

			testRing1.Equals(testRing2).Should().Be(true);
		}

		[Test]
		public void TwoRingsWithTheSamePointsButInDifferentOrderShouldNotBeEquals()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 18),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			var reversedPointList = listPoints.Reverse<SimplePoint>().ToList();

			var testRing1 = new Ring(listPoints);
			var testRing2 = new Ring(reversedPointList);

			testRing1.Equals(testRing2).Should().Be(false);
		}

		[Test]
		public void TwoRingsWithTheDifferentPointsShouldNotBeEquals()
		{
			var listPoints1 = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 18),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			var listPoints2 = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			var listPoints3 = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 17),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			var testRing1 = new Ring(listPoints1);
			var testRing2 = new Ring(listPoints2);
			var testRing3 = new Ring(listPoints3);

			testRing1.Equals(testRing2).Should().Be(false);
			testRing1.Equals(testRing3).Should().Be(false);
			testRing2.Equals(testRing3).Should().Be(false);
		}

		[Test]
		public void ShouldCreateAProperArrayWithPoints()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			var testRing = new Ring(listPoints);
			var ringArray = testRing.ToArray();

			ringArray.Should().Not.Be.Null();
			ringArray.GetType().Should().Be<double[][]>();
			ringArray.Length.Should().Be(4);

			ringArray[0].Length.Should().Be(2);
			ringArray[1].Length.Should().Be(2);
			ringArray[2].Length.Should().Be(2);
			ringArray[3].Length.Should().Be(2);

			ringArray[0][0].Should().Be(10);
			ringArray[0][1].Should().Be(20);
			ringArray[1][0].Should().Be(15);
			ringArray[1][1].Should().Be(20);
			ringArray[2][0].Should().Be(20);
			ringArray[2][1].Should().Be(20);
			ringArray[3][0].Should().Be(10);
			ringArray[3][1].Should().Be(20);
		}
	}
}
