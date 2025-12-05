public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        if (value == Data)  // Handles duplicate: if value already exist, do nothing.
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2

        if (value == Data)  // Base case of the recursion.
            return true;

        // Start searching on the left subtree if value is smaller.
        if (value < Data)
            return Left is not null && Left.Contains(value);

        // Otherwise, search the right subtree.
        return Right is not null && Right.Contains(value);
    }

    public int GetHeight()
    {
        // TODO Start Problem 4

        // Declare variables for left and right heights
        int leftHeight = 0;
        int rightHeight = 0;

        // Check the height of the left subtree.
        if (Left != null)
            leftHeight = Left.GetHeight();

        // Check the height of the right subtree.
        if (Right != null)
            rightHeight = Right.GetHeight();

        // Get the higher height and add 1. That's the height of the tree.
        return 1 + Math.Max (leftHeight, rightHeight);
    }
}