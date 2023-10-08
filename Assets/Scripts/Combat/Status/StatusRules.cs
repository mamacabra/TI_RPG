using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public static class StatusRules
    {
        public static void MapStatus(List<Member> members)
        {
            members.ForEach(member =>
            {
                if (member.Character.IsDead) return;
                member.Character.Status.ForEach(status =>
                {
                    DispatchStatusEffect(member.Character, status.type);
                    member.Character.CountDownStatus();
                    member.Character.Updated();
                });
            });
        }

        private static void DispatchStatusEffect(Character character, StatusType statusType)
        {
            switch (statusType)
            {
                case StatusType.Bleed:
                    StatusEffects.ApplyStatusBleed(character);
                    break;
            }
        }
    }
}
