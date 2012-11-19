namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIAddOperationResponse
	{
		#region Fields and Properties

		[DataMember(Name = "addResults")]
		public List<RestAPIOperationResponseBase> Responses { get; set; }

		#endregion
	}
}
