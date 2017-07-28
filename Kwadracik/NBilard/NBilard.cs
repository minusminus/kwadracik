using System;
using Bilard;
using NUnit.Framework;
using Shouldly;

namespace NBilard
{
    [TestFixture]
    public class NBilard
    {
        private Bilard.Bilard _pobj = new Bilard.Bilard();

        [Test]
        public void SpecifyPocketTests()
        {
            const int sx = 4;
            const int sy = 2;

            _pobj.SpecifyPocket(sx, sy, 0, 0).ShouldBe(Pocket.DL);
            _pobj.SpecifyPocket(sx, sy, sx, 0).ShouldBe(Pocket.DP);
            _pobj.SpecifyPocket(sx, sy, sx/2, 0).ShouldBe(Pocket.DS);
            _pobj.SpecifyPocket(sx, sy, 0, sy).ShouldBe(Pocket.GL);
            _pobj.SpecifyPocket(sx, sy, sx, sy).ShouldBe(Pocket.GP);
            _pobj.SpecifyPocket(sx, sy, sx/2, sy).ShouldBe(Pocket.GS);
        }

        [Test]
        public void TestMethod1()
        {
        }
    }
}