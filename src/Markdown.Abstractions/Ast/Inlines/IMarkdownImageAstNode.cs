namespace OwlDomain.Markdown.Ast.Inlines;

/// <summary>
/// 	Represents an AST node for a markdown image.
/// </summary>
public interface IMarkdownImageAstNode : IMarkdownInlineAstNode, IMarkdownInlineContainerAstNode
{
	#region Properties
	/// <summary>The source of the image.</summary>
	string? Source { get; }

	/// <summary>The title of the image.</summary>
	string? Title { get; }
	#endregion
}
