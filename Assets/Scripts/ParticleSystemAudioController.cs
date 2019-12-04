using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemAudioController : MonoBehaviour
{
	public Util.AudioGroup collisionAudio;

	private ParticleSystem _part;

	void Start()
	{
		_part = GetComponent<ParticleSystem>();
	}

	public void OnParticleCollision(GameObject other)
	{
		return;
		// Audio
		List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
		_part.GetCollisionEvents(other, collisionEvents);

		foreach (ParticleCollisionEvent e in collisionEvents)
		{
			collisionAudio.Play(
				e.intersection,
				0.25f * Mathf.Clamp(e.velocity.magnitude / 80, 0, 1));
		}
	}
}
