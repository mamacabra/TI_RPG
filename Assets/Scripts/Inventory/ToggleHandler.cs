using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    Image image;
    Toggle toggle;
    [SerializeField] Color selectedColor = Color.white;
    [SerializeField] Color defaultColor = Color.white;

    private void Awake()
    {
        image = this.GetComponent<Image>();
        toggle = this.GetComponent<Toggle>();
        defaultColor = image.color;
    }

    public void OnSelected()
    {
        if (toggle.isOn)
        {
            image.color = selectedColor;
        }
        else
        {
            image.color = defaultColor;
        }
    }
}
