using System;
using System.Collections;
using System.Collections.Generic;

namespace whiteshore.OptionType
{
    /// <summary>
    /// Case class for missing value
    /// </summary>
    /// <typeparam name="T">Option type holder</typeparam>
    public class None<T> : IOption<T>
    {
        /// <summary>
        /// Checks if Option has value.
        /// </summary>
        public bool HasValue
        {
            get { return false; }
        }

        /// <summary>
        /// Throws an InvalidOperationException because value is missing
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if value is missing</exception>
        public T Value
        {
            get { throw new InvalidOperationException("Cannot get value from None"); }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a Option
        /// </summary>
        /// <returns>Empty sequence</returns>
        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
