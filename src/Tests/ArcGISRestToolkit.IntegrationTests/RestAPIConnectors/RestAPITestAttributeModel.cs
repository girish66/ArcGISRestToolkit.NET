namespace ArcGISRestToolkit.IntegrationTests.RestAPIConnectors
{
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.RestAPI;

	[DataContract]
	public class RestAPITestAttributeModel : IRestAPIAttribute
	{
		[DataMember(Name = "objectid")]
		public long ID { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }
	}
}
