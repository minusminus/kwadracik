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

        private Pocket SpecifyPocket(int sx, int sy, int x, int y)
        {
            
        }

        public Pocket CheckPocket(int sx, int sy, int px, int py, int wx, int wy)
        {
            //od razu w luzie
            if (IsMeshPoint(sx, sy, px, py))
            {
                
            }

            //przypadki pionowe

            //i pozostale
            return Pocket.NIE;
        }
    }
}
