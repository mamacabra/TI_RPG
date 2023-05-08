using UnityEngine;

namespace Combat
{
    public class Target : MonoBehaviour
    {
        private Member _member;

        private void Awake()
        {
            _member = GetComponent<Member>();
        }

        private void OnMouseOver()
        {
            TargetController.Instance.SetTarget(_member);
            VFXSelected.SetHoveredTarget(_member);
        }

        private void OnMouseExit()
        {
            TargetController.Instance.RemoveTarget(_member);
            VFXSelected.SetHoveredTarget(null);
        }
    }
}
