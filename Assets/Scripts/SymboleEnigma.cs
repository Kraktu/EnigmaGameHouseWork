using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymboleEnigma : MonoBehaviour
{
	public SwitchScript[] _symboleModulesArrowUp;
	public GameObject _lightBulbToActivate;
	int[] _symboleModulesShowedSymbolIndex;
	string[] _symboleModulesShowedSymbolName;
	bool _symbolMatching=false;

	private void Start()
	{
		_symboleModulesShowedSymbolIndex = new int[_symboleModulesArrowUp.Length];
		_symboleModulesShowedSymbolName = new string[_symboleModulesArrowUp.Length];
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
			_symboleModulesShowedSymbolName[i] = _symboleModulesArrowUp[i]._instantiatedGO.GetComponent<TileSymboleScript>()._mychoosedMatString;
		}
		
		for (int i = 1; i < _symboleModulesShowedSymbolIndex.Length; i++)
		{
			_symbolMatching = true;
			for (int j = 0; j < _symboleModulesShowedSymbolIndex.Length; j++)
			{
				if (_symboleModulesArrowUp[j]._instantiatedGO == null)
				{
					_symbolMatching = false;
					break;
				}
			}
			
			if (_symboleModulesShowedSymbolName[i-1]!= _symboleModulesShowedSymbolName[i])
			{
				_symbolMatching = false;
				break;
			}
		}
		if (_symbolMatching==true)
		{
			_lightBulbToActivate.GetComponent<LightBulbScript>().ActivateLightBulb();
		}
	}
}
