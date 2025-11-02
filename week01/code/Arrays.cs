using System.Globalization;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN for Problem 1
        // 1. Create an empty List<double> to store the multiples.
        // 2. Use a loop variable i from 1 to 'length'.
        // 3. Compute multiple = number * i.
        // 4. Add the computed mulitple to the list of nultiples.
        // 5. Print the list to an array.

        var multiples = new List<double>();

        for (int i = 1; i <= length; i++)
        {
            double multiple = number * i;
            multiples.Add(multiple);
        }

        return multiples.ToArray(); // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN for Problem 2 (assumption: amount <= data.Count)
        // 1. Initialize the n = data.Count (number of items in 'data').
        // 2. Create a tempList1<int> for the rightmost 'data' as determined by the 'amount'.
        // 3. Create a tempList2<int> for the remaining 'data' from the original list.
        // 4. Clear the original list.
        // 5. Add the range from tempList1, then tempList2.
        // 6. Return the array.

        int n = data.Count;

        List<int> tempList1 = data.GetRange(n - amount, amount);

        List<int> tempList2 = data.GetRange(0, n - amount);

        data.Clear();

        data.AddRange(tempList1);

        data.AddRange(tempList2);

        return;
    }
}
