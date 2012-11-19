namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class PathTest
	{
		[Test]
		public void PathShouldNotAcceptNullPointList()
		{
			(new Action(() => new Path(null))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void PathShouldNotAcceptPointListWithANullObject()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(5, 5),
				new SimplePoint(5, 10),
				null,
				new SimplePoint(5, 25)
			};

			(new Action(() => new Path(listPoints))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void PathShouldNotAcceptPointListWithLessThanTwoPoints()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20)
			};

			(new Action(() => new Path(listPoints))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void PathShouldNotAcceptPointListWhereTheFirstAndLastPointsAreTheSame()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(10, 20)
			};

			(new Action(() => new Path(listPoints))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldCreateAProperPathWithTheCorrectArguments()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			(new Action(() => new Path(listPoints))).Should().NotThrow();

			var testPath = new Path(listPoints);
			testPath.Should().Not.Be.Null();
			testPath.Points.Should().Not.Be.Null();
			testPath.Points.Count.Should().Be(4);
		}

		[Test]
		public void TwoPathsWithTheSamePointsAndInTheSameOrderShouldBeEquals()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 18),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			var testPath1 = new Path(listPoints);
			var testPath2 = new Path(listPoints);

			testPath1.Equals(testPath2).Should().Be(true);
		}

		[Test]
		public void TwoPathsWithTheSamePointsButInDifferentOrderShouldNotBeEquals()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 18),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			var reversedPointList = listPoints.Reverse<SimplePoint>().ToList();

			var testPath1 = new Path(listPoints);
			var testPath2 = new Path(reversedPointList);

			testPath1.Equals(testPath2).Should().Be(false);
		}

		[Test]
		public void TwoPathsWithTheDifferentPointsShouldNotBeEquals()
		{
			var listPoints1 = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 18),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			var listPoints2 = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			var listPoints3 = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(15, 17),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			var testPath1 = new Path(listPoints1);
			var testPath2 = new Path(listPoints2);
			var testPath3 = new Path(listPoints3);

			testPath1.Equals(testPath2).Should().Be(false);
			testPath1.Equals(testPath3).Should().Be(false);
			testPath2.Equals(testPath3).Should().Be(false);
		}

		[Test]
		public void ShouldCreateAProperArrayWithPoints()
		{
			var listPoints = new List<SimplePoint>
			{
				new SimplePoint(10, 20),
				new SimplePoint(15, 20),
				new SimplePoint(20, 20),
				new SimplePoint(15, 25)
			};

			var testPath = new Path(listPoints);
			var ringArray = testPath.ToArray();

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
			ringArray[3][0].Should().Be(15);
			ringArray[3][1].Should().Be(25);
		}
	}
}
