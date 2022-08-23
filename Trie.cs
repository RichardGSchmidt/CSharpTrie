using System;

public class Trie
{
	private readonly Node _root;

	public Trie() //base constructor. Initializes root as a null parentless node.
    {
		_root = new Node("^", 0, null);
    }

	

	public Node Prefix(string stringIn)
    {
		Node currentNode = _root;
		Node result = currentNode;
		foreach(char c in stringIn)
        {
			currentNode = currentNode.FindChildNode(c);
			if (currentNode == null)
            {
				break;
            }
			result = currentNode;
        }
		return result;
    }

	public bool Search(string queryString)
    {
		Node prefix = Prefix(queryString);
		return (prefix.Depth == queryString.Length && prefix.FindChildNode('$')!=null);
    }

	public void Insert(string s)
    {
		Node prefix = Prefix(s);
		Node current = prefix;
		for (int i = current.Depth; i < s.Length; i++)
        {
			Node newNode = new Node(s[i], current.Depth+1, current);
			current.Children.Add(newNode);
			current = newNode;
        }
		current.Children.Add(new Node('$', current.Depth + 1, current);
    }

	public void InsertList(List<string> items)
    {
		foreach(string item in items)
        		Insert(item);
    }

	public void Delete(string s)
    {
		if (Search(s))
        {
			Node node = Prefix(s).FindChildNode('$');
			while(node.IsLeaf())
            {
				Node parent = node.Parent;
				parent.Children.DeleteChild(node.Value);
				node = parent;
            }

        }
    }
}

public class Node
{
	public char Value { get; set; }
	public List<Node> Children { get; set; }
	public Node Parent { get; set; }
	public int Depth { get; set; }

	public Node(char value, int depth, Node parent)
	{
		Value = value;
		Children = new List<Node>();
		Depth = depth;
		Parent = parent;

	}

	public bool IsLeaf()
	{
		return (Children.Count == 0);
	}

	public Node FindChildNode(char c)
    {
		foreach (Node child in Children)
        {
			if (child.Value == c)
				return child;
        }
		return null;
    }

	public void DeleteChild(char c)
    {
		for(int i = 0; i < Children.Count; i++)
        {
			if (Children[i].Value == c)
            {
				Children.RemoveAt(i);
            }
        }
    }
}
