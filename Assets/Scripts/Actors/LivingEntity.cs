﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
	public float startingHealth;
	public float health;
	protected bool dead;

	protected virtual void Start()
	{
		health = startingHealth;
	}
	public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		TakeDamage(damage);
	}

	public virtual void TakeDamage(float damage)
	{
		health -= damage;
		if(health <= 0 && !dead)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		dead = true;
		GameObject.Destroy(gameObject);
	}
}
