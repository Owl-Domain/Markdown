namespace OwlDomain.Markdown.Ast;

/// <summary>
/// 	Represents a markdown AST node.
/// </summary>
public interface IMarkdownAstNode
{
	#region Properties
	/// <summary>The position that the node takes up in the parsed text.</summary>
	NodePosition Position { get; }
	#endregion
}
