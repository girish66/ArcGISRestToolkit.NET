namespace ArcGISRestToolkit.Helper
{
	using System.IO;
	using System.Runtime.Serialization.Json;
	using System.Text;

	public class Json
	{
		public static string Serialize<T>(T obj)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));
			using (var mem = new MemoryStream())
			{
				serializer.WriteObject(mem, obj);
				return Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
			}
		}

		public static T Deserialize<T>(string jsonString)
		{
			var binaryData = Encoding.UTF8.GetBytes(jsonString);
			using (var ms = new MemoryStream(binaryData))
			{
				var serializer = new DataContractJsonSerializer(typeof(T));
				return (T)serializer.ReadObject(ms);
			}
		}
	}
}