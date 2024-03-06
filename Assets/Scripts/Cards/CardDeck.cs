using System;
using System.Collections.Generic;
using Random = Unity.Mathematics.Random;

public class CardDeck
{
    private List<CardType> deck = new (50);
    private Random rand;

    public CardDeck()
    {
        rand = new Random((uint)DateTime.Now.Ticks);
    }
    
    public void Init()
    {
        for (int i = 0; i < 50; i++)
            deck.Add((CardType)rand.NextInt(0, Enum.GetValues(typeof(CardType)).Length));
    }

    public void Clear()
    {
        deck.Clear();
    }

    public CardType DrawOne()
    {
        int index = rand.NextInt(0, deck.Count);
        CardType drawnCard = deck[index];
        deck.RemoveAt(index);
        return drawnCard;
    }

    public void AddCard(CardType card)
    {
        deck.Add(card);
    }

    public void Shuffle()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int swapIndex = rand.NextInt(0, i + 1);
            (deck[i], deck[swapIndex]) = (deck[swapIndex], deck[i]);
        }
    }
}
