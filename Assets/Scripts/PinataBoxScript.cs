﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataBoxScript : MonoBehaviour
{
	public int _numberOfClicksNeeded;
	public float _delayBeforeReset;
	int _numberOfClicks;
	bool _isTimerStarted;
	public GameObject _keyToInstantiate;
	public GameObject _lightBulbToActivate;
	public PinataScript _pinata;
	public bool _isActive = false;

	private void OnMouseDown()
	{
		if (_isActive)
		{
			if (_isTimerStarted == false)
			{
				StartCoroutine(TimerBeforeReset());
			}
			_numberOfClicks++;
			if (_numberOfClicks == _numberOfClicksNeeded)
			{
				_pinata.DestroyMe();
				this.gameObject.GetComponent<Rigidbody>().useGravity = true;
				GameObject go = Instantiate(_keyToInstantiate, transform.position + Vector3.forward * 2, Quaternion.identity);
				go.GetComponent<KeyScript>()._objectToSetActive = _lightBulbToActivate;
			}
		}
	}

	IEnumerator TimerBeforeReset()
	{
		float timer = 0;
		_isTimerStarted = true;
		while (timer < _delayBeforeReset)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		_numberOfClicks = 0;
		_isTimerStarted = false;
	}
}
