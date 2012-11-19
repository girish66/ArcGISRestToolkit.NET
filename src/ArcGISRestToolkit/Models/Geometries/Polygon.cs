namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;

	[DataContract]
	public class Polygon : IRestAPIGeometry
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;
		private const int MINIMUM_AMMOUNT_OF_RINGS = 1;

		private ICollection<Ring> _rings;

		[DataMember(Name = "rings")]
		private double[][][] _ringsArray;

		public List<Ring> Rings { get { return this._rings.ToList(); } }

		[DataMember(Name = "spatialReference")]
		public SpatialReference SpatialReference { get; set; }

		#endregion

		#region Constructor

		public Polygon(ICollection<Ring> rings)
		{
			if (rings == null) { throw new ArgumentNullException("rings"); }
			if (rings.Any(x => x == null)) { throw new ArgumentException("rings", ErrorMessages.msgPolygon_Error_NoNullRingAllowedInTheCollection); }
			if (rings.Count < MINIMUM_AMMOUNT_OF_RINGS) { throw new ArgumentException("rings", ErrorMessages.msgPolygon_Error_AtLeastOneRingInTheCollection); }

			this._rings = rings;
		}

		#endregion

		#region Public Methods

		public double[][][] ToArray()
		{
			double[][][] polygonArray = new double[this.Rings.Count][][];
			for (var i = 0; i < this.Rings.Count; i++)
			{
				polygonArray[i] = this.Rings.ElementAt(i).ToArray();
			}
			return polygonArray;
		}

		public override int GetHashCode()
		{
			return this.Rings.Aggregate(HASH_PRIME_NUMBER_SEED, (current, ring) => current * ring.GetHashCode());
		}

		public override bool Equals(object obj)
		{
			var ringToCompare = obj as Polygon;
			if (ringToCompare == null) { return false; }
			if (ringToCompare.Rings.Count != this.Rings.Count) { return false; }

			var areAllRingsEquals = true;
			for (var i = 0; i < this.Rings.Count; i++)
			{
				if (!this.Rings.ElementAt(i).Equals(ringToCompare.Rings.ElementAt(i)))
				{
					areAllRingsEquals = false;
				}
			}

			return areAllRingsEquals;
		}

		#endregion

		#region Internal Methods

		[OnSerializing]
		internal void OnSerializing(StreamingContext context)
		{
			this._ringsArray = this.ToArray();
		}

		[OnDeserialized]
		internal void OnDeserialized(StreamingContext context)
		{
			this._rings = new List<Ring>();
			foreach (var currentRingPointList in this._ringsArray.Select(
				ring => ring.Select(
					point => new SimplePoint(point[0], point[1])).ToList()))
			{
				this._rings.Add(new Ring(currentRingPointList));
			}

			if (this.Rings.Any(x => x == null)) { throw new InvalidOperationException(ErrorMessages.msgPolygon_Error_NoNullRingAllowedInTheCollection); }
		}

		#endregion
	}
}
