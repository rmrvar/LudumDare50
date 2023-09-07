namespace Ai
{
    public abstract class DecoratorNode : CompositeNode
    {
        public DecoratorNode(Node child) : base(child)
        {
        }

        public Node Child => Children[0];
    }
}
