using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatManager : MonoBehaviour, ICombatStateObserver, ICharacterObserver
    {
        public static CombatManager Instance;

        public List<Character> heroesGameObject;
        public List<Character> enemiesGameObject;

        private List<CharacterRefs> heroes;
        private List<CharacterRefs> enemies;

        private void Awake()
        {
            Instance = this;

            heroes = new List<CharacterRefs>();
            enemies = new List<CharacterRefs>();
        }

        private void Start()
        {
            CombatState.Instance.AddObserver(this);

            new CharacterFactory(heroesGameObject, enemiesGameObject);
            new DeckFactory(heroesGameObject, enemiesGameObject);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.HeroDeckShuffle:
                    ShuffleDeck(heroes);
                    break;
                case CombatStateType.EnemyDeckShuffle:
                    ShuffleDeck(enemies);
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
            int dead = heroes.FindAll(hero => hero.character.isDead).Count;
            if (dead == heroes.Count) CombatState.Instance.SetState(CombatStateType.Defeat);
        }

        private void CheckEnemiesDead()
        {
            int dead = enemies.FindAll(hero => hero.character.isDead).Count;
            if (dead == enemies.Count) CombatState.Instance.SetState(CombatStateType.Victory);
        }

        public void SetCharacterDeck(Character character, Deck deck)
        {
            CharacterRefs characterRefs = new CharacterRefs()
            {
                character = character,
                deck = deck,
                hand = deck.Shuffle(),
            };

            if (character.Type == CharacterType.Hero) heroes.Add(characterRefs);
            else enemies.Add(characterRefs);
        }

        private static void ShuffleDeck(List<CharacterRefs> decks)
        {
            foreach (CharacterRefs characterDeck in decks)
            {
                var deck = characterDeck;
                deck.hand = deck.deck.Shuffle();
            }
        }
    }
}
