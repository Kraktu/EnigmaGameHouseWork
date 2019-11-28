using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnigma : MonoBehaviour
{
	public SwitchScript[] _soundButtons;
	public Vector3 _spawnKeyPlace;
	int _nbrOfButtonPressed=0;
	public int _nbrOfSequencesNeededToWin;
	public GameObject _lightBulbToActivate;
	public GameObject _achievementButton;
	public Material _InactiveButtonsMat, _activeButtonMat;

	private void Start()
	{
		for (int i = 0; i < _soundButtons.Length; i++)
		{
			_soundButtons[i].GetComponent<BoxCollider>().enabled = false;
			_soundButtons[i].GetComponent<MeshRenderer>().material = _InactiveButtonsMat;
		}
	}
	public void SoundButtonPressed()
	{
		_nbrOfButtonPressed++;
		if (_nbrOfButtonPressed==_soundButtons.Length*_nbrOfSequencesNeededToWin)
		{
			GameObject go = Instantiate(_lightBulbToActivate, _spawnKeyPlace, Quaternion.identity);
			go.GetComponent<KeyScript>()._objectToSetActive = _achievementButton;
		}
	}
	public void OnMouseDown()
	{
		for (int i = 0; i < _soundButtons.Length; i++)
		{
			_soundButtons[i].GetComponent<BoxCollider>().enabled = true;
			_soundButtons[i].GetComponent<MeshRenderer>().material = _activeButtonMat;
		}
	}
}
