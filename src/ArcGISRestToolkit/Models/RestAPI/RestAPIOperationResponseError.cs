namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIOperationResponseError
	{
		[DataMember(Name = "code")]
		public int Code { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }
	}
}
