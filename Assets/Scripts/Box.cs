using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	public ParticleSystem collisionParticle;

	public Util.AudioGroup collisionAudio;

	public void OnCollisionEnter2D(Collision2D other)
	{
		foreach (ContactPoint2D contact in other.contacts)
		{
			ParticleSystem p = Instantiate(collisionParticle, contact.point, Quaternion.identity);
			p.gameObject.transform.localScale *= Mathf.Clamp(contact.normalImpulse / 40, 0, 2);
			Destroy(p.gameObject, p.main.duration);

			// Audio
			collisionAudio.Play(contact.point, Mathf.Clamp(contact.normalImpulse / 40, 0, 2));
		}
	}
}
