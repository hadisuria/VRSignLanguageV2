using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SignLanguageDictionary
{
	/* 
     * ---REPRESENTATION OF DATA TO BE USED IN GUIDEBALL MOVEMENT---
	 * ---guidball---
	 * guideBallDataList = 
     * [
	 *  {
	 *	 word: "hello"
	 *	 left: [Vector3, Vector3, Vector3,]
	 *	 right: [ Vector3 ]
     *  },
     *  {
	 *	 word: "What"
	 *	 left: [Vector3, Vector3, Vector3,]
	 *	 right: [ Vector3 ]
     *  },
	 * ];
     */

	public List<GuideBall> guideBallDataList = new List<GuideBall>();

	public void LoadData()
	{
		// load the guideball data if any
		string saveString = SaveSystem.LoadData(SaveSystem.SAVE_SIGN_LANGUAGE_DICTIONARY);
		if (saveString != null)
		{
			SignLanguageDictionary savedGuideBallDataObj = JsonUtility.FromJson<SignLanguageDictionary>(saveString);
			guideBallDataList = savedGuideBallDataObj.guideBallDataList;
		}
	}

	public void AddWord(GuideBall newGuideBall)
	{
		guideBallDataList.Add(newGuideBall);
		// Savedata
		string json = JsonUtility.ToJson(this, true);
		SaveSystem.SaveData(json, SaveSystem.SAVE_SIGN_LANGUAGE_DICTIONARY);

		LoadData();
	}

	//public SignLanguageDictionary(List<GuideBall> savedGuideBallData)
	//{
	//	this.guideBallDataList = savedGuideBallData;
	//}
}
