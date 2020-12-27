using System;
using NUnit.Framework;
using IIG.PasswordHashingUtils;
namespace lab4.tests
{
    [TestFixture()]
    public class PasswordHashingUtilsTest
    {
        [Test]
        public void GetHashWithNullPassed()
        {

            try
            {
                PasswordHasher.GetHash(null, null, null);
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains("Value cannot be null", e.Message);
                return;
            }

            Assert.Fail("The ArgumentNullException was not thrown.");

        }

        [Test]
        public void GetHashWithEmptyString()
        {
            Assert.IsNotEmpty(PasswordHasher.GetHash(""));

        }

        [Test]
        public void GetHashWithLatinCharactersString()
        {
            Assert.IsNotEmpty(PasswordHasher.GetHash("testpassword"));

        }

        [Test]
        public void GetHashWithCyrillicCharactersString()
        {
            Assert.IsNotEmpty(PasswordHasher.GetHash("тестовыйпароль"));

        }

        [Test]
        public void GetHashWithSpecialCharactersString()
        {
            Assert.IsNotEmpty(PasswordHasher.GetHash("!_.@#&%"));
            Assert.IsNotEmpty(PasswordHasher.GetHash("12345"));
            Assert.IsNotEmpty(PasswordHasher.GetHash("    "));

        }

        [Test]
        public void GetHashReturnsSameResultWithSameParameters()
        {
            string password = "password";
            string hashed = PasswordHasher.GetHash(password);

            Assert.AreEqual(hashed, PasswordHasher.GetHash(password));

        }

        [Test]
        public void GetHashReturnsDifferentHashForStringsInDifferentCases()
        {
            string lowerCasePassword = "test";
            string upperCasePassword = "TEST";

            Assert.AreNotEqual(PasswordHasher.GetHash(lowerCasePassword), PasswordHasher.GetHash(upperCasePassword));

        }

        [Test]
        public void GetHashReturnsDifferentResultWithDifferentParameters()
        {
            string firstPassword = "testpassword";
            string secondPassword = "anotherpassword";

            Assert.AreNotEqual(PasswordHasher.GetHash(firstPassword), PasswordHasher.GetHash(secondPassword));

        }

        [Test]
        public void InitWithWrongSalt()
        {
            string salt1 = null;
            string salt2 = "";
            string password = "testpassword";
            uint defaultModAdler32 = 65521;


            string hashedPassword = PasswordHasher.GetHash(password);
            PasswordHasher.Init(salt1, defaultModAdler32);

            Assert.AreEqual(hashedPassword, PasswordHasher.GetHash(password));

            PasswordHasher.Init(salt2, defaultModAdler32);

            Assert.AreEqual(hashedPassword, PasswordHasher.GetHash(password));


        }

        [Test]
        public void InitWithWrongModAdler32()
        {
            uint adler = 0;
            string password = "testpassword";

            string hashedPassword = PasswordHasher.GetHash(password);
            PasswordHasher.Init(null, adler);

            Assert.AreEqual(hashedPassword, PasswordHasher.GetHash(password));

        }

        [Test]
        public void InitWithCorrectModAdler32DiffValue()
        {
            uint adler = 32;
            string password = "testpassword";

            string hashedPassword = PasswordHasher.GetHash(password);
            PasswordHasher.Init(null, adler);

            Assert.AreNotEqual(hashedPassword, PasswordHasher.GetHash(password));

        }

        [Test]
        public void GetHashWithCorrectSalt()
        {
            uint defaultAdler = 65521;
            string salt = "any salt";
            string password = "testpassword";

            Assert.NotNull(PasswordHasher.GetHash(password, salt, defaultAdler));

        }

        [Test]
        public void GetHashWithCorrectModAdler32()
        {
            uint adler = 32;
            string password = "testpassword";

            Assert.NotNull(PasswordHasher.GetHash(password, null, adler));

        }

        [Test]
        public void InitWithCorrectSaltDiffValue()
        {
            uint defaultAdler = 65521;
            string salt = "any salt";
            string password = "testpassword";

            string hashedPassword = PasswordHasher.GetHash(password);
            PasswordHasher.Init(salt, defaultAdler);

            Assert.AreNotEqual(hashedPassword, PasswordHasher.GetHash(password));

        }

        [Test]
        public void GetHashReturnsSameResultSpecifyingSalt()
        {
            uint defaultAdler = 65521;
            string salt = "any salt";
            string password = "testpassword";

            string hashedPassword = PasswordHasher.GetHash(password, salt, defaultAdler);

            Assert.AreEqual(hashedPassword, PasswordHasher.GetHash(password, salt, defaultAdler));

        }

        [Test]
        public void GetHashReturnsSameResultSpecifyingAdler()
        {
            uint adler = 44;
            string password = "testpassword";

            string hashedPassword = PasswordHasher.GetHash(password, null, adler);

            Assert.AreEqual(hashedPassword, PasswordHasher.GetHash(password, null, adler));

        }

        [Test]
        public void GetHashSameLengthForPasswordsWithDiffLengths()
        {
            string password = "testpassword";
            string passwordLonger = "muchlongerpassword";

            string hashedPassword = PasswordHasher.GetHash(password);
            string hashedPasswordLong = PasswordHasher.GetHash(passwordLonger);

            Assert.AreEqual(hashedPassword.Length, hashedPasswordLong.Length);

        }

        [Test]
        public void GetHashSameLengthForPasswordsWithSameLengths()
        {
            string password = "testpassword";
            string passwordLonger = "samepassword";

            string hashedPassword = PasswordHasher.GetHash(password);
            string hashedPassword2 = PasswordHasher.GetHash(passwordLonger);

            Assert.AreEqual(hashedPassword.Length, hashedPassword2.Length);

        }

    }
}
