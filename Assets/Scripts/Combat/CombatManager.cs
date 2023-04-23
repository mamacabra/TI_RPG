using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatManager : MonoBehaviour, ICombatStateObserver, ICharacterObserver
    {
        public static CombatManager Instance;

        [Header("Characters TEMP")]
        [SerializeField] private List<Character> heroesGameObject;
        [SerializeField] private List<Character> enemiesGameObject;

        public List<Member> Heroes { get; private set; }
        public List<Member> Enemies { get; private set; }

        private void Awake()
        {
            Instance = this;

            Heroes = new List<Member>();
            Enemies = new List<Member>();
        }

        private void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.Start:
                    InitFactories();
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.HeroDeckShuffle:
                    ShuffleDeck(Heroes);
                    ResetActionPoints(Heroes);
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.EnemyDeckShuffle:
                    ShuffleDeck(Enemies);
                    ResetActionPoints(Enemies);
                    CombatState.Instance.NextState();
                    break;
            }
        }

        public void OnCharacterCreated(Character character)
        {
            // if (character.Type == CharacterType.Hero) heroes.Add(character);
            // else enemies.Add(character);
        }

        public void OnCharacterUpdated(Character character)
        {
            if (character.Type == CharacterType.Hero) CheckHeroesDead();
            else CheckEnemiesDead();
        }

        private void CheckHeroesDead()
        {
            int dead = Heroes.FindAll(hero => hero.character.IsDead).Count;
            if (dead == Heroes.Count) CombatState.Instance.SetState(CombatStateType.Defeat);
        }

        private void CheckEnemiesDead()
        {
            int dead = Enemies.FindAll(hero => hero.character.IsDead).Count;
            if (dead == Enemies.Count) CombatState.Instance.SetState(CombatStateType.Victory);
        }

        public void SetCharacterDeck(Character character, Deck deck)
        {
            Member member = new Member(character, deck);

            if (character.Type == CharacterType.Hero) Heroes.Add(member);
            else Enemies.Add(member);
        }

        private void InitFactories()
        {
            new CharacterFactory(heroesGameObject, enemiesGameObject);
            new DeckFactory(heroesGameObject, enemiesGameObject);
        }

        private static void ShuffleDeck(List<Member> members)
        {
            foreach (Member member in members)
            {
                member.hand = member.deck.Shuffle();
            }
        }

        private static void ResetActionPoints(List<Member> members)
        {
            foreach (Member member in members)
            {
                member.character.ResetActionPoints();
            }
        }

        public static void UseCard(Member member, Card3D card, Member target)
        {
            if (member.character.ConsumeActionPoints(card.card.Cost) == false) return;

            if (card.card.Damage > 0)
            {
                target.character.ReceiveDamage(card.card.Damage);
                VFXManager.Instance.PlayDamageVFX(target.character.transform);
            }
            if (card.card.Heal > 0)
            {
                target.character.ReceiveHealing(card.card.Heal);
                VFXManager.Instance.PlayHealingVFX(target.character.transform);
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

            member.hand.Remove(card.card);
            PlayerCards.Instance.DrawCards();
        }

        public static void UseCard(Member member, Card card, Member target)
        {
            if (member.character.ConsumeActionPoints(card.Cost) == false) return;

            if (card.Damage > 0)
            {
                target.character.ReceiveDamage(card.Damage);
                VFXManager.Instance.PlayDamageVFX(target.character.transform);
            }
            if (card.Heal > 0)
            {
                target.character.ReceiveHealing(card.Heal);
                VFXManager.Instance.PlayHealingVFX(target.character.transform);
            };
            if (card.DrawCard > 0)
            {
                member.DrawRandomCard(card.DrawCard);
                Debug.Log("DrawCard");
            }
            if (card.DropTargetCard > 0)
            {
                target.DropRandomCard(card.DropTargetCard);
                Debug.Log("DropTargetCard");
            }
            if (card.AddEmptyCard)
            {
                target.AddEmptyCard();
                Debug.Log("AddEmptyCard");
            }

            member.hand.Remove(card);
            PlayerCards.Instance.DrawCards();
        }

        public static void UseRandomCard(Member member, Member target)
        {
            int r = Random.Range(0, member.hand.Count);
            UseCard(member, member.hand[r], target);
        }
    }
}
