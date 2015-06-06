using System;
using UnityEngine;


public class SpikePaths
{
	public Vector3[] topRight = new Vector3[] {
		new Vector3 (24.13f, 18.66f, 0f),
		new Vector3 (19.49f, 19.92f, 0f),
		new Vector3 (12.12f, 18.35f, 0f),
		new Vector3 (9.8f, 16.03f, 0f),
		new Vector3 (9.8f, 16.03f, 0f),
		new Vector3 (18.7f, 9.39f, 0f),
		new Vector3 (11.74f, 0.24f, 0f),
		new Vector3 (4.33f, 3.98f, 0f),
		new Vector3 (4.33f, 3.98f, 0f),
		new Vector3 (-3.48f, 3.07f, 0f),
		new Vector3 (1.01f, 14.38f, 0f),
		new Vector3 (-4.75f, 16.24f, 0f),
		new Vector3 (-4.75f, 16.24f, 0f),
		new Vector3 (-9.8f, 10.7f, 0f),
		new Vector3 (-24f, 16.9f, 0f),
		new Vector3 (-24f, 10f, 0f),
		new Vector3 (-24f, 10f, 0f),
		new Vector3 (-32.52f, 9.58f, 0f),
		new Vector3 (-28.26f, 9.79f, 0f),
		new Vector3 (-38.2f, 9.3f, 0f)
	};
	
	public Vector3[] topLeft = new Vector3[]{ 
		new Vector3(-24.13f,18.66f,0f), 
		new Vector3(-19.49f,19.92f,0f), 
		new Vector3(-12.12f,18.35f,0f), 
		new Vector3(-9.8f,16.03f,0f), 
		new Vector3(-9.8f,16.03f,0f), 
		new Vector3(-18.7f,9.39f,0f), 
		new Vector3(-11.74f,0.24f,0f), 
		new Vector3(-4.33f,3.98f,0f), 
		new Vector3(-4.33f,3.98f,0f), 
		new Vector3(3.48f,3.07f,0f), 
		new Vector3(-1.01f,14.38f,0f), 
		new Vector3(4.75f,16.24f,0f), 
		new Vector3(4.75f,16.24f,0f), 
		new Vector3(9.8f,10.7f,0f), 
		new Vector3(24f,16.9f,0f), 
		new Vector3(24f,10f,0f), 
		new Vector3(24f,10f,0f), 
		new Vector3(32.52f,9.58f,0f), 
		new Vector3(28.26f,9.79f,0f), 
		new Vector3(38.2f,9.3f,0f) };

	public Vector3[] topMid = new Vector3[] {
		new Vector3 (1.8f, 19.4f, 0f),
		new Vector3 (1.9f, 15.8f, 0f),
		new Vector3 (1.8f, 12.6f, 0f),
		new Vector3 (1.8f, 10f, 0f),
		new Vector3 (1.8f, 10f, 0f),
		new Vector3 (-0.82f, 10.29f, 0f),
		new Vector3 (-3.85f, 9.99f, 0f),
		new Vector3 (-7.69f, 9.76f, 0f),
		new Vector3 (-7.69f, 9.76f, 0f),
		new Vector3 (-3.85f, 9.99f, 0f),
		new Vector3 (-0.82f, 10.29f, 0f),
		new Vector3 (1.8f, 10f, 0f),
		new Vector3 (1.8f, 10f, 0f),
		new Vector3 (3.72f, 10.01f, 0f),
		new Vector3 (7.27f, 10f, 0f),
		new Vector3 (9.49f, 10.01f, 0f),
		new Vector3 (9.49f, 10.01f, 0f),
		new Vector3 (6.88f, 10.01f, 0f),
		new Vector3 (3.62f, 9.95f, 0f),
		new Vector3 (1.8f, 10f, 0f)
		//TODO: Make Spike stay in place here.
	};
	public Vector3[] midLeft = new Vector3[]{ 
		new Vector3(-27.24f,0.17f,0f), 
		new Vector3(-26.54f,1.69f,0f), 
		new Vector3(-17.8f,4.03f,0f), 
		new Vector3(-17.03f,3.19f,0f), 
		new Vector3(-17.03f,3.19f,0f), 
		new Vector3(-14.73f,8.13f,0f), 
		new Vector3(-6.28f,7.22f,0f), 
		new Vector3(-4.65f,9.35f,0f), 
		new Vector3(-4.65f,9.35f,0f),  
		//TODO: Make Spike stay in place here.

	};

	public Vector3[] midRight = new Vector3[]{ 
		new Vector3(27.24f,0.17f,0f), 
		new Vector3(26.54f,1.69f,0f), 
		new Vector3(17.8f,4.03f,0f), 
		new Vector3(17.03f,3.19f,0f), 
		new Vector3(17.03f,3.19f,0f), 
		new Vector3(14.73f,8.13f,0f), 
		new Vector3(6.01f,7.58f,0f),
		new Vector3(4.65f,9.35f,0f) 
		//TODO: Make Spike stay in place here.
	};
	public Vector3[] bottomLeft = new Vector3[] {
		new Vector3 (-24.45f, -7.79f, 0f),
		new Vector3 (-24.31f, -3.18f, 0f),
		new Vector3 (-23.72f, 5.58f, 0f),
		new Vector3 (-23.41f, 9.85f, 0f),
		new Vector3 (-23.41f, 9.85f, 0f),
		new Vector3 (-23.36f, 13.1f, 0f),
		new Vector3 (-23.14f, 16.78f, 0f),
		new Vector3 (-23.08f, 19.86f, 0f),
		new Vector3 (-23.08f, 19.86f, 0f),
		new Vector3 (-19.93f, 19.65f, 0f),
		new Vector3 (-13.71f, 19.76f, 0f),
		new Vector3 (-8.65f, 19.8f, 0f),
		new Vector3 (-8.65f, 19.8f, 0f),
		new Vector3 (-1.881999f, 19.89f, 0f),
		new Vector3 (1.38f, 12.94f, 0f),
		new Vector3 (-2.4f, 10.15f, 0f),
		new Vector3 (-2.4f, 10.15f, 0f),
		new Vector3 (-7.57f, 5.75f, 0f),
		new Vector3 (2.13f, 1.77f, 0f),
		new Vector3 (5.26f, 4.5f, 0f),
		new Vector3 (5.26f, 4.5f, 0f),
		new Vector3 (8.95f, 4.26f, 0f),
		new Vector3 (13.63f, 3.61f, 0f),
		new Vector3 (18.09f, 3.25f, 0f),
		new Vector3 (18.09f, 3.25f, 0f),
		new Vector3 (24.51f, 6.39f, 0f),
		new Vector3 (27.57f, -3.79f, 0f),
		new Vector3 (25.35f, -6.45f, 0f),
		new Vector3 (25.35f, -6.45f, 0f),
		new Vector3 (25.854f, -14.154f, 0f),
		new Vector3 (25.602f, -10.302f, 0f),
		new Vector3 (26.19f, -19.29f, 0f)
	};
	public Vector3[] bottomRight = new Vector3[] {
		new Vector3 (26.19f, -19.29f, 0f),
		new Vector3 (25.602f, -10.302f, 0f),
		new Vector3 (25.854f, -14.154f, 0f),
		new Vector3 (25.35f, -6.45f, 0f),
		new Vector3 (25.35f, -6.45f, 0f),
		new Vector3 (27.57f, -3.79f, 0f),
		new Vector3 (24.51f, 6.39f, 0f),
		new Vector3 (18.09f, 3.25f, 0f),
		new Vector3 (18.09f, 3.25f, 0f),
		new Vector3 (13.63f, 3.61f, 0f),
		new Vector3 (8.95f, 4.26f, 0f),
		new Vector3 (5.26f, 4.5f, 0f),
		new Vector3 (5.26f, 4.5f, 0f),
		new Vector3 (2.13f, 1.77f, 0f),
		new Vector3 (-7.57f, 5.75f, 0f),
		new Vector3 (-2.4f, 10.15f, 0f),
		new Vector3 (-2.4f, 10.15f, 0f),
		new Vector3 (1.38f, 12.94f, 0f),
		new Vector3 (-1.881999f, 19.89f, 0f),
		new Vector3 (-8.65f, 19.8f, 0f),
		new Vector3 (-8.65f, 19.8f, 0f),
		new Vector3 (-13.71f, 19.76f, 0f),
		new Vector3 (-19.93f, 19.65f, 0f),
		new Vector3 (-23.08f, 19.86f, 0f),
		new Vector3 (-23.08f, 19.86f, 0f),
		new Vector3 (-23.14f, 16.78f, 0f),
		new Vector3 (-23.36f, 13.1f, 0f),
		new Vector3 (-23.41f, 9.85f, 0f),
		new Vector3 (-23.41f, 9.85f, 0f),
		new Vector3 (-23.72f, 5.58f, 0f),
		new Vector3 (-24.31f, -3.18f, 0f),
		new Vector3 (-24.45f, -7.79f, 0f)
	};
	
	
}
