
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryButtonClickScript : MonoBehaviour
{
	public string _gallerySoundToPlay;
	private void OnMouseDown()
	{
		SoundManager.Instance.PlaySoundEffect(_gallerySoundToPlay);
	}
}
