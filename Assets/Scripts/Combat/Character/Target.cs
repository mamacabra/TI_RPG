using UnityEngine;

namespace Combat
{
    public class Target : MonoBehaviour
    {
        private Member _member;
        public static Member ClickedTarget { get; private set; }
        public static Member HoveredTarget { get; private set; }

        private void Awake()
        {
            _member = GetComponent<Member>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(1) || ClickedTarget != _member) return;
            ClickedTarget = null;
        }

        private void OnMouseOver()
        {
            HoveredTarget = _member;
            CharacterCardSelectedVFX.SetHoveredTarget(_member);
        }

        private void OnMouseExit()
        {
            if (HoveredTarget == _member) HoveredTarget = null;
            CharacterCardSelectedVFX.SetHoveredTarget(null);
        }

        private void OnMouseDown()
        {
            ClickedTarget = _member;
        }

        private void OnDisable()
        {
            ClickedTarget = null;
            HoveredTarget = null;
        }

        public static void ClearClickedTarget()
        {
            ClickedTarget = null;
        }
    }
}
