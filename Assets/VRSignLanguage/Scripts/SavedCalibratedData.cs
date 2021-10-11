using System;
using UnityEngine;

[Serializable]
public class SavedCalibratedData
{
	public float maxHandDistance;
	public float headLevelHeight;
	public float bellyHeight;
	public Vector3 leftShoulderOffset;
	public Vector3 rightShoulderOffset;
	public float maxXReach;

	public SavedCalibratedData(SavedCalibratedData savedCalibratedData){
		maxHandDistance = savedCalibratedData.maxHandDistance;
		headLevelHeight = savedCalibratedData.headLevelHeight;
		bellyHeight = savedCalibratedData.bellyHeight;
		leftShoulderOffset = savedCalibratedData.leftShoulderOffset;
		rightShoulderOffset = savedCalibratedData.rightShoulderOffset;
		maxXReach = savedCalibratedData.maxXReach;
	}
	public SavedCalibratedData(float handDistance, float height, float calibratedBellyHeight, Vector3 leftShoulder, Vector3 rightShoulder, float maxXReachPoint)
	{
		maxHandDistance = handDistance;
		headLevelHeight = height;
		bellyHeight = calibratedBellyHeight;
		leftShoulderOffset = leftShoulder;
		rightShoulderOffset = rightShoulder;
		maxXReach = maxXReachPoint;
	}
}
