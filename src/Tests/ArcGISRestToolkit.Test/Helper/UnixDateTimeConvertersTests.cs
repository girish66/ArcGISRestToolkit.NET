namespace ArcGISRestToolkit.Test.Helper
{
	using System;
	using NUnit.Framework;
	using ArcGISRestToolkit.Helper;
	using SharpTestsEx;

	[TestFixture]
	public class UnixDateTimeConvertersTests
	{
		[Test]
		public void ShouldReturnALongWhenConvertingToUnixTimestamp()
		{
			var exampleDateTime = new DateTime(2012, 10, 08, 12, 0, 0, DateTimeKind.Utc);

			var result = exampleDateTime.ConvertToUnixTimestamp();
			result.GetType().Should().Be<long>();
		}

		[Test]
		public void ShouldReturnADateTimeWhenConvertingFromUnixTimestamp()
		{
			const long ExampleTimestamp = 1349697600000;

			var result = ExampleTimestamp.ConvertFromUnixTimestamp();
			result.GetType().Should().Be<DateTime>();
		}

		[Test]
		public void ShouldReturnAValidDateTimeWhenConvertingFromUnixTimestamp()
		{
			var exampleDateTime = new DateTime(2012, 10, 08, 12, 0, 0, DateTimeKind.Utc);
			const long ExampleTimestamp = 1349697600000;

			var result = ExampleTimestamp.ConvertFromUnixTimestamp();
			result.GetType().Should().Be<DateTime>();
			result.Should().Be(exampleDateTime);
		}

		[Test]
		public void ShouldReturnAValidTimestampTimeWhenConvertingToUnixTimestamp()
		{
			var exampleDateTime = new DateTime(2012, 10, 08, 12, 0, 0, DateTimeKind.Utc);
			const long ExampleTimestamp = 1349697600000;

			var result = exampleDateTime.ConvertToUnixTimestamp();
			result.GetType().Should().Be<long>();
			result.Should().Be(ExampleTimestamp);
		}

		[Test]
		public void WhenConvertingToUnixTimestampAndBackShouldKeepTheSameValue()
		{
			var exampleDateTime = new DateTime(2012, 10, 08, 12, 0, 0, DateTimeKind.Utc);

			var convertedTimeStamp = exampleDateTime.ConvertToUnixTimestamp();
			var returnedDateTime = convertedTimeStamp.ConvertFromUnixTimestamp();

			returnedDateTime.GetType().Should().Be<DateTime>();
			returnedDateTime.Should().Be(exampleDateTime);
		}

		[Test]
		public void WhenConvertingFromUnixTimestampAndBackShouldKeepTheSameValue()
		{
			const long ExampleTimestamp = 1349697600000;

			var convertedDateTime = ExampleTimestamp.ConvertFromUnixTimestamp();
			var returnedTimeStamp = convertedDateTime.ConvertToUnixTimestamp();

			returnedTimeStamp.GetType().Should().Be<long>();
			returnedTimeStamp.Should().Be(ExampleTimestamp);
		}
	}
}
