using UnityEngine;

namespace Combat
{
    public class Target : MonoBehaviour
    {
        private Member _member;
        public static Member HoveredTarget { get; private set; }

        private void Awake()
        {
            _member = GetComponent<Member>();
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
    }
}
