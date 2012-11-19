namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIResponseErrorDetail
	{
		[DataMember(Name = "code")]
		public int Code { get; set; }

		[DataMember(Name = "message")]
		public string Message { get; set; }

		[DataMember(Name = "details")]
		public List<string> Details { get; set; }
	}
}
