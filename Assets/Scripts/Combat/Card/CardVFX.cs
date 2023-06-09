using UnityEngine;

namespace Combat
{
    public class CardVFX : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material mouseOverMaterial;

        private void Start()
        {
            if (meshRenderer != null && defaultMaterial == null)
            {
                defaultMaterial = meshRenderer.material;
            }
        }

        public void SetOverMaterial()
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = mouseOverMaterial;
            }
        }

        public void SetDefaultMaterial()
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = defaultMaterial;
            }
        }
    }
}
