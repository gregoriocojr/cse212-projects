using System.Globalization;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        if (words == null || words.Length == 0)     // Handles empty set.
        {
            return Array.Empty<string>();
        }

        var seen = new HashSet<string>();   // HashSet to store seen words. 
        var result = new List<string>();    // 'result' will contain pairs of words.

        foreach (var w in words)       // Iterate through each word (w) in the 'words'.
        {
            if (string.IsNullOrEmpty(w) || w.Length != 2)   // Error handling when the length of a 'word' in 'words' is not equal to 2.
            {
                continue;
            }

            if (w[0] == w[1])   // Handles special case for a word with same letters.
            {
                continue;
            }

            string rev = new string(new[] { w[1], w[0] });  // Determines the reverse (rev) of the 2-character word in words.
            if (seen.Contains(rev))     // If rev is already seen, then there is a symmetric pair.
            {
                result.Add($"{w} & {rev}"); // Add the pair in the 'result'.
            }
            else
            {
                seen.Add(w);    // Handles the characteristic of a set that order does not matter.
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            var degree = fields[3].Trim();      // Trim whitespace in the 4th column where the degree is.
            if (degrees.TryGetValue(degree, out var count)) // Tally the degree.
            {
                degrees[degree] = count + 1;
            }
            else
            {
                degrees[degree] = 1;    // If a degree appeared once.
            }
        }
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        var counts = new Dictionary<char, int>();

        int len1 = 0;   // Effective length of word1.
        foreach (char ch in word1)
        {
            if (char.IsWhiteSpace(ch))  // Ignore all whitespace.
            {
                continue;
            }
            char c = char.ToLowerInvariant(ch); // Ignore letter case.
            counts.TryGetValue(c, out int cur); // Increment count of characters in word1.
            counts[c] = cur + 1;
            len1++;
        }

        int len2 = 0;   // Effective length of word2.
        foreach (char ch in word2)
        {
            if (char.IsWhiteSpace(ch))
            {
                continue;
            }
            char c = char.ToLowerInvariant(ch);
            counts.TryGetValue(c, out int cur); // Decrement count of characters in word2.
            counts[c] = cur - 1;
            len2++;
        }

        if (len1 != len2)   // Handles immediately different effective lengths.
        {
            return false;
        }

        foreach (var kvp in counts) // Checks difference in occurence of letters. 
        {                           // Key value pairs (kvp) should always be equal to zero.
            if (kvp.Value != 0)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        var lines = new List<string>();     // Build the output.

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature?.Properties == null)    // Skip if feature or its properties are missing.
                {
                    continue;
                }
                string? place = feature.Properties.Place;
                double? mag = feature.Properties.Mag;

                if (string.IsNullOrWhiteSpace(place) || !mag.HasValue)  // Skip entries that don't have both a place and a magnitude.
                {
                    continue;
                }
                string magText = mag.Value.ToString("0.##", CultureInfo.InvariantCulture);  // Format magnitude with dot decimal and up to 2 decimals.

                lines.Add(place + " - Mag " + magText); // Build the required format.
            }
        } 
        return lines.ToArray();
    }
}