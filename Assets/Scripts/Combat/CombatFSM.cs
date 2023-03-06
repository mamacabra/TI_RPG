using UnityEngine;

namespace Combat
{
    public class CombatFSM : MonoBehaviour
    {
        static CombatFSM Instance; 
        private ICombatState combatState;
        
        [Header("Combat Panels")]
        [SerializeField] public GameObject modalPlayerHUD;
        [SerializeField] public GameObject modalVictory;
        [SerializeField] public GameObject modalDefeat;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }
    }
}
