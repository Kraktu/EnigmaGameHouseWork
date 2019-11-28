using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
	bool _isFirstUse = true;
	bool _isAllLightBulbActivated;
	public LightBulbScript[] _lightBulbs;
	public TextMesh _NewGameText;
	public Material _deactivatedMat, _activatedMat;
	public GameObject[] _gameObjectsToInitialize;

	private void OnMouseDown()
	{
		if (_isFirstUse)
		{
			for (int i = 0; i < _lightBulbs.Length; i++)
			{
				_lightBulbs[i].DeActivateLightBulb();
			}
			_NewGameText.gameObject.SetActive(false);
			this.gameObject.GetComponent<MeshRenderer>().material = _deactivatedMat;
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			_isFirstUse = false;
			InitializeAllGameObjects();

		}


		else if (!_isFirstUse )
		{
			SceneManager.LoadScene("EndOfTheGame");
		}
	}
	public void ReactivateNewGameButton()
	{
		_isAllLightBulbActivated = true;
		for (int i = 0; i < _lightBulbs.Length; i++)
		{
			if (_lightBulbs[i]._isLightBulbOn==false)
			{
				_isAllLightBulbActivated = false;
				break;
			}
		}
		if (_isAllLightBulbActivated==true)
		{
			_NewGameText.gameObject.SetActive(true);
			this.gameObject.GetComponent<MeshRenderer>().material = _activatedMat;
			this.gameObject.GetComponent<BoxCollider>().enabled = true;
		}
	}
	public void InitializeAllGameObjects()
	{
		for (int i = 0; i < _gameObjectsToInitialize.Length; i++)
		{
			LambdaBall lambdaBall = _gameObjectsToInitialize[i].GetComponent<LambdaBall>();
			if (lambdaBall!=null)
			{
				lambdaBall._isActive = true;
			}
		}
		for (int i = 0; i < _gameObjectsToInitialize.Length; i++)
		{
			PinataScript pinata = _gameObjectsToInitialize[i].GetComponent<PinataScript>();
			if (pinata!=null)
			{
				pinata._isActive = true;
			}
		}
		for (int i = 0; i < _gameObjectsToInitialize.Length; i++)
		{
			SwitchScript switchScript = _gameObjectsToInitialize[i].GetComponent<SwitchScript>();
			if (switchScript != null)
			{
				switchScript._isActive = true;
			}
		}
		for (int i = 0; i < _gameObjectsToInitialize.Length; i++)
		{
			SoundEnigma soundEnigma = _gameObjectsToInitialize[i].GetComponent<SoundEnigma>();
			if (soundEnigma != null)
			{
				soundEnigma._isActive = true;
			}
		}
	}
}
