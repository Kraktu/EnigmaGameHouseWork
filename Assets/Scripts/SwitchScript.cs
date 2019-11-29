using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhereNewSymboleCome
{
	fromLeft,
	fromRight,
	fromTop,
	fromBottom
}
public enum TypeOfActivator
{
	SwitchActivator,
	ButtonActivator,
	ButtonPlaySoud,
	ButtonInstantiateAnywherePlusAnimation,
	PinataButton,
	MusicSwitch,
	SkyBoxSwitch,
	ButtonSymboleModule
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
	public string _soundEffectToPlayOnButtonPlaySoundClick;
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
	[Tooltip("MusicSwitch")]
	public string[] _musicToPlay;
	[Tooltip("SkyBoxSwitch")]
	public Material[] _skyBoxes;
	[Tooltip("ButtonSymboleModule")]
	public WhereNewSymboleCome _direction;
	[Tooltip("ButtonSymboleModule")]
	public Material[] _possibleMat;

	public string _SymboleRollSound,_switchSound;

	int _choosedMat=0;
	//[HideInInspector]
	public bool _isActive = false;
	int _nbrOfTimeClicked=0,_musicIndex=0,_skyBoxIndex=0;
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
				case TypeOfActivator.ButtonInstantiateAnywherePlusAnimation:
					ButtonInstantiatePlusAnimationClick();
					break;
				case TypeOfActivator.PinataButton:
					PinataButtonClick();
					break;
				case TypeOfActivator.MusicSwitch:
					MusicSwitchClick();
					break;
				case TypeOfActivator.SkyBoxSwitch:
					SkyBoxSwitchClick();
					break;
				case TypeOfActivator.ButtonSymboleModule:
					ButtonSymboleModuleClick();
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
				SoundManager.Instance.PlaySoundEffect(_switchSound);
			}

		}
		if (_isLightOn)
		{
			_switch.transform.Rotate(_rotationAngle, 0, 0);
			if (_objectToActivate != null)
			{
				_objectToActivate.SetActive(true);
				SoundManager.Instance.PlaySoundEffect(_switchSound);
			}
		}
	}
	void ButtonActivatorClick()
	{
		_objectToActivate.SetActive(true);
	}
	void ButtonPlaySoundClick()
	{
		SoundManager.Instance.PlaySoundEffect(_soundEffectToPlayOnButtonPlaySoundClick);
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

		StartCoroutine(InstantationPlusAnimation(_startPosition,_endPosition));
		
	}
	void MusicSwitchClick()
	{
		_musicIndex++;
		if (_musicIndex==_musicToPlay.Length)
		{
			_musicIndex = 0;
		}
		SoundManager.Instance.ChangeMusic(_musicToPlay[_musicIndex]);
	}
	void SkyBoxSwitchClick()
	{
		_skyBoxIndex++;
		if (_skyBoxIndex == _skyBoxes.Length)
		{
			_skyBoxIndex = 0;
		}
		RenderSettings.skybox = _skyBoxes[_skyBoxIndex];
	}
	void ButtonSymboleModuleClick()
	{
		switch (_direction)
		{
			case WhereNewSymboleCome.fromLeft:
				StartCoroutine(InstantationPlusAnimation(new Vector3(-_oldGO.transform.localScale.x,0,0),_oldGO.transform.localPosition));
				break;
			case WhereNewSymboleCome.fromRight:
				StartCoroutine(InstantationPlusAnimation(new Vector3(_oldGO.transform.localScale.x,0,0), _oldGO.transform.localPosition));
				break;
			case WhereNewSymboleCome.fromTop:
				StartCoroutine(InstantationPlusAnimation(new Vector3(0, _oldGO.transform.localScale.y,0), _oldGO.transform.localPosition));
				break;
			case WhereNewSymboleCome.fromBottom:
				StartCoroutine(InstantationPlusAnimation(new Vector3(0, -_oldGO.transform.localScale.y,0), _oldGO.transform.localPosition));
				break;
			default:
				break;
		}
		
	}
	IEnumerator InstantationPlusAnimation(Vector3 startPos,Vector3 endPos)
	{
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
		_invertedMovementButton.gameObject.GetComponent<BoxCollider>().enabled = false;
		_instantiatedGO = Instantiate(_objectToActivate, startPos, _symboleEnigma.gameObject.transform.rotation, _referenceSpace.transform);
		_choosedMat++;
		if (_choosedMat==_possibleMat.Length)
		{
			_choosedMat = 0;
		}
		_instantiatedGO.GetComponent<MeshRenderer>().material = _possibleMat[_choosedMat];
		_instantiatedGO.GetComponent<TileSymboleScript>()._mychoosedMat = _choosedMat;
		_instantiatedGO.GetComponent<TileSymboleScript>()._mychoosedMatString = _possibleMat[_choosedMat].ToString();
		float time = 0;
		float tRatio;
		SoundManager.Instance.PlaySoundEffect(_SymboleRollSound);
		while (time<=_animationDuration)
		{
			tRatio = _movementAnimationCurve.Evaluate(time / _animationDuration);
			_instantiatedGO.transform.localPosition = Vector3.Lerp(startPos, endPos, tRatio);

			_oldGO.transform.localPosition = Vector3.Lerp(endPos, endPos - startPos, tRatio);

			time += Time.deltaTime;
			yield return null;
		}

		Destroy(_oldGO);
		_instantiatedGO.transform.localPosition = endPos;
		_invertedMovementButton._instantiatedGO = _instantiatedGO;
		_symboleEnigma.CheckIfSymboleMatching();
		this.gameObject.GetComponent<BoxCollider>().enabled = true;
		_invertedMovementButton.gameObject.GetComponent<BoxCollider>().enabled = true;
		if (_instantiatedGO != null)
		{
			_invertedMovementButton._oldGO=_instantiatedGO;
			_oldGO = _instantiatedGO;
		}
	}
}
