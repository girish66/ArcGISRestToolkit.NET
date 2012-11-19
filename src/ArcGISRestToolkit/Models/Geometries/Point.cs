namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;

	[DataContract]
	public class Point : SimplePoint, IRestAPIGeometry
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;

		[DataMember(Name = "spatialReference", IsRequired = false)]
		public SpatialReference SpatialReference { get; set; }

		#endregion

		#region Constructor

		public Point(double x, double y, int spatialReference) : this(x, y, new SpatialReference(spatialReference)) { }

		public Point(double x, double y, SpatialReference spatialReference)
			: base(x, y)
		{
			if (spatialReference == null) { throw new ArgumentNullException("spatialReference"); }
			if (spatialReference.WKID <= 0) { throw new ArgumentException(ErrorMessages.msgPoint_Error_SpatialReferenceWKIDCannotBeZeroOrNegative, "spatialReference"); }

			this.SpatialReference = spatialReference;
		}

		#endregion

		#region Public Methods

		public override bool Equals(object obj)
		{
			var pointToCompare = obj as Point;
			if (pointToCompare == null) { return false; }

			return (pointToCompare.X.Equals(this.X) && pointToCompare.Y.Equals(this.Y) && pointToCompare.SpatialReference.Equals(this.SpatialReference));
		}

		public override int GetHashCode()
		{
			return this.X.GetHashCode() * this.Y.GetHashCode() * this.SpatialReference.GetHashCode() * HASH_PRIME_NUMBER_SEED;
		}

		#endregion
	}
}
