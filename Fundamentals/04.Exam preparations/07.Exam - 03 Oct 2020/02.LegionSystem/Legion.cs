namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private SortedDictionary<int, IEnemy> enemiesBySpeed = new SortedDictionary<int, IEnemy>();

        public int Size => enemiesBySpeed.Count;

        public bool Contains(IEnemy enemy) => enemiesBySpeed.ContainsKey(enemy.AttackSpeed);

        public void Create(IEnemy enemy)
        {
            if (!Contains(enemy))
            {
                enemiesBySpeed.Add(enemy.AttackSpeed, enemy);
            }
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            if (!enemiesBySpeed.ContainsKey(speed))
            {
                return null;
            }

            return enemiesBySpeed[speed];
        }

        public IEnemy GetFastest()
        {
            ValidateCollectionNotEmpty();

            return enemiesBySpeed.Last().Value;
        }

        public IEnemy GetSlowest()
        {
            ValidateCollectionNotEmpty();

            return enemiesBySpeed.First().Value;
        }

        public void ShootFastest()
            => enemiesBySpeed.Remove(GetFastest().AttackSpeed);

        public void ShootSlowest()
             => enemiesBySpeed.Remove(GetSlowest().AttackSpeed);

        public IEnemy[] GetOrderedByHealth()
            => enemiesBySpeed.Values
                .OrderByDescending(x => x.Health)
                .ToArray();

        public List<IEnemy> GetFaster(int speed)
            => enemiesBySpeed.Where(kv => kv.Key > speed)
                            .Select(kv => kv.Value)
                            .ToList();

        public List<IEnemy> GetSlower(int speed)
           => enemiesBySpeed.Where(kv => kv.Key < speed)
                            .Select(kv => kv.Value)
                            .ToList();

        private void ValidateCollectionNotEmpty()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }

    }
}
