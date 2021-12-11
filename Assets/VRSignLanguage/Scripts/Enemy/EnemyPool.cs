﻿using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPos = new Transform[3];
    private List<Enemy> unusedEnemies = new List<Enemy>();
    public readonly List<Enemy> usedEnemies = new List<Enemy>();

    public Enemy SummonEnemy()
    {
        Enemy temp = new Enemy();

        if (unusedEnemies.Count > 0)
        {
            temp = unusedEnemies[0];
            unusedEnemies.Remove(temp);
        }
        else
        {
            temp = Instantiate(Resources.Load<Enemy>("Enemy"));
        }

        //initialize random alphabet for enemy
        temp.alphabet = (AlphabetID)UnityEngine.Random.Range(0, 26);

        temp.transform.position = spawnPos[UnityEngine.Random.Range(0, 3)].position;
        temp.gameObject.SetActive(true);
        usedEnemies.Add(temp);
        return temp;
    }

    public void DestroyEnemy(Enemy target)
    {
        usedEnemies.Remove(target);
        unusedEnemies.Add(target);
        target.gameObject.SetActive(false);
    }
}
