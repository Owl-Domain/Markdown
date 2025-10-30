namespace OwlDomain.Markdown.Tests.Parsing;

[TestClass]
public sealed class TextElementTests
{
	#region Constructor tests
	[DataRow("a", DisplayName = "Single letter")]
	[DataRow("👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void Constructor_WithValidTextElement_SetsExpectedFields(string text)
	{
		// Arrange
		ReadOnlySpan<char> expectedSpan = text;

		// Act
		TextElement Act() => _ = new TextElement(text);

		// Assert
		Assert.That
			.DoesNotThrowAnyException(Act, out TextElement element)
			.IsTrue(element.Span == expectedSpan);
	}

	[DataRow("", DisplayName = "Empty")]
	[DataRow("aa", DisplayName = "Multiple letters")]
	[TestMethod]
	public void Constructor_WithInvalidTextElement_ThrowsArgumentException(string text)
	{
		// Arrange
		const string expectedParameterName = "span";

		// Act
		void Act() => _ = new TextElement(text);

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentException exception)
			.AreEqual(exception.ParamName, expectedParameterName);
	}
	#endregion

	#region Equality tests
	[DataRow("a", "a", DisplayName = "Single letter")]
	[DataRow("👪", "👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void Equals_WithTextElement_WithEqualValues_ReturnsTrue(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		TextElement other = new(otherValue);

		// Act
		bool result = sut.Equals(other);

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", "b", DisplayName = "Single letter")]
	[DataRow("👪", "🧌", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👍🏽", DisplayName = "Complex emoji")]
	[TestMethod]
	public void Equals_WithTextElement_WithDifferentValues_ReturnsFalse(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		TextElement other = new(otherValue);

		// Act
		bool result = sut.Equals(other);

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", "a", DisplayName = "Single letter")]
	[DataRow("👪", "👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void Equals_WithSpan_WithEqualValues_ReturnsTrue(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		ReadOnlySpan<char> other = otherValue;

		// Act
		bool result = sut.Equals(other);

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", "b", DisplayName = "Single letter")]
	[DataRow("👪", "🧌", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👍🏽", DisplayName = "Complex emoji")]
	[TestMethod]
	public void Equals_WithSpan_WithDifferentValues_ReturnsFalse(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		ReadOnlySpan<char> other = otherValue;

		// Act
		bool result = sut.Equals(other);

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", 'a', DisplayName = "Single letter")]
	[TestMethod]
	public void Equals_WithChar_WithEqualValues_ReturnsTrue(string sutValue, char otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);

		// Act
		bool result = sut.Equals(otherValue);

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", 'b', DisplayName = "Single letter")]
	[TestMethod]
	public void Equals_WithChar_WithDifferentValues_ReturnsFalse(string sutValue, char otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);

		// Act
		bool result = sut.Equals(otherValue);

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", "a", DisplayName = "Single letter")]
	[DataRow("👪", "👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void EqualityOperator_WithTextElement_WithEqualValues_ReturnsTrue(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		TextElement other = new(otherValue);

		// Act
		bool result = sut == other;

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", "b", DisplayName = "Single letter")]
	[DataRow("👪", "🧌", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👍🏽", DisplayName = "Complex emoji")]
	[TestMethod]
	public void EqualityOperator_WithTextElement_WithDifferentValues_ReturnsFalse(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		TextElement other = new(otherValue);

		// Act
		bool result = sut == other;

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", "a", DisplayName = "Single letter")]
	[DataRow("👪", "👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void EqualityOperator_WithSpan_WithEqualValues_ReturnsTrue(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		ReadOnlySpan<char> other = otherValue;

		// Act
		bool result = sut == other;

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", "b", DisplayName = "Single letter")]
	[DataRow("👪", "🧌", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👍🏽", DisplayName = "Complex emoji")]
	[TestMethod]
	public void EqualityOperator_WithSpan_WithDifferentValues_ReturnsFalse(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		ReadOnlySpan<char> other = otherValue;

		// Act
		bool result = sut == other;

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", 'a', DisplayName = "Single letter")]
	[TestMethod]
	public void EqualityOperator_WithChar_WithEqualValues_ReturnsTrue(string sutValue, char otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);

		// Act
		bool result = sut == otherValue;

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", 'b', DisplayName = "Single letter")]
	[TestMethod]
	public void EqualityOperator_WithChar_WithDifferentValues_ReturnsFalse(string sutValue, char otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);

		// Act
		bool result = sut == otherValue;

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", "b", DisplayName = "Single letter")]
	[DataRow("👪", "🧌", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👍🏽", DisplayName = "Complex emoji")]
	[TestMethod]
	public void InequalityOperator_WithTextElement_WithDifferentValues_ReturnsTrue(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		TextElement other = new(otherValue);

		// Act
		bool result = sut != other;

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", "a", DisplayName = "Single letter")]
	[DataRow("👪", "👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void InequalityOperator_WithTextElement_WithEqualValues_ReturnsFalse(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		TextElement other = new(otherValue);

		// Act
		bool result = sut != other;

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", "b", DisplayName = "Single letter")]
	[DataRow("👪", "🧌", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👍🏽", DisplayName = "Complex emoji")]
	[TestMethod]
	public void InequalityOperator_WithSpan_WithDifferentValues_ReturnsTrue(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		ReadOnlySpan<char> other = otherValue;

		// Act
		bool result = sut != other;

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", "a", DisplayName = "Single letter")]
	[DataRow("👪", "👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", "👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void InequalityOperator_WithSpan_WithEqualValues_ReturnsFalse(string sutValue, string otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);
		ReadOnlySpan<char> other = otherValue;

		// Act
		bool result = sut != other;

		// Assert
		Assert.That.IsFalse(result);
	}

	[DataRow("a", 'b', DisplayName = "Single letter")]
	[TestMethod]
	public void InequalityOperator_WithChar_WithDifferentValues_ReturnsTrue(string sutValue, char otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);

		// Act
		bool result = sut != otherValue;

		// Assert
		Assert.That.IsTrue(result);
	}

	[DataRow("a", 'a', DisplayName = "Single letter")]
	[TestMethod]
	public void InequalityOperator_WithChar_WithEqualValues_ReturnsFalse(string sutValue, char otherValue)
	{
		// Arrange
		TextElement sut = new(sutValue);

		// Act
		bool result = sut != otherValue;

		// Assert
		Assert.That.IsFalse(result);
	}
	#endregion

	#region Obsolete / unsupported method tests
	[TestMethod]
	public void Equals_Untyped_ThrowsNotSupportedException()
	{
		// Arrange
		object other = new();

		// Act
		void Act()
		{
			TextElement sut = new("a");
			_ = sut.Equals(other);
		}

		// Assert
		Assert.That.ThrowsExactException<NotSupportedException>(Act);
	}

	[TestMethod]
	public void GetHashCode_ThrowsNotSupportedException()
	{
		// Act
		static void Act()
		{
			TextElement sut = new("a");
			_ = sut.GetHashCode();
		}

		// Assert
		Assert.That.ThrowsExactException<NotSupportedException>(Act);
	}
	#endregion

	#region Other tests
	[DataRow("a", DisplayName = "Single letter")]
	[DataRow("👪", DisplayName = "Simple emoji")]
	[DataRow("👩‍👩‍👧‍👦", DisplayName = "Complex emoji")]
	[TestMethod]
	public void ToString_ReturnsExpectedString(string expectedResult)
	{
		// Arrange
		TextElement sut = new(expectedResult);

		// Act
		string result = sut.ToString();

		// Assert
		Assert.That.AreEqual(result, expectedResult);
	}
	#endregion
}
