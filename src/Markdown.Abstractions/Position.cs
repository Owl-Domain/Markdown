namespace OwlDomain.Markdown;

/// <summary>
/// 	Represents a single position inside of the parsed text.
/// </summary>
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(), nq}}")]
public readonly struct Position :
#if NET7_0_OR_GREATER
	IEqualityOperators<Position, Position, bool>,
	IComparisonOperators<Position, Position, bool>,
#endif
	IEquatable<Position>,
	IComparable<Position>
{
	#region Fields
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly int _line, _column;
	#endregion

	#region Properties
	/// <summary>The character based index inside of the parsed text.</summary>
	/// <remarks>This value is zero-based and it will never be negative.</remarks>
	public readonly int Index { get; }

	/// <summary>The line inside of the parsed text.</summary>
	/// <remarks>This value is one-based and it will never be negative.</remarks>
	public readonly int Line => _line + 1;

	/// <summary>The column inside of the parsed text.</summary>
	/// <remarks>This value is one-based and it will never be negative.</remarks>
	public readonly int Column => _column + 1;
	#endregion

	#region Constructors
	/// <summary>Creates a new instance of the <see cref="Position"/>.</summary>
	/// <param name="index">The character based index inside of the parsed text.</param>
	/// <param name="line">The line inside of the parsed text.</param>
	/// <param name="column">The column inside of the parsed text.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// 	Thrown if either:
	/// 	<list type="bullet">
	/// 		<item>The given <paramref name="index"/> is negative (less than <c>0</c>).</item>
	/// 		<item>The given <paramref name="line"/> is less than <c>1</c>.</item>
	/// 		<item>The given <paramref name="column"/> is less than <c>1</c>.</item>
	/// 	</list>
	/// </exception>
	public Position(int index, int line, int column)
	{
		index.ThrowIfLessThan(0, nameof(index));
		line.ThrowIfLessThan(1, nameof(line));
		column.ThrowIfLessThan(1, nameof(column));

		Index = index;
		_line = line - 1;
		_column = column - 1;
	}
	#endregion

	#region Methods
	/// <inheritdoc/>
	public override string ToString() => $"({Index}, {Line}, {Column})";

	/// <inheritdoc/>
	public bool Equals(Position other)
	{
		return
			Index == other.Index &&
			_line == other._line &&
			_column == other._column;
	}

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		if (obj is Position other)
			return Equals(other);

		return false;
	}

	/// <inheritdoc/>
	public int CompareTo(Position other) => Index.CompareTo(other.Index);

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(Index, _line, _column);
	#endregion

	#region Helpers
	private string DebuggerDisplay()
	{
		const string typeName = nameof(Position);
		const string indexName = nameof(Index);
		const string lineName = nameof(Line);
		const string columnName = nameof(Column);

		return $"{typeName} {{ {indexName} = ({Index:n0}), {lineName} = ({Line}:n0), {columnName} = ({Column}) }}";
	}
	#endregion

	#region Operators
	/// <summary>Compares two values to determine equality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator ==(Position left, Position right) => left.Equals(right);

	/// <summary>Compares two values to determine inequality.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator !=(Position left, Position right) => left.Equals(right) is false;

	/// <summary>Compares two values to determine which is lesser.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator <(Position left, Position right) => left.CompareTo(right) < 0;

	/// <summary>Compares two values to determine which is lesser or equal.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator <=(Position left, Position right) => left.CompareTo(right) <= 0;

	/// <summary>Compares two values to determine which is greater.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator >(Position left, Position right) => left.CompareTo(right) > 0;

	/// <summary>Compares two values to determine which is greater or equal.</summary>
	/// <param name="left">The value to compare with <paramref name="right"/>.</param>
	/// <param name="right">The value to compare with <paramref name="left"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>, <see langword="false"/> otherwise.</returns>
	public static bool operator >=(Position left, Position right) => left.CompareTo(right) >= 0;
	#endregion
}
