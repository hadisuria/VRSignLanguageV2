using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBallPath : MonoBehaviour
{
	private List<Vector3> guideBallPath = new List<Vector3>();

	// Retrieve guideball path based on choosen word from json data
	// Check active guideball left, right, or both
	// calculate guideball based on characted position offset
	// Position guideball based on saved location 
	// Move guideball following the path that has been saved

	/*
	 * REPRESENTATION OF DATA TO BE USED IN GUIDEBALL MOVEMENT
	 * ---guidball---
	 * [
	 * {
	 *	word: "hello"
	 *	guidball: {
	 *		left: [Vector3, Vector3, Vector3,]
	 *		right: [ Vector3 ]
	 *	}
	 * },
	 * {
	 *	word: "hello"
	 *	guidball: {
	 *		left: {
	 *			pivot/anchor : [ enum/string, enum/string, enum/string ]
	 *			offset : [ vector3, vector3, vector3 ]
	 *		}
	 *		right: {
	 *			pivot/anchor : [ enum/string, enum/string, enum/string ]
	 *			offset : [ vector3, vector3, vector3 ]
	 *		}
	 *	}
	 * }
	 * ]
	 */

	/*
	 * playerHmd(x,0,z)
	 * posisiin tangan di tempat ingin naro guideball
	 * Tekan button buat naro guideball 
	 * (
	 *		ngambil titik (vektor3) guideball, sama kepala, 
	 *		ngambil titik ke n tinggal masukin aja, 
	 *		
	 * )
	 * tekan tombol done untuk kalkulasi offset
	 * guideball - kepala = offset
	 * 
	 * 
	 * -------
	 * pas mau ngeluarin guideball baru kalkulasi posisi guideball berdasarkan offset yang udah disimpen di data
	 * bodycalibrator hmdPosition + offset
	 */

	/*
	 * Menu untuk nambah kata
	 * STEP:
	 * 1. Show new menu
	 * 2. Show step and instructions
	 * 3. Word yang sudah dimasukin di show di papan / menu
	 * 4. Create function to put guideball and take guideball position
	 * 5. Save data dari guideball yang dibikin
	 * 6. Kalkulasi offset dari data guideball 
	 * 7. Menu to insert word
	 * 8. Save data ke JSON (words.json)
	 * 9. Create menu to modify saved word, we can modify guideball position, hand gesture, etc (NOT PRIORITY)
	 * 
	 */

	//public enum WordKey
	//{
	//	Hello,
	//	GoodBye,
	//	ThankYou,
	//	Welcome,
	//	Morning,
	//	Night
	//}

	//private void Update()
	//{
	//	SearchData(WordKey.)
	//}

	//private void SearchData(WordKey word)
	//{
	//	if(data.word == word.ToString())
			
	//}


}
