namespace OwlDomain.Markdown.Ast.Inlines;

/// <summary>
/// 	Represents an AST node for markdown text.
/// </summary>
public interface IMarkdownTextAstNode : IMarkdownInlineAstNode
{
	#region Properties
	/// <summary>The text contained inside of the node.</summary>
	string Text { get; }
	#endregion
}
