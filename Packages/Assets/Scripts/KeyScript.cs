using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
	public GameObject _objectToSetActive;

	private void OnMouseDown()
	{
		_objectToSetActive.GetComponent<LightBulbScript>().ActivateLightBulb();
		Destroy(this.gameObject);
	}
}
