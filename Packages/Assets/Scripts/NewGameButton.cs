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
		}


		if (!_isFirstUse )
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
}
