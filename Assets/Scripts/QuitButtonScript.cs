using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonScript : MonoBehaviour
{
	[HideInInspector]
	public bool _isQuitWorking = true;
	int _nbrOfCLicksCount=0;
	public int _nbrOfClicksNeededToQuit;
	public GameObject _lightBulbToActivate;
	public int _numberOfCandyToSpaw;
	public GameObject _candyToSpawn;
	public Vector3 _maxCandyInstantiationDistance;
	public string _fireworkSound;
	private void OnMouseDown()
	{
		if (_isQuitWorking)
		{
			Debug.Log("Doing Application.Quit, not shown in Unity, only in Build");
			Application.Quit();
		}
		else if (!_isQuitWorking)
		{
			_nbrOfCLicksCount++;
			SoundManager.Instance.PlaySoundEffect(_fireworkSound);
			for (int i = 0; i < _numberOfCandyToSpaw; i++)
			{
				Instantiate(_candyToSpawn, transform.position + new Vector3(Random.Range(-_maxCandyInstantiationDistance.x, _maxCandyInstantiationDistance.x), Random.Range(-_maxCandyInstantiationDistance.y, _maxCandyInstantiationDistance.y), Random.Range(-_maxCandyInstantiationDistance.z, transform.position.z-0.5f)), Quaternion.identity); ;
			}
			if (_nbrOfCLicksCount==1)
			{
				_lightBulbToActivate.GetComponent<LightBulbScript>().ActivateLightBulb();
			}
			if (_nbrOfCLicksCount>=_nbrOfClicksNeededToQuit)
			{
				Debug.Log("Doing Application.Quit, not shown in Unity, only in Build");
				Application.Quit();
			}
		}
	}
	public void OnStartPressed()
	{
		_isQuitWorking = false;
	}
}
