using System.Globalization;

namespace OwlDomain.Markdown.Parsing;

/// <summary>
/// 	Represents a single text element (extended grapheme cluster) in a piece of text.
/// </summary>
public readonly ref struct TextElement
{
	#region Properties
	/// <summary>The span of characters that represents the text element.</summary>
	public readonly ReadOnlySpan<char> Span { get; }
	#endregion

	#region Constructors
	/// <summary>Creates a new instance of the <see cref="TextElement"/>.</summary>
	/// <param name="span">The span of characters that represents the text element.</param>
	/// <exception cref="ArgumentException">Thrown if the given <paramref name="span"/> does not contain exactly 1 text element.</exception>
	public TextElement(ReadOnlySpan<char> span)
	{
		if (IsValid(span) is false)
			Throw.New.ArgumentException(nameof(span), "The given span should contain exactly 1 text element (extended grapheme cluster).");

		Span = span;
	}
	#endregion

	#region Methods
	/// <summary>Compares the current text element with the <paramref name="other"/> given text element.</summary>
	/// <param name="other">The other text element to check for equality.</param>
	/// <param name="comparison">The type of string comparison to use for the equality check.</param>
	/// <returns>
	/// 	<see langword="true"/> if the current text element is equal to the
	/// 	<paramref name="other"/> given text element, <see langword="false"/> otherwise.
	/// </returns>
	public bool Equals(TextElement other, StringComparison comparison = StringComparison.Ordinal) => Span.Equals(other.Span, comparison);

	/// <summary>Compares the current text element with the <paramref name="other"/> given <see langword="char"/> span.</summary>
	/// <param name="other">The other <see langword="char"/> span to check for equality.</param>
	/// <param name="comparison">The type of string comparison to use for the equality check.</param>
	/// <returns>
	/// 	<see langword="true"/> if the current text element is equal to the
	/// 	<paramref name="other"/> given <see langword="char"/> span, <see langword="false"/> otherwise.
	/// </returns>
	public bool Equals(ReadOnlySpan<char> other, StringComparison comparison = StringComparison.Ordinal) => Span.Equals(other, comparison);

	/// <summary>Compares the current text element with the <paramref name="other"/> given <see langword="char"/>.</summary>
	/// <param name="other">The other <see langword="char"/> to check for equality.</param>
	/// <param name="comparison">The type of string comparison to use for the equality check.</param>
	/// <returns>
	/// 	<see langword="true"/> if the current text element is equal to the
	/// 	<paramref name="other"/> given <see langword="char"/>, <see langword="false"/> otherwise.
	/// </returns>
	public bool Equals(char other, StringComparison comparison = StringComparison.Ordinal) => Span.Equals([other], comparison);

	/// <inheritdoc/>
	public override string ToString() => Span.ToString();
	#endregion

	#region Obsolete methods
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

	/// <summary>This method is not supported as text elements cannot be boxed, either use the equality operator or a different overload.</summary>
	/// <exception cref="NotSupportedException">Will always be thrown.</exception>
	[Obsolete($"{nameof(Equals)}(object) is not supported, either use a different overload or the equality operator.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		Throw.New.NotSupportedException($"{nameof(Equals)}(object) is not supported, use either the equality operator or a different overload.");
		return default;
	}

	/// <summary>This method is not supported as text elements cannot be boxed.</summary>
	/// <exception cref="NotSupportedException">Will always be thrown.</exception>
	[Obsolete($"{nameof(GetHashCode)}() is not supported as text elements cannot be boxed.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int GetHashCode()
	{
		Throw.New.NotSupportedException($"{nameof(GetHashCode)} is not supported.");
		return default;
	}

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
	#endregion

	#region Helpers
	private static bool IsValid(ReadOnlySpan<char> span)
	{
		int length = StringInfo.GetNextTextElementLength(span);

		return length > 0 && length == span.Length;
	}
	#endregion

	#region Operators
	/// <summary>Compares two values to determine equality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator ==(TextElement left, TextElement right) => left.Equals(right);

	/// <summary>Compares two values to determine inequality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator !=(TextElement left, TextElement right) => left.Equals(right) is false;

	/// <summary>Compares two values to determine equality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator ==(TextElement left, ReadOnlySpan<char> right) => left.Equals(right);

	/// <summary>Compares two values to determine inequality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator !=(TextElement left, ReadOnlySpan<char> right) => left.Equals(right) is false;

	/// <summary>Compares two values to determine equality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator ==(TextElement left, char right) => left.Equals(right);

	/// <summary>Compares two values to determine inequality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator !=(TextElement left, char right) => left.Equals(right) is false;
	#endregion
}

/// <summary>
/// 	Contains various extension methods related to the <see cref="TextElement"/>.
/// </summary>
public static class TextElementExtensions
{
	#region Methods
	/// <summary>Gets the next text element in the given <see langword="char"/> <paramref name="span"/>.</summary>
	/// <param name="span">The <see langword="char"/> span to get the next text element in.</param>
	/// <returns>The next text element in the given <see langword="char"/> <paramref name="span"/>.</returns>
	public static TextElement GetNextTextElement(this ReadOnlySpan<char> span)
	{
		int length = StringInfo.GetNextTextElementLength(span);
		ReadOnlySpan<char> slice = span[..length];

		return new(slice);
	}

	/// <summary>Gets the next text element in the given <see langword="char"/> <paramref name="span"/>.</summary>
	/// <param name="span">The <see langword="char"/> span to get the next text element from.</param>
	/// <param name="remaining">The span of the remaining <see langword="char"/> values.</param>
	/// <returns>The next text element in the given <see langword="char"/> <paramref name="span"/>.</returns>
	public static TextElement GetNextTextElement(this ReadOnlySpan<char> span, out ReadOnlySpan<char> remaining)
	{
		int length = StringInfo.GetNextTextElementLength(span);

		ReadOnlySpan<char> slice = span[..length];
		remaining = span[length..];

		return new(slice);
	}

	/// <summary>Gets the next text <paramref name="element"/> in the given <see langword="char"/> <paramref name="span"/>.</summary>
	/// <param name="span">The <see langword="char"/> span to get the next text <paramref name="element"/> from.</param>
	/// <param name="element">The next text element in the given <see langword="char"/> <paramref name="span"/>.</param>
	/// <returns>The span of the remaining <see langword="char"/> values.</returns>
	public static ReadOnlySpan<char> GetNextTextElement(this ReadOnlySpan<char> span, out TextElement element)
	{
		int length = StringInfo.GetNextTextElementLength(span);

		ReadOnlySpan<char> slice = span[..length];
		element = new(slice);

		return span[length..];
	}
	#endregion
}
