using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	public ParticleSystem collisionParticle;

	public Util.AudioGroup collisionAudio;

	public AnimationCurve collisionIntensity;
	public AnimationCurve collisionSharpness;

	public void OnCollisionEnter2D(Collision2D other)
	{
		foreach (ContactPoint2D contact in other.contacts)
		{
			ParticleSystem p = Instantiate(collisionParticle, contact.point, Quaternion.identity);
			p.gameObject.transform.localScale *= Mathf.Clamp(contact.normalImpulse / 40, 0, 2);
			Destroy(p.gameObject, p.main.duration);

			// Audio
			float volume = Mathf.Clamp(contact.normalImpulse / 40, 0, 2);
			collisionAudio.Play(contact.point, volume);

			// Vibration
			WiggleKit.StartVibration(
				ScaleCurve(collisionIntensity, volume),
				ScaleCurve(collisionSharpness, volume)
			);
		}
	}

	private AnimationCurve ScaleCurve(AnimationCurve curve, float scale)
	{
		Keyframe[] keys = curve.keys;

		for (int i = 0; i < keys.Length; i++)
		{
			keys[i].value *= scale;
		}

		return new AnimationCurve(keys);
	}
}
