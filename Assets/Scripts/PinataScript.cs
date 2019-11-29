using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataScript : MonoBehaviour
{
	public int _numberOfClicksNeeded;
	public float _delayBeforeReset;
	int _numberOfClicks;
	bool _isTimerStarted;
	[HideInInspector]
	public bool _firstStep = false, _secondStep = false;
	bool _isTimerNeeded=true;
	public Material _activeMaterial;
	public GameObject _objectToSwapWith,_candyToSpawn;
	public Vector3 _maxCandyInstantiationDistance;
	public int _numberOfCandyToSpaw;
	[HideInInspector]
	public bool _isActive=false;
	public string _pinataExplodeSound,_pinataHitSound,_pinataSwapSound;

	private void OnMouseDown()
	{
		if (_isActive)
		{
			if (_isTimerNeeded)
			{
				if (_isTimerStarted == false)
				{
					StartCoroutine(TimerBeforeReset());
				}
				_numberOfClicks++;
				SoundManager.Instance.PlaySoundEffect(_pinataHitSound);
			}

			if (!_secondStep && _firstStep && _numberOfClicks == _numberOfClicksNeeded)
			{
				SoundManager.Instance.PlaySoundEffect(_pinataSwapSound);
				_secondStep = true;
				_isTimerNeeded = false;
				Vector3 originalPosition = transform.position;
				transform.position = _objectToSwapWith.transform.position;
				_objectToSwapWith.transform.position = originalPosition;
				_objectToSwapWith.GetComponent<PinataBoxScript>()._isActive = true;
			}
		}
		
	}
	private void OnMouseEnter()
	{
		if (_isActive)
		{
			if (_firstStep == false)
			{
				_isTimerNeeded = false;
				gameObject.GetComponent<MeshRenderer>().enabled = false;
			}
		}
		
	}
	private void OnMouseExit()
	{
		if (_isActive)
		{
			if (_firstStep == false)
			{
				_isTimerNeeded = true;
				gameObject.GetComponent<MeshRenderer>().enabled = true;
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
	public void PinataFirstStep()
	{
		if (_isActive)
		{
			_firstStep = true;
			this.gameObject.GetComponent<MeshRenderer>().material = _activeMaterial;

		}
	}
	public void DestroyMe()
	{
		if (_isActive)
		{
			SoundManager.Instance.PlaySoundEffect(_pinataExplodeSound);
			for (int i = 0; i < _numberOfCandyToSpaw; i++)
			{
				Instantiate(_candyToSpawn, transform.position + new Vector3(Random.Range(-_maxCandyInstantiationDistance.x, _maxCandyInstantiationDistance.x), Random.Range(0, _maxCandyInstantiationDistance.y), Random.Range(-_maxCandyInstantiationDistance.z, _maxCandyInstantiationDistance.z)), Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
	}
}

