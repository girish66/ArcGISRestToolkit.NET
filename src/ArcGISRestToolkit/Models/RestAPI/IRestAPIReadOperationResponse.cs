namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;

	public interface IRestAPIReadOperationResponse<TF, TG, TA>
		where TF : IRestAPIFeature<TG, TA>
		where TG : IRestAPIGeometry
		where TA : IRestAPIAttribute
	{
		string ObjectIDFieldName { get; set; }
		string GlobalIDFieldName { get; set; }
		string GeometryType { get; set; }
		SpatialReference SpatialReference { get; set; }
		List<Field> Fields { get; set; }
		List<TF> Features { get; set; }
	}
}
