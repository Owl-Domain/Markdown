namespace OwlDomain.Markdown.Ast.Blocks.Containers;

/// <summary>
/// 	Represents an AST node for a markdown container block.
/// </summary>
public interface IMarkdownContainerBlockAstNode : IMarkdownBlockAstNode
{
	#region Properties
	/// <summary>The child blocks inside of the container.</summary>
	IReadOnlyList<IMarkdownBlockAstNode> Children { get; }
	#endregion
}

/// <summary>
/// 	Represents an AST node for a markdown container block.
/// </summary>
/// <typeparam name="T">The type of the child blocks.</typeparam>
public interface IMarkdownContainerBlockAstNode<out T> : IMarkdownContainerBlockAstNode
	where T : class, IMarkdownBlockAstNode
{
	#region Properties
	/// <summary>The child blocks inside of the container.</summary>
	new IReadOnlyList<T> Children { get; }
	IReadOnlyList<IMarkdownBlockAstNode> IMarkdownContainerBlockAstNode.Children => Children;
	#endregion
}
