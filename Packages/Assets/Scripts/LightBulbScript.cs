using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbScript : MonoBehaviour
{
	[HideInInspector]
	public bool _isLightBulbOn = false;
	public Material _activatedMat, _deActivatedMat;
	public NewGameButton _newGameButton;

	public void ActivateLightBulb()
	{
		_isLightBulbOn = true;
		this.gameObject.GetComponent<MeshRenderer>().material = _activatedMat;
		_newGameButton.ReactivateNewGameButton();
	}
	public void DeActivateLightBulb()
	{
		_isLightBulbOn = false;
		this.gameObject.GetComponent<MeshRenderer>().material = _deActivatedMat;
	}
}
