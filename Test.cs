using NUnit.Framework;
using System;
using IIG.BinaryFlag;

namespace lab2
{
    [TestFixture()]
    public class Test
    {

        [Test]
        public void Constructor_WithMaxUlong_ReturnsFalse()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(18446744073709551615, false);

            Assert.False(binaryFlag.GetFlag());
        }

        [Test]
        public void Constructor_WithMaxUlong_ReturnsFalse2()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(9223372036854775807, false);

            Assert.False(binaryFlag.GetFlag());
        }

        [Test]
        public void Constructor_WithZero_ThrowsException()
        {

            try
            {
                MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(0, true);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains("Specified argument was out of the range of valid values.", e.Message);
                return;
            }

            Assert.Fail("The ArgumentOutOfRangeException was not thrown.");
        }

        [Test]
        public void Constructor_With_1()
        {
            Assert.DoesNotThrow(() => new MultipleBinaryFlag(1, false));
        }

        [Test]
        public void Constructor_WithValidUlong()
        {
            Assert.DoesNotThrow(() => new MultipleBinaryFlag(115));
        }

        [Test]
        public void Get_ReturnsTrue()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, true);
            Assert.True(multipleBinaryFlag.GetFlag());
        }

        [Test]
        public void Get_ReturnsFalse()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(10, false);
            Assert.False(multipleBinaryFlag.GetFlag());
        }

        [Test]
        public void SetFlag_GreaterThanLastIndex_ThrowsException()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(5, false);

            try
            {
                binaryFlag.SetFlag(5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains("Specified argument was out of the range of valid values.", e.Message);
                return;
            }

            Assert.Fail("The ArgumentOutOfRangeException was not thrown.");
        }

        [Test]
        public void Set_ValidPosition_GetReturnsFalse()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(5, false);

            multipleBinaryFlag.SetFlag(4);

            Assert.False(multipleBinaryFlag.GetFlag());

        }

        [Test]
        public void Set_AllPositions_ReturnsTrue()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(25, false);

            for (ulong x = 0; x < 25; x++)
            {
                multipleBinaryFlag.SetFlag(x);
            }

            Assert.True(multipleBinaryFlag.GetFlag());

        }

        [Test]
        public void Reset_ValidPosition_GetReturnsFalse()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(5);

            multipleBinaryFlag.ResetFlag(3);

            Assert.False(multipleBinaryFlag.GetFlag());

        }

        [Test]
        public void ResetFlag_GreaterThanLastIndex_ThrowsException()
        {
            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(5, true);

            try
            {
                binaryFlag.ResetFlag(5);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains("Specified argument was out of the range of valid values.", e.Message);
                return;
            }

            Assert.Fail("The ArgumentOutOfRangeException was not thrown.");
        }

        [Test]
        public void Reset_AllPositions_ReturnsFalse()
        {
            MultipleBinaryFlag multipleBinaryFlag = new MultipleBinaryFlag(25, true);

            for (ulong x = 0; x < 25; x++)
            {
                multipleBinaryFlag.ResetFlag(x);
            }

            Assert.False(multipleBinaryFlag.GetFlag());

        }

        [Test]
        public void Dispose_SetsFlag_Null()
        {

            MultipleBinaryFlag binaryFlag = new MultipleBinaryFlag(5, true);

            binaryFlag.Dispose();

            Assert.Null(binaryFlag);
        }

    }
}