using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbScript : MonoBehaviour
{
	[HideInInspector]
	public bool _isLightBulbOn = false;
	public Material _activatedMat, _deActivatedMat;
	public NewGameButton _newGameButton;
	public string _enigmaClearSound, _lightBulbLight;

	public void ActivateLightBulb()
	{
		_isLightBulbOn = true;
		StartCoroutine(ActivateLightBulbCoroutine());
	}
	public void DeActivateLightBulb()
	{
		_isLightBulbOn = false;
		this.gameObject.GetComponent<MeshRenderer>().material = _deActivatedMat;
	}

	IEnumerator ActivateLightBulbCoroutine()
	{
		yield return new WaitForSeconds(1);
		SoundManager.Instance.PlaySoundEffect(_enigmaClearSound);
		yield return new WaitForSeconds(2);
		this.gameObject.GetComponent<MeshRenderer>().material = _activatedMat;
		SoundManager.Instance.PlaySoundEffect(_lightBulbLight);
		_newGameButton.ReactivateNewGameButton();
	}
}
