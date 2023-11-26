using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatFactory : MonoBehaviour
    {
        public static CombatFactory Instance;

        public Member wormLv1;

        private void Awake()
        {
            Instance = this;
        }

        public List<Member> SpawnEnemies(int depth, IslandDirection direction)
        {
            List<Member> enemies = new List<Member>();

            switch (depth)
            {
                case 1:
                    Member a = Instantiate(wormLv1, Vector3.zero, Quaternion.identity);
                    Member b = Instantiate(wormLv1, Vector3.zero + Vector3.right * 5, Quaternion.identity);
                    enemies.Add(a);
                    enemies.Add(b);
                    break;
                case 2:
                    if (direction == IslandDirection.Left)
                    {
                        Debug.Log("Spawn worm lv1");
                        Debug.Log("Spawn worm lv2");
                        Debug.Log("Spawn worm lv2");
                    }
                    else
                    {
                        Debug.Log("Spawn worm lv1");
                        Debug.Log("Spawn skull lv1");
                        Debug.Log("Spawn skull lv2");
                    }
                    break;
                default:
                    Debug.Log("No enemies to spawn");
                    break;
            }

            return enemies;
        }
    }
}
