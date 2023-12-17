using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    [RequireComponent(typeof(CardController))]
    public class CardVFX : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material mouseOverMaterial;

        private CardController _cardController;

        private void Start()
        {
            _cardController = GetComponent<CardController>();

            // if (meshRenderer != null && defaultMaterial == null)
            // {
            //     defaultMaterial = meshRenderer.material;
            // }
            if (backgroundImage != null && defaultMaterial == null)
            {
                defaultMaterial = backgroundImage.material;
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
            // if (meshRenderer is not null)
            // {
            //     meshRenderer.material = mouseOverMaterial;
            // }
            if (backgroundImage is not null)
            {
                backgroundImage.material = mouseOverMaterial;
            }
        }

        private void SetDefaultMaterial()
        {
            // if (meshRenderer is not null)
            // {
            //     meshRenderer.material = defaultMaterial;
            // }
            if (backgroundImage is not null)
            {
                backgroundImage.material = defaultMaterial;
            }
        }
    }
}
