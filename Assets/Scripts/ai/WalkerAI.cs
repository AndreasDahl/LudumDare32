﻿using UnityEngine;
using System.Collections;

public class WalkerAI : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3 (-0.1f, 0.0f, 0.0f);
	}
}