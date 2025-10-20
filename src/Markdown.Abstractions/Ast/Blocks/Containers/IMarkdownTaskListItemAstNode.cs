namespace OwlDomain.Markdown.Ast.Blocks.Containers;

/// <summary>
/// 	Represents an AST node for a markdown task list item.
/// </summary>
public interface IMarkdownTaskListItemAstNode : IMarkdownListItemAstNode
{
	#region Properties
	/// <summary>The state of the task.</summary>
	/// <remarks>
	/// 	A value of <see langword="null"/> represents an indeterminate state,
	/// 	but not every markdown flavour will support this.
	/// </remarks>
	bool? State { get; }
	#endregion
}
