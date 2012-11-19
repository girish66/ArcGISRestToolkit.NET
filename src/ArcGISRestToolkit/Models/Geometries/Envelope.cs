namespace ArcGISRestToolkit.Models.Geometries
{
	using System;
	using System.Runtime.Serialization;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;

	[DataContract]
	public class Envelope : IRestAPIGeometry
	{
		#region Fields and Properties

		private const int HASH_PRIME_NUMBER_SEED = 13;

		[DataMember(Name = "xmin")]
		public double XMin { get; set; }

		[DataMember(Name = "ymin")]
		public double YMin { get; set; }

		[DataMember(Name = "xmax")]
		public double XMax { get; set; }

		[DataMember(Name = "ymax")]
		public double YMax { get; set; }

		[DataMember(Name = "spatialReference", IsRequired = false)]
		public SpatialReference SpatialReference { get; set; }

		#endregion

		#region Constructor

		public Envelope(double xMin, double yMin, double xMax, double yMax, int spatialReference) : this(xMin, yMin, xMax, yMax, new SpatialReference(spatialReference)) { }

		public Envelope(double xMin, double yMin, double xMax, double yMax, SpatialReference spatialReference)
		{
			if (spatialReference == null) { throw new ArgumentNullException("spatialReference"); }
			if (spatialReference.WKID <= 0) { throw new ArgumentException(ErrorMessages.msgPoint_Error_SpatialReferenceWKIDCannotBeZeroOrNegative, "spatialReference"); }

			this.XMin = xMin;
			this.YMin = yMin;
			this.XMax = xMax;
			this.YMax = yMax;
			this.SpatialReference = spatialReference;
		}

		#endregion

		#region Public Methods

		public override bool Equals(object obj)
		{
			var envelopeToCompare = obj as Envelope;
			if (envelopeToCompare == null) { return false; }

			return (envelopeToCompare.XMin.Equals(this.XMin)
				&& envelopeToCompare.YMin.Equals(this.YMin)
				&& envelopeToCompare.XMax.Equals(this.XMax)
				&& envelopeToCompare.YMax.Equals(this.YMax)
				&& envelopeToCompare.SpatialReference.Equals(this.SpatialReference));
		}

		public override int GetHashCode()
		{
			return this.XMin.GetHashCode() * this.YMin.GetHashCode() * this.XMax.GetHashCode() * this.YMax.GetHashCode()
				* this.SpatialReference.GetHashCode() * HASH_PRIME_NUMBER_SEED;
		}

		#endregion
	}
}
