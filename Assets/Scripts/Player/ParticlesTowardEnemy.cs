using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesTowardEnemy : MonoBehaviour
{
	public Transform targetTransform;

	[SerializeField] private ParticleSystem system;

	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

	int count;

	void Update()
	{

		count = system.GetParticles(particles);

		for (int i = 0; i < count; i++)
		{
			ParticleSystem.Particle particle = particles[i];

			Vector3 v1 = system.transform.TransformPoint(particle.position);
			Vector3 v2 = targetTransform.transform.position;


			Vector3 tarPosi = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);
			particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
			particles[i] = particle;
		}

		system.SetParticles(particles, count);

		if (system.time > system.main.duration)
		{
			gameObject.SetActive(false);
		}
	}

	public void StartEffect(GameObject target)
	{
		targetTransform = target.transform;
		system.Play();
	}

	public void StopEffect()
    {
		system.Stop();
    }
}