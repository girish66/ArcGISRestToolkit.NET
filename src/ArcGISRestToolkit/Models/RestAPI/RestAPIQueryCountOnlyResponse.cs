namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIQueryCountOnlyResponse
	{
		[DataMember(Name = "count")]
		public int Count { get; set; }
	}
}
