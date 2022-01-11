using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshPro alphabetText;

    public AlphabetID alphabet;

    private GameObject targetPoint;
    private bool initialized = false;

    private const float distanceToPlayer = .1f;

    public void Initialize()
    {
        if (!initialized)
        {
            targetPoint = GameObject.Find("EnemyTargetPoint");
            initialized = true;
        }
        alphabetText.text = alphabet.ToString();
    }

    private void Update()
    {
        Move(targetPoint.transform.position);
        if (Vector3.Distance(transform.position, targetPoint.transform.position) <= distanceToPlayer)
        {
            GameState.SetIsEnemyHitPlayer(true);
        }
    }

    private void Move(Vector3 targetPos)
    {
        transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.fixedDeltaTime);
    }
}
