namespace OwlDomain.Markdown.Ast.Blocks.Containers;

/// <summary>
/// 	Represents an AST node for an ordered markdown list.
/// </summary>
public interface IMarkdownOrderedListAstNode : IMarkdownListBlockAstNode
{
	#region Properties
	/// <summary>The number that the list starts at.</summary>
	int Start { get; }
	#endregion
}
