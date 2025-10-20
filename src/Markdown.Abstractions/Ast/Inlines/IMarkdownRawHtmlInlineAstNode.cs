namespace OwlDomain.Markdown.Ast.Inlines;

/// <summary>
/// 	Represents an AST node for raw inline HTML code in a markdown document.
/// </summary>
public interface IMarkdownRawHtmlInlineAstNode : IMarkdownInlineAstNode
{
	#region Properties
	/// <summary>The raw HTML code text.</summary>
	string Text { get; }
	#endregion
}

