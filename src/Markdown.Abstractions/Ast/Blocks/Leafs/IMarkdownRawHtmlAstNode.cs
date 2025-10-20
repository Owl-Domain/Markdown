namespace OwlDomain.Markdown.Ast.Blocks.Leafs;

/// <summary>
/// 	Represents an AST node for a raw HTML block in a markdown document.
/// </summary>
public interface IMarkdownRawHtmlBlockAstNode : IMarkdownLeafBlockAstNode
{
	#region Properties
	/// <summary>The raw HTML code text.</summary>
	string Text { get; }
	#endregion
}
