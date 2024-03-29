﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[Header("AudioSources")]
	public AudioSource _aSourceSFX, _aSourceMusic;
	static public SoundManager Instance { get; private set; }
	public List<AudioClipStruct> _soundEffects = new List<AudioClipStruct>();
	[Header("Listof sound effect clips")]
	Dictionary<string, AudioClip> _soundEffectsDict = new Dictionary<string, AudioClip>();

	[System.Serializable]
	public struct AudioClipStruct
	{
		public string name;
		public AudioClip clip;
	}
	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		GenerateSoundEffectDict();
	}
	void GenerateSoundEffectDict()
	{
		foreach (AudioClipStruct audioClip in _soundEffects)
		{
			_soundEffectsDict.Add(audioClip.name, audioClip.clip);
		}
	}
	public void PlaySoundEffect(string clipName, float pitch = 1, float volume = 0)
	{
		//_aSourceSFX.outputAudioMixerGroup.audioMixer.SetFloat("SFXPitch", pitch);
		//_aSourceSFX.outputAudioMixerGroup.audioMixer.SetFloat("SFXVolume", volume);
		_aSourceSFX.PlayOneShot(_soundEffectsDict[clipName]);
	}
	public void ChangeMusic(string clipName, float pitch = 1, float volume = 0)
	{
		//_aSourceMusic.outputAudioMixerGroup.audioMixer.SetFloat("MusicPitch", pitch);
		//_aSourceMusic.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", volume);
		_aSourceMusic.clip = _soundEffectsDict[clipName];
		_aSourceMusic.Play();
	}
}
