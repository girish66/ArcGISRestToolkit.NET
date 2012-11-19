namespace ArcGISRestToolkit.Models.AGOToken
{
	using System;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Helper;

	[DataContract]
	public class AGOTokenManagerResponse
	{
		[DataMember(Name = "token")]
		public string Token { get; set; }

		[DataMember(Name = "expires")]
		public long? ExpiresUnixTimestamp { get; set; }

		[DataMember(Name = "ssl")]
		public bool Ssl { get; set; }

		public DateTime? Expires { get; set; }

		#region Internal Methods

		[OnSerializing]
		internal void OnSerializing(StreamingContext context)
		{
			this.ExpiresUnixTimestamp = this.Expires.HasValue ? this.Expires.Value.ConvertToUnixTimestamp() : (long?)null;
		}

		[OnDeserialized]
		internal void OnDeserialized(StreamingContext context)
		{
			this.Expires = this.ExpiresUnixTimestamp.HasValue ? this.ExpiresUnixTimestamp.Value.ConvertFromUnixTimestamp() : (DateTime?)null;
		}

		#endregion
	}
}
