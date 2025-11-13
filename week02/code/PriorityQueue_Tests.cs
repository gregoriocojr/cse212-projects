using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several 'values' with varying priorities, including 'values' with the same highest priority,
    // then dequeue all 'values' to verify correct order, FIFO tie-breaking for equal priorities,
    // and that 'values' are removed after each dequeue.
    // Expected Result: Items are dequeued in descending priority order. Ties are resolved FIFO. All items are removed.
    // Defect(s) Found: First 'value' was skipped, FIFO logic is not implemented, and no actual dequeue was observed.
    public void TestPriorityQueue_FullOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Peter", 2);
        priorityQueue.Enqueue("James", 1);
        priorityQueue.Enqueue("Judas", 4);
        priorityQueue.Enqueue("John", 3);
        priorityQueue.Enqueue("Mark", 3);

        string[] expectedOrder = { "Judas", "John", "Mark", "Peter", "James" };
        int i = 0;

        while (true)
        {
            try
            {
                var value = priorityQueue.Dequeue();
                Assert.AreEqual(expectedOrder[i], value, $"Expected {expectedOrder[i]} but got {value}");
                i++;
            }
            catch (InvalidOperationException)
            {
                break; // Queue is empty
            }
        }

        Assert.AreEqual(expectedOrder.Length, i, "All items should have been dequeued in the expected order and removed from the queue.");
    }
    
    [TestMethod]
    // Scenario: Try to dequeue from empty queue.
    // Expected Result: InvalidOperationException is thrown with message 'The queue is empty.'
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", ex.Message, "Expected an InvalidOperationException with the correct message.");
    }
}