namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;

	[DataContract]
	public class Polyline : IRestAPIGeometry
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;
		private const int MINIMUM_AMMOUNT_OF_PATHS = 1;

		private ICollection<Path> _paths;

		[DataMember(Name = "paths")]
		private double[][][] _pathsArray;

		public List<Path> Paths { get { return this._paths.ToList(); } }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }

		#endregion

		#region Constructor

		public Polyline(ICollection<Path> paths)
		{
			if (paths == null) { throw new ArgumentNullException("paths"); }
			if (paths.Any(x => x == null)) { throw new ArgumentException("paths", ErrorMessages.msgPolyline_Error_NoNullPathAllowedInTheCollection); }
			if (paths.Count < MINIMUM_AMMOUNT_OF_PATHS) { throw new ArgumentException("paths", ErrorMessages.msgPolyline_Error_AtLeastOnePathInTheCollection); }

			this._paths = paths;
		}

		#endregion

		#region Public Methods

		public double[][][] ToArray()
		{
			double[][][] polylineArray = new double[this.Paths.Count][][];
			for (var i = 0; i < this.Paths.Count; i++)
			{
				polylineArray[i] = this.Paths.ElementAt(i).ToArray();
			}
			return polylineArray;
		}

		public override int GetHashCode()
		{
			return this.Paths.Aggregate(HASH_PRIME_NUMBER_SEED, (current, ring) => current * ring.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			var pathToCompare = obj as Polyline;
			if (pathToCompare == null) { return false; }
			if (pathToCompare.Paths.Count != this.Paths.Count) { return false; }

			var areAllPathsEquals = true;
			for (var i = 0; i < this.Paths.Count; i++)
			{
				if (!this.Paths.ElementAt(i).Equals(pathToCompare.Paths.ElementAt(i)))
				{
					areAllPathsEquals = false;
				}
			}

			return areAllPathsEquals;
		}

		#endregion

		#region Internal Methods

		[OnSerializing]
		internal void OnSerializing(StreamingContext context)
		{
			this._pathsArray = this.ToArray();
		}

		[OnDeserialized]
		internal void OnDeserialized(StreamingContext context)
		{
			this._paths = new List<Path>();
			foreach (var currentPathPointList in this._pathsArray.Select(
				path => path.Select(
					point => new SimplePoint(point[0], point[1])).ToList()))
			{
				this._paths.Add(new Path(currentPathPointList));
			}

			if (this.Paths.Any(x => x == null)) { throw new InvalidOperationException(ErrorMessages.msgPolyline_Error_NoNullPathAllowedInTheCollection); }
		}

		#endregion
	}
}
