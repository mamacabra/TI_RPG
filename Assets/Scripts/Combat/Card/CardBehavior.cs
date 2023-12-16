namespace Combat
{
    public static class CardBehavior
    {
        public static void Use(Member striker, Card card, Member target)
        {
            ConsumeActionPoints(striker, card);
            ReceiveActionPoints(striker, card, target);

            ApplyHeal(target, card);
            ApplyDamage(striker, card, target);
            ApplyStatus(target, card);

            EffectDrawCard(striker, card);

            TargetEffectDropCard(card, target);
            TargetEffectAddCard(card, target);

            Target.ClearClickedTarget();
        }

        private static void ConsumeActionPoints(Member striker, Card card)
        {
            striker.Character.ConsumeActionPoints(card.ActionPointsCost);
        }

        private static void ReceiveActionPoints(Member striker, Card card, Member target)
        {
            if (card.ActionPointsReceive > 0)
                target.Character.ReceiveActionPoints(card.ActionPointsReceive);
        }

        private static void ApplyDamage(Member striker, Card card, Member target)
        {
            if (card.Damage <= 0) return;
            CombatCharacterPassive.Attack(striker.Character, Damage);
            return;

            void Damage()
            {
                target.Character.ReceiveDamage(card.Damage);
                AttackVFX.Instance.PlayDamageVFX(target.Character.transform);
            }
        }

        private static void ApplyHeal(Member target, Card card)
        {
            if (card.Heal <= 0) return;
            CombatCharacterPassive.Attack(target.Character, Heal);
            return;

            void Heal()
            {
                target.Character.ReceiveHealing(card.Heal);
                AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            }
        }

        private static void ApplyStatus(Member target, Card card)
        {
            // if (card.Status.Bewitch)
            // {
            //     target.Character.ReceiveStatus(StatusType.Bewitch);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            if (card.Status.Bleed)
            {
                target.Character.ReceiveStatus(StatusType.Sangramento);
                AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            }
            // if (card.Status.Burn)
            // {
            //     target.Character.ReceiveStatus(StatusType.Burn);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            if (card.Status.Confuse)
            {
                target.Character.ReceiveStatus(StatusType.Confuso);
                AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            }
            // if (card.Status.Curse)
            // {
            //     target.Character.ReceiveStatus(StatusType.Curse);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            // if (card.Status.Freeze)
            // {
            //     target.Character.ReceiveStatus(StatusType.Freeze);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            // if (card.Status.Pierce)
            // {
            //     target.Character.ReceiveStatus(StatusType.Pierce);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            // if (card.Status.Poison)
            // {
            //     target.Character.ReceiveStatus(StatusType.Poison);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            if (card.Status.Reflect)
            {
                target.Character.ReceiveStatus(StatusType.Refletir);
                AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            }
            if (card.Status.Stun)
            {
                target.Character.ReceiveStatus(StatusType.Atordoar);
                target.Character.ClearActionPoints();
                AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            }
            // if (card.Status.Unlucky)
            // {
            //     target.Character.ReceiveStatus(StatusType.Unlucky);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            // if (card.Status.Vulnerable)
            // {
            //     target.Character.ReceiveStatus(StatusType.Vulnerable);
            //     AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            // }
            if (card.Status.Weak)
            {
                target.Character.ReceiveStatus(StatusType.Fraqueza);
                AttackVFX.Instance.PlayHealingVFX(target.Character.transform);
            }
        }

        private static void EffectDrawCard(Member striker, Card card)
        {
            AttackVFX.Instance.PlayHealingVFX(striker.Character.transform);
            for (int i = 0; i < card.DrawCard; i++)
            {
                var newCard = striker.DrawHandCard();
                if (newCard != null) HandController.Instance.AddCard(striker, (Card) newCard);
            }
        }

        private static void TargetEffectDropCard(Card card, Member target)
        {
            for (int i = 0; i < card.DropTargetCard; i++)
            {
                target.DropHandCard();
                AttackVFX.Instance.PlayDropCardVFX(target.Character.transform);
            }
        }

        private static void TargetEffectAddCard(Card card, Member target)
        {
            foreach (var cardToAdd in card.AddTargetCard)
            {
                if (cardToAdd is null) continue;
                target.AddCard(cardToAdd);
                AttackVFX.Instance.PlayAddEnemyCardVFX(target.Character.transform);
            }
        }
    }
}
