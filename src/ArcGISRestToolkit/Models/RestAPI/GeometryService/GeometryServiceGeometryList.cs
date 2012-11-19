namespace ArcGISRestToolkit.Models.RestAPI.GeometryService
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.Geometries;

	[DataContract]
	public class GeometryServiceGeometryList<T> where T : IRestAPIGeometry
	{
		#region Fields and Properties
		
		[DataMember(Name = "geometryType")]
		private string GeometryTypeName
		{
			get { return GeometryTypes.Point.GetGeometryTypeName(); }
			set
			{
				var geometryTypeName = value;
				GeometryType = geometryTypeName.GetGeometryType();
			}
		}

		public GeometryTypes GeometryType { get; set; }

		[DataMember(Name = "geometries")]
		public List<T> Geometries { get; set; }

		#endregion
	}
}
