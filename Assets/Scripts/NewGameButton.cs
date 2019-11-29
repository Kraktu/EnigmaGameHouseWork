using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGameButton : MonoBehaviour
{
	bool _isFirstUse = true;
	bool _isAllLightBulbActivated;
	public LightBulbScript[] _lightBulbs;
	public TextMeshPro _NewGameText;
	public Material _deactivatedMat, _activatedMat;
	public GameObject[] _gameObjectsToInitialize;
	public QuitButtonScript _quitButton;
	public string _firstTimeNewGamePressedSound;

	private void OnMouseDown()
	{
		if (_isFirstUse)
		{
			SoundManager.Instance.PlaySoundEffect(_firstTimeNewGamePressedSound);
			for (int i = 0; i < _lightBulbs.Length; i++)
			{
				_lightBulbs[i].DeActivateLightBulb();
			}
            _NewGameText.GetComponent<TextMeshPro>().color = new Color32(108, 108, 108, 92);
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			_isFirstUse = false;
			InitializeAllGameObjects();
			_quitButton.OnStartPressed();

		}


		else if (!_isFirstUse )
		{
			SceneManager.LoadScene("Display");
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
            _NewGameText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
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
