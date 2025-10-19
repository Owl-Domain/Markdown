namespace OwlDomain.Markdown.Ast.Blocks.Leafs;

/// <summary>
/// 	Represents an AST node for a markdown code block.
/// </summary>
public interface IMarkdownCodeBlockAstNode : IMarkdownLeafBlockAstNode
{
	#region Properties
	/// <summary>The info text specified for the code block.</summary>
	string? InfoText { get; }

	/// <summary>The text contained inside of the node.</summary>
	string Text { get; }
	#endregion
}
