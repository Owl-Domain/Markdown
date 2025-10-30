namespace OwlDomain.Markdown.Tests.Parsing;

[TestClass]
public sealed class StringTextParserTests : TextParserTestsBase<StringTextParser>
{
	#region Methods
	protected override StringTextParser CreateParser(string text) => new(text);
	#endregion
}

[TestClass]
public sealed class SpanTextParserTests : TextParserTestsBase<SpanTextParser>
{
	#region Constructor tests
	[TestMethod]
	public void Constructor_WithCorrectlySizedBuffer_SetExpectedInitialState()
	{
		// Arrange
		Position expectedPosition = new(0, 1, 1);
		TextElement expectedTextElement = default;
		string text = "";

		// Act
		using SpanTextParser sut = new(text, stackalloc int[text.Length]);

		// Assert
		Assert.That
			.AreEqual(sut.Position, expectedPosition)
			.IsTrue(sut.Current == expectedTextElement)
			.IsTrue(sut.Next == expectedTextElement)
			.IsTrue(sut.IsAtEnd)
			.IsFalse(sut.HasRemaining);
	}

	[TestMethod]
	public void Constructor_WithIncorrectlySizedBuffer_ThrowsArgumentException()
	{
		// Arrange
		const string expectedParameterName = "buffer";

		// Act
		static void Act() => _ = new SpanTextParser("a", []);

		// Assert
		Assert.That
			.ThrowsExactException(Act, out ArgumentException exception)
			.AreEqual(exception.ParamName, expectedParameterName);
	}
	#endregion

	#region Dispose tests
	[TestMethod]
	public void Dispose_DoubleDispose_DoesntThrowAnyExceptions()
	{
		// Act
		static void Act()
		{
			SpanTextParser sut = new("1");
			sut.Dispose();
			sut.Dispose();
		}

		// Assert
		Assert.That.DoesNotThrowAnyException(Act);
	}
	#endregion

	#region Methods
	protected override SpanTextParser CreateParser(string text) => new(text);
	#endregion
}
