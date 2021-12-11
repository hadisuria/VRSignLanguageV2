using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    public AlphabetID alphabet;

    private void Update()
    {
        Move(Vector3.zero);
    }

    private void Move(Vector3 targetPos)
    {
        transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.fixedDeltaTime);
    }
}
