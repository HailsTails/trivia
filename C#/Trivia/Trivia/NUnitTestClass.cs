using NUnit.Framework;
using System;
using System.IO;
namespace Trivia
{
    [TestFixture()]
    public class NUnitTestClass
    {
        [Test()]
        public void TestCase()
        {
            TextWriter oldOut = Console.Out;
            StringWriter output = new StringWriter();

            Console.WriteLine(Directory.GetCurrentDirectory());

            string text = System.IO.File.ReadAllText("/Users/helen.giapitzakis/Development/trivia/C#/Trivia/Trivia/Output.txt");

            Console.WriteLine("gonna game");

            Console.SetOut(output);

            for (int seed = 0; seed < 10; seed++)
            {
                GameRunner.runGame(seed);
            }

            Console.SetOut(oldOut);
            output.Close();

            Assert.AreEqual(text, output.ToString());
           
            Console.WriteLine("Done");
        }
    }
}
