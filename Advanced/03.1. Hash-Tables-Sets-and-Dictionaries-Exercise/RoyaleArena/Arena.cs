namespace RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Arena : IArena
    {
        private Dictionary<int, BattleCard> cards;

        public Arena()
        {
            cards = new Dictionary<int, BattleCard>();
        }

        public int Count => cards.Count;

        public void Add(BattleCard card)
        {
            if (Contains(card))
            {
                throw new ArgumentException("Card alrady exists!");
            }

            cards.Add(card.Id, card);
        }

        public void ChangeCardType(int id, CardType type)
        {
            var card = GetById(id);
            card.Type = type;
        }

        public bool Contains(BattleCard card) => cards.ContainsKey(card.Id);

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (n > Count)
            {
                throw new InvalidOperationException();
            }

            return cards.Values
               .OrderBy(c => c.Swag)
               .ThenBy(c => c.Id)
               .Take(n);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
            => cards.Values
            .Where(c => c.Swag >= lo && c.Swag <= hi)
            .OrderBy(c => c.Swag);

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            var cardsOtType = cards.Values
            .Where(c => c.Type == type)
            .OrderByDescending(c => c.Damage)
            .ThenBy(c => c.Id);

            if (cardsOtType.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return cardsOtType;
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            var result = GetByCardType(type).Where(c => c.Damage <= damage);

            if (result.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public BattleCard GetById(int id)
        {
            if (!cards.ContainsKey(id))
            {
                throw new InvalidOperationException($"Card with {id} id doesn't exist");
            }

            return cards[id];
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            var result = GetByNameOrderedBySwagDescending(name).Where(c => c.Swag >= lo && c.Swag <= hi);

            if (result.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            var result = cards.Values
                .Where(c => c.Name == name)
                .OrderByDescending(c => c.Swag);

            if (result.Count() == 0)
            {
                throw new InvalidOperationException($"There are no cards with name {name}");
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
            => GetByCardType(type).Where(c => c.Damage >= lo && c.Damage <= hi);

        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (var kvp in cards)
            {
                yield return kvp.Value;
            }
        }

        public void RemoveById(int id)
        {
            if (!cards.ContainsKey(id))
            {
                throw new InvalidOperationException($"Card with {id} id doesn't exist");
            }

            cards.Remove(id);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}