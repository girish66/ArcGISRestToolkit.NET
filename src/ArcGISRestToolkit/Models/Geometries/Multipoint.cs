namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;

	[DataContract]
	public class Multipoint : IRestAPIGeometry
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;
		private const int MINIMUM_AMMOUNT_OF_POINTS = 1;

		private ICollection<SimplePoint> _points;

		[DataMember(Name = "points")]
		private double[][] _pointsArray;

		public List<SimplePoint> Points { get { return this._points.ToList(); } }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }

		#endregion

		#region Constructor

		public Multipoint(ICollection<SimplePoint> points)
		{
			if (points == null) { throw new ArgumentNullException("points"); }
			if (points.Any(x => x == null)) { throw new ArgumentException("points", ErrorMessages.msgMultipoint_Error_NoNullPointAllowedInTheCollection); }
			if (points.Count < MINIMUM_AMMOUNT_OF_POINTS) { throw new ArgumentException("points", ErrorMessages.msgMultipoint_Error_AtLeastOnePointInTheCollection); }

			this._points = points;
		}

		#endregion

		#region Public Methods

		public double[][] ToArray()
		{
			double[][] multipointArray = new double[this.Points.Count][];
			for (var i = 0; i < this.Points.Count; i++)
			{
				multipointArray[i] = this.Points.ElementAt(i).ToArray();
			}
			return multipointArray;
		}

		public override int GetHashCode()
		{
			return this.Points.Aggregate(HASH_PRIME_NUMBER_SEED, (current, point) => current * point.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			var multipointToCompare = obj as Multipoint;
			if (multipointToCompare == null) { return false; }
			if (multipointToCompare.Points.Count != this.Points.Count) { return false; }

			var areAllPointsEquals = true;
			for (var i = 0; i < this.Points.Count; i++)
			{
				if (!this.Points.ElementAt(i).Equals(multipointToCompare.Points.ElementAt(i)))
				{
					areAllPointsEquals = false;
				}
			}

			return areAllPointsEquals;
		}

		#endregion

		#region Internal Methods

		[OnSerializing]
		internal void OnSerializing(StreamingContext context)
		{
			this._pointsArray = this.ToArray();
		}

		[OnDeserialized]
		internal void OnDeserialized(StreamingContext context)
		{
			this._points = new List<SimplePoint>();
			foreach (var currentPoint in this._pointsArray.Select(point => new SimplePoint(point[0], point[1])))
			{
				this._points.Add(currentPoint);
			}

			if (this.Points.Any(x => x == null)) { throw new InvalidOperationException(ErrorMessages.msgMultipoint_Error_NoNullPointAllowedInTheCollection); }
		}

		#endregion
	}
}
