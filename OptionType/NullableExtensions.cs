using System;

namespace whiteshore.OptionType
{
    /// <summary>
    /// Monadic extensions for Nullable<T> type
    /// </summary>
    public static class NullableExtensions
    {
        /// <summary>
        /// Simple projection function
        /// </summary>
        /// <typeparam name="A">Input Option inner type</typeparam>
        /// <typeparam name="B">Output Option inner type</typeparam>
        /// <param name="option">Input option</param>
        /// <param name="func">Function applied over input Option</param>
        /// <returns>Mapped output Option</returns>
        public static Nullable<B> Select<A, B>(this Nullable<A> option, Func<A, Nullable<B>> func) 
            where A: struct 
            where B: struct
        {
            return option.HasValue ? func(option.Value) : (Nullable<B>)null;
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
        public static Nullable<C> SelectMany<A, B, C>(this Nullable<A> option, Func<A, Nullable<B>> func, Func<A, B, C> select)
            where A: struct
            where B: struct
            where C: struct
        {
            return option.Select(aval => func(aval).Select(bval => (Nullable<C>)(select(aval, bval))));
        }
    }
}
