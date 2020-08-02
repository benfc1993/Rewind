﻿using UnityEngine;

public interface IDamagable
{
	void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection);
}