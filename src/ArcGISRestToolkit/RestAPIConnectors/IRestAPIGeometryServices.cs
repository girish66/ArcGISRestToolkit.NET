namespace ArcGISRestToolkit.RestAPIConnectors
{
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;

	public interface IRestAPIGeometryServices<T>
	{
		List<T> Reproject(List<T> geometriesToReproject, SpatialReference inputSpatialReference, SpatialReference outputSpatialReference);
	}
}
