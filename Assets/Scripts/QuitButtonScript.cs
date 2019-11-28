using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonScript : MonoBehaviour
{
	[HideInInspector]
	public bool _isQuitWorking = true;
	int _nbrOfCLicksCount=0;
	public int _nbrOfClicksNeededToQuit;
	public GameObject _keyToInstantiate;
	public GameObject _lightBulbToActivate;
	public int _numberOfCandyToSpaw;
	public GameObject _candyToSpawn;
	public Vector3 _maxCandyInstantiationDistance;
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
			for (int i = 0; i < _numberOfCandyToSpaw; i++)
			{
				Instantiate(_candyToSpawn, transform.position + new Vector3(Random.Range(-_maxCandyInstantiationDistance.x, _maxCandyInstantiationDistance.x), Random.Range(-_maxCandyInstantiationDistance.y, _maxCandyInstantiationDistance.y), Random.Range(-_maxCandyInstantiationDistance.z, transform.position.z-3)), Quaternion.identity); ;
			}
			if (_nbrOfCLicksCount==1)
			{
				GameObject go = Instantiate(_keyToInstantiate, transform.position + Vector3.back * 3, Quaternion.identity);
				go.GetComponent<KeyScript>()._objectToSetActive = _lightBulbToActivate;
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
