namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIFeature<TG, TA> : IRestAPIFeature<TG, TA>
		where TG : IRestAPIGeometry
		where TA : IRestAPIAttribute
	{
		#region Fields and Properties

		[DataMember(Name = "geometry")]
		public TG Geometry { get; set; }

		[DataMember(Name = "attributes")]
		public TA Attributes { get; set; }

		#endregion
	}
}
