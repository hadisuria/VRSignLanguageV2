using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshPro alphabetText;

    public AlphabetID alphabet;

    private GameObject targetPoint;
    private bool initialized = false;
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
    }

    private void Move(Vector3 targetPos)
    {
        transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.fixedDeltaTime);
    }
}
