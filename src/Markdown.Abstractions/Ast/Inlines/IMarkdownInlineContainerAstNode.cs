namespace OwlDomain.Markdown.Ast.Inlines;

/// <summary>
/// 	Represents an AST node that contains markdown inline elements.
/// </summary>
public interface IMarkdownInlineContainerAstNode : IMarkdownAstNode
{
	#region Properties
	/// <summary>The child inlines inside of the container.</summary>
	IReadOnlyList<IMarkdownInlineAstNode> Children { get; }
	#endregion
}

/// <summary>
/// 	Represents an AST node that contains markdown inline elements.
/// </summary>
/// <typeparam name="T">The type of the child inline elements.</typeparam>
public interface IMarkdownInlineContainerAstNode<out T> : IMarkdownInlineContainerAstNode
	where T : class, IMarkdownInlineAstNode
{
	#region Properties
	/// <summary>The child inlines inside of the container.</summary>
	new IReadOnlyList<T> Children { get; }
	IReadOnlyList<IMarkdownInlineAstNode> IMarkdownInlineContainerAstNode.Children => Children;
	#endregion
}
