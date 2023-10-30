using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyFabric")]
public class EnemyFabric : ScriptableObject
{
    [SerializeField]
    private EnemyAI _eneemyMelee;

    [SerializeField]
    private EnemyAI _enemyRange;

    private EnemyAI Get(EnemyAI enemy, Vector3 pos )
    {
        Debug.Log("GetEnemy");

        return Instantiate(enemy, pos, Quaternion.identity);
    }

    public EnemyAI Get(DefaultEnemyTypes enemyType, Vector3 pos)
    {
        switch (enemyType)
        {
            case DefaultEnemyTypes.melee:
                {
                    return Get(_eneemyMelee, pos);
                }
            case DefaultEnemyTypes.range:
                {
                    return Get(_enemyRange, pos);
                }
        }

        return null;

    }
}
    public enum DefaultEnemyTypes
    {
        melee,
        range
    }