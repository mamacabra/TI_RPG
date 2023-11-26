using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatManager : MonoBehaviour, ICombatStateObserver, ICharacterObserver
    {
        public static CombatManager Instance;

        [SerializeField] private List<Member> heroes;
        public Party HeroParty { get; private set; }

        [SerializeField] private List<Member> enemies;
        public Party EnemyParty { get; private set; }

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
                    HeroParty = new Party(heroes);
                    enemies = CombatFactory.Instance.SpawnEnemies(MapManager.Instance.currentIslandDepth, MapManager.Instance.currentIslandDirection);
                    EnemyParty = new Party(enemies);
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
    }
}
