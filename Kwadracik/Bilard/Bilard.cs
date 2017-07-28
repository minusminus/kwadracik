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
        private bool IsMeshPoint(int sx, int sy, int x, int y)
        {
            return (x%(sx/2) == 0) && (y%sy == 0);
        }

        public Pocket SpecifyPocket(int sx, int sy, int x, int y)
        {
            Pocket res = Pocket.GL; //domyslnie w lewej gornej
            //if (x%(2*sx) == 0) res = Pocket.GL;
            if (x > 0)
            {
                if (x%sx == 0) res = Pocket.GP; //prawej
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
            {
                if (py == 0)
                {
                    if (wx > 0)
                    {
                        if (px < sx/2) return Pocket.DS;
                        return Pocket.DP;
                    }
                    if (px > sx / 2) return Pocket.DS;
                    return Pocket.DL;
                }
                if (py == sy)
                {
                    if (wx > 0)
                    {
                        if (px < sx / 2) return Pocket.GS;
                        return Pocket.GP;
                    }
                    if (px > sx / 2) return Pocket.GS;
                    return Pocket.GL;
                }
                return Pocket.NIE;
            }

            //przypadki pionowe
            if (wx == 0)
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

            //i pozostale
            double a = (double) wy/(double) wx;
            double b = py - a*px;   

            return Pocket.NIE;
        }
    }
}
