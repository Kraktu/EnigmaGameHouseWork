using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSymboleScript : MonoBehaviour
{
	public Material[] _possibleMat;
	public int _mychoosedMat;

	private void Start()
	{
		_mychoosedMat = Random.Range(0, _possibleMat.Length);
		GetComponent<MeshRenderer>().material = _possibleMat[_mychoosedMat];
	}
}
