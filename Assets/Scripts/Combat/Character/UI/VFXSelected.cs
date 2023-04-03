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
            if (HandController.Instance.HasCardSelected)
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
