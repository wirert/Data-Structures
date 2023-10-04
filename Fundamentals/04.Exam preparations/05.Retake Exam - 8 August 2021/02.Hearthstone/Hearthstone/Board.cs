using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    private Dictionary<string, Card> deck = new Dictionary<string, Card>();

    public bool Contains(string name) => deck.ContainsKey(name);

    public int Count() => deck.Count;

    public void Draw(Card card) => deck.Add(card.Name, card);

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!deck.ContainsKey(attackerCardName)
            || !deck.ContainsKey(attackedCardName))
        {
            throw new ArgumentException();
        }

        var attackerCard = deck[attackerCardName];
        var defenderCard = deck[attackedCardName];

        if (attackerCard.Level != defenderCard.Level)
        {
            throw new ArgumentException();
        }

        if (defenderCard.Health <= 0 || attackerCard.Health <= 0) return;

        defenderCard.Health -= attackerCard.Damage;

        if (defenderCard.Health <= 0)
        {
            attackerCard.Score += defenderCard.Level;
        }
    }

    public void Remove(string name)
    {
        if (deck.Remove(name) == false)
        {
            throw new ArgumentException();
        }
    }

    public void RemoveDeath()
    {
        var deathCards = deck.Values.Where(c => c.Health <= 0).ToArray();

        foreach (var card in deathCards)
        {
            deck.Remove(card.Name);
        }

        //deck = deck.Where(d => d.Value.Health > 0)
        //        .ToDictionary(x => x.Key, x => x.Value);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
        => deck.Values.Where(c => c.Score >= start && c.Score <= end)
                    .OrderByDescending(c => c.Level);

    public void Heal(int health)
        => deck.Values.OrderBy(c => c.Health).First().Health += health;
    
    public IEnumerable<Card> ListCardsByPrefix(string prefix)
        => deck.Values.Where(c => c.Name.StartsWith(prefix))
                    .OrderBy(c => string.Join("", c.Name.Reverse()))
                    .ThenBy(c => c.Level);

    public IEnumerable<Card> SearchByLevel(int level)
        => deck.Values.Where(c => c.Level == level)
                    .OrderByDescending(c => c.Score);       
}