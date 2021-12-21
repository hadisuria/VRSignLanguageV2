using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshPro alphabetText;

    public AlphabetID alphabet;

    public void Initialize()
	{
        alphabetText.text = alphabet.ToString();
	}

    private void Update()
    {
        Move(Vector3.zero);
    }

    private void Move(Vector3 targetPos)
    {
        transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.fixedDeltaTime);
    }
}
