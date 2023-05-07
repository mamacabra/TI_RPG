using UnityEngine;

namespace Combat
{
    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance { get; private set; }

        [Header("Damage VFX")]
        [SerializeField] private GameObject damageVFX;
        [SerializeField] private Vector3 damagePositiveVFX;
        [SerializeField] private Vector3 damageScaleVFX;

        [Space()]
        [Header("Healing VFX")]
        [SerializeField] private GameObject healingVFX;
        [SerializeField] private Vector3 healingPositiveVFX;
        [SerializeField] private Vector3 healingScaleVFX;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayDamageVFX(Transform target)
        {
            if (!damageVFX) return;

            GameObject vfx = Instantiate(damageVFX, target.position, Quaternion.identity);
            vfx.transform.position += damagePositiveVFX;
            vfx.transform.localScale = damageScaleVFX;
        }

        public void PlayHealingVFX(Transform target)
        {
            if (!healingVFX) return;

            GameObject vfx = Instantiate(healingVFX, target.position, Quaternion.identity);
            vfx.transform.position += healingPositiveVFX;
            vfx.transform.localScale = healingScaleVFX;
        }
    }
}
