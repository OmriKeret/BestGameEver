using System;
using UnityEngine;


public class CannonPaths
{

	public static Vector3[] bottomLeft = new Vector3[] {
		new Vector3 (-25.4f, -3.7f, 0f),
		new Vector3 (-24.05f, -3.7f, 0f),
		new Vector3 (-22f, -3.7f, 0f),
		new Vector3 (-20.5f, -3.7f, 0f),
		new Vector3 (-20.5f, -3.7f, 0f),
		//TODO: From here, the cannon should move FASTER
		new Vector3 (-22.72f, 8.38f, 0f),
		new Vector3 (-10.98f, 19.09f, 0f),
		new Vector3 (3.26f, 21.35f, 0f)
		//TODO: Get here in 2 seconds.
	};
    public static Vector3[] bottomRight = new Vector3[] {
		new Vector3 (25.4f, -3.7f, 0f),
		new Vector3 (24.05f, -3.7f, 0f),
		new Vector3 (22f, -3.7f, 0f),
		new Vector3 (20.5f, -3.7f, 0f),
		new Vector3 (20.5f, -3.7f, 0f),
		//TODO: From here, the cannon should move FASTER
		new Vector3 (22.72f, 8.38f, 0f),
		new Vector3 (10.98f, 19.09f, 0f),
		new Vector3 (3.26f, 21.35f, 0f)
		//TODO: Get here in 2 seconds.
	};

    public static Vector3[] flyFromLeft = new Vector3[]
    {
        new Vector3(-25f, -4.35f, 0.03f), new Vector3(10f, 20f, 0.03064619f), new Vector3(-10f, 20f, 0.03064619f),
        new Vector3(25f, -4.6f, 0.03f)
    };

    public static Vector3[] flyFromRight = new Vector3[]
    {
        new Vector3(25f, -4.599999f, 0.03f), new Vector3(-10f, 20f, 0.03064619f), new Vector3(10f, 20f, 0.03064619f),
        new Vector3(-25f, -4.349999f, 0.03f)
    };
}
