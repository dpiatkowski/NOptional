namespace NOptional
{
    public static class OptionalEx
    {
        /// <summary>
        /// Wraps current value in Optional type
        /// </summary>
        /// <typeparam name="T">Inner value type</typeparam>
        /// <param name="value">Some object that may or may not be null</param>
        /// <returns>Some(value) if value is not null, empty Optional otherwise</returns>
        public static Optional<T> AsOption<T>(this T value) where T : class
        {
            return Optional<T>.OfNullable(value);
        }
    }
}
