using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneratorTest : MonoBehaviour
{
    [SerializeField] private GameObject island;
    private const int MaxDepth = 10;

    private MapNodeTest root;
    
    private void Start()
    {
        GenerateMap();
    }
    
    public void GenerateMap()
    {
        TypeOfIsland type;
        type = TypeOfIsland.Initial;

        root = GenerateNode(null, type);
       
        Queue<MapNodeTest> queue = new Queue<MapNodeTest>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            MapNodeTest node = queue.Dequeue();

            if (node.Depth >= MaxDepth)
                continue;

            int numChildren = 0;
            
            
            if(node.Depth == 0 || node.Depth == 1)
            {
                type = node.Depth == 0 ? TypeOfIsland.StoreOrForge : TypeOfIsland.CommonCombat;
                numChildren = 1;
            }
            else
            {
                if (type == TypeOfIsland.CommonCombat || type == TypeOfIsland.BossCombat)
                    numChildren = 2;
                else if (type == TypeOfIsland.StoreOrForge || type == TypeOfIsland.Camp)
                {
                    numChildren = 1;
                    type = TypeOfIsland.CommonCombat;
                }
                
            }

            List<TypeOfIsland> types = new List<TypeOfIsland>();
            if (numChildren >= 2)
            {
                int storeOrCamp = Random.Range(0, 2);

                types.Add(storeOrCamp == 0 ? TypeOfIsland.StoreOrForge : TypeOfIsland.Camp);
                types.Add(TypeOfIsland.CommonCombat);
            }
            

            for (int i = 0; i < numChildren; i++)
            {
                if (types.Count > 0)
                {
                    int t = Random.Range(0, types.Count);
                    type = types[t];
                    types.RemoveAt(t);
                }
                
                MapNodeTest child = GenerateNode(node, type);
             
                node.childrens.Add(child);
                queue.Enqueue(child);
            }
        }

        MapNodeTest lastNode = FindLastNode(root);

        foreach (MapNodeTest n in root.GetComponentsInChildren<MapNodeTest>())
        {
            if (n.childrens.Count == 0 && n !=lastNode)
            {
                n.childrens.Add(lastNode);
                lastNode.SetParent(n);
            }

            if (n == lastNode)
                n.typeOfIsland = TypeOfIsland.Final;
        }
    }
    
    MapNodeTest FindLastNode(MapNodeTest node)
    {
        if (node.childrens.Count == 0)
            return node;
        
        return FindLastNode(node.childrens[0]);
    }
    
    private MapNodeTest GenerateNode(MapNodeTest parent, TypeOfIsland type)
    {
        GameObject go = Instantiate(island, Vector3.zero, Quaternion.identity, parent == null ? transform : parent.transform);
        MapNodeTest node = go.GetComponent<MapNodeTest>();
        node.SetParent(parent);
        node.typeOfIsland = type;
        
        return node;
    }
    
    MapNodeTest GetRoot()
    {
        return root;
    }
    
    public MapNodeTest FindLastNode()
    {
        Queue<MapNodeTest> queue = new Queue<MapNodeTest>();
        queue.Enqueue(root);

        MapNodeTest lastNode = null;

        while (queue.Count > 0)
        {
            MapNodeTest node = queue.Dequeue();

            if (node.childrens.Count == 0)
                lastNode = node;

            foreach (MapNodeTest child in node.childrens)
                queue.Enqueue(child);
        }

        return lastNode;
    }
}
