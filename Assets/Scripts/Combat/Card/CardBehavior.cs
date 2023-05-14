namespace Combat
{
    public static class CardBehavior
    {
        public static void Use(Member striker, Card card, Member target)
        {
            ConsumeActionPoints(striker, card);
            ReceiveActionPoints(striker, card);

            ApplyDamage(target, card);
            ApplyHeal(target, card);

            EffectDrawCard(striker, card);

            TargetEffectDropCard(card, target);
            TargetEffectAddCard(card, target);
        }

        private static void ConsumeActionPoints(Member striker, Card card)
        {
            striker.Character.ConsumeActionPoints(card.Cost);
        }

        private static void ReceiveActionPoints(Member striker, Card card)
        {
            if (card.ActionPointsReceive > 0)
                striker.Character.ReceiveActionPoints(card.ActionPointsReceive);
        }

        private static void ApplyDamage(Member target, Card card)
        {
            if (card.Damage <= 0) return;
            target.Character.ReceiveDamage(card.Damage);
            VFXManager.Instance.PlayDamageVFX(target.Character.transform);
        }

        private static void ApplyHeal(Member target, Card card)
        {
            if (card.Heal <= 0) return;
            target.Character.ReceiveHealing(card.Heal);
            VFXManager.Instance.PlayHealingVFX(target.Character.transform);
        }

        private static void EffectDrawCard(Member striker, Card card)
        {
            for (int i = 0; i < card.DrawCard; i++)
            {
                Card newCard = striker.DrawHandCard();
                HandController.Instance.AddCard(striker, newCard);
                VFXManager.Instance.PlayHealingVFX(striker.Character.transform);
            }
        }

        private static void TargetEffectDropCard(Card card, Member target)
        {
            for (int i = 0; i < card.DropTargetCard; i++)
            {
                target.DropHandCard();
                VFXManager.Instance.PlayDropCardVFX(target.Character.transform);
            }
        }

        private static void TargetEffectAddCard(Card card, Member target)
        {
            foreach (var cardToAdd in card.AddCards)
            {
                target.AddCard(cardToAdd);
                VFXManager.Instance.PlayAddEnemyCardVFX(target.Character.transform);
            }
        }
    }
}
