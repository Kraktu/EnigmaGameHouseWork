using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambdaBall : MonoBehaviour
{
	public int _numberOfClicksNeeded;
	public float _delayBeforeReset;
	int _numberOfClicks;
	bool _isTimerStarted;
	public GameObject _keyToInstantiate;	public GameObject _lightBulbToActivate;
	public bool _isActive=false;

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
				_lightBulbToActivate.GetComponent<LightBulbScript>().ActivateLightBulb();
			}
		}
	}

	IEnumerator TimerBeforeReset()
	{
		float timer = 0;
		_isTimerStarted = true;
		while (timer<_delayBeforeReset)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		_numberOfClicks = 0;
		_isTimerStarted = false;
	}
}
