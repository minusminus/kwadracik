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
            _pobj.CheckPocket(4, 3, 3, 0, 1, 1).ShouldBe(Pocket.GS);
            _pobj.CheckPocket(4, 3, 3, 0, 1, 3).ShouldBe(Pocket.GP);
            _pobj.CheckPocket(4, 2, 3, 0, 1, 1).ShouldBe(Pocket.NIE);
        }

        [Test]
        public void SampleTests_m1_1()
        {
            _pobj.CheckPocket(4, 3, 3, 0, -1, 1).ShouldBe(Pocket.GL);
            _pobj.CheckPocket(4, 2, 3, 0, -1, 1).ShouldBe(Pocket.NIE);
        }

        [Test]
        public void SampleTests_1_m1()
        {
            _pobj.CheckPocket(4, 3, 3, 0, 1, -1).ShouldBe(Pocket.GS);
            _pobj.CheckPocket(4, 2, 3, 0, 1, -1).ShouldBe(Pocket.NIE);
        }

        [Test]
        public void SampleTests_sx4()
        {
            _pobj.CheckPocket(4, 1, 1, 0, 2, 1).ShouldBe(Pocket.NIE);
            _pobj.CheckPocket(4, 2, 1, 0, 2, 1).ShouldBe(Pocket.NIE);
        }

        [Test]
        public void OriginalTests()
        {
            //0
            _pobj.CheckPocket(10, 5, 7, 4, 1, 2).ShouldBe(Pocket.DP);
            //1
            _pobj.CheckPocket(100,80,0,2,0,0).ShouldBe(Pocket.NIE);
            _pobj.CheckPocket(100,17,20,12,1,3).ShouldBe(Pocket.DS);
            //2
            _pobj.CheckPocket(2000, 1000 ,1, 0, 2, 0).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(1000, 973, 573, 123, 53, -123).ShouldBe(Pocket.DS);
            //3
            _pobj.CheckPocket(996, 1273, 1, 0, -1, -1).ShouldBe(Pocket.GL);
            _pobj.CheckPocket(1000000, 100, 0, 1, 1, -100).ShouldBe(Pocket.NIE);
            //4
            _pobj.CheckPocket(1000, 977, 999, 975, -1, -2).ShouldBe(Pocket.GS);
            _pobj.CheckPocket(876444, 876444, 100, 213, 975, 975).ShouldBe(Pocket.NIE);
            //5
            _pobj.CheckPocket(50000, 123, 10, 34, 327, -127).ShouldBe(Pocket.DP);
            _pobj.CheckPocket(984, 5117, 0, 5116, -1, 1).ShouldBe(Pocket.DS);
            //6
            _pobj.CheckPocket(45000, 45000, 22500, 22500, 1, 2).ShouldBe(Pocket.NIE);
            _pobj.CheckPocket(44998, 44999, 22500, 22500, 1, -1).ShouldBe(Pocket.GS);
            //7
            _pobj.CheckPocket(10000, 8521, 117, 4321, 121, -987).ShouldBe(Pocket.DL);
            _pobj.CheckPocket(50986, 12309, 2342, 12, 1, 117).ShouldBe(Pocket.GS);
            //8
            _pobj.CheckPocket(300000, 123873, 1, 1, 1, 1).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(234000, 954271, 1, 0, -1, -1).ShouldBe(Pocket.GL);
            //9
            _pobj.CheckPocket(23164, 98731, 1231, 987, -1, 3).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(98742, 21097, 21, 1, -111, -231).ShouldBe(Pocket.DL);
            //10
            _pobj.CheckPocket(1000000, 954271, 1, 1, 1, 1).ShouldBe(Pocket.DS);
            _pobj.CheckPocket(982214, 852841, 12, 12, -21, 87).ShouldBe(Pocket.DL);
        }
    }
}