using UnityEngine;

namespace Combat
{
    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance { get; private set; }

        [Header("Damage VFX")]
        [SerializeField] private GameObject damageVFX;
        [SerializeField] private Vector3 damagePositionVFX;
        [SerializeField] private Vector3 damageScaleVFX;

        [Space()]
        [Header("Healing VFX")]
        [SerializeField] private GameObject healingVFX;
        [SerializeField] private Vector3 healingPositionVFX;
        [SerializeField] private Vector3 healingScaleVFX;

        [Space()]
        [Header("Drop Enemy Card VFX")]
        [SerializeField] private GameObject dropCardVFX;
        [SerializeField] private Vector3 dropCardPositionVFX;
        [SerializeField] private Vector3 dropCardScaleVFX;

        [Space()]
        [Header("Add Enemy Card VFX")]
        [SerializeField] private GameObject addCardVFX;
        [SerializeField] private Vector3 addCardPositionVFX;
        [SerializeField] private Vector3 addCardScaleVFX;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayDamageVFX(Transform target)
        {
            if (!damageVFX) return;

            GameObject vfx = Instantiate(damageVFX, target.position, Quaternion.identity);
            vfx.transform.position += damagePositionVFX;
            vfx.transform.localScale = damageScaleVFX;
        }

        public void PlayHealingVFX(Transform target)
        {
            if (!healingVFX) return;

            GameObject vfx = Instantiate(healingVFX, target.position, Quaternion.identity);
            vfx.transform.position += healingPositionVFX;
            vfx.transform.localScale = healingScaleVFX;
        }

        public void PlayDropCardVFX(Transform target)
        {
            if (!dropCardVFX) return;

            GameObject vfx = Instantiate(dropCardVFX, target.position, Quaternion.identity);
            vfx.transform.position += dropCardPositionVFX;
            vfx.transform.localScale = dropCardScaleVFX;
        }

        public void PlayAddEnemyCardVFX(Transform target)
        {
            if (!addCardVFX) return;

            GameObject vfx = Instantiate(addCardVFX, target.position, Quaternion.identity);
            vfx.transform.position += addCardPositionVFX;
            vfx.transform.localScale = addCardScaleVFX;
        }
    }
}
