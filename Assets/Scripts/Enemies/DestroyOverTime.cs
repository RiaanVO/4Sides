﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float lifeTime;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifeTime);
	}
}
