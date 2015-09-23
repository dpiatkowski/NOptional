using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NOptional
{
    /// <summary>
    /// Represents value that may or may not exist, and it's existence can be checked at compile-time
    /// </summary>
    /// <typeparam name="T">Refence type</typeparam>
    public class Optional<T> : IEnumerable<T>, IEquatable<Optional<T>> where T : class
    {
        private readonly T _value;

        private readonly bool _isEmpty;

        private Optional(T value)
        {
            _value = value;
        }

        private Optional()
        {
            _isEmpty = true;
        }

        /// <summary>
        /// Returns an empty Optional instance.
        /// </summary>
        public static readonly Optional<T> Empty = new Optional<T>();

        /// <summary>
        /// Return true if there is a value present, otherwise false.
        /// </summary>
        public bool IsPresent => !_isEmpty;

        /// <summary>
        /// Returns an Optional with the specified present non-null value.
        /// </summary>
        /// <param name="value">The non-null value to be present</param>
        /// <exception cref="ArgumentNullException">Thrown if value is null</exception>
        /// <returns>Optional instance with the value present</returns>
        public static Optional<T> Of(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "Optional cannot be instatiated with null reference");
            }

            return new Optional<T>(value);
        }

        /// <summary>
        /// Returns an Optional with the specified value, if non-null, otherwise returns an empty Optional.
        /// </summary>
        /// <param name="value">Possibly-null value</param>
        /// <returns>Optional instance with a present value or an empty Optional</returns>
        public static Optional<T> OfNullable(T value)
        {
            return value == null ? new Optional<T>() : new Optional<T>(value);
        }

        /// <summary>
        /// If a value is present in this Optional, returns the value
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if there is no value present</exception>
        /// <returns>The non-null value held by this Optional</returns>
        public T Get()
        {
            if (_isEmpty)
            {
                throw new InvalidOperationException();
            }

            return _value;
        }

        /// <summary>
        /// Return the value if present, otherwise return fallback value.
        /// </summary>
        /// <param name="fallbackValue">The value to be returned if there is no value present. May be null</param>
        /// <returns></returns>
        public T OrElse(T fallbackValue)
        {
            return _isEmpty ? fallbackValue : _value;
        }

        /// <summary>
        /// Return the value if present, otherwise invoke supplier and return the result of that invocation.
        /// </summary>
        /// <param name="fallbackValueSupplier">Supplier function which result is returned if no value is present</param>
        /// <exception cref="ArgumentNullException">Throws if no value is present and fallbackValueSupplier is null</exception>
        /// <returns>Value if present otherwise the result of supplier function</returns>
        public T OrElseGet(Func<T> fallbackValueSupplier)
        {
            if (_isEmpty && fallbackValueSupplier == null)
            {
                throw new ArgumentNullException("fallbackValueSupplier", "Fallback value supplier cannot be null");
            }

            return _isEmpty ? fallbackValueSupplier() : _value;
        }

        /// <summary>
        /// Return the value if present, otherwise invoke async supplier and return the result of that invocation.
        /// </summary>
        /// <param name="asyncFallbackValueSupplier">Supplier async function which result is returned if no value is present</param>
        /// <exception cref="ArgumentNullException">Throws if no value is present and asyncFallbackValueSupplier is null</exception>
        /// <returns>Value if present otherwise the result of supplier function</returns>
        public async Task<T> OrElseGetAsync(Func<Task<T>> asyncFallbackValueSupplier)
        {
            if (_isEmpty && asyncFallbackValueSupplier == null)
            {
                throw new ArgumentNullException("asyncFallbackValueSupplier", "Fallback value supplier cannot be null");
            }

            return _isEmpty ? await asyncFallbackValueSupplier() : _value;
        }

        /// <summary>
        /// Return the contained value, if present, otherwise throw an exception to be created by the provided factory.
        /// </summary>
        /// <typeparam name="TException">Type of the exception to be thrown</typeparam>
        /// <param name="exceptionFactory">Factory which will return the exception to be thrown</param>
        /// <exception cref="ArgumentNullException">Throws if no value is present and exceptionSupplier is null</exception>
        /// <returns>Present value</returns>
        public T OrElseThrow<TException>(Func<TException> exceptionFactory) where TException : Exception, new()
        {
            if (_isEmpty && exceptionFactory == null)
            {
                throw new ArgumentNullException("exceptionFactory", "Exception factory cannot be null");
            }

            if (_isEmpty)
            {
                throw exceptionFactory();
            }

            return _value;
        }

        /// <summary>
        /// If a value is present, invoke the specified action with the value, otherwise do nothing.
        /// </summary>
        /// <param name="consumer">Action to be exectued</param>
        public void IfPresent(Action<T> consumer)
        {
            if (!_isEmpty)
            {
                consumer(_value);
            }
        }

        /// <summary>
        /// If a value is present, invoke the specified async action with the value, otherwise do nothing.
        /// </summary>
        /// <param name="asyncConsumer">Action to be exectued</param>
        /// <returns>Awaitable action</returns>
        public async Task IfPresentAsync(Func<T, Task> asyncConsumer)
        {
            if (!_isEmpty)
            {
                await asyncConsumer(_value);
            }
        }

        /// <summary>
        /// Simple projection function
        /// </summary>
        /// <typeparam name="Y">Output Optional inner type</typeparam>
        /// <param name="func">Function applied over input Optional</param>
        /// <returns>Mapped output Optional</returns>
        public Optional<Y> Select<Y>(Func<T, Optional<Y>> func) where Y : class
        {
            return _isEmpty ? new Optional<Y>() : func(Get());
        }

        /// <summary>
        /// Monadic bind function
        /// </summary>
        /// <typeparam name="Y">Intermediate, not flatenned Option inner type</typeparam>
        /// <typeparam name="Z">Output Option inner type</typeparam>
        /// <param name="func">Function applied over input Optional</param>
        /// <param name="select">Function flattening result of application</param>
        /// <returns>Flat-mapped output Optional</returns>
        /// <remarks>
        /// Monad laws:
        /// 1. Left identity: Optional.Of(obj).SelectMany(f) == f(obj)
        /// 2. Right identity: option.SelectMany(o => Optional.Of(o)) == option
        /// 3. Associativity: o1.SelectMany(f).SelectMany(g) == o1.SelectMany(x => f(x).SelectMany(g))
        /// </remarks>
        public Optional<Z> SelectMany<Y, Z>(Func<T, Optional<Y>> func, Func<T, Y, Z> select) where Y : class where Z : class
        {
            return Select(aval => func(aval).Select(bval => new Optional<Z>(select(aval, bval))));
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!_isEmpty)
            {
                yield return _value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Two optionals are equal if:
        /// 1. both instances have no value present or;
        /// 2. the present values are "equal to" each other via Equals().
        /// </summary>
        /// <param name="other">Second optional</param>
        /// <returns>True if objects are equal, false otherwise.</returns>
        public bool Equals(Optional<T> other)
        {
            if (!IsPresent && !other.IsPresent)
            {
                return true;
            }

            if (IsPresent && other.IsPresent)
            {
                return Get().Equals(other.Get());
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Optional<T>;

            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return _isEmpty ? 0 : _value.GetHashCode();
        }

        public override string ToString()
        {
            return _isEmpty ? "NONE" : $"Some({_value})";
        }
    }
}
