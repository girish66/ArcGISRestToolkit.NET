namespace ArcGISRestToolkit.Helper
{
	using System;

	public static class UnixDateTimeConverters
	{
		private static readonly DateTime _unixEpochStartDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); 

		public static DateTime ConvertFromUnixTimestamp(this long timestamp)
		{
			var origin = _unixEpochStartDate;
			return origin.AddMilliseconds(timestamp);
		}

		public static long ConvertToUnixTimestamp(this DateTime dateTime)
		{
			var tspan = dateTime.Subtract(_unixEpochStartDate);
			return (long)Math.Truncate(tspan.TotalMilliseconds);
		}
	}
}