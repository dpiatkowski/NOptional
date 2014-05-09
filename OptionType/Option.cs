using System;

namespace whiteshore.OptionType
{
    /// <summary>
    /// Extensions for Option type
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// Static factory method of Option object
        /// </summary>
        /// <typeparam name="T">Option inner type</typeparam>
        /// <param name="obj">Object value</param>
        /// <returns>None if <paramref name="obj"/> is null, Some of <paramref name="obj"/> otherwise</returns>
        /// <remarks><typeparamref name="T"/> must be a class, otherwise Option is redundant on nullable structs</remarks>
        public static IOption<T> Of<T>(T obj) where T: class
        {
            if (obj == null)
            {
                return new None<T>();
            }

            return new Some<T>(obj);
        }

        /// <summary>
        /// Static factory method for None object
        /// </summary>
        /// <typeparam name="T">Option type holder</typeparam>
        /// <returns>None object</returns>
        public static IOption<T> None<T>()
        {
            return new None<T>();
        }

        /// <summary>
        /// Converts object to an Option object
        /// </summary>
        /// <typeparam name="T">Option inner type</typeparam>
        /// <param name="value">Arbitrary object</param>
        /// <returns>Some object with given value</returns>
        public static IOption<T> AsOption<T>(this T value)
        {
            return new Some<T>(value);
        }

        /// <summary>
        /// Simple projection function
        /// </summary>
        /// <typeparam name="A">Input Option inner type</typeparam>
        /// <typeparam name="B">Output Option inner type</typeparam>
        /// <param name="option">Input option</param>
        /// <param name="func">Function applied over input Option</param>
        /// <returns>Mapped output Option</returns>
        public static IOption<B> Select<A, B>(this IOption<A> option, Func<A, IOption<B>> func)
        {
            return option.HasValue ? func(option.Value) : new None<B>();
        }

        /// <summary>
        /// Monadic bind function
        /// </summary>
        /// <typeparam name="A">Input Option inner type</typeparam>
        /// <typeparam name="B">Intermediate, not flatenned Option inner type</typeparam>
        /// <typeparam name="C">Output Option inner type</typeparam>
        /// <param name="option">Input option</param>
        /// <param name="func">Function applied over input Option</param>
        /// <param name="select">Function flattening result of application</param>
        /// <returns>Flat-mapped output Option</returns>
        /// <remarks>
        /// Monad laws:
        /// 1. Left identity: Option.Of(obj).SelectMany(f) == f(obj)
        /// 2. Right identity: option.SelectMany(o => Option.Of(o)) == option
        /// 3. Associativity: o1.SelectMany(f).SelectMany(g) == o1.SelectMany(x => f(x).SelectMany(g))
        /// </remarks>
        public static IOption<C> SelectMany<A, B, C>(this IOption<A> option, Func<A, IOption<B>> func, Func<A, B, C> select)
        {
            return option.Select(aval => func(aval).Select(bval => select(aval, bval).AsOption()));
        }
    }
}
