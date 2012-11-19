namespace ArcGISRestToolkit.Models.Geometries
{
	using System.Runtime.Serialization;

	[DataContract]
	public class SpatialReference
	{
		#region Fields and Properties

		[DataMember(Name = "wkid")]
		public int WKID { get; private set; }

		#endregion

		#region Constructor

		public SpatialReference(int wkid)
		{
			this.WKID = wkid;
		}

		#endregion

		#region Public Methods

		public bool IsValid()
		{
			return (this.WKID > 0);
		}

		public override bool Equals(object obj)
		{
			var spatialReferenceToCompare = obj as SpatialReference;
			return ((spatialReferenceToCompare != null) && (spatialReferenceToCompare.WKID.Equals(this.WKID)));
		}

		public override int GetHashCode()
		{
			return this.WKID.GetHashCode() * 13;
		}

		#endregion
	}
}
