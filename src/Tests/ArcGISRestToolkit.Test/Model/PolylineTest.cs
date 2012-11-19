namespace ArcGISRestToolkit.Test.Model
{
	using System;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class PolylineTest
	{
		#region Fields and Properties

		private readonly Path _validPath1 = new Path(new List<SimplePoint>
		{
			new SimplePoint(-97.06138, 32.837),
			new SimplePoint(-97.06133, 32.836),
			new SimplePoint(-97.06124, 32.834),
			new SimplePoint(-97.06127, 32.832)
		});

		private readonly Path _validPath2 = new Path(new List<SimplePoint>
		{
			new SimplePoint(-97.06326, 32.759),
			new SimplePoint(-97.06298, 32.755),
			new SimplePoint(-97.06153, 32.749)
		});

		#endregion

		[Test]
		public void PolylineShouldNotAcceptNullPointList()
		{
			(new Action(() => new Polyline(null))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void PolylineShouldNotAcceptAnEmptyPathList()
		{
			(new Action(() => new Polyline(new List<Path>()))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void PolylineShouldNotAcceptPathListWithANullObject()
		{
			(new Action(() => new Polyline(new List<Path> { this._validPath1, null }))).Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldCreateAProperPolylineWithTheCorrectlyParameters()
		{
			var newPolyline = new Polyline(new List<Path> { this._validPath1, this._validPath2 });
			newPolyline.Should()
				.Not.Be.Null()
				.And.Be.OfType<Polyline>();
			newPolyline.Paths.Should().Not.Be.Null();
			newPolyline.Paths.Count.Should().Be(2);
			newPolyline.Paths[0].Should().Not.Be.Null();
			newPolyline.Paths[0].Equals(this._validPath1).Should().Be(true);
			newPolyline.Paths[1].Should().Not.Be.Null();
			newPolyline.Paths[1].Equals(this._validPath2).Should().Be(true);
		}

		[Test]
		public void ShouldCreateAProperArray()
		{
			var newPolyline = new Polyline(new List<Path> { this._validPath1, this._validPath2 });
			var polygonArray = newPolyline.ToArray();
			
			polygonArray.Should().Not.Be.Null();
			polygonArray.GetType().Should().Be<double[][][]>();
			polygonArray.Length.Should().Be(2);

			polygonArray[0].Length.Should().Be(4);
			polygonArray[1].Length.Should().Be(3);
		}
	}
}
