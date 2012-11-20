namespace ArcGISRestToolkit.IntegrationTests.RestAPIConnectors
{
	using System;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.Geometries;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.RestAPIConnectors;
	using NUnit.Framework;
	using SharpTestsEx;

	[TestFixture]
	public class RestAPIFeatureServiceConnectorTests
	{
		#region Fields and Properties

		private const string TEST_FEATURE_URL = "http://sampleserver3.arcgisonline.com/ArcGIS/rest/services/Fire/Sheep/FeatureServer/2";

		readonly SimplePoint _point1 = new SimplePoint(10.162353515000063, 55.354135310000567);
		readonly SimplePoint _point2 = new SimplePoint(10.178353515000053, 55.368725310000061);
		readonly SimplePoint _point3 = new SimplePoint(10.256687515000078, 55.368725310000061);

		#endregion

		#region Tests

		[Test]
		public void ShouldRetriveTestFeaturesFromTheSampleFeatureServiceAtArcGISOnline()
		{
			var restAPIConnector = new RestAPIFeatureServiceConnector<RestAPIFeature<Polygon, RestAPITestAttributeModel>, Polygon, RestAPITestAttributeModel>(TEST_FEATURE_URL);

			var result = restAPIConnector.QueryFeaturesByWhereClause("0=0");

			result.Should().Not.Be.Null();
			result.Count.Should().Be.GreaterThan(0);
		}

		[Test]
		public void ShouldCreateANewFeatureInTheSampleFeatureServiceAtArcGISOnline()
		{
			List<RestAPIOperationResponseBase> responseList;
			var featuresToAddList = CreateFeaturesList();
			var testName = featuresToAddList[0].Attributes.Name;

			var restAPIConnector = new RestAPIFeatureServiceConnector<RestAPIFeature<Polygon, RestAPITestAttributeModel>, Polygon, RestAPITestAttributeModel>(TEST_FEATURE_URL);
			var sucess = restAPIConnector.AddFeatures(featuresToAddList, out responseList);

			sucess.Should().Be.True();
			responseList.Should().Not.Be.Null();
			responseList.Count.Should().Be(1);
			responseList.ForEach(x => x.Success.Should().Be.True());

			var result = restAPIConnector.QueryFeaturesByWhereClause(string.Format("name='{0}'", testName));
			result.Should().Not.Be.Null();
			result.Count.Should().Be(1);
			result[0].Attributes.Name.Should().Be(testName);
			result[0].Attributes.Description.Should().Be(testName);
		}

		[Test]
		public void ShouldUpdateAnExistentFeatureInTheSampleFeatureServiceAtArcGISOnline()
		{
			List<RestAPIOperationResponseBase> responseList;
			var featuresList = CreateFeaturesList();
			var testName = featuresList[0].Attributes.Name;

			var restAPIConnector = new RestAPIFeatureServiceConnector<RestAPIFeature<Polygon, RestAPITestAttributeModel>, Polygon, RestAPITestAttributeModel>(TEST_FEATURE_URL);
			var sucessAdd = restAPIConnector.AddFeatures(featuresList, out responseList);
			sucessAdd.Should().Be.True();

			var result = restAPIConnector.QueryFeaturesByWhereClause(string.Format("name='{0}'", testName));
			result.Should().Not.Be.Null();
			result[0].Attributes.Name.Should().Be(testName);

			var newName = CreateFeatureName();
			result[0].Attributes.Name = newName;

			var sucessUpdate = restAPIConnector.UpdateFeatures(result, out responseList);
			sucessUpdate.Should().Be.True();

			var result2 = restAPIConnector.QueryFeaturesByWhereClause(string.Format("name='{0}'", newName));
			result2.Should().Not.Be.Null();
			result2[0].Attributes.Name.Should().Be(newName);
		}

		[Test]
		public void ShouldDeleteAnExistentFeatureInTheSampleFeatureServiceAtArcGISOnline()
		{
			List<RestAPIOperationResponseBase> responseList;
			var featuresList = CreateFeaturesList();
			var testName = featuresList[0].Attributes.Name;

			var restAPIConnector = new RestAPIFeatureServiceConnector<RestAPIFeature<Polygon, RestAPITestAttributeModel>, Polygon, RestAPITestAttributeModel>(TEST_FEATURE_URL);
			var sucessAdd = restAPIConnector.AddFeatures(featuresList, out responseList);
			sucessAdd.Should().Be.True();

			var result = restAPIConnector.QueryFeaturesByWhereClause(string.Format("name='{0}'", testName));
			result.Should().Not.Be.Null();
			result[0].Attributes.Name.Should().Be(testName);

			var featuresToDelete = new List<long> { result[0].Attributes.ID };

			var sucessDelete = restAPIConnector.DeleteFeatures(featuresToDelete, out responseList);
			sucessDelete.Should().Be.True();

			var result2 = restAPIConnector.QueryFeaturesByWhereClause(string.Format("name='{0}'", testName));
			result2.Should().Not.Be.Null();
			result2.Count.Should().Be(0);
		}

		//[Test]
		public void DeleteAllItems()
		{
			var restAPIConnector = new RestAPIFeatureServiceConnector<RestAPIFeature<Polygon, RestAPITestAttributeModel>, Polygon, RestAPITestAttributeModel>(TEST_FEATURE_URL);
			var result = restAPIConnector.QueryFeaturesIDsOnly("0=0");
			
			List<RestAPIOperationResponseBase> responseList;
			var deleteResult = restAPIConnector.DeleteFeatures(result.ObjectIDs, out responseList);
			deleteResult.Should().Be(true);
			responseList.Should().Not.Be.Null();
			responseList.Count.Should().Be.GreaterThan(0);

			var result2 = restAPIConnector.QueryFeaturesIDsOnly("0=0");
			result2.ObjectIDs.Count.Should().Be(0);
		}

		#endregion

		#region Auxiliary Methods

		private RestAPIFeature<Polygon, RestAPITestAttributeModel> CreateSingleFeature()
		{
			var testName = CreateFeatureName();

			var ring = new Ring(new List<SimplePoint> { this._point1, this._point2, this._point3, this._point1 });
			var polygonToAdd = new Polygon(new List<Ring> { ring });

			var attributesToAdd = new RestAPITestAttributeModel
			{
				Name = testName,
				Description = testName
			};

			var featureToAdd = new RestAPIFeature<Polygon, RestAPITestAttributeModel>
			{
				Attributes = attributesToAdd,
				Geometry = polygonToAdd
			};

			return featureToAdd;
		}

		private List<RestAPIFeature<Polygon, RestAPITestAttributeModel>> CreateFeaturesList()
		{
			var featuresToAddList = new List<RestAPIFeature<Polygon, RestAPITestAttributeModel>>
			{
				CreateSingleFeature()
			};

			return featuresToAddList;
		}

		private string CreateFeatureName()
		{
			return string.Format("TestRestAPIConnector{0}", DateTime.Now.Ticks);
		}

		#endregion
	}
}
