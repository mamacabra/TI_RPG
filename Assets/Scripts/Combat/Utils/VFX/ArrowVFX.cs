using UnityEngine;

namespace Combat
{
    public class ArrowVFX : MonoBehaviour
    {
        public static ArrowVFX Instance { get; private set; }

        [SerializeField] private GameObject arrowVFX;
        private Transform _origin;
        private Transform _target;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            GetArrowEdges();

            if (_origin && _target && arrowVFX)
            {
                arrowVFX.SetActive(true);
                SetArrowEdges();
            }
            else
            {
                arrowVFX.SetActive(false);
            }
        }

        private void GetArrowEdges()
        {
            if (CardController.DraggedCard) _origin = CardController.DraggedCard.transform;
            else if (CardController.ClickedCard) _origin = CardController.ClickedCard.transform;
            else _origin = null;

            _target = Target.HoveredTarget ? Target.HoveredTarget.transform : null;
        }

        private void SetArrowEdges()
        {
            VFXLine line = arrowVFX.GetComponent<VFXLine>();

            if (!line) return;
            line.SetInitialPoint(_origin);
            line.SetTargetPoint(_target);
        }
    }
}
