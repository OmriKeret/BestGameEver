﻿using UnityEngine;
using System.Collections;

public class LTSplineIterate2dCS : MonoBehaviour {

	private float iter;
	public LeanTweenPath ltPath;
	public GameObject ltLogo;

	private LTSpline s;

	void Start () {
		s = new LTSpline( ltPath.splineVector() );
	}
	
	void Update () {
		s.place2d( ltLogo.transform, iter );

		iter += Time.deltaTime * 0.1f;
		if(iter>1f)
			iter = 0f;
	}
}
