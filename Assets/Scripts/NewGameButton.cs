using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
	private void OnMouseDown()
	{
		SceneManager.LoadScene("EndOfTheGame");
	}
}
