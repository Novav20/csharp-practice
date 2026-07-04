public static class PredicateComposer
{
    public static Func<T, bool> And<T>(this Func<T, bool> left, Func<T, bool> right)
    => reading => left(reading) && right(reading);
    public static Func<T, bool> Or<T>(this Func<T, bool> left, Func<T, bool> right)
    => reading => left(reading) || right(reading);
}