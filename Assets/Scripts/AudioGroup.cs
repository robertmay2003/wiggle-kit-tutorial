using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
	[System.Serializable]
	public class AudioGroup
	{
		[SerializeField]
		public List<AudioClip> clips;

		public void Play(float volume = 1.0f)
		{
			AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], Vector2.zero, volume);
		}

		public void Play(Vector2 position, float volume = 1.0f)
		{
			AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], position, volume);
		}
	}
}
