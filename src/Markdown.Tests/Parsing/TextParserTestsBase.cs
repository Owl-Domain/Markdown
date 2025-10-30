namespace OwlDomain.Markdown.Tests.Parsing;

public abstract class TextParserTestsBase<T>
	where T : ITextParser, allows ref struct
{
	#region Constructor tests
	[TestMethod]
	public void Constructor_WithNoText_SetExpectedInitialState()
	{
		// Arrange
		Position expectedPosition = new(0, 1, 1);
		TextElement expectedTextElement = default;

		// Act
		using T sut = CreateParser("");

		// Assert
		Assert.That
			.AreEqual(sut.Position, expectedPosition)
			.IsTrue(sut.Current == expectedTextElement)
			.IsTrue(sut.Next == expectedTextElement)
			.IsTrue(sut.IsAtEnd)
			.IsFalse(sut.HasRemaining);
	}
	#endregion

	#region Advance tests
	[DataRow(-1, DisplayName = "Negative amount")]
	[DataRow(0, DisplayName = "No amount")]
	[TestMethod]
	public void Advance_WithInvalidAmount_ThrowsArgumentOutOfRangeException(int amount)
	{
		// Arrange
		const string expectedParameterName = "amount";

		// Act
		void Act()
		{
			using T sut = CreateParser("");
			Assert.IsConclusiveIf.IsTrue(sut.IsAtEnd); // Note(Nightowl): Make sure exception takes priority;

			sut.Advance(amount);
		}

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentOutOfRangeException exception)
			.AreEqual(exception.ActualValue, amount)
			.AreEqual(exception.ParamName, expectedParameterName);
	}

	[TestMethod]
	public void Advance_WithValidAmount_AdvancesParser()
	{
		// Arrange
		const int amount = 1;

		Position expectedStartPosition = new(0, 1, 1);
		Position expectedEndPosition = new(amount, 1, amount + 1);

		using T sut = CreateParser("a");

		// Arrange assert
		Assert.IsConclusiveIf
			.AreEqual(sut.Position, expectedStartPosition)
			.IsFalse(sut.IsAtEnd)
			.IsTrue(sut.HasRemaining);

		// Act
		sut.Advance(amount);

		// Assert
		Assert.That
			.AreEqual(sut.Position, expectedEndPosition)
			.IsTrue(sut.IsAtEnd)
			.IsFalse(sut.HasRemaining);
	}

	[TestMethod]
	public void Advance_TooFar_AdvancesToEndAndNoFurther()
	{
		// Arrange
		const int maxAmount = 1;
		const int amount = 2;

		Position expectedStartPosition = new(0, 1, 1);
		Position expectedEndPosition = new(maxAmount, 1, maxAmount + 1);

		using T sut = CreateParser("a");

		// Arrange assert
		Assert.IsConclusiveIf
			.AreEqual(sut.Position, expectedStartPosition)
			.IsFalse(sut.IsAtEnd)
			.IsTrue(sut.HasRemaining);

		// Act
		sut.Advance(amount);

		// Assert
		Assert.That
			.AreEqual(sut.Position, expectedEndPosition)
			.IsTrue(sut.IsAtEnd)
			.IsFalse(sut.HasRemaining);
	}

	[TestMethod]
	public void Advance_AlreadyAtEnd_NoMovement()
	{
		// Arrange
		Position expectedPosition = new(0, 1, 1);

		using T sut = CreateParser("");

		// Arrang eassert
		Assert.IsConclusiveIf
			.AreEqual(sut.Position, expectedPosition)
			.IsTrue(sut.IsAtEnd);

		// Act
		sut.Advance();

		// Assert
		Assert.That
			.AreEqual(sut.Position, expectedPosition)
			.IsTrue(sut.IsAtEnd);
	}

	[TestMethod]
	public void Advance_WithComplexTextElements_AdvancesTheParser()
	{
		// Arrange
		const string text = "👩‍👩‍👧‍👦👍🏽";

		TextElement expectedTextElement1 = new("👩‍👩‍👧‍👦");
		TextElement expectedTextElement2 = new("👍🏽");

		Position expectedStartPosition = new(0, 1, 1);
		Position expectedEndPosition = new(1, 1, 2);

		using T sut = CreateParser(text);

		// Arrange assert
		Assert.IsConclusiveIf
			.AreEqual(sut.Position, expectedStartPosition)
			.IsTrue(sut.Current == expectedTextElement1);

		// Act
		sut.Advance();

		// Assert
		Assert.That
			.AreEqual(sut.Position, expectedEndPosition)
			.IsTrue(sut.Current == expectedTextElement2);
	}
	#endregion

	#region TryPeek tests
	[DataRow(-1, "🧌", DisplayName = "-1 offset (previous element)")]
	[DataRow(0, "👩‍👩‍👧‍👦", DisplayName = "No offset (current element)")]
	[DataRow(1, "👍🏽", DisplayName = "1 offset (next element)")]
	[TestMethod]
	public void TryPeek_WithValidOffset_ReturnsTrueAndExpectedTextElement(int offset, string expectedText)
	{
		// Arrange
		TextElement expectedTextElement = new(expectedText);

		using T sut = CreateParser("🧌👩‍👩‍👧‍👦👍🏽");
		sut.Advance();

		// Act
		bool result = sut.TryPeek(offset, out TextElement textElementResult);

		// Assert
		Assert.That
			.IsTrue(result)
			.IsTrue(textElementResult == expectedTextElement);
	}

	[DataRow(-1, DisplayName = "Too far before")]
	[DataRow(1, DisplayName = "Too far after")]
	[TestMethod]
	public void TryPeek_TooFar_ReturnsFalse(int offset)
	{
		// Arrange
		using T sut = CreateParser("a");

		// Act
		bool result = sut.TryPeek(offset, out _);

		// Assert
		Assert.That.IsFalse(result);
	}
	#endregion

	#region Peek tests
	[DataRow(-1, "🧌", DisplayName = "-1 offset (previous element)")]
	[DataRow(0, "👩‍👩‍👧‍👦", DisplayName = "No offset (current element)")]
	[DataRow(1, "👍🏽", DisplayName = "1 offset (next element)")]
	[TestMethod]
	public void Peek_WithValidOffset_ReturnsExpectedTextElement(int offset, string expectedText)
	{
		// Arrange
		TextElement expectedTextElement = new(expectedText);

		using T sut = CreateParser("🧌👩‍👩‍👧‍👦👍🏽");
		sut.Advance();

		// Act
		TextElement result = sut.Peek(offset);

		// Assert
		Assert.That.IsTrue(result == expectedTextElement);
	}

	[DataRow(-1, DisplayName = "Too far before")]
	[DataRow(1, DisplayName = "Too far after")]
	[TestMethod]
	public void Peek_TooFar_ReturnsEmptyTextElement(int offset)
	{
		// Arrange
		TextElement expectedTextElement = default;
		using T sut = CreateParser("a");

		// Act
		TextElement result = sut.Peek(offset);

		// Assert
		Assert.That.IsTrue(result == expectedTextElement);
	}
	#endregion

	#region MarkNewLine tests
	[TestMethod]
	public void MarkNewLine_IncrementsLineAndResetsColumn()
	{
		// Arrange
		Position expectedStartPosition = new(1, 1, 2);
		Position expectedEndPosition = new(1, 2, 1);

		using T sut = CreateParser("a");
		sut.Advance();

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedStartPosition);

		// Act
		sut.MarkNewLine();

		// Assert
		Assert.That.AreEqual(sut.Position, expectedEndPosition);
	}
	#endregion

	#region Match tests
	[TestMethod]
	public void Match_WithTextElement_Matching_ReturnsTrueAndAdvancesParser()
	{
		// Arrange
		const string text = "a";
		Position expectedStartPosition = new(0, 1, 1);
		Position expectedEndPosition = new(1, 1, 2);

		TextElement textElement = new(text);
		using T sut = CreateParser(text);

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedStartPosition);

		// Act
		bool result = sut.Match(textElement);

		// Assert
		Assert.That
			.IsTrue(result)
			.AreEqual(sut.Position, expectedEndPosition);
	}

	[TestMethod]
	public void Match_WithTextElement_Different_ReturnsFalseAndDoesntMoveParser()
	{
		// Arrange
		Position expectedPosition = new(0, 1, 1);

		TextElement textElement = new("b");
		using T sut = CreateParser("a");

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedPosition);

		// Act
		bool result = sut.Match(textElement);

		// Assert
		Assert.That
			.IsFalse(result)
			.AreEqual(sut.Position, expectedPosition);
	}

	[TestMethod]
	public void Match_WithChar_Matching_ReturnsTrueAndAdvancesParser()
	{
		// Arrange
		const char text = 'a';
		Position expectedStartPosition = new(0, 1, 1);
		Position expectedEndPosition = new(1, 1, 2);

		char textElement = text;
		using T sut = CreateParser(text.ToString());

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedStartPosition);

		// Act
		bool result = sut.Match(textElement);

		// Assert
		Assert.That
			.IsTrue(result)
			.AreEqual(sut.Position, expectedEndPosition);
	}

	[TestMethod]
	public void Match_WithChar_Different_ReturnsFalseAndDoesntMoveParser()
	{
		// Arrange
		Position expectedPosition = new(0, 1, 1);

		char textElement = 'b';
		using T sut = CreateParser("a");

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedPosition);

		// Act
		bool result = sut.Match(textElement);

		// Assert
		Assert.That
			.IsFalse(result)
			.AreEqual(sut.Position, expectedPosition);
	}

	[DataRow("a", 1, DisplayName = "Simple")]
	[DataRow("🧌👪", 2, DisplayName = "Simple emojis")]
	[DataRow("👩‍👩‍👧‍👦👍🏽", 2, DisplayName = "Complex emojis")]
	[TestMethod]
	public void Match_WithSpan_Matching_ReturnsTrueAndAdvancesParser(string text, int textElementCount)
	{
		Position expectedStartPosition = new(0, 1, 1);
		Position expectedEndPosition = new(textElementCount, 1, textElementCount + 1);

		ReadOnlySpan<char> span = text;
		using T sut = CreateParser(text);

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedStartPosition);

		// Act
		bool result = sut.Match(span);

		// Assert
		Assert.That
			.IsTrue(result)
			.AreEqual(sut.Position, expectedEndPosition);
	}

	[TestMethod]
	public void Match_WithSpan_Different_ReturnsFalseAndDoesntMoveParser()
	{
		// Arrange
		Position expectedPosition = new(0, 1, 1);
		ReadOnlySpan<char> span = "🧌👩‍👩‍👧‍👦👍🏽";

		using T sut = CreateParser("abc");

		// Arrange assert
		Assert.IsConclusiveIf.AreEqual(sut.Position, expectedPosition);

		// Act
		bool result = sut.Match(span);

		// Assert
		Assert.That
			.IsFalse(result)
			.AreEqual(sut.Position, expectedPosition);
	}
	#endregion

	#region Methods
	protected abstract T CreateParser(string text);
	#endregion
}
