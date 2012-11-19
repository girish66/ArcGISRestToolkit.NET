namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using ArcGISRestToolkit.Resource;

	public class Path
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;
		private const int MINIMUM_AMMOUNT_OF_POINTS = 2;

		private readonly IList<SimplePoint> _points;

		public IList<SimplePoint> Points
		{
			get
			{
				return this._points.ToList();
			}
		}

		#endregion

		#region Constructor

		public Path(IList<SimplePoint> points)
		{
			if (points == null) { throw new ArgumentNullException("points"); }
			if (points.Any(x => x == null)) { throw new ArgumentException("points", ErrorMessages.msgPath_Error_NoNullPointAllowedInTheCollection); }
			if (points.Count < MINIMUM_AMMOUNT_OF_POINTS) { throw new ArgumentException("points", ErrorMessages.msgPath_Error_AtLeastTwoPointsInTheCollection); }
			if (points.First().Equals(points.Last())) { throw new ArgumentException("points", ErrorMessages.msgPath_Error_FirstAndLastPointInTheCollectionShouldNotBeTheSame); }

			this._points = points;
		}

		#endregion

		#region Public Methods

		public double[][] ToArray()
		{
			double[][] pathArray = new double[this._points.Count][];
			for (var i = 0; i < this._points.Count; i++)
			{
				pathArray[i] = this._points.ElementAt(i).ToArray();
			}
			return pathArray;
		}
		
		public override bool Equals(object obj)
		{
			var pathToCompare = obj as Path;
			if (pathToCompare == null) { return false; }
			if (pathToCompare.Points.Count != this.Points.Count) { return false; }

			var areAllPointsEquals = true;
			for (var i = 0; i < this.Points.Count; i++)
			{
				if (!this.Points.ElementAt(i).Equals(pathToCompare.Points.ElementAt(i)))
				{
					areAllPointsEquals = false;
				}
			}

			return areAllPointsEquals;
		}

		public override int GetHashCode()
		{
			return this._points.Aggregate(HASH_PRIME_NUMBER_SEED, (current, point) => current * point.GetHashCode());
		}

		#endregion
	}
}
