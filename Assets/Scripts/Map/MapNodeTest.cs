using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum TypeOfIsland
{
    CommonCombat,
    BossCombat,
    StoreOrForge,
    Camp,
    Initial,
    Final
}*/

public class MapNodeTest : MonoBehaviour
{
    [SerializeField] private MapNodeTest parent;
    public List<MapNodeTest> childrens = new List<MapNodeTest>();

    public TypeOfIsland TypeOfIsland;
    [SerializeField] int depth;
    public int Depth
    {
        get
        {
            MapNodeTest node = parent;

            while (node!=null)
            {
                depth++;
                node = node.parent;
            }

            return depth;
        }
    }
    
    public void SetParent (MapNodeTest parent)
    {
        this.parent = parent;
    }
}
