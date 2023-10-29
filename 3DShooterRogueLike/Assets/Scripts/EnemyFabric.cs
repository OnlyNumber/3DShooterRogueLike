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

    private EnemyAI Get(EnemyAI enemy)
    {
        Debug.Log("GetEnemy");

        return Instantiate(enemy);
    }

    public EnemyAI Get(DefaultEnemyTypes enemyType)
    {
        switch (enemyType)
        {
            case DefaultEnemyTypes.melee:
                {
                    return Get(_eneemyMelee);
                }
            case DefaultEnemyTypes.range:
                {
                    return Get(_enemyRange);
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