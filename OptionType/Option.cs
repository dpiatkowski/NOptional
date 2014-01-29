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
        /// <typeparam name="T">Option type holder</typeparam>
        /// <param name="obj">Object value</param>
        /// <returns>None if <paramref name="obj"/> is null, Some otherwise</returns>
        /// <remarks><typeparamref name="T"/> must be a class, otherwise Option is redundant</remarks>
        public static IOption<T> Of<T>(T obj) where T: class
        {
            if(obj == null)
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
        /// Converts object to Option object
        /// </summary>
        /// <typeparam name="T">Option type holder</typeparam>
        /// <param name="value">Arbitrary object</param>
        /// <returns>Some object with given value</returns>
        public static IOption<T> AsOption<T>(this T value)
        {
            return new Some<T>(value);
        }

        /// <summary>
        /// Simple projection function
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="option"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IOption<B> Select<A, B>(this IOption<A> option, Func<A, IOption<B>> func)
        {
            return option.HasValue ? func(option.Value) : new None<B>();
        }

        /// <summary>
        /// Monadic bind function
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="option"></param>
        /// <param name="func"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static IOption<C> SelectMany<A, B, C>(this IOption<A> option, Func<A, IOption<B>> func, Func<A, B, C> select)
        {
            return option.Select(aval => func(aval).Select(bval => select(aval, bval).AsOption()));
        }
    }
}
