using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingEnigmaScript : MonoBehaviour
{
	public TextMesh _userTextMesh;
	public string[] _letterToShow;
	public string[] _letterToType;
	int _letterIndex;

	private void Start()
	{
		_letterIndex = 0;
	}
	void CheckText(string keyPressed)
	{

		if (keyPressed==_letterToType[_letterIndex])
		{
			if (_letterIndex==0)
			{
				_userTextMesh.text = "";
			}
			_userTextMesh.text += _letterToShow[_letterIndex];
			_letterIndex++;
		}
		if (_letterIndex==_letterToShow.Length)
		{
			//Destroy(this.gameObject);
		}
	}
	void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey)
		{
			
			if (_letterIndex<_letterToType.Length)
			{
				string KeyPressedString = e.keyCode.ToString();
				CheckText(KeyPressedString);
			}
		}
	}


}
