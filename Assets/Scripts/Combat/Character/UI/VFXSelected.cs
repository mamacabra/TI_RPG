using UnityEngine;

namespace Combat
{
    public class VFXSelected : MonoBehaviour
    {
        public GameObject vfx;

        private void Start()
        {
            ToggleVFX(false);
        }

        private void OnMouseEnter()
        {
            if (TargetController.Instance.HasCardSelected)
                ToggleVFX(true);
        }

        private void OnMouseExit()
        {
            ToggleVFX(false);
        }

        private void ToggleVFX(bool state)
        {
            if (vfx) vfx.SetActive(state);
        }
    }
}
