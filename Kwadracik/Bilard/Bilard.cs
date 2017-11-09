using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberTheory;

/// <summary>
/// http://main.edu.pl/pl/archive/pa/2005/bil
/// </summary>
namespace Bilard
{
    public enum Pocket {NIE, GL, GP, GS, DL, DP, DS};

    public class Bilard
    {
        private bool IsMeshPoint(int sx, int sy, long x, long y)
        {
            return (x%(sx/2) == 0) && (y%sy == 0);
        }

        public Pocket SpecifyPocket(int sx, int sy, long x, long y)
        {
            Pocket res = Pocket.GL; //domyslnie w lewej gornej
            if (x > 0)
            {
                if (x%sx == 0)
                {
                    if ((x/sx)%2 != 0) res = Pocket.GP; //w prawej dla nieparzystych krat calego stolu (0-lewa, 1-prawa, 2-lewa, 3-prawa, ...)
                }
                else if (x%(sx/2) == 0) res = Pocket.GS; //srodkowej
            }
            if (y%(2*sy) == 0) res += 3;    //dolne kieszenie
            return res;
        }

        /// <summary>
        /// sx, sy - rozmiar stolu (sx zawsze parzyste)
        /// px, py - polozenie kuli
        /// wx, wy - wektor ruchu/uderzenia
        /// </summary>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="wx"></param>
        /// <param name="wy"></param>
        /// <returns></returns>
        public Pocket CheckPocket(int sx, int sy, int px, int py, int wx, int wy)
        {
            //od razu w luzie
            if (IsMeshPoint(sx, sy, px, py))
                return SpecifyPocket(sx, sy, px, py);

            //bez ruchu
            if ((wx == 0) && (wy == 0)) return Pocket.NIE;

            //przypadki poziome
            if (wy == 0)
                return GetHorizontalPocket(sx, sy, px, py, wx);

            //przypadki pionowe
            if (wx == 0)
                return GetVerticalPocket(sx, px, wy);

            //i pozostale, liczone dla rucho w prawo do gory, inne warianty trzeba przeksztalcic

            //jezeli ruch w lewo to odbijamy stol wzgledem srodka
            bool tablefilppedvertical = (wx < 0);
            bool tableflippedhorizontal = (wy < 0);
            if (tablefilppedvertical)
                FlipVertical(sx, ref px, ref wx);
            if (tableflippedhorizontal)
                FlipHorizontal(sy, ref py, ref wy);

            //przeskalowanie wektora do najmniejszego mozliwego
            int wgcd = (int) NumbersTheory.GCDBinary(wx, wy);
            wx /= wgcd;
            wy /= wgcd;

            long a, b, c;
            a = (sx/2)*wy;
            b = wx*py - wy*px;
            c = sy*wx;
            long n, m, p, q;
            n = a/c;
            m = NumbersTheory.Mod(a, c);
            p = b/c;
            q = NumbersTheory.Mod(b, c);

            //analizowane 4 przypadki A/C i B/C
            bool found = true;
            long coefa = 0;
            if ((m == 0) && (q == 0))
            {
                //t > 0 -> a > px/w
                coefa = px / (sx / 2) + 1;
            }
            else if ((m == 0) && (q != 0))
            {
                found = false;
            }
            else if ((m != 0) && (q == 0))
            {
                coefa = c / NumbersTheory.GCDBinary(m, c);
            }
            else if ((m != 0) && (q != 0))
            {
                long l, k;
                long d = NumbersTheory.GCDExt(m, c, out l, out k);
                if (-q%d == 0)
                {
                    long x0 = NumbersTheory.Mod(l*(-q/d), c);
                    coefa = x0;
                    for (long i = 1; i < d; i++) //zgodnie z alg z Cormena powinno byc od 0, ale element 0 jest obliczony wczesniej
                    {
                        long xx = NumbersTheory.Mod(x0 + i*(c/d), c);
                        if (xx < coefa) coefa = xx;
                    }
                }
                else
                    found = false;
            }
            //ustalenie punktu kratowego i luzy
            Pocket res = Pocket.NIE;
            if (found)
            {
                long t = (coefa * (sx / 2) - px) / wx;
                long rx = px + t * wx;
                long ry = py + t * wy;
                if (IsMeshPoint(sx, sy, rx, ry))
                    res = SpecifyPocket(sx, sy, rx, ry);
            }


            //korekta wyniku jezeli stol byl odbijany
            if (tablefilppedvertical)   //ruch byl w lewo - stol byl odbity pionowo -> zamieniamy luzy lewe z prawymi
                res = BackflipVertical(res);
            if (tableflippedhorizontal) //ruch byl w dol - stol odbity poziomo -> zmieniamy luzy gorne i dolne
                res = BackflipHorizontal(res);

            return res;
        }

        private Pocket BackflipHorizontal(Pocket res)
        {
            if (res == Pocket.GL) res = Pocket.DL;
            else if (res == Pocket.GP) res = Pocket.DP;
            else if (res == Pocket.GS) res = Pocket.DS;
            else if (res == Pocket.DL) res = Pocket.GL;
            else if (res == Pocket.DP) res = Pocket.GP;
            else if (res == Pocket.DS) res = Pocket.GS;
            return res;
        }

        private Pocket BackflipVertical(Pocket res)
        {
            if (res == Pocket.GL) res = Pocket.GP;
            else if (res == Pocket.GP) res = Pocket.GL;
            else if (res == Pocket.DL) res = Pocket.DP;
            else if (res == Pocket.DP) res = Pocket.DL;
            return res;
        }

        private void FlipHorizontal(int sy, ref int py, ref int wy)
        {
            py = sy - py;
            wy = -wy;
        }

        private void FlipVertical(int sx, ref int px, ref int wx)
        {
            px = (sx/2) + (sx/2 - px);
            wx = -wx;
        }

        private Pocket GetVerticalPocket(int sx, int px, int wy)
        {
            if (px == 0)
            {
                if (wy > 0) return Pocket.GL;
                return Pocket.DL;
            }
            if (px == sx/2)
            {
                if (wy > 0) return Pocket.GS;
                return Pocket.DS;
            }
            if (px == sx)
            {
                if (wy > 0) return Pocket.GP;
                return Pocket.DP;
            }
            return Pocket.NIE;
        }

        private Pocket GetHorizontalPocket(int sx, int sy, int px, int py, int wx)
        {
            if (py == 0)
            {
                if (wx > 0)
                {
                    if (px < sx/2) return Pocket.DS;
                    return Pocket.DP;
                }
                if (px > sx/2) return Pocket.DS;
                return Pocket.DL;
            }
            if (py == sy)
            {
                if (wx > 0)
                {
                    if (px < sx/2) return Pocket.GS;
                    return Pocket.GP;
                }
                if (px > sx/2) return Pocket.GS;
                return Pocket.GL;
            }
            return Pocket.NIE;
        }
    }
}
