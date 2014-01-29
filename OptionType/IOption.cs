using System.Collections.Generic;

namespace whiteshore.OptionType
{
    /// <summary>
    /// Option type is a wrapper arround possibly missing value of an object.
    /// 
    /// It aims to explicitly show that value could be missing and avoid null reference.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOption<out T> : IEnumerable<T>
    {
        /// <summary>
        /// Checks if Option has value.
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// Gets value from Option
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if value is missing</exception>
        T Value { get; }
    }
}
