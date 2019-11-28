using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbEnigma : MonoBehaviour
{
	private void OnMouseDown()
	{
		gameObject.GetComponent<LightBulbScript>().ActivateLightBulb();
	}
}
