using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatFactory : MonoBehaviour
    {
        public static CombatFactory Instance;
        private List<Member> enemies;

        public Member wormLv1;
        public Member wormLv2;
        public Member skullLv1;
        public Member skullLv2;
        public Member skullLv3;
        public Member crawfishLv1;

        public enum EnemyPosition
        {
            Left,
            Middle,
            Right
        }

        private void Awake()
        {
            Instance = this;
            enemies = new List<Member>();
        }

        public List<Member> SpawnEnemies(int depth, IslandDirection direction)
        {
            switch (depth)
            {
                case 1:
                    SpawnEnemy(wormLv1, EnemyPosition.Left);
                    SpawnEnemy(wormLv1, EnemyPosition.Middle);
                    break;
                case 2:
                    if (direction == IslandDirection.Left)
                    {
                        SpawnEnemy(wormLv1, EnemyPosition.Left);
                        SpawnEnemy(wormLv2, EnemyPosition.Middle);
                        SpawnEnemy(wormLv2, EnemyPosition.Right);
                    }
                    else
                    {
                        SpawnEnemy(wormLv1, EnemyPosition.Left);
                        SpawnEnemy(skullLv1, EnemyPosition.Middle);
                        SpawnEnemy(skullLv1, EnemyPosition.Right);
                    }
                    break;
                case 3:
                    if (direction == IslandDirection.Left)
                    {
                        SpawnEnemy(wormLv1, EnemyPosition.Left);
                        SpawnEnemy(skullLv2, EnemyPosition.Middle);
                        SpawnEnemy(wormLv2, EnemyPosition.Right);
                    }
                    else if (direction == IslandDirection.Middle)
                    {
                        SpawnEnemy(skullLv3, EnemyPosition.Left);
                        SpawnEnemy(skullLv2, EnemyPosition.Middle);
                        SpawnEnemy(skullLv3, EnemyPosition.Right);
                    }
                    else
                    {
                        SpawnEnemy(skullLv1, EnemyPosition.Left);
                        SpawnEnemy(skullLv2, EnemyPosition.Middle);
                        SpawnEnemy(skullLv2, EnemyPosition.Right);
                    }
                    break;
                case 4:
                    if (direction == IslandDirection.Left)
                    {
                        SpawnEnemy(wormLv2, EnemyPosition.Middle);
                        SpawnEnemy(skullLv2, EnemyPosition.Right);
                    }
                    else
                    {
                        SpawnEnemy(skullLv2, EnemyPosition.Middle);
                        SpawnEnemy(skullLv2, EnemyPosition.Right);
                    }
                    break;
                case 5:
                    SpawnEnemy(crawfishLv1, EnemyPosition.Middle);
                    break;
                default:
                    Debug.Log("No enemies to spawn");
                    break;
            }

            return enemies;
        }

        private void SpawnEnemy(Member enemy, EnemyPosition position)
        {
            Vector3 pos = new Vector3(7, 1.25f, 0);

            switch (position)
            {
                case EnemyPosition.Left:
                    pos.x = 3;
                    break;
                case EnemyPosition.Middle:
                    pos.x = 5;
                    break;
                case EnemyPosition.Right:
                    pos.x = 7;
                    break;
                default:
                    Debug.LogError("Invalid enemy position");
                    break;
            }

            if (enemy)
            {
                Member e = Instantiate(enemy, pos, Quaternion.identity);
                enemies.Add(e);
            }
            else Debug.LogError("Enemy is null");
        }
    }
}
