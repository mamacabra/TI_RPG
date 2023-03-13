using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeOfIsland
{
    CommonCombat,
    BossCombat,
    StoreOrForge,
    Camp,
    Initial,
    Final
}
public class MapNodeTest2 : MonoBehaviour
{
    [SerializeField] private MapNodeTest2 parent;
    public List<MapNodeTest2> children = new List<MapNodeTest2>();

    public TypeOfIsland TypeOfIsland;
    public int depth;

    public void SetParent (MapNodeTest2 parent)
    {
        this.parent = parent;
    }
}
