using System.Collections;
using System.Collections.Generic;

namespace whiteshore.OptionType
{
    /// <summary>
    /// Case class wrapping existing value
    /// </summary>
    /// <typeparam name="T">Option type holder</typeparam>
    public class Some<T> : IOption<T>
    {
        /// <summary>
        /// Gets value from Option
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Constructs Some object with given value
        /// </summary>
        /// <param name="value">Object to be stored as Option</param>
        public Some(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Checks if Option has value.
        /// </summary>
        public bool HasValue
        {
            get { return true; }
        }

        /// <summary>
        /// String representation of inner value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a Option
        /// </summary>
        /// <returns>Enumerator over sequence with single element</returns>
        public IEnumerator<T> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
