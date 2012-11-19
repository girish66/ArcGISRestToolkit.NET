namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Resource;

	[DataContract]
	public class SimplePoint
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;

		[DataMember(Name = "x")]
		public double X { get; private set; }

		[DataMember(Name = "y")]
		public double Y { get; private set; }

		#endregion

		#region Constructor

		public SimplePoint(double x, double y)
		{
			if (x.Equals(double.MinValue)) { throw new ArgumentException("x", ErrorMessages.msgSimplePoint_Error_PointCoordinatesCannotBeInvalid); }
			if (y.Equals(double.MinValue)) { throw new ArgumentException("y", ErrorMessages.msgSimplePoint_Error_PointCoordinatesCannotBeInvalid); }

			this.X = x;
			this.Y = y;
		}

		#endregion

		#region Public Methods
		
		public double[] ToArray()
		{
			double[] pointArray = new[] { this.X, this.Y };
			return pointArray;
		}
		
		public override bool Equals(object obj)
		{
			var simplePointToCompare = obj as SimplePoint;
			if (simplePointToCompare == null) { return false; }

			return (simplePointToCompare.X.Equals(this.X) && simplePointToCompare.Y.Equals(this.Y));
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() * this.Y.GetHashCode() * HASH_PRIME_NUMBER_SEED;
		}

		#endregion
	}
}
