using System.IO;
using NUnit.Framework;
using IIG.BinaryFlag;
using IIG.FileWorker;
namespace lab4.tests
{
    [TestFixture]
    public class BinaryFlagTestWithFileWorker
    {
        private static string writeFile = "/home/heirloom/Projects/lab4/lab4/txt_src/write.txt";
        private static string readLinesFile = "/home/heirloom/Projects/lab4/lab4/txt_src/read_lines.txt";
        private static string readAllFile = "/home/heirloom/Projects/lab4/lab4/txt_src/read_all.txt";
        private static string tryWriteFile = "/home/heirloom/Projects/lab4/lab4/txt_src/try_write.txt";
        private static string tryWriteFileFails = "/home/heirloom/Projects/lab4/lab4/txt_src/try_write_fails.txt";

        [TearDown]
        public void TearDown()
        {
            File.WriteAllText(writeFile, string.Empty);
            File.WriteAllText(tryWriteFile, string.Empty);

        }

        [Test]
        public void TestWriteGetFlag()
        {
            string expectedOutput = "True";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(44);

            Assert.IsTrue(BaseFileWorker.Write(flag.GetFlag().ToString(), writeFile));
            Assert.AreEqual(expectedOutput, File.ReadAllText(writeFile));
        }

        [Test]
        public void TestWriteGetFlagFails()
        { 
            MultipleBinaryFlag flag = new MultipleBinaryFlag(44);

            Assert.IsFalse(BaseFileWorker.Write(flag.GetFlag().ToString(), tryWriteFileFails));
            Assert.IsEmpty(File.ReadAllText(writeFile));
        }

        [Test]
        public void TestTryWriteGetFlag()
        {

            string expectedOutput = "False";

            MultipleBinaryFlag flag = new MultipleBinaryFlag(4, false);
            Assert.IsTrue(BaseFileWorker.TryWrite(flag.GetFlag().ToString(), tryWriteFile, 3));
            Assert.AreEqual(expectedOutput, File.ReadAllText(tryWriteFile));
        }

        [Test]
        public void TestTryWriteGetFlagFails()
        {

            MultipleBinaryFlag flag = new MultipleBinaryFlag(2);
            Assert.IsFalse(BaseFileWorker.TryWrite(flag.GetFlag().ToString(), tryWriteFileFails, 3));
            Assert.IsEmpty(File.ReadAllText(tryWriteFileFails));
        }



        [Test]
        public void TestReadLinesFlag()
        {
            //set up flags so the output is equal to the one in file
            MultipleBinaryFlag[] flags = { new MultipleBinaryFlag(3, true),
                new MultipleBinaryFlag(4, true),
                new MultipleBinaryFlag(55555555) };

            flags[0].ResetFlag(1);

            // Thus, the output is false, true, true

            string[] results = BaseFileWorker.ReadLines(readLinesFile);

            Assert.AreEqual(flags.Length, results.Length);

            for (int i = 0; i < flags.Length; i++)
            {
                Assert.AreEqual(flags[i].GetFlag().ToString(), results[i]);
            }
        }


        [Test]
        public void TestReadLinesFlagNotEqual()
        {
            //set up flags so the output is not equal to the one in file
            MultipleBinaryFlag[] flags = { new MultipleBinaryFlag(2, false),
               new MultipleBinaryFlag(44, false),
               new MultipleBinaryFlag(2)  };

            flags[0].SetFlag(0);
            flags[0].SetFlag(1);

            flags[2].ResetFlag(1);

            // Thus, the output is true, false, false


            string[] results = BaseFileWorker.ReadLines(readLinesFile);

            Assert.AreEqual(flags.Length, results.Length);
            for (int i = 0; i < flags.Length; i++)
            {
                Assert.AreNotEqual(flags[i].GetFlag().ToString(), results[i]);
            }
        }

       

        [Test]
        public void TestReadAllEqualGetFlag()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2);
            Assert.AreEqual(flag.GetFlag().ToString(), BaseFileWorker.ReadAll(readAllFile));

        }
        [Test]
        public void TestReadlAllNotEqualGetFlag()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2);
            flag.ResetFlag(1);
            Assert.AreNotEqual(flag.GetFlag().ToString(), BaseFileWorker.ReadAll(readAllFile));
        }



    }
}
