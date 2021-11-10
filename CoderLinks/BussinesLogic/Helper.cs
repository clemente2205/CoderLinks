using CoderLinks.Modal;
using CoderLinks.Models;
using CoderLinks.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoderLinks.BussinesLogic
{
    public class Helper
    {
        #region Singleton

        private Helper() { }
        private static Helper instance = null;
        public static Helper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Helper();
                }
                return instance;
            }
        }

        #endregion Singleton

        #region Variables

        /// <summary>
        /// temporal list of cards for war saved until someone wins
        /// </summary>
        List<Cards> StackWar = new List<Cards>();
        List<Cards> StackPlayerOne = new List<Cards>();
        List<Cards> StackPlayerTwo = new List<Cards>();

        #endregion Variables

        /// <summary>
        /// principal method of war game
        /// </summary>
        /// <returns></returns>
        public StringBuilder StartGame()
        {
            StringBuilder MovementHistory = new StringBuilder();
            List<Cards> Deck = new List<Cards>();
            List<Cards> PlayerOne = new List<Cards>();
            List<Cards> PlayerTwo = new List<Cards>();
            int DropCard = 0;

            try
            {
                //create a normal deck of 52 cards and then shuffle
                Deck = DeckShuffle(DeckGenerator(1));

                //asign to player one the first 26 cards
                PlayerOne = Deck.Take(26).ToList();
                //asign to player two the rest 
                PlayerTwo = Deck.Skip(26).ToList();

                DrawTopCard(ref PlayerOne, ref PlayerTwo, ref MovementHistory, ref DropCard);

                return MovementHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WarLog> GetLog()
        {
            try
            {
                return Querys.Instance.LogHistory();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DrawTopCard(ref List<Cards> one, ref List<Cards> two, ref StringBuilder MovementHistory, ref int dropCard, bool war = false)
        {
            try
            {
                dropCard++;

                if (one.Count == 0 || two.Count == 0)
                {
                    MovementHistory.AppendLine("Game finish at round " + dropCard.ToString());
                    if (one.Count == 0)
                    {
                        MovementHistory.AppendLine("Player two wins");
                        SaveWinner("player two", dropCard);
                    }
                    else
                    {
                        MovementHistory.AppendLine("Player one wins");
                        SaveWinner("player one", dropCard);
                    }
                    return;
                }

                if (one[0].Value > two[0].Value)
                {
                    if (war)
                    {
                        MovementHistory.AppendLine("Payer one Wins war with " + one[0].Value.ToString() + ", and player two Lost war with " + two[0].Value.ToString() + ", in drop card round " + dropCard.ToString());
                        StackPlayerOne.Add(two[0]);
                        two.RemoveAt(0);
                        StackPlayerOne.Add(one[0]);
                        one.RemoveAt(0);
                        StackPlayerOne.AddRange(StackWar);
                        StackWar = new List<Cards>();
                        war = false;
                    }
                    else
                    {
                        MovementHistory.AppendLine("Payer one has " + one[0].Value.ToString() + ", player two has " + two[0].Value.ToString() + ", player one wins in drop card round " + dropCard.ToString());
                        StackPlayerOne.Add(one[0]);
                        one.RemoveAt(0);
                        one.Add(two[0]);
                        two.RemoveAt(0);
                    }
                }
                else if (one[0].Value < two[0].Value)
                {
                    if (war)
                    {
                        MovementHistory.AppendLine("Payer one Lost war with " + one[0].Value.ToString() + ", and player two Wins war with " + two[0].Value.ToString() + ", in drop card round " + dropCard.ToString());

                        StackPlayerTwo.Add(two[0]);
                        two.RemoveAt(0);
                        two.Add(one[0]);
                        one.RemoveAt(0);
                        StackPlayerTwo.AddRange(StackWar);
                        StackWar = new List<Cards>();
                        war = false;
                    }
                    else
                    {
                        MovementHistory.AppendLine("Payer one has " + one[0].Value.ToString() + ", player two has " + two[0].Value.ToString() + ", player two wins in drop card round " + dropCard.ToString());
                        StackPlayerTwo.Add(two[0]);
                        two.RemoveAt(0);
                        StackPlayerTwo.Add(one[0]);
                        one.RemoveAt(0);
                    }
                }
                else
                {
                    war = true;
                    MovementHistory.AppendLine("Players get same card this is war in round " + dropCard.ToString());

                    if (one.Count <= 2)
                    {
                        MovementHistory.AppendLine("Player one doesnt has enough cards for war, player two wins " + dropCard.ToString());
                        SaveWinner("player two", dropCard);
                        return;
                    }

                    if (two.Count <= 2)
                    {
                        MovementHistory.AppendLine("Player two doesnt has enough cards for war, player one wins " + dropCard.ToString());
                        SaveWinner("player one", dropCard);
                        return;
                    }

                    //we remove from both players the equal card and the next down card so the nex drop would be the third card 
                    StackWar.AddRange(one.Take(2));
                    StackWar.AddRange(two.Take(2));
                    one.RemoveRange(0, 2);
                    two.RemoveRange(0, 2);
                }

                DrawTopCard(ref one, ref two, ref MovementHistory, ref dropCard, war);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveWinner(string player, int round)
        {
            try
            {
                Querys.Instance.SaveWinner(player, round);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method for generate N decks for example 1 is for a normal deck with 52 cards, 2 is for two decks = 64 cards TODO: extra MODE
        /// </summary>
        /// <param name="Decks"></param>
        /// <returns></returns>
        public List<Cards> DeckGenerator(int Decks)
        {
            try
            {
                List<Cards> card = new List<Cards>();

                for (int i = 0; i < Decks * 4; i++)
                {
                    card.AddRange(CardGenerator());
                }

                return card;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method for return simple constructor of one serie
        /// </summary>
        /// <returns></returns>
        public List<Cards> CardGenerator()
        {
            try
            {
                List<Cards> Cartas = new List<Cards>();


                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.As,
                    Value = 1
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Two,
                    Value = 2
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Three,
                    Value = 3
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Four,
                    Value = 4
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Five,
                    Value = 5
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Six,
                    Value = 6
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Seven,
                    Value = 7
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Eight,
                    Value = 8
                });


                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Nine,
                    Value = 9
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Ten,
                    Value = 10
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Jack,
                    Value = 11
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.Queen,
                    Value = 12
                });

                Cartas.Add(new Cards()
                {
                    Symbol = Constants.Symbols.King,
                    Value = 13
                });

                return Cartas;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Method for shuffle decks
        /// </summary>
        /// <param name="Decks"></param>
        /// <returns></returns>
        public List<Cards> DeckShuffle(List<Cards> Decks)
        {
            try
            {
                List<Cards> ShuffleDeck = new List<Cards>();
                int auxiliar;
                int numeroCartas;

                numeroCartas = Decks.Count;

                for (int i = 0; i < numeroCartas; i++)
                {
                    auxiliar = Randomizer(0, Decks.Count);
                    ShuffleDeck.Add(Decks[auxiliar]);
                    Decks.RemoveAt(auxiliar);
                }

                return ShuffleDeck;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Randomizer the order
        /// </summary>
        /// <param name="Min"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        public int Randomizer(int Min = 1, int Max = 13)
        {
            Random r = new Random();
            int index = r.Next(Min, Max);
            return index;
        }

    }
}
