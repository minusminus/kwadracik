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
            //double a = (double) wy/(double) wx;
            //double b = py - a*px;
            //long kx = sx/2;
            //long lcm;
            //if (px >= kx)
            //    lcm = NumbersTheory.LCM(wx, kx - (px - kx));
            //else
            //    lcm = NumbersTheory.LCM(wx, kx - px);

            //jezeli ruch w lewo to odbijamy stol wzgledem srodka
            bool tablefilppedvertical = (wx < 0);
            bool tableflippedhorizontal = (wy < 0);
            if (tablefilppedvertical)
            {
                px = (sx/2) + (sx/2 - px);
                wx = -wx;
            }
            if (tableflippedhorizontal)
            {
                py = sy - py;
                wy = -wy;
            }

            //obliczenia dla ruchu w prawo do gory
            long mx, my, mp;
            //napierw do prawej krawedzi siatki
            long kx = sx/2;
            if (px >= kx)
                mp = NumbersTheory.LCM(wx, kx - (px - kx))/wx;
            else
                mp = NumbersTheory.LCM(wx, kx - px)/wx;
            mx = px + wx*mp;
            my = py + wy*mp;
            //jezeli po przeskalowaniu punkt jest wewnatrz oryginalnego stolu, to skalowanie w pionie
            if (!IsMeshPoint(sx, sy, (int) mx, (int) my) && (wy*mp < sy))
            {
                if (py < sy)
                    mp = NumbersTheory.LCM(wy, sy - py)/wy;
                else
                    mp = NumbersTheory.LCM(wy, sy)/wy;
                mx = px + wx*mp;
                my = py + wy*mp;
            }

            Pocket res = Pocket.NIE;
            if (IsMeshPoint(sx, sy, (int) mx, (int) my))
                res = SpecifyPocket(sx, sy, (int) mx, (int) my);
            if (tablefilppedvertical)
            {   //ruch byl w lewo - stol byl odbity pionowo -> zamieniamy luzy lewe z prawymi
                if (res == Pocket.GL) res = Pocket.GP;
                else if (res == Pocket.GP) res = Pocket.GL;
                else if (res == Pocket.DL) res = Pocket.DP;
                else if (res == Pocket.DP) res = Pocket.DL;
            }
            if (tableflippedhorizontal)
            {   //ruch byl w dol - stol odbity poziomo -> zmieniamy luzy gorne i dolne
                if (res == Pocket.GL) res = Pocket.DL;
                else if (res == Pocket.GP) res = Pocket.DP;
                else if (res == Pocket.GS) res = Pocket.DS;
                else if (res == Pocket.DL) res = Pocket.GL;
                else if (res == Pocket.DP) res = Pocket.GP;
                else if (res == Pocket.DS) res = Pocket.GS;
            }

            return res;
        }
    }
}
