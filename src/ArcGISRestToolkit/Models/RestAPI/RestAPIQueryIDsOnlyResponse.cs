namespace ArcGISRestToolkit.Models.RestAPI
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class RestAPIQueryIDsOnlyResponse
	{
		[DataMember(Name = "objectIdFieldName")]
		public string IdFieldName { get; set; }

		[DataMember(Name = "objectIds")]
		public List<long> ObjectIDs { get; set; }
	}
}
