namespace ArcGISRestToolkit.RestAPIConnectors
{
	using System;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Helper;
	using ArcGISRestToolkit.Models.Geometries;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Models.RestAPI.GeometryService;

	public class RestAPIGeometryServices<T> : RestAPIBaseWebRequest, IRestAPIGeometryServices<T> where T : IRestAPIGeometry
	{
		#region Fields and Properties

		private const string PROJECT_FEATURES_URL_SECTION = "/project";
		private const string PROJECT_FEATURES_REQUEST_PARAMS = "inSR={0}&outSR={1}&geometries={2}&f=pjson";

		private readonly string _geometryServiceUrl;

		#endregion

		#region Constructor

		public RestAPIGeometryServices(string geometryServiceUrl)
		{
			if (string.IsNullOrEmpty(geometryServiceUrl)) { throw new ArgumentNullException("geometryServiceUrl"); }
			this._geometryServiceUrl = geometryServiceUrl;
		}

		#endregion

		#region Public Methods

		public List<T> Reproject(List<T> geometriesToReproject, SpatialReference inputSpatialReference, SpatialReference outputSpatialReference)
		{
			if (geometriesToReproject == null) { throw new ArgumentNullException("geometriesToReproject"); }
			if (inputSpatialReference == null) { throw new ArgumentNullException("inputSpatialReference"); }
			if (outputSpatialReference == null) { throw new ArgumentNullException("outputSpatialReference"); }

			var geometryServiceUrl = string.Format("{0}{1}", this._geometryServiceUrl, PROJECT_FEATURES_URL_SECTION);

			var geometryServiceReprojectFeaturesList = new GeometryServiceGeometryList<T>
			{
				Geometries = geometriesToReproject
			};

			var serviceParams = string.Format(PROJECT_FEATURES_REQUEST_PARAMS,
				inputSpatialReference.WKID,
				outputSpatialReference.WKID,
				Json.Serialize(geometryServiceReprojectFeaturesList));

			var httpWebResponse = BaseWebRequest(geometryServiceUrl, serviceParams);
			if (httpWebResponse != null)
			{
				var result = Json.Deserialize<GeometryServiceGeometryList<T>>(httpWebResponse);
				result.Geometries.ForEach(x => x.SpatialReference = outputSpatialReference);
				return result.Geometries;
			}

			return null;
		}

		#endregion

		#region Private Methods

		#endregion
	}
}
