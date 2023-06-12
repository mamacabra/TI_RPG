using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CardController))]
    public class CardVFX : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material mouseOverMaterial;

        private CardController _cardController;

        private void Start()
        {
            _cardController = GetComponent<CardController>();

            if (meshRenderer != null && defaultMaterial == null)
            {
                defaultMaterial = meshRenderer.material;
            }
        }

        private void FixedUpdate()
        {
            bool equalClicked = CardController.ClickedCard is not null && CardController.ClickedCard == _cardController;
            bool equalHovered = CardController.HoveredCard is not null && CardController.HoveredCard == _cardController;

            if (equalClicked || equalHovered) SetOverMaterial();
            else SetDefaultMaterial();
        }

        private void SetOverMaterial()
        {
            if (meshRenderer is not null)
            {
                meshRenderer.material = mouseOverMaterial;
            }
        }

        private void SetDefaultMaterial()
        {
            if (meshRenderer is not null)
            {
                meshRenderer.material = defaultMaterial;
            }
        }
    }
}
