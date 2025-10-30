namespace OwlDomain.Markdown.Ast.Blocks.Containers;

/// <summary>
/// 	Represents an AST node for a markdown list item.
/// </summary>
public interface IMarkdownListItemAstNode : IMarkdownContainerBlockAstNode
{
	#region Properties
	/// <summary>The implicit number given to the list item.</summary>
	int Number { get; }
	#endregion
}
