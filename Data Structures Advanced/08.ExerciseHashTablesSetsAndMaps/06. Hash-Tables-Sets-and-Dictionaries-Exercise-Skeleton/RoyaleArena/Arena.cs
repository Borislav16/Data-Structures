namespace RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Arena : IArena
    {
        private Dictionary<int, BattleCard> battlecards;

        public Arena()
        {
            battlecards = new Dictionary<int, BattleCard>();
        }

        public int Count => battlecards.Count;

        public void Add(BattleCard card)
        {
            if(!battlecards.ContainsKey(card.Id))
            {
                battlecards.Add(card.Id, card);
            }
        }

        public void ChangeCardType(int id, CardType type)
        {
            if(!battlecards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            battlecards[id].Type = type;
        }

        public bool Contains(BattleCard card)
        {
            return battlecards.ContainsKey(card.Id);
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if(Count < n)
            {
                throw new InvalidOperationException();
            }

            return battlecards
                .OrderBy(kvp => kvp.Value.Swag)
                .ThenBy(kvp => kvp.Value.Id)
                .Take(n)
                .Select(kvp => kvp.Value);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            return battlecards
                .Where(kvp => kvp.Value.Swag >= lo && kvp.Value.Swag <= hi)
                .Select(kvp => kvp.Value)
                .OrderBy(bc => bc.Swag);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            var filteredBattleCards = battlecards
                .Where(kvp => kvp.Value.Type == type)
                .Select(kvp => kvp.Value);

            if(filteredBattleCards.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return filteredBattleCards
                .OrderByDescending(bc => bc.Damage)
                .ThenBy(bc => bc.Id);
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            var cards = battlecards
                .Where(kvp => kvp.Value.Type == type && kvp.Value.Damage <= damage)
                .Select(kvp => kvp.Value)
                .OrderByDescending(bc => bc.Damage)
                .ThenBy(bc => bc.Id);

            if(cards.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return cards;
        }

        public BattleCard GetById(int id)
        {
            if(battlecards.ContainsKey(id))
            {
                return battlecards[id];
            }

            throw new InvalidOperationException();
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            var cards = GetByNameOrderedBySwagDescending(name)
                .Where(bc => bc.Swag >= lo && bc.Swag < hi);

            if(cards.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return cards;

        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            var cards = battlecards
                .Where(kvp => kvp.Value.Name == name)
                .Select(kvp => kvp.Value)
                .OrderByDescending(bc => bc.Swag)
                .ThenBy(bc => bc.Id);

            if(cards.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return cards;
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            var cards = GetByCardTypeAndMaximumDamage(type, hi)
                .Where(bc => bc.Damage >= lo);

            if(cards.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            return cards;
        }

        public void RemoveById(int id)
        {
            if(!battlecards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            battlecards.Remove(id);
        }

        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (var card in battlecards)
            {
                yield return card.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}