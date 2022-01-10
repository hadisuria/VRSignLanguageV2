using UnityEngine;
using UnityEngine.UI;

public class AlphabetDetailHandler : MonoBehaviour
{
    private AlphabetID currAlphaID;

    [SerializeField] private Image targetImage;
    [SerializeField] private Image targetImageFront;

    public void InitializeImage(AlphabetID targetID)
    {
        gameObject.SetActive(true);
        currAlphaID = targetID;

        targetImageFront.sprite = Resources.Load<Sprite>("Screenshots/" + currAlphaID.ToString() + "-front");
        targetImage.sprite = Resources.Load<Sprite>("Screenshots/" + currAlphaID.ToString());
    }
}
