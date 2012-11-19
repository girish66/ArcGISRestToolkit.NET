namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIDeleteOperationResponse
	{
		#region Fields and Properties

		[DataMember(Name = "deleteResults")]
		public List<RestAPIOperationResponseBase> Responses { get; set; }

		#endregion
	}
}
