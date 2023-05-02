using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Member))]
    public class Target : MonoBehaviour
    {
        private Member member;

        private void Awake()
        {
            member = GetComponent<Member>();
        }

        private void OnMouseOver()
        {
            TargetController.Instance.SetTarget(member);
        }

        private void OnMouseDown()
        {
            TargetController.Instance.SetTarget(member);
        }

        private void OnMouseExit()
        {
            TargetController.Instance.RemoveTarget(member);
        }
    }
}
