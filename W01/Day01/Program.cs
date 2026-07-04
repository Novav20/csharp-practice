static IEnumerable<Reading> GetCriticalReadings(IEnumerable<Reading>? readings)
{
    ArgumentNullException.ThrowIfNull(readings);

    foreach (var reading in readings)
    {
        if (reading.Vibration > 7.2 || reading.Temperature > 95)
        {
            yield return reading;
        }
    }

}


Console.WriteLine("Exercise 1: Filtering critical readings");

IEnumerable<Reading>? readings =
[
    new Reading(102.1, 70.1, 5.5 ),
    new Reading(101.3, 110.7, 4.4 ),
    new Reading(105.5, 98.4, 7.5 ),
    new Reading(111.6, 122.5, 8.1 ),
];


try
{

    var readings_critical = GetCriticalReadings(readings);
    foreach (var reading in readings_critical)
    {
        Console.WriteLine(reading);
    }
}
catch (ArgumentNullException ex)
{

    Console.WriteLine($"ERROR: {ex.Message}");
}

