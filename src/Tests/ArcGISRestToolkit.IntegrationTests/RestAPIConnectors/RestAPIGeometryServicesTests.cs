namespace ArcGISRestToolkit.IntegrationTests.RestAPIConnectors
{
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using ArcGISRestToolkit.RestAPIConnectors;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class RestAPIGeometryServicesTests
	{
		private const string GEOMETRY_SERVICE_URL = "http://tasks.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer";
		private const int WKID_WGS84 = 4326;
		private const int WKID_WEBMERCATOR = 102100;

		[Test]
		public void ShouldReprojectProperly()
		{
			var outputSpatialReference = new SpatialReference(WKID_WEBMERCATOR);
			var pointsToReproject = new List<Point>
			{
				new Point(-104.53, 34.74, WKID_WGS84),
				new Point(-63.53, 10.23, WKID_WGS84),
				new Point(-117, 34, WKID_WGS84)
			};

			var geometryServices = new RestAPIGeometryServices<Point>(GEOMETRY_SERVICE_URL);
			var result = geometryServices.Reproject(pointsToReproject, new SpatialReference(WKID_WGS84), outputSpatialReference);

			result.Should().Not.Be.Null().And.Not.Be.Empty();
			result.Count.Should().Be(3);
			result[0].SpatialReference.Should().Be(outputSpatialReference);
			result[1].SpatialReference.Should().Be(outputSpatialReference);
			result[2].SpatialReference.Should().Be(outputSpatialReference);
		}
	}
}
