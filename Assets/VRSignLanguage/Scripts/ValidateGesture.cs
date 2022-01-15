using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidateGesture : MonoBehaviour
{
    [SerializeField] private EnemyPool enemies;
    private AlphabetDetailHandler dictionaryAlphabetDetail;


    // public bool CheckGesture(AlphabetID targetAlphabet)
    // {
    //     if (enemies.usedEnemies[0].alphabet == targetAlphabet)
    //     {
    //         enemies.DestroyEnemy(enemies.usedEnemies[0]);
    //         return true;
    //     }
    //     else
    //         return false;
    // }
    public void CheckGesture(int targetAlphabet)
    {
        if (enemies.usedEnemies.Count > 0)
        {
            if (enemies.usedEnemies[0].alphabet == (AlphabetID)targetAlphabet)
            {
                enemies.DestroyEnemy(enemies.usedEnemies[0]);
                GameState.SetIsGestureCorrect(true);
            }
        }

        if (GameState.currState == GameState.state.Dictionary)
        {
            if (dictionaryAlphabetDetail == null)
            {
                dictionaryAlphabetDetail = GameObject.FindObjectOfType<AlphabetDetailHandler>();
            }
            else if (dictionaryAlphabetDetail.currAlphaID == (AlphabetID)targetAlphabet)
            {
                GameState.SetIsGestureCorrect(true);
            }
        }
    }
}
