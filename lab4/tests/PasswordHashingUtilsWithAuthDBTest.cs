using System;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;
using NUnit.Framework;
namespace lab4.tests
{
    [TestFixture]
    public class PasswordHashingUtilsWithAuthDBTest
    {

        private const string Server = @"localhost";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"passwordSA123456";
        private const int ConnectionTime = 75;


        AuthDatabaseUtils authDatabaseUtils = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);

        [Test]
        public void AddLatinCredentialsReturnsTrue()
        {
            string login = "testaddLogin";
            string password = "password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));

        }

        [Test]
        public void AddCyrillicCredentialsReturnsTrue()
        {
            string login = "testaddлогин";
            string password = "пароль";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));

        }

        [Test]
        public void AddCredentialsWithSpeciaCharsReturnsTrue()
        {
            string login = "testadd_log-in";
            string password = "п.ар_о-ль";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));

        }

        [Test]
        public void AddCredentialsSameLoginReturnsFalse()
        {

            string login = "testadd_same";
            string password = "testaddsamepass";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));

        }

        [Test]
        public void AddCredentialsWithNullOrEmptyLoginReturnsFalse()
        {
            Assert.IsFalse(authDatabaseUtils.AddCredentials(null, PasswordHasher.GetHash("nullpassword")));
            Assert.IsFalse(authDatabaseUtils.AddCredentials("", PasswordHasher.GetHash("emptypassword")));

        }

        [Test]
        public void AddCredentialsWithEmptyPasswordReturnsTrue()
        {
            string login = "testadd_emptypass";
            string password = "";
            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
        }

        [Test]
        public void AddCredentialsWithNullPasswordThrowsError()
        {
            try
            {
                authDatabaseUtils.AddCredentials("testadd_nullpass", PasswordHasher.GetHash(null));
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains("Value cannot be null", e.Message);
                return;
            }

            Assert.Fail("The ArgumentNullException was not thrown.");

        }

        [Test]
        public void UpdateCredentialsWithPasswordChangeReturnTrue()
        {
            string login = "testupdate_new_pass";
            string password = "password";
            string passwordNew = "password_updated";


            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password),
               login, PasswordHasher.GetHash(passwordNew)));

        }


        [Test]
        public void UpdateCredentialsWithLoginChangeReturnsTrue()
        {
            string login = "testupdate_new_login";
            string password = "password";
            string loginNew = "login_updated";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password),
            loginNew, PasswordHasher.GetHash(password)));

        }

        [Test]
        public void UpdateCredentialsWithSameValuesReturnsTrue()
        {
            string login = "testupdate_same";
            string password = "password";
            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password),
            login, PasswordHasher.GetHash(password)));

        }


        [Test]
        public void UpdateCredentialsWithEmptyOrNullLogin()
        {
            string login = "testupdate_empty_login";
            string password = "password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password),
            "", PasswordHasher.GetHash(password)));

            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(password),
            null, PasswordHasher.GetHash(password)));

        }


        [Test]
        public void UpdateCredentialsReturnsFalseWithWrongLoginOrPassword()
        {

            string login = "testupdate_with_wrong_creds";
            string password = "password";
            string wrongLogin = "testupdate_smth";
            string wrongPassword = "wrong_password";
            string newLogin = "testupdate_new_login_not_shown";


            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(login, PasswordHasher.GetHash(wrongPassword),
            newLogin, PasswordHasher.GetHash(password)));

            Assert.IsFalse(authDatabaseUtils.UpdateCredentials(wrongLogin, PasswordHasher.GetHash(password),
            newLogin, PasswordHasher.GetHash(password)));
        }




        [Test]
        public void CheckValidCredentialsReturnsTrue()
        {
            string login = "testcheck_true";
            string password = "password";
            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));

        }



        [Test]
        public void CheckInValidCredentialsTest()
        {
            string login = "testcheck_with_wrong_creds";
            string password = "password";
            string wrongPassword = "wrong_password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(wrongPassword)));

        }




        public void CheckCredentialsNonExistentLoginTest()
        {

            string login = "testcheck_non_existent";
            string password = "password";
            Assert.IsFalse(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));

        }


        [Test]
        public void DeleteExistingCredentialsTestReturnsTrue()
        {
            string login = "testdelete_true";
            string password = "password";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsTrue(authDatabaseUtils.DeleteCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));

        }


        [Test]
        public void DeleteInvalidCredentialsTest()
        {
            string login = "testdelete_with_wrong_creds";
            string password = "password";
            string wrongPassword = "wrongpassword";

            Assert.IsTrue(authDatabaseUtils.AddCredentials(login, PasswordHasher.GetHash(password)));
            Assert.IsFalse(authDatabaseUtils.DeleteCredentials(login, PasswordHasher.GetHash(wrongPassword)));
            Assert.IsTrue(authDatabaseUtils.CheckCredentials(login, PasswordHasher.GetHash(password)));
        }

        [Test]
        public void DeleteNonExistentCredentialsTest()
        {
            string loginNonExistent = "testdelete_nonexistent";
            string password = "password";


            Assert.IsFalse(authDatabaseUtils.DeleteCredentials(loginNonExistent, PasswordHasher.GetHash(password)));

        }


    }
}
