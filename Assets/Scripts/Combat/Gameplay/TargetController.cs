using UnityEngine;

namespace Combat
{
    public class TargetController : MonoBehaviour
    {
        public static TargetController Instance { get; private set; }
        [SerializeField] private GameObject vfx;

        private Member _striker;
        private CardController _card;
        private Member _target;

        public bool HasCardSelected => _card != null;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (_card && _target)
            {
                vfx.SetActive(true);
                VFXLine line = vfx.GetComponent<VFXLine>();
                if (line)
                {
                    line.SetInitialPoint(_card.transform);
                    line.SetTargetPoint(_target.transform);
                }
            }
            else vfx.SetActive(false);
        }

        public void SetTarget(Member target)
        {
            _target = target;
        }

        private void RemoveTarget()
        {
            _target = null;
        }

        public void RemoveTarget(Member target)
        {
            if (_target == target) _target = null;
        }

        public void SetCard(Member striker, CardController card)
        {
            _striker = striker;
            _card = card;
        }

        private void RemoveCard()
        {
            _striker = null;
            _card = null;
        }

        public void UseCard()
        {
            if (_card && _target)
            {
                _striker.UseCard(_card.Card, _target);
            }

            RemoveCard();
            RemoveTarget();
        }
    }
}
