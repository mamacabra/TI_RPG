using UnityEngine;

namespace Combat
{
    public class VFXSelected : MonoBehaviour
    {
        private Member _member;
        public GameObject vfx;

        private static Member _hoveredStriker;
        private static Member _hoveredTarget;
        private static Member _clickedStriker;

        private void Start()
        {
            _member = GetComponent<Member>();
            ToggleVFX(false);
        }

        private void FixedUpdate()
        {
            if (TargetController.Instance == null) return;

            if (_clickedStriker == _member)
            {
                ToggleVFX(true);
                return;
            }

            if (_clickedStriker && _hoveredTarget == _member)
            {
                ToggleVFX(true);
                return;
            }

            if (_hoveredStriker == _member)
            {
                ToggleVFX(true);
                return;
            }

            ToggleVFX(false);
        }

        private void ToggleVFX(bool state)
        {
            if (vfx) vfx.SetActive(state);
        }

        public static void SetHoveredStriker(Member striker)
        {
            _hoveredStriker = striker;
        }

        public static void SetHoveredTarget(Member target)
        {
            _hoveredTarget = target;
        }

        public static void SetClickedStriker(Member striker)
        {
            _clickedStriker = striker;
        }
    }
}
