namespace OwlDomain.Markdown.Ast.Inlines;

/// <summary>
/// 	Represents an AST node for a markdown code span.
/// </summary>
public interface IMarkdownCodeSpanAstNode : IMarkdownInlineAstNode
{
	#region Properties
	/// <summary>The text contained inside of the node.</summary>
	string Text { get; }
	#endregion
}
