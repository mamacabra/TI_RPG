using UnityEngine;

namespace Combat
{
    public class CharacterHealthBarController : MonoBehaviour
    {
        public static CharacterHealthBarController Instance;

        private int _heroHealthBarsCount;
        [SerializeField] private CharacterUI[] heroHealthBars = new CharacterUI[3];

        private int _enemyHealthBarsCount;
        [SerializeField] private CharacterUI[] enemyHealthBars = new CharacterUI[3];

        public void Awake()
        {
            Instance = this;

            foreach (CharacterUI heroHealthBar in heroHealthBars)
            {
                heroHealthBar.gameObject.SetActive(false);
            }
            foreach (CharacterUI enemyHealthBar in enemyHealthBars)
            {
                enemyHealthBar.gameObject.SetActive(false);
            }
        }

        public CharacterUI GetHeroHealthBars()
        {
            CharacterUI ui = heroHealthBars[_heroHealthBarsCount];
            ui.gameObject.SetActive(true);
            _heroHealthBarsCount++;
            return ui;
        }

        public CharacterUI GetEnemyHealthBars()
        {
            CharacterUI ui = enemyHealthBars[_enemyHealthBarsCount];
            ui.gameObject.SetActive(true);
            _enemyHealthBarsCount++;
            return ui;
        }
    }
}
