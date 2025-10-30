namespace OwlDomain.Markdown;

/// <summary>
/// 	Represents the position of a node inside of the parsed text.
/// </summary>
public readonly struct NodePosition
{
	#region Properties
	/// <summary>The inclusive start position of the node.</summary>
	public readonly Position Start { get; }

	/// <summary>The inclusive end position of the node.</summary>
	public readonly Position End { get; }

	/// <summary>The amount of text elements that the node takes up.</summary>
	public readonly int Length => checked(End.Offset - Start.Offset + 1);

	/// <summary>The amount of lines that the node spans.</summary>
	public readonly int LineCount => checked(End.Line - Start.Line + 1);

	/// <summary>Whether the node spans multiple lines.</summary>
	public readonly bool IsMultiline => Start.Line != End.Line;
	#endregion

	#region Constructors
	/// <summary>Creates a new instance of the <see cref="NodePosition"/>.</summary>
	/// <param name="start">The inclusive start position of the node.</param>
	/// <param name="end">The inclusive end position of the node.</param>
	/// <exception cref="ArgumentException">
	/// 	Thrown if the given <paramref name="end"/> position comes
	/// 	before the given <paramref name="start"/> position.
	/// </exception>
	public NodePosition(Position start, Position end)
	{
		if (start.Offset > end.Offset)
			Throw.New.ArgumentException(nameof(end), $"Expected the end position ({end}) to be after the start position ({start}).");

		Start = start;
		End = end;
	}
	#endregion
}
