using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        public static void Main(String[] args)
        {
            runGame(5);
        }

        public static void runGame(int seed)
        {
            bool notAWinner;

            Game aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            Random rand = new Random(seed);

            do
            {

                aGame.players[aGame.currentPlayer] = aGame.roll(aGame.players[aGame.currentPlayer], rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }

            } while (notAWinner);
        }


    }

   
}

