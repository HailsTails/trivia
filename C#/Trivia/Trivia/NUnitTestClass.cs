using NUnit.Framework;
using System;
using System.IO;
using UglyTrivia;
namespace Trivia
{
    [TestFixture()]
    public class NUnitTestClass
    {
        [Test()]
        public void GoldenTest()
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

        [Test()]
        public void TestBaseGame()
        {
            Game game = new Game();

            Assert.AreEqual(false, game.isPlayable());
            Assert.AreEqual(0, game.howManyPlayers());
        }

        [Test()]
        public void DidPlayerWin()
        {
            Player player = new Player("Sam");

            Game game = new Game();
            bool playerWon = game.hasPlayerWon(player);

            Assert.AreEqual(false, playerWon);
        }


        [Test()]
        public void PlayerRoll()
        {
            Player player = new Player("Sam");
            Player playerCopy = new Player("Sam");

            Game game = new Game();
            Player playerNewState = game.roll(player, 3);

            Assert.AreEqual(player.inPenaltyBox, playerCopy.inPenaltyBox);
            Assert.AreEqual(player.name, playerCopy.name);
            Assert.AreEqual(player.place, playerCopy.place);
            Assert.AreEqual(player.purse, playerCopy.purse);

            Assert.AreEqual(player.inPenaltyBox, playerNewState.inPenaltyBox);
            Assert.AreEqual(player.name, playerNewState.name);
            Assert.AreEqual(3, playerNewState.place);
            Assert.AreEqual(player.purse, playerNewState.purse);
        }
    }
}
