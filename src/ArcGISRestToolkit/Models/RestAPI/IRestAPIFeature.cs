namespace ArcGISRestToolkit.Models.RestAPI
{
	public interface IRestAPIFeature<TG, TA>
		where TG : IRestAPIGeometry
		where TA : IRestAPIAttribute
	{
		TG Geometry { get; set; }
		TA Attributes { get; set; }
	}
}
