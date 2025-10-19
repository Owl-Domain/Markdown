namespace OwlDomain.Markdown.Ast.Inlines;

/// <summary>
/// 	Represents an AST node for a markdown link.
/// </summary>
public interface IMarkdownLinkAstNode : IMarkdownInlineAstNode, IMarkdownInlineContainerAstNode
{
	#region Properties
	/// <summary>The destination of the link.</summary>
	string? Destination { get; }

	/// <summary>The title for the link.</summary>
	string? Title { get; }
	#endregion
}
