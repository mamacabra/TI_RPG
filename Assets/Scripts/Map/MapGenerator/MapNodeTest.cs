using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum TypeOfIsland
{
    CommonCombat,
    BossCombat,
    Store_Forge_Camp,
    StoreOrForge,
    Camp,
    Initial,
    Final
}

public class MapNodeTest : MonoBehaviour
{
    public List<MapNodeTest> parent = new List<MapNodeTest>();
    public List<MapNodeTest> childrens = new List<MapNodeTest>();

    public TypeOfIsland typeOfIsland;
    public int Depth;

    private TextMeshPro textTypeOfIsland;

    private LineRenderer lineRenderer1, lineRenderer2;

    [SerializeField] private string combatScene;


    private void Awake()
    {
        if (typeOfIsland == TypeOfIsland.Store_Forge_Camp)
        {
            int r = Random.Range(3, 5);
            typeOfIsland = (TypeOfIsland)r;
        }
        textTypeOfIsland = transform.GetChild(2).GetComponent<TextMeshPro>();
        textTypeOfIsland.text = typeOfIsland.ToString();

        lineRenderer1 = transform.GetChild(0).GetComponent<LineRenderer>();
        lineRenderer2 = transform.GetChild(1).GetComponent<LineRenderer>();
        Line();

        gameObject.name = typeOfIsland.ToString();
    }
    void Line()
    {
        if (childrens.Count < 1)
        {
            lineRenderer1.enabled = lineRenderer2.enabled = false;
            return;
        }

        lineRenderer1.positionCount = 2;
        lineRenderer1.SetPosition(0, transform.position);
        lineRenderer1.SetPosition(1, childrens[0].transform.position);
        if (childrens.Count > 1)
        {
            lineRenderer2.positionCount = 2;
            lineRenderer2.SetPosition(0, transform.position);
            lineRenderer2.SetPosition(1, childrens[1].transform.position);
        }
    }


    public void SetParent(MapNodeTest parent)
    {
        this.parent.Add(parent);
    }

    public string GetScene => combatScene;
}
