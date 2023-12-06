namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Trainer> trainersById = new Dictionary<int, Trainer>();
        private Dictionary<int, Member> membersById = new Dictionary<int, Member>();

        public int MemberCount => membersById.Count;

        public int TrainerCount => trainersById.Count;

        public void AddMember(Member member)
        {
            if (Contains(member))
            {
                throw new ArgumentException();
            }

            membersById.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (Contains(trainer))
            {
                throw new ArgumentException();
            }

            trainersById.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!Contains(member))
            {
                membersById.Add(member.Id, member);
            }

            if (!Contains(trainer) || member.Trainer != null)
            {
                throw new ArgumentException();
            }

            member.Trainer = trainer;
            trainer.Trainees.Add(member);
        }

        public bool Contains(Member member) => membersById.ContainsKey(member.Id);

        public bool Contains(Trainer trainer) => trainersById.ContainsKey(trainer.Id);

        public Trainer FireTrainer(int id)
        {
            if (!trainersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var trainer = trainersById[id];
            trainersById.Remove(id);

            foreach (var member in trainer.Trainees)
            {
                member.Trainer = null;
            }

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!membersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var member = membersById[id];
            membersById.Remove(id);

            if (member.Trainer != null)
            {
                member.Trainer.Trainees.Remove(member);
            }

            return member;
        }

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
            => membersById.Values
            .OrderBy(m => m.RegistrationDate)
            .ThenByDescending(m => m.Name);

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
            => trainersById.Values.OrderBy(m => m.Popularity);

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
            => trainer.Trainees
            .OrderBy(m => m.RegistrationDate)
            .ThenBy(m => m.Name);

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
            => membersById.Values
            .Where(m => m.Trainer.Popularity >= lo && m.Trainer.Popularity <= hi)
            .OrderBy(m => m.Visits)
            .ThenBy(m => m.Name);

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
            => trainersById.Values
                 .OrderBy(t => t.Trainees.Count)
                 .ThenBy(t => t.Popularity)
                 .ToDictionary(t => t, t => t.Trainees);
    }
}