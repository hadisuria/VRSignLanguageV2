using System.Collections;
using UnityEngine;

public class LaserPointerHandler : MonoBehaviour
{
    [SerializeField]
    private float LaserReactivateDelay = 0.5f;
    [SerializeField]
    private GameObject laserPointerObject;
    private bool isReady = true;

    public void ToggleLaserPointer()
    {
        Debug.Log("Toggle laser pointer" + laserPointerObject.active);
        if (GameState.currState == GameState.state.Stop)
        {
            if (isReady)
            {
                if (laserPointerObject.active)
                {
                    laserPointerObject.SetActive(false);
                }
                else
                {
                    laserPointerObject.SetActive(true);
                }
                isReady = false;
            }
        }
        StartCoroutine(EnableButtonAfterDelay(LaserReactivateDelay));
    }

    IEnumerator EnableButtonAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isReady = true;
    }
}
