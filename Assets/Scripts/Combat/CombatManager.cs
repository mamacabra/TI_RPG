using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatManager : MonoBehaviour, ICombatStateObserver, ICharacterObserver
    {
        public static CombatManager Instance;

        public Party HeroParty { get; private set; }
        public Party EnemyParty { get; private set; }

        [Header("Characters TEMP")]
        [SerializeField] private List<Character> heroesGameObject;
        [SerializeField] private List<Character> enemiesGameObject;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.PreparationStage:
                    // InitFactories();
                    HeroParty = new Party(heroesGameObject);
                    EnemyParty = new Party(enemiesGameObject);
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.HeroDeckShuffle:
                    HeroParty.ShuffleDeck();
                    HeroParty.ResetActionPoints();
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.EnemyDeckShuffle:
                    EnemyParty.ShuffleDeck();
                    EnemyParty.ResetActionPoints();
                    CombatState.Instance.NextState();
                    break;
            }
        }

        public void OnCharacterCreated(Character character) {}

        public void OnCharacterUpdated(Character character)
        {
            switch (character.Type)
            {
                case CharacterType.Hero when HeroParty.IsDefeated:
                    CombatState.Instance.SetState(CombatStateType.Defeat);
                    break;
                case CharacterType.Enemy when EnemyParty.IsDefeated:
                    CombatState.Instance.SetState(CombatStateType.Victory);
                    break;
            }
        }

        public static void UseCard(Member member, Card3D card, Member target)
        {
            if (member.Character.ConsumeActionPoints(card.card.Cost) == false) return;

            if (card.card.Damage > 0)
            {
                target.Character.ReceiveDamage(card.card.Damage);
                VFXManager.Instance.PlayDamageVFX(target.Character.transform);
            }
            if (card.card.Heal > 0)
            {
                target.Character.ReceiveHealing(card.card.Heal);
                VFXManager.Instance.PlayHealingVFX(target.Character.transform);
            };
            if (card.card.DrawCard > 0)
            {
                member.DrawRandomCard(card.card.DrawCard);
            }
            if (card.card.DropTargetCard > 0)
            {
                target.DropRandomCard(card.card.DropTargetCard);
            }
            if (card.card.AddEmptyCard)
            {
                target.AddEmptyCard();
                Debug.Log("AddEmptyCard");
            }

            member.Hand.Remove(card.card);
            PlayerCards.Instance.DrawCards();
        }
    }
}
