namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class MultipointTest
	{
		#region Fields and Properties

		private readonly List<SimplePoint> _pointList = new List<SimplePoint>
		{
			new SimplePoint(-97.06138, 32.837),
			new SimplePoint(-97.06133, 32.836),
			new SimplePoint(-97.06124, 32.834),
			new SimplePoint(-97.06127, 32.832)
		};

		private readonly List<SimplePoint> _invalidPointList = new List<SimplePoint>
		{
			new SimplePoint(-97.06138, 32.837),
			null,
			new SimplePoint(-97.06124, 32.834),
			new SimplePoint(-97.06127, 32.832)
		};

		#endregion

		[Test]
		public void MultipointShouldNotAcceptNullPointList()
		{
			(new Action(() => new Multipoint(null))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void MultipointShouldNotAcceptAnEmptyPointList()
		{
			(new Action(() => new Multipoint(new List<SimplePoint>()))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void MultipointShouldNotAcceptAPointListWithANullObject()
		{
			(new Action(() => new Multipoint(_invalidPointList))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldCreateAProperMultipointWithTheCorrectlyParameters()
		{
			var newMultipoint = new Multipoint(_pointList);
			newMultipoint.Should()
				.Not.Be.Null()
				.And.Be.OfType<Multipoint>();
			newMultipoint.Points.Should().Not.Be.Null();
			newMultipoint.Points.Count.Should().Be(4);
			newMultipoint.Points[0].Should().Not.Be.Null();
			newMultipoint.Points[0].Equals(new SimplePoint(-97.06138, 32.837)).Should().Be(true);
			newMultipoint.Points[1].Should().Not.Be.Null();
			newMultipoint.Points[1].Equals(new SimplePoint(-97.06133, 32.836)).Should().Be(true);
			newMultipoint.Points[2].Should().Not.Be.Null();
			newMultipoint.Points[2].Equals(new SimplePoint(-97.06124, 32.834)).Should().Be(true);
			newMultipoint.Points[3].Should().Not.Be.Null();
			newMultipoint.Points[3].Equals(new SimplePoint(-97.06127, 32.832)).Should().Be(true);
		}

		[Test]
		public void ShouldCreateAProperArray()
		{
			var newMultipoint = new Multipoint(_pointList);
			var polygonArray = newMultipoint.ToArray();
			
			polygonArray.Should().Not.Be.Null();
			polygonArray.GetType().Should().Be<double[][]>();
			polygonArray.Length.Should().Be(4);

			polygonArray[0].Length.Should().Be(2);
			polygonArray[1].Length.Should().Be(2);
		}
	}
}
