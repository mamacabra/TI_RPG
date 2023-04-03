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

        public List<CharacterData> Heroes { get; private set; }
        public List<CharacterData> Enemies { get; private set; }

        private void Awake()
        {
            Instance = this;

            Heroes = new List<CharacterData>();
            Enemies = new List<CharacterData>();
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
            CharacterData characterData = new CharacterData(character, deck);

            if (character.Type == CharacterType.Hero) Heroes.Add(characterData);
            else Enemies.Add(characterData);
        }

        private void InitFactories()
        {
            new CharacterFactory(heroesGameObject, enemiesGameObject);
            new DeckFactory(heroesGameObject, enemiesGameObject);
        }

        private static void ShuffleDeck(List<CharacterData> characters)
        {
            foreach (CharacterData character in characters)
            {
                character.hand = character.deck.Shuffle();
            }
        }

        private static void ResetActionPoints(List<CharacterData> characters)
        {
            foreach (CharacterData character in characters)
            {
                character.character.ResetActionPoints();
            }
        }

        public static void UseCard(Character character, Card card, Character target)
        {
            if (character.ConsumeActionPoints(card.Cost) == false) return;

            if (card.Damage > 0)
            {
                target.ReceiveDamage(card.Damage);
                VFXManager.Instance.PlayDamageVFX(target.transform);
            }
            if (card.Heal > 0)
            {
                target.ReceiveHealing(card.Heal);
                VFXManager.Instance.PlayHealingVFX(target.transform);
            };
        }

        public static void UseRandomCard(CharacterData characterData, Character target)
        {
            int r = Random.Range(0, characterData.hand.Count);
            UseCard(characterData.character, characterData.hand[r], target);
        }
    }
}
