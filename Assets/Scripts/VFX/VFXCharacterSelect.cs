using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCharacterSelect : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    private Material material;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        SetColor(color);
    }
    public void SetColor(Color _color)
    {
        material.SetColor("_BaseColor", _color);
        material.SetColor("_EmissionColor", _color * 2.5f);
    }
}
