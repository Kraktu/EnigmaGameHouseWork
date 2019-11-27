using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymboleEnigma : MonoBehaviour
{
	public SwitchScript[] _symboleModulesArrowUp;
	public GameObject _galleryMenu;
	int[] _symboleModulesShowedSymbolIndex;
	bool _symbolMatching=false;
	public GameObject _keyToInstantiate;
	public Vector3 _keySpawnPos;

	private void Start()
	{
		_symboleModulesShowedSymbolIndex = new int[_symboleModulesArrowUp.Length];
	}
	public void CheckIfSymboleMatching()
	{
		for (int i = 0; i <_symboleModulesArrowUp.Length ; i++)
		{
			if (_symboleModulesArrowUp[i]._instantiatedGO==null)
			{
				break;
			}
			_symboleModulesShowedSymbolIndex[i]=_symboleModulesArrowUp[i]._instantiatedGO.GetComponent<TileSymboleScript>()._mychoosedMat;
		}
		
		for (int i = 1; i < _symboleModulesShowedSymbolIndex.Length; i++)
		{
			_symbolMatching = true;
			if (_symboleModulesArrowUp[i]._instantiatedGO == null)
			{
				_symbolMatching = false;
				break;
			}
			if (_symboleModulesShowedSymbolIndex[i-1]!=_symboleModulesShowedSymbolIndex[i])
			{
				_symbolMatching = false;
				break;
			}
		}
		if (_symbolMatching==true)
		{
			GameObject go = Instantiate(_keyToInstantiate,_keySpawnPos, Quaternion.identity);
			go.GetComponent<KeyScript>()._objectToSetActive = _galleryMenu;
		}
	}
}
