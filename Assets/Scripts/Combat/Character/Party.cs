using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat
{
    public class Party
    {
        public List<Member> Members { get; }
        public bool IsDefeated => Members.TrueForAll(member => member.Character.IsDead);

        public Party(List<Member> members)
        {
            Members = new List<Member>();
            foreach (Member member in members) Members.Add(member);
        }

        public void ShuffleDeck()
        {
            foreach (Member member in Members)
                member.ShuffleDeck();
        }

        public void ResetActionPoints()
        {
            foreach (Member member in Members)
                member.Character.ResetActionPoints();
        }

        [CanBeNull]
        public Member GetRandomMember()
        {
            List<Member> livingMembers = Members.Where(member => member.Character.IsDead == false).ToList();

            if (livingMembers.Count == 0) return null;

            int r = Random.Range(0, livingMembers.Count);
            return livingMembers[r];
        }
    }
}
