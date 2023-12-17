using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryTooltip : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Vector2 offset;
    public void ShowTooltip(string message){
        gameObject.SetActive(true);

        Vector2 position = Input.mousePosition;
        position += offset;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

        textMeshPro.text = message;
    }
    public void HideTooltip(){
        gameObject.SetActive(false);
        textMeshPro.text = "";
    }
}
