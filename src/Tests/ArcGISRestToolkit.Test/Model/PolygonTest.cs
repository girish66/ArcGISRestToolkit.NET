namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class PolygonTest
	{
		#region Fields and Properties

		private readonly Ring _validRing1 = new Ring(new List<SimplePoint>
		{
			new SimplePoint(-97.06138, 32.837),
			new SimplePoint(-97.06133, 32.836),
			new SimplePoint(-97.06124, 32.834),
			new SimplePoint(-97.06127, 32.832),
			new SimplePoint(-97.06138, 32.837)
		});

		private readonly Ring _validRing2 = new Ring(new List<SimplePoint>
		{
			new SimplePoint(-97.06326, 32.759),
			new SimplePoint(-97.06298, 32.755),
			new SimplePoint(-97.06153, 32.749),
			new SimplePoint(-97.06326, 32.759)
		});

		#endregion

		[Test]
		public void PolygonShouldNotAcceptNullPointList()
		{
			(new Action(() => new Polygon(null))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void PolygonShouldNotAcceptAnEmptyRingList()
		{
			(new Action(() => new Polygon(new List<Ring>()))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void PolygonShouldNotAcceptRingListWithANullObject()
		{
			(new Action(() => new Polygon(new List<Ring> { this._validRing1, null }))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldCreateAProperPolygonWithTheCorrectlyParameters()
		{
			var newPolygon = new Polygon(new List<Ring> { this._validRing1, this._validRing2 });
			newPolygon.Should()
				.Not.Be.Null()
				.And.Be.OfType<Polygon>();
			newPolygon.Rings.Should().Not.Be.Null();
			newPolygon.Rings.Count.Should().Be(2);
			newPolygon.Rings[0].Should().Not.Be.Null();
			newPolygon.Rings[0].Equals(this._validRing1).Should().Be(true);
			newPolygon.Rings[1].Should().Not.Be.Null();
			newPolygon.Rings[1].Equals(this._validRing2).Should().Be(true);
		}

		[Test]
		public void ShouldCreateAProperArray()
		{
			var newPolygon = new Polygon(new List<Ring> { this._validRing1, this._validRing2 });
			var polygonArray = newPolygon.ToArray();
			
			polygonArray.Should().Not.Be.Null();
			polygonArray.GetType().Should().Be<double[][][]>();
			polygonArray.Length.Should().Be(2);

			polygonArray[0].Length.Should().Be(5);
			polygonArray[1].Length.Should().Be(4);
		}
	}
}
