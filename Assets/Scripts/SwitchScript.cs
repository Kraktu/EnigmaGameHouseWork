using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfActivator
{
	SwitchActivator,
	ButtonActivator,
	ButtonPlaySoud,
	ButtonInstantiatePlusAnimation,
	PinataButton
}
public class SwitchScript : MonoBehaviour
{
	public TypeOfActivator _typeOfActivator;
	[Tooltip("SwitchActivator,ButtonActivator,ButtonInstantiatePlusAnimation,PinataButton")]
	public GameObject _objectToActivate;
	[Tooltip("SwitchActivator")]
	public GameObject _switch;
	[Tooltip("SwitchActivator")]
	public float _rotationAngle;
	[Tooltip("SwitchActivator")]
	public bool _isObjectDisablable = false;
	[Tooltip("ButtonPlaySound")]
	public string _soundEffectToPlay;
	[Tooltip("ButtonPlaySound")]
	public SoundEnigma _soundEnigma;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public Vector3 _startPosition, _endPosition;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public float _animationDuration;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public AnimationCurve _movementAnimationCurve;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public GameObject _oldGO;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public GameObject _referenceSpace;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public SwitchScript _invertedMovementButton;
	[Tooltip("ButtonInstantiatePlusAnimation")]
	public SymboleEnigma _symboleEnigma;

	[HideInInspector]
	public bool _isActive = false;
	int _nbrOfTimeClicked=0;
	bool _isLightOn = false;
	[HideInInspector]
	public GameObject _instantiatedGO;

	private void Start()
	{
		switch (_typeOfActivator)		
		{
			case TypeOfActivator.SwitchActivator:
				_switch.transform.Rotate(-_rotationAngle * 0.5f, 0, 0);
				break;

			case TypeOfActivator.ButtonActivator:
				break;

			default:
				break;
		}
		
	}
	private void OnMouseDown()
	{
		if (_isActive)
		{
			switch (_typeOfActivator)
			{
				case TypeOfActivator.SwitchActivator:
					SwitchActivatorClick();
					break;

				case TypeOfActivator.ButtonActivator:
					ButtonActivatorClick();
					break;

				case TypeOfActivator.ButtonPlaySoud:
					ButtonPlaySoundClick();
					break;
				case TypeOfActivator.ButtonInstantiatePlusAnimation:
					ButtonInstantiatePlusAnimationClick();
					break;
				case TypeOfActivator.PinataButton:
					PinataButtonClick();
					break;


				default:
					break;
			}
		}
	}

	void PinataButtonClick()
	{
		_objectToActivate.GetComponent<PinataScript>().PinataFirstStep();
	}
	void SwitchActivatorClick()
	{
		_isLightOn = !_isLightOn;
		if (!_isLightOn)
		{
			_switch.transform.Rotate(-_rotationAngle, 0, 0);
			if (_isObjectDisablable)
			{
				_objectToActivate.SetActive(false);
			}

		}
		if (_isLightOn)
		{
			_switch.transform.Rotate(_rotationAngle, 0, 0);
			if (_objectToActivate != null)
			{
				_objectToActivate.SetActive(true);
			}
		}
	}
	void ButtonActivatorClick()
	{
		_objectToActivate.SetActive(true);
	}
	void ButtonPlaySoundClick()
	{
		SoundManager.Instance.PlaySoundEffect(_soundEffectToPlay);
		if (_soundEnigma != null)
		{
			_nbrOfTimeClicked++;
			if (_nbrOfTimeClicked <= _soundEnigma._nbrOfSequencesNeededToWin)
			{
				_soundEnigma.SoundButtonPressed();
			}
		}
	}
	void ButtonInstantiatePlusAnimationClick()
	{

		StartCoroutine(InstantationPlusAnimation());
		
	}
	IEnumerator InstantationPlusAnimation()
	{
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
		_invertedMovementButton.gameObject.GetComponent<BoxCollider>().enabled = false;
		if (_instantiatedGO != null)
		{
			_oldGO = _instantiatedGO;
		}

		

		_instantiatedGO = Instantiate(_objectToActivate, _startPosition, Quaternion.identity, _referenceSpace.transform);
		float time = 0;
		float tRatio;
		while (time<=_animationDuration)
		{
			tRatio = _movementAnimationCurve.Evaluate(time / _animationDuration);
			_instantiatedGO.transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, tRatio);
			_oldGO.transform.localPosition = Vector3.Lerp(_endPosition, _endPosition-_startPosition, tRatio);

			time += Time.deltaTime;
			yield return null;
		}

		Destroy(_oldGO);
		_instantiatedGO.transform.localPosition = _endPosition;
		_invertedMovementButton._instantiatedGO = _instantiatedGO;
		_symboleEnigma.CheckIfSymboleMatching();
		this.gameObject.GetComponent<BoxCollider>().enabled = true;
		_invertedMovementButton.gameObject.GetComponent<BoxCollider>().enabled = true;
	}
}
