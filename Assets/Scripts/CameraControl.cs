using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	Transform _camera;
	Transform _rotationCenter;
	Vector3 _localRotation;
	float _cameraDistance = 10;
	public float _mouseSensitivity=4;
	public float _scrollSensitivity=2;
	public float _orbitDampening=10;
	public float _scrollDampening=6;
	public float _minCameraDistance=1.5f;
	public float _maxCameraDistance=100;

	public bool _cameraDisabled;

	private void Start()
	{
		this._camera = this.transform;
		this._rotationCenter = this.transform.parent;
		StartCoroutine(CameraControlRotation());
	}

	IEnumerator CameraControlRotation()
	{
		while (true)
		{

			if (!_cameraDisabled)
			{
				if (Input.GetKey(KeyCode.Mouse1))
				{
					if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
					{
						_localRotation.x += Input.GetAxis("Mouse X") * _mouseSensitivity;
						_localRotation.y -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
						_localRotation.y = Mathf.Clamp(_localRotation.y, 0, 90);
					}
				}

				if (Input.GetAxis("Mouse ScrollWheel") != 0)
				{
					float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * _scrollSensitivity;
					ScrollAmount *= (this._cameraDistance * 0.3f);
					this._cameraDistance += ScrollAmount * -1;
					this._cameraDistance = Mathf.Clamp(_cameraDistance, _minCameraDistance, _maxCameraDistance);
				}
			}

			Quaternion QT = Quaternion.Euler(_localRotation.y, _localRotation.x, 0);
			this._rotationCenter.rotation = Quaternion.Lerp(this._rotationCenter.rotation, QT, Time.deltaTime * _orbitDampening);

			if (this._camera.localPosition.z != this._cameraDistance * -1)
			{
				this._camera.localPosition = new Vector3(0, 0, Mathf.Lerp(this._camera.localPosition.z, this._cameraDistance * -1, Time.deltaTime * _scrollDampening));
			}

			yield return null;
		}
	}
}
