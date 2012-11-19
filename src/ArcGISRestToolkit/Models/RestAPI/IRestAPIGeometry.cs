namespace ArcGISRestToolkit.Models.RestAPI
{
	using ArcGISRestToolkit.Models.Geometries;

	public interface IRestAPIGeometry
	{
		SpatialReference SpatialReference { get; set; }
	}
}
