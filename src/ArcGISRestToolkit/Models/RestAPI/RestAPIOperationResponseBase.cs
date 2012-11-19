namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIOperationResponseBase
	{
		#region Fields and Properties

		[DataMember(Name = "objectId")]
		public string ObjectID { get; set; }

		[DataMember(Name = "globalId")]
		public string GlobalID { get; set; }

		[DataMember(Name = "success")]
		public bool Success { get; set; }

		[DataMember(Name = "error")]
		public RestAPIOperationResponseError Error { get; set; }

		#endregion
	}
}
