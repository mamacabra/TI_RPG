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

        public List<CharacterRefs> Heroes { get; private set; }
        public List<CharacterRefs> Enemies { get; private set; }

        private void Awake()
        {
            Instance = this;

            Heroes = new List<CharacterRefs>();
            Enemies = new List<CharacterRefs>();
        }

        private void Start()
        {
            CombatState.Instance.AddObserver(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.Start:
                    new CharacterFactory(heroesGameObject, enemiesGameObject);
                    new DeckFactory(heroesGameObject, enemiesGameObject);
                    CombatState.Instance.SetState(CombatStateType.HeroTurn);
                    break;
                case CombatStateType.HeroDeckShuffle:
                    ShuffleDeck(Heroes);
                    CombatState.Instance.SetState(CombatStateType.EnemyTurn);
                    break;
                case CombatStateType.EnemyDeckShuffle:
                    ShuffleDeck(Enemies);
                    CombatState.Instance.SetState(CombatStateType.HeroTurn);
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
            int dead = Heroes.FindAll(hero => hero.character.isDead).Count;
            if (dead == Heroes.Count) CombatState.Instance.SetState(CombatStateType.Defeat);
        }

        private void CheckEnemiesDead()
        {
            int dead = Enemies.FindAll(hero => hero.character.isDead).Count;
            if (dead == Enemies.Count) CombatState.Instance.SetState(CombatStateType.Victory);
        }

        public void SetCharacterDeck(Character character, Deck deck)
        {
            CharacterRefs characterRefs = new CharacterRefs()
            {
                character = character,
                deck = deck,
                hand = deck.Shuffle(),
            };

            if (character.Type == CharacterType.Hero) Heroes.Add(characterRefs);
            else Enemies.Add(characterRefs);
        }

        private static void ShuffleDeck(List<CharacterRefs> characters)
        {
            foreach (CharacterRefs c in characters)
            {
                var character = c;
                character.hand = character.deck.Shuffle();
            }
        }

        public static void UseCard(Character character, Card card, Character target)
        {
            if (character.ConsumeActionPoints(card.Cost) == false) return;

            if (card.Damage > 0) target.ReceiveDamage(card.Damage);
            if (card.Heal > 0) character.ReceiveHealing(card.Heal);
        }
    }
}
