namespace OwlDomain.Markdown.Ast.Blocks.Leafs;

/// <summary>
/// 	Represents an AST node for a markdown heading.
/// </summary>
public interface IMarkdownHeadingAstNode : IMarkdownLeafBlockAstNode, IMarkdownInlineContainerAstNode
{
	#region Properties
	/// <summary>The level of the heading.</summary>
	int Level { get; }
	#endregion
}
