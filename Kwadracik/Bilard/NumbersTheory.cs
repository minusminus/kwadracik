using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberTheory
{
    /// <summary>
    /// Klasa obsługi mniejszych obliczeń
    /// </summary>
    public class NumbersTheory
    {
        /// <summary>
        /// największy wspólny dzielnik
        /// https://en.wikipedia.org/wiki/Binary_GCD_algorithm
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static long GCDBinary(long u, long v)
        {
            int shift;

            /* GCD(0,v) == v; GCD(u,0) == u, GCD(0,0) == 0 */
            if (u == 0) return v;
            if (v == 0) return u;

            /* Let shift := lg K, where K is the greatest power of 2
                  dividing both u and v. */
            for (shift = 0; ((u | v) & 1) == 0; ++shift)
            {
                u >>= 1;
                v >>= 1;
            }

            while ((u & 1) == 0)
                u >>= 1;

            /* From here on, u is always odd. */
            do
            {
                /* remove all factors of 2 in v -- they are not common */
                /*   note: v is not zero, so while will terminate */
                while ((v & 1) == 0)  /* Loop X */
                    v >>= 1;

                /* Now u and v are both odd. Swap if necessary so u <= v,
                   then set v = v - u (which is even). For bignums, the
                   swapping is just pointer movement, and the subtraction
                   can be done in-place. */
                if (u > v)
                {
                    long t = v; v = u; u = t;
                }  // Swap u and v.
                v = v - u;                       // Here v >= u.
            } while (v != 0);

            /* restore common factors of 2 */
            return u << shift;
        }

        /// <summary>
        /// najmniejsza wspolna wielokrotnosc
        /// https://en.wikipedia.org/wiki/Least_common_multiple
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static long LCM(long u, long v)
        {
            long g = GCDBinary(u, v);
            return (u*v)/g;
        }

        /// <summary>
        /// a^b mod q
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static long ExpMod(long a, long b, long q)
        {
            long p = 1;
            while (b > 0)
            {
                if ((b & 1) == 1) p = (a*p)%q;
                a = (a*a)%q;
                b /= 2;
            }
            return p;
        }
    }
}
