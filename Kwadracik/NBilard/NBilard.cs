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
        public void NotInPocketNotMoving()
        {
            _pobj.CheckPocket(4, 2, 1, 1, 0, 0).ShouldBe(Pocket.NIE);
        }

        [Test]
        public void IniitallyInPocket()
        {
            const int sx = 4;
            const int sy = 2;

            _pobj.CheckPocket(sx, sy, 0, 0, 1, 1).ShouldBe(Pocket.DL);
            _pobj.CheckPocket(sx, sy, sx, 0, 1, 1).ShouldBe(Pocket.DP);
            _pobj.CheckPocket(sx, sy, sx/2, 0, 1, 1).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(sx, sy, 0, sy, 1, 1).ShouldBe(Pocket.GL);
            _pobj.CheckPocket(sx, sy, sx, sy, 1, 1).ShouldBe(Pocket.GP);
            _pobj.CheckPocket(sx, sy, sx/2, sy, 1, 1).ShouldBe(Pocket.GS);
        }

        [Test]
        public void HorizontalMove()
        {
            const int sx = 4;
            const int sy = 2;

            _pobj.CheckPocket(sx, sy, 1, 1, 1, 0).ShouldBe(Pocket.NIE);
            _pobj.CheckPocket(sx, sy, 1, 1, -1, 0).ShouldBe(Pocket.NIE);

            _pobj.CheckPocket(sx, sy, 1, 0, 1, 0).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(sx, sy, 3, 0, 1, 0).ShouldBe(Pocket.DP);
            _pobj.CheckPocket(sx, sy, 1, 0, -1, 0).ShouldBe(Pocket.DL);
            _pobj.CheckPocket(sx, sy, 3, 0, -1, 0).ShouldBe(Pocket.DS);

            _pobj.CheckPocket(sx, sy, 1, sy, 1, 0).ShouldBe(Pocket.GS);
            _pobj.CheckPocket(sx, sy, 3, sy, 1, 0).ShouldBe(Pocket.GP);
            _pobj.CheckPocket(sx, sy, 1, sy, -1, 0).ShouldBe(Pocket.GL);
            _pobj.CheckPocket(sx, sy, 3, sy, -1, 0).ShouldBe(Pocket.GS);
        }

        [Test]
        public void VerticalMove()
        {
            const int sx = 4;
            const int sy = 2;

            _pobj.CheckPocket(sx, sy, 1, 1, 0, 1).ShouldBe(Pocket.NIE);
            _pobj.CheckPocket(sx, sy, 1, 1, 0, -1).ShouldBe(Pocket.NIE);

            _pobj.CheckPocket(sx, sy, 0, 1, 0, 1).ShouldBe(Pocket.GL);
            _pobj.CheckPocket(sx, sy, 0, 1, 0, -1).ShouldBe(Pocket.DL);
            _pobj.CheckPocket(sx, sy, sx/2, 1, 0, 1).ShouldBe(Pocket.GS);
            _pobj.CheckPocket(sx, sy, sx/2, 1, 0, -1).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(sx, sy, sx, 1, 0, 1).ShouldBe(Pocket.GP);
            _pobj.CheckPocket(sx, sy, sx, 1, 0, -1).ShouldBe(Pocket.DP);
        }

        [Test]
        public void SampleTests_1_1()
        {
            _pobj.CheckPocket(4, 3, 3, 0, 1, 1).ShouldBe(Pocket.GP);
            _pobj.CheckPocket(4, 2, 3, 0, 1, 1).ShouldBe(Pocket.NIE);
        }

        [Test]
        public void SampleTests_m1_1()
        {
            _pobj.CheckPocket(4, 3, 3, 0, -1, 1).ShouldBe(Pocket.GL);
            _pobj.CheckPocket(4, 2, 3, 0, -1, 1).ShouldBe(Pocket.NIE);
        }
    }
}