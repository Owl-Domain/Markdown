namespace OwlDomain.Markdown.Tests;

[TestClass]
public sealed class PositionTests
{
	#region Constructor tests
	[TestMethod]
	public void Constructor_WithNegativeOffset_ThrowsArgumentOutOfRangeException()
	{
		// Arrange
		const string expectedParameterName = "offset";
		const int offset = -1, line = 1, column = 1;

		// Act
		static void Act() => _ = new Position(offset, line, column);

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentOutOfRangeException exception)
			.AreEqual(exception.ParamName, expectedParameterName)
			.AreEqual(exception.ActualValue, offset);
	}

	[DataRow(0, DisplayName = "Line zero")]
	[DataRow(-1, DisplayName = "Negative line")]
	[TestMethod]
	public void Constructor_WithInvalidLine_ThrowsArgumentOutOfRangeException(int line)
	{
		// Arrange
		const string expectedParameterName = "line";
		const int offset = 0, column = 1;

		// Act
		void Act() => _ = new Position(offset, line, column);

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentOutOfRangeException exception)
			.AreEqual(exception.ParamName, expectedParameterName)
			.AreEqual(exception.ActualValue, line);
	}

	[DataRow(0, DisplayName = "Column zero")]
	[DataRow(-1, DisplayName = "Negative column")]
	[TestMethod]
	public void Constructor_WithInvalidColumn_ThrowsArgumentOutOfRangeException(int column)
	{
		// Arrange
		const string expectedParameterName = "column";
		const int offset = 0, line = 1;

		// Act
		void Act() => _ = new Position(offset, line, column);

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentOutOfRangeException exception)
			.AreEqual(exception.ParamName, expectedParameterName)
			.AreEqual(exception.ActualValue, column);
	}

	[TestMethod]
	public void Constructor_WithValidValues_SetsExpectedProperties()
	{
		// Arrange
		const int offset = 1, line = 2, column = 3;

		// Act
		static Position Act() => new(offset, line, column);

		// Assert
		Assert.That
			.DoesNotThrowAnyException(Act, out Position result)
			.AreEqual(result.Offset, offset)
			.AreEqual(result.Line, line)
			.AreEqual(result.Column, column);
	}
	#endregion

	#region Tests
	[TestMethod]
	public void DefaultValueIsValid()
	{
		// Arrange
		Position sut = default;
		Position expected = new(0, 1, 1);

		// Assert
		Assert.That.AreEqual(expected, sut);
	}
	#endregion
}
