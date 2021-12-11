using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateGesture : MonoBehaviour
{
    [SerializeField] private EnemyPool enemies;

    public bool CheckGesture(AlphabetID targetAlphabet)
    {
        if (enemies.usedEnemies[0].alphabet == targetAlphabet)
        {
            enemies.DestroyEnemy(enemies.usedEnemies[0]);
            return true;
        }
        else
            return false;
    }
}
