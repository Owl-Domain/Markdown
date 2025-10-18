namespace OwlDomain.Markdown.Tests;

[TestClass]
public sealed class PositionTests
{
	#region Constructor tests
	[TestMethod]
	public void Constructor_WithNegativeIndex_ThrowsArgumentOutOfRangeException()
	{
		// Arrange
		const string expectedParameterName = "index";
		const int index = -1, line = 1, column = 1;

		// Act
		static void Act() => _ = new Position(index, line, column);

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentOutOfRangeException exception)
			.AreEqual(exception.ParamName, expectedParameterName)
			.AreEqual(exception.ActualValue, index);
	}

	[DataRow(0, DisplayName = "Line zero")]
	[DataRow(-1, DisplayName = "Negative line")]
	[TestMethod]
	public void Constructor_WithInvalidLine_ThrowsArgumentOutOfRangeException(int line)
	{
		// Arrange
		const string expectedParameterName = "line";
		const int index = 0, column = 1;

		// Act
		void Act() => _ = new Position(index, line, column);

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
		const int index = 0, line = 1;

		// Act
		void Act() => _ = new Position(index, line, column);

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
		const int index = 1, line = 2, column = 3;

		// Act
		static Position Act() => new(index, line, column);

		// Assert
		Assert.That
			.DoesNotThrowAnyException(Act, out Position result)
			.AreEqual(result.Index, index)
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
