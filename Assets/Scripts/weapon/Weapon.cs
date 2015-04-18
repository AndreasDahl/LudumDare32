﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	virtual public void fire(GameObject owner) {
		owner.GetComponent<PlayerControl> ().timer.circle.setColor(getPulseColor ());
	}

	public abstract Color getPulseColor();
}