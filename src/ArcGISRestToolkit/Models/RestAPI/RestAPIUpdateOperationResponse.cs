namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIUpdateOperationResponse
	{
		#region Fields and Properties

		[DataMember(Name = "updateResults")]
		public List<RestAPIOperationResponseBase> Responses { get; set; }

		#endregion
	}
}
