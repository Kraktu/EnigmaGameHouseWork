using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingEnigmaScript : MonoBehaviour
{
	public TextMesh _userTextMesh;
	public string[] _letterToShow;
	public string[] _letterToType;
	int _letterIndex=0;
	public GameObject _lightBulbToActivate;
	public string _wordWrittenAtTheEnd;
	bool _isWrittingPossible = true;
	public int _minLetterToType=0;
	int _typedLetter;



	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)&&_typedLetter>= _minLetterToType)
		{
			_userTextMesh.text = _wordWrittenAtTheEnd;
			_isWrittingPossible = false;
			_lightBulbToActivate.GetComponent<LightBulbScript>().ActivateLightBulb();
		}
	}
	void OnGUI()
	{
		if (Input.anyKeyDown)
		{
			Event e = Event.current;
			if (_isWrittingPossible && e.isKey)
			{
				if (_letterIndex < _letterToType.Length)
				{
					string KeyPressedString = e.keyCode.ToString();
					if (KeyPressedString != "None")
					{
						_typedLetter++;
						_userTextMesh.text += KeyPressedString;
					}
				}
			}
		}
		
	}

	//private void Start()
	//{
	//	_letterIndex = 0;
	//}
	//void CheckText(string keyPressed)
	//{
	//
	//	if (keyPressed==_letterToType[_letterIndex])
	//	{
	//		if (_letterIndex==0)
	//		{
	//			_userTextMesh.text = "";
	//		}
	//		_userTextMesh.text += _letterToShow[_letterIndex];
	//		_letterIndex++;
	//	}
	//	if (_letterIndex==_letterToShow.Length)
	//	{
	//		_lightBulbToActivate.GetComponent<LightBulbScript>().ActivateLightBulb();
	//		Destroy(this.gameObject);
	//	}
	//}
	//void OnGUI()
	//{
	//	Event e = Event.current;
	//	if (e.isKey)
	//	{
	//		
	//		if (_letterIndex<_letterToType.Length)
	//		{
	//			string KeyPressedString = e.keyCode.ToString();
	//			CheckText(KeyPressedString);
	//		}
	//	}
	//}


}
