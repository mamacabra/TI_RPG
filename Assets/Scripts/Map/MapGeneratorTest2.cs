using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorTest2 : MonoBehaviour
{
    [SerializeField] private GameObject island;
    private int depth = 11;

    private MapNodeTest2 lastParent;
    private List<MapNodeTest2> nodes;

    private MapNodeTest2 root;
    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        int numOfChildrens = 0;
        TypeOfIsland type;

        type = TypeOfIsland.Initial;
        root =  InstantiateIsland(null, type,0);
        
        
        type = TypeOfIsland.StoreOrForge;
        MapNodeTest2 node1 = InstantiateIsland(root, type, 1);
        root.children.Add(node1);
        
        type = TypeOfIsland.CommonCombat;
        MapNodeTest2 node2 = InstantiateIsland(lastParent, type, 2);
        node1.children.Add(node2);

        MapNodeTest2 lastNode = node2;
        for (int i = 3; i < depth; i++)
        {
            List<TypeOfIsland> types = new List<TypeOfIsland>();
            if (type == TypeOfIsland.CommonCombat || type == TypeOfIsland.BossCombat)
            {
                numOfChildrens = 2;

                int storeOrCamp = Random.Range(0, 2);

                types.Add(storeOrCamp == 0 ? TypeOfIsland.StoreOrForge : TypeOfIsland.Camp);
                types.Add(TypeOfIsland.CommonCombat);
                for (int j = 0; j < numOfChildrens; j++)
                {
                    if (types.Count > 0)
                    {
                        int t = Random.Range(0, types.Count);
                        type = types[t];
                        types.RemoveAt(t);
                    }
                    
                    MapNodeTest2 child =  InstantiateIsland(lastParent, type, i);
                    lastNode.children.Add(child);
                }
            }
            else if(type == TypeOfIsland.StoreOrForge || type == TypeOfIsland.Camp)
            {
                numOfChildrens = 1;

                types.Add(TypeOfIsland.CommonCombat);
                
                for (int j = 0; j < numOfChildrens; j++)
                {
                    if (types.Count > 0)
                    {
                        int t = Random.Range(0, types.Count);
                        type = types[t];
                        types.RemoveAt(t);
                    }
                    
                    MapNodeTest2 child =  InstantiateIsland(lastParent, type, i);
                    lastNode.children.Add(child);
                }
            }
        }
    }
    
    MapNodeTest2 InstantiateIsland(MapNodeTest2 parent, TypeOfIsland type, int d)
    {
        GameObject go = Instantiate(island, Vector3.zero, Quaternion.identity, parent == null ? transform : parent.transform);
        MapNodeTest2 n = go.GetComponent<MapNodeTest2>();
        
        n.SetParent(parent);
        n.TypeOfIsland = type;
        n.depth = d;

        lastParent = parent;
        return n;
    }

    void AddChildrens()
    {
        
    }
}
