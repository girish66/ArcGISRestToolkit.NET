namespace ArcGISRestToolkit.RestAPIConnectors
{
	using System;
	using System.IO;
	using System.Net;
	using System.Text;

	public class RestAPIBaseWebRequest
	{
		private const string REQUEST_METHOD = "POST";
		private const string REQUEST_CONTENT_TYPE = "application/x-www-form-urlencoded";

		protected static string BaseWebRequest(string requestUrl, string serviceParams)
		{
			var sendGeometriesRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
			sendGeometriesRequest.Method = REQUEST_METHOD;
			sendGeometriesRequest.ContentType = REQUEST_CONTENT_TYPE;

			var encoding = new UTF8Encoding();
			var jsonData = encoding.GetBytes(serviceParams);
			sendGeometriesRequest.ContentLength = jsonData.Length;
			var postStream = sendGeometriesRequest.GetRequestStream();
			postStream.Write(jsonData, 0, jsonData.Length);
			postStream.Close();

			var httpWebResponse = (HttpWebResponse)sendGeometriesRequest.GetResponse();
			var httpWebResponseStream = httpWebResponse.GetResponseStream();

			if (httpWebResponseStream != null)
			{
				using (var responseResult = new StreamReader(httpWebResponseStream))
				{
					return responseResult.ReadToEnd();
				}
			}
			return null;
		}
	}
}
