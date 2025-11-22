using System.ComponentModel;

public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    public List<Feature> Features { get; set; } // Maps the root "features" array from the USGS GeoJSON feed.
}

public class Feature    // Each feature has a "properties" object containing "mag" and "place".
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public double? Mag { get; set; }     // Double is set for Magnitude. It is nullable to handle null from USGS.
    public string? Place { get; set; }   // String is set for Place. It is nullable to handle null from USGS.
}