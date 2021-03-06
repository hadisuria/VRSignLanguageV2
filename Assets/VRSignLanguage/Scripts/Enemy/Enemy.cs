using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshPro alphabetText;
    [SerializeField] private SpriteRenderer alphabetSprite;

    public AlphabetID alphabet;

    private GameObject targetPoint;
    private bool initialized = false;

    private const float distanceToPlayer = .1f;

    public void Initialize(bool isBeginnerMode)
    {
        if (!initialized)
        {
            targetPoint = GameObject.Find("EnemyTargetPoint");
            initialized = true;
        }
        alphabetText.text = alphabet.ToString();
        if (isBeginnerMode)
        {
            alphabetSprite.sprite = Resources.Load<Sprite>("Screenshots/" + alphabet.ToString());
            alphabetSprite.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            alphabetSprite.transform.parent.gameObject.SetActive(false);
        }
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
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * speed);
        // transform.LookAt(targetPoint.transform);
        transform.forward = Vector3.Normalize(targetPos - transform.position);
        // transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.fixedDeltaTime, Space.World);
    }
}
