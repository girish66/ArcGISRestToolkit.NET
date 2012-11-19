namespace ArcGISRestToolkit.Models.Geometries
{
	using System.Runtime.Serialization;

	[DataContract]
	public class Field
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "alias")]
		public string Alias { get; set; }

		[DataMember(Name = "sqlType", IsRequired = false)]
		public string SqlType { get; set; }

		[DataMember(Name = "length", IsRequired = false)]
		public int Length { get; set; }

		[DataMember(Name = "domain", IsRequired = false)]
		public string Domain { get; set; }

		[DataMember(Name = "defaultValue", IsRequired = false)]
		public string DefaultValue { get; set; }
	}
}
