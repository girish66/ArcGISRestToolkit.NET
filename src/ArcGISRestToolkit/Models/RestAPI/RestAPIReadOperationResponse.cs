namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.Geometries;

	[DataContract]
	public class RestAPIReadOperationResponse<TF, TG, TA> : IRestAPIReadOperationResponse<TF, TG, TA>
		where TF : IRestAPIFeature<TG, TA>
		where TG : IRestAPIGeometry
		where TA : IRestAPIAttribute
	{
		[DataMember(Name = "objectIdFieldName")]
		public string ObjectIDFieldName { get; set; }

		[DataMember(Name = "globalIdFieldName")]
		public string GlobalIDFieldName { get; set; }

		[DataMember(Name = "geometryType")]
		public string GeometryType { get; set; }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }

		[DataMember(Name = "fields")]
		public List<Field> Fields { get; set; }

		[DataMember(Name = "features")]
		public List<TF> Features { get; set; }
	}
}
