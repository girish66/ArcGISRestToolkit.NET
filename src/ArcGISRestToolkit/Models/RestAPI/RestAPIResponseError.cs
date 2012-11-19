namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIResponseError
	{
		#region Fields and Properties

		[DataMember(Name = "error")]
		public RestAPIResponseErrorDetail Error { get; set; }

		#endregion
	}
}
