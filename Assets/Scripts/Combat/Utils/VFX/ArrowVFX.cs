using UnityEngine;

namespace Combat
{
    public class ArrowVFX : MonoBehaviour
    {
        public static ArrowVFX Instance { get; private set; }

        private VFXLine _vfxLine;
        [SerializeField] private GameObject arrowVFX;

        private Transform _origin;
        private Transform _target;

        private void Start()
        {
            Instance = this;

            if (arrowVFX)
            {
                _vfxLine = arrowVFX.GetComponent<VFXLine>();
            }
        }

        private void FixedUpdate()
        {
            GetArrowEdges();
            SetArrowEdges();

            // if (arrowVFX && origin && target )
            // {
            //     arrowVFX.SetActive(true);
            //     SetArrowEdges();
            // }
            // else
            // {
            //     arrowVFX.SetActive(false);
            // }
        }

        private void GetArrowEdges()
        {
            _origin = CardController.ClickedCard
                ? CardController.ClickedCard.transform
                : null;

            _target = Target.HoveredTarget
                ? Target.HoveredTarget.transform
                : null;
        }

        private void SetArrowEdges()
        {
            _vfxLine.SetInitialPoint(_origin);
            _vfxLine.SetTargetPoint(_target);
        }
    }
}
