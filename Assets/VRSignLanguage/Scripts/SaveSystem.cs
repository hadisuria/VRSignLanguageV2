using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // CONSTANT FOR SAVE FILE
    public static readonly string SAVE_CALIBRATION = "SavedCalibration.json";
    public static readonly string SAVE_SIGN_LANGUAGE_DICTIONARY = "SavedSignLanguageDictionary.json";

#if UNITY_EDITOR
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
#else
    public static string SAVE_FOLDER { get{ return Application.persistentDataPath + "/Saves/";}}
#endif
    public static void Init()
	{
        // check if save folder exist 
        if (!Directory.Exists(SAVE_FOLDER)) {
            // create save folder
            Directory.CreateDirectory(SAVE_FOLDER);
        } 
	}

    public static void SaveData(string saveString, string fileName)
    {
        File.WriteAllText(SAVE_FOLDER + fileName, saveString);
    }


    /**
     * @param {string} fileName - expected filename to load
     */

    public static string LoadData(string fileName)
    {
#if !UNITY_EDITOR
        if(fileName == SAVE_SIGN_LANGUAGE_DICTIONARY)
		{
            TextAsset dataText = Resources.Load<TextAsset>("SavedSignLanguageDictionary");
            //string saveString = File.ReadAllText(dataText.text);
            return dataText.text;
		}else
#endif
        if (File.Exists(SAVE_FOLDER + fileName))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + fileName);
            return saveString;
        } else
		{
            return null;
		}
    }
}
