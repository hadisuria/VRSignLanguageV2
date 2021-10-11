using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBallPathSpawner : MonoBehaviour
{
    private GuideBall guideBallData;

	private List<MeshRenderer> leftBallRendererList = new List<MeshRenderer>();
	private List<MeshRenderer> rightBallRendererList = new List<MeshRenderer>();

	private GameManager gameManager;

	[SerializeField] private Color leftActiveColor;
	[SerializeField] private Color rightActiveColor;
	[SerializeField] private float spawnDelay = 0.1f;
	[SerializeField] private float colorChangeInterval = 0.1f;

	private Color baseColorLeft = new Color(1, 0, 0, 1);
	private Color baseColorRight = new Color(0, 0, 1, 1);
	private bool isLeftFinished = true;
	private bool isRightFinished = true;

	private enum Position
	{
		Left,
		Right
	}

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		InitBall();
	}

	public void InitBall()
	{
		List<Vector3> leftGuideBallData = new List<Vector3>(guideBallData.leftHandPath);
		List<Vector3> rightGuideBallData = new List<Vector3>(guideBallData.rightHandPath);

		StartCoroutine (SpawnGuideBall(leftGuideBallData, Position.Left));
		StartCoroutine (SpawnGuideBall(rightGuideBallData, Position.Right));
	}

	private void Update()
	{
		if(isLeftFinished && leftBallRendererList.Count == guideBallData.leftHandPath.Count)
		{
			StartCoroutine(ChangeGuideBallColor(Position.Left));
		}

		if(isRightFinished &&  rightBallRendererList.Count == guideBallData.rightHandPath.Count)
		{
			StartCoroutine(ChangeGuideBallColor(Position.Right));
		}
	}

	public void SetGuideBallData(GuideBall value)
	{
		guideBallData = value;
	}

	public void ResetData()
	{
		for(int i = 0; i < leftBallRendererList.Count; i++)
		{
			Destroy(leftBallRendererList[i].gameObject);
		}
		for (int i = 0; i < rightBallRendererList.Count; i++)
		{
			Destroy(rightBallRendererList[i].gameObject);
		}
		leftBallRendererList.Clear();
		rightBallRendererList.Clear();
		isLeftFinished = true;
		isRightFinished = true;
	}

	private IEnumerator SpawnGuideBall(List<Vector3> guideBallPath, Position targetPos)
	{
		SavedCalibratedData calibratedData = null;
		try
		{
			calibratedData = new SavedCalibratedData(gameManager.savedCalibratedData);
		}
		catch(System.Exception e)
		{
			Debug.Log("Error : " + e);
		}

		if (guideBallPath.Count > 0 && calibratedData != null)
		{
			for (int i=0; i < guideBallPath.Count; i++)
			{
				GameObject temp = Instantiate(Resources.Load<GameObject>("GuideBall" + targetPos.ToString()));
				temp.transform.position = new Vector3(
						guideBallPath[i].x * calibratedData.maxXReach, 
						guideBallPath[i].y * calibratedData.headLevelHeight, 
						guideBallPath[i].z * calibratedData.maxHandDistance 
						);
				if (targetPos == Position.Left)
					leftBallRendererList.Add(temp.GetComponent<MeshRenderer>());
				else
					rightBallRendererList.Add(temp.GetComponent<MeshRenderer>());

				// delay spawn
				yield return new WaitForSeconds(spawnDelay);
			}
		}
	}

	private IEnumerator ChangeGuideBallColor(Position targetPos)
	{
		if(targetPos == Position.Left)
		{
			isLeftFinished = false;
			for (int i = 0; i < leftBallRendererList.Count; i++)
			{
				leftBallRendererList[i].material.color = leftActiveColor;
				//yield return new WaitForSeconds(colorChangeInterval);
				if (i == 0)
				{
					leftBallRendererList[leftBallRendererList.Count - 1].material.color = baseColorLeft;
				}
				else
				{
					leftBallRendererList[i - 1].material.color = baseColorLeft;
				}

				// delay color change
				yield return new WaitForSeconds(colorChangeInterval);
			}
			isLeftFinished = true;
		}
		else
		{
			isRightFinished = false;
			for (int i = 0; i < rightBallRendererList.Count; i++)
			{
				rightBallRendererList[i].material.color = rightActiveColor;
				//yield return new WaitForSeconds(colorChangeInterval);
				if (i == 0)
				{
					rightBallRendererList[rightBallRendererList.Count - 1].material.color = baseColorRight;
				}
				else
				{
					rightBallRendererList[i - 1].material.color = baseColorRight;
				}

				// delay color change
				yield return new WaitForSeconds(colorChangeInterval);
			}
			isRightFinished = true;
		}
	}
}
