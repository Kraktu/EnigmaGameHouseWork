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
	public Material _InactiveButtonsMat, _activeButtonMat;
	[HideInInspector]
	public bool _isActive;
	public string _soundEnigmaWrongSound;

	private void Start()
	{
		for (int i = 0; i < _soundButtons.Length; i++)
		{
			_soundButtons[i].GetComponent<BoxCollider>().enabled = false;
			//_soundButtons[i].GetComponent<MeshRenderer>().material = _InactiveButtonsMat;
		}
	}
	public void SoundButtonPressed()
	{
		_nbrOfButtonPressed++;

		if (_nbrOfButtonPressed == _soundButtons.Length)
		{
			StartCoroutine(SoundButtonPressedCoroutine());
		}
		if (_nbrOfButtonPressed==_soundButtons.Length*_nbrOfSequencesNeededToWin)
		{
			_lightBulbToActivate.GetComponent<LightBulbScript>().ActivateLightBulb();

		}
	}
	public void OnMouseDown()
	{
		if (_isActive)
		{
			for (int i = 0; i < _soundButtons.Length; i++)
			{
				_soundButtons[i].GetComponent<BoxCollider>().enabled = true;
				//_soundButtons[i].GetComponent<MeshRenderer>().material = _activeButtonMat;
			}
		}
	}
	IEnumerator SoundButtonPressedCoroutine()
	{
		yield return new WaitForSeconds(1);
		SoundManager.Instance.PlaySoundEffect(_soundEnigmaWrongSound);
	}
}
