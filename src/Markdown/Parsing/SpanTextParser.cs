using System.Buffers;
using System.Globalization;

namespace OwlDomain.Markdown.Parsing;

/// <summary>
/// 	Represents a general text parser for <see langword="char"/> <see cref="ReadOnlySpan{T}"/> values.
/// </summary>
public ref struct SpanTextParser : ITextParser
{
	#region Fields
	private readonly ReadOnlySpan<char> _span;
	private int _offset, _line = 1, _column = 1;

	private readonly ReadOnlySpan<int> _lookup;
	private readonly int[]? _rentedBuffer;

	private int _lookupIndex = 0;
	private bool _disposed;
	#endregion

	#region Properties
	/// <inheritdoc/>
	public readonly TextElement Current => Peek(0);

	/// <inheritdoc/>
	public readonly TextElement Next => Peek(1);

	/// <inheritdoc/>
	public readonly Position Position => new(_offset, _line, _column);

	/// <inheritdoc/>
	public readonly bool IsAtEnd => _lookupIndex >= _lookup.Length;

	/// <inheritdoc/>
	public readonly bool HasRemaining => _lookupIndex < _lookup.Length;
	#endregion

	#region Constructors
	/// <summary>Creates a new instance of the <see cref="SpanTextParser"/>.</summary>
	/// <param name="span">The text to parse.</param>
	public SpanTextParser(ReadOnlySpan<char> span)
	{
		_span = span;
		_lookup = ParseCombiningCharacters(span, [], out _rentedBuffer);
	}

	/// <summary>Creates a new instance of the <see cref="SpanTextParser"/>.</summary>
	/// <param name="span">The text to parse.</param>
	/// <param name="buffer">The buffer used to store the text element indecies from the given <paramref name="span"/>, must be at least the same size.</param>
	/// <exception cref="ArgumentException">Thrown if the given <paramref name="buffer"/> is not as big as the given <paramref name="span"/>.</exception>
	public SpanTextParser(ReadOnlySpan<char> span, Span<int> buffer)
	{
		if (buffer.Length < span.Length)
			Throw.New.ArgumentException(nameof(buffer), $"The given buffer size ({buffer.Length:n0}) was not big enough to fit the maximum possible amount ({span.Length:n0}) of the text element indecies.");

		_span = span;
		_lookup = ParseCombiningCharacters(span, buffer, out _rentedBuffer);
		Debug.Assert(_rentedBuffer is null);
	}
	#endregion

	#region Methods
	/// <inheritdoc/>
	public void Advance(int amount = 1)
	{
		amount.ThrowIfLessThan(1, nameof(amount));

		if (IsAtEnd)
			return;

		int remaining = _lookup.Length - _lookupIndex;
		int actual = Math.Min(amount, remaining);

		_lookupIndex += actual;
		_offset += actual;
		_column += actual;
	}

	/// <inheritdoc/>
	public readonly TextElement Peek(int offset)
	{
		if (TryPeek(offset, out TextElement element))
			return element;

		return default;
	}

	/// <inheritdoc/>
	public readonly bool TryPeek(int offset, out TextElement element)
	{
		int lookupIndex = _lookupIndex + offset;

		if (lookupIndex < 0 || lookupIndex >= _lookup.Length)
		{
			element = default;
			return false;
		}

		int stringIndex = _lookup[lookupIndex];
		ReadOnlySpan<char> slice;

		if (lookupIndex == _lookup.Length - 1)
			slice = _span[stringIndex..];
		else
		{
			int end = _lookup[lookupIndex + 1];
			slice = _span[stringIndex..end];
		}

		element = new(slice);
		return true;
	}

	/// <inheritdoc/>
	public bool Match(TextElement element)
	{
		if (Current == element)
		{
			Advance();
			return true;
		}

		return false;
	}

	/// <inheritdoc/>
	public bool Match(char character)
	{
		if (Current == character)
		{
			Advance();
			return true;
		}

		return false;
	}

	/// <inheritdoc/>
	public bool Match(ReadOnlySpan<char> span)
	{
		int offset = 0;

		while (span.IsEmpty is false)
		{
			TextElement current = span.GetNextTextElement(out ReadOnlySpan<char> remaining);

			if (Peek(offset) != current)
				return false;

			span = remaining;
			offset++;
		}

		Advance(offset);
		return true;
	}

	/// <inheritdoc/>
	public void MarkNewLine()
	{
		_line++;
		_column = 1;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		if (_disposed)
			return;

		if (_rentedBuffer is not null)
			ArrayPool<int>.Shared.Return(_rentedBuffer);

		_disposed = true;
	}
	#endregion

	#region Helpers
	private static ReadOnlySpan<int> ParseCombiningCharacters(ReadOnlySpan<char> span, Span<int> buffer, out int[]? rentedBuffer)
	{
		int used = 0;
		int offset = 0;

		if (span.Length > buffer.Length)
		{
			rentedBuffer = ArrayPool<int>.Shared.Rent(span.Length);
			buffer = rentedBuffer;
		}
		else
			rentedBuffer = null;

		while (span.IsEmpty is false)
		{
			buffer[used++] = offset;
			int length = StringInfo.GetNextTextElementLength(span);

			offset += length;
			span = span[length..];
		}

		return buffer[..used];
	}
	#endregion
}
