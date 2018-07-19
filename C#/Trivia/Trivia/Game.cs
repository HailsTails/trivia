using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Player {
        public string name;

        public int place;
        public int purse;

        public bool inPenaltyBox;

        public Player(string name) {
            this.name = name;
            place = 0;
            purse = 0;
            inPenaltyBox = false;
        }
        public Player(Player player)
        {
            this.name = player.name;
            place = player.place;
            purse = player.purse;
            inPenaltyBox = player.inPenaltyBox;
        }
    }

    public class Game
    {
        private const int minimumPlayers = 2;
        private const int BoardSize = 12;
        private const int questionCount = 50;

        public List<Player> players = new List<Player>();

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        public int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < questionCount; i++)
            {
                popQuestions.AddLast(createQuestion("Pop", i));
                scienceQuestions.AddLast(createQuestion("Science", i));
                sportsQuestions.AddLast(createQuestion("Sports", i));
                rockQuestions.AddLast(createQuestion("Rock", i));
            }
        }

        public String createQuestion(string category, int index)
        {
            return category + " Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= minimumPlayers);
        }

        public bool add(String playerName)
        {

            Player player = new Player(playerName);
            players.Add(player);
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public Player roll(Player player, int roll)
        {
            Console.WriteLine(player.name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (player.inPenaltyBox)
            {
                if (isOdd(roll))
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(player.name + " is getting out of the penalty box");

                    Player newPlayer = MovePlayer(player, roll);
                    askQuestion(newPlayer);
                    return newPlayer;
                }
                else
                {
                    Console.WriteLine(player.name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                    return player;
                }

            }
            else
            {
                Player newPlayer = MovePlayer(player, roll);
                askQuestion(newPlayer);
                return newPlayer;
            }

        }

        private static bool isOdd(int roll)
        {
            return roll % 2 != 0;
        }

        private Player MovePlayer(Player player, int spacesToMove) {
            Player newPlayer = new Player(player);
            newPlayer.place = newPlayer.place + spacesToMove;
            if (newPlayer.place >= BoardSize) 
                newPlayer.place = newPlayer.place - BoardSize;


            Console.WriteLine(newPlayer.name
                 + "'s new location is "
                              + newPlayer.place);
            Console.WriteLine("The category is " + currentCategory(newPlayer));

            return newPlayer;
        }

        private void askQuestion(Player player)
        {
            switch(currentCategory(player)) {
                case Category.Pop:
                    Console.WriteLine(popQuestions.First());
                    popQuestions.RemoveFirst();
                    break;
                case Category.Science:
                    Console.WriteLine(scienceQuestions.First());
                    scienceQuestions.RemoveFirst();
                    break;
                case Category.Sports:
                    Console.WriteLine(sportsQuestions.First());
                    sportsQuestions.RemoveFirst();
                    break;
                case Category.Rock:
                    Console.WriteLine(rockQuestions.First());
                    rockQuestions.RemoveFirst();
                    break;
            }
        }

        private enum Category
        {
            Pop = 0,
            Science,
            Sports,
            Rock
        }

        private Category currentCategory(Player player)
        {
            switch (player.place)
            {
                case 0:
                case 4:
                case 8:
                    return Category.Pop;
                case 1:
                case 5:
                case 9:
                    return Category.Science;
                case 2:
                case 6:
                case 10:
                    return Category.Sports;
                default:
                    return Category.Rock;
            }

        }

        public bool wasCorrectlyAnswered()
        {
            if (players[currentPlayer].inPenaltyBox && !isGettingOutOfPenaltyBox) {
                    NextPlayer();
                    return true;
            }

            CorrectAnswer();
            bool hasPlayerNotWon = !hasPlayerWon(players[currentPlayer]);
            NextPlayer();

            return hasPlayerNotWon;
        }

        private void CorrectAnswer() {
            if(players[currentPlayer].inPenaltyBox && isGettingOutOfPenaltyBox) {
                Console.WriteLine("Answer was correct!!!!");
            } else {
                Console.WriteLine("Answer was corrent!!!!");
            }
            players[currentPlayer].purse++;
            Console.WriteLine(players[currentPlayer].name
                    + " now has "
                              + players[currentPlayer].purse
                    + " Gold Coins.");

        } 

        private void NextPlayer() {
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer].name + " was sent to the penalty box");
            players[currentPlayer].inPenaltyBox = true;

            NextPlayer();
            return true;
        }


        public bool hasPlayerWon(Player player)
        {
            return player.purse == 6;
        }
    }

}
