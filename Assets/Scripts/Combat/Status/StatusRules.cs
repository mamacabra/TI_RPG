using System.Collections.Generic;

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
                    member.Character.Updated();
                });
            });
        }

        private static void DispatchStatusEffect(Character character, StatusType statusType)
        {
            switch (statusType)
            {
                case StatusType.Bleed:
                    StatusEffects.ApplyStatusBleed(character, statusType);
                    break;
                case StatusType.Stun:
                    StatusEffects.ApplyStatusStun(character, statusType);
                    break;
                case StatusType.Weak:
                    StatusEffects.ApplyStatusWeak(character, statusType);
                    break;
            }
        }
    }
}
