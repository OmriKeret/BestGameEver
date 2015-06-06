using System;
using UnityEngine;


    public class StupidPaths
    {
	public Vector3[] topRight = new Vector3[] {

		new Vector3 (SceneStats.RightEdge, 18f), 
                    new Vector3 (21.5f, 9f),
                    new Vector3 (16f, 6f),
                    new Vector3 (8.5f, 10f),
                    new Vector3 (8.5f, 10f),
                    new Vector3 (-5f, 16.1f), 
                    new Vector3 (-15f, 8.1f),
			new Vector3 (SceneStats.LeftEdge, 0)
	};
       
            public Vector3[] topLeft = new Vector3[]
       {
                    new Vector3(SceneStats.LeftEdge,18f), 
                    new Vector3(-21.5f,9f),
                    new Vector3(-16f,6f),
                    new Vector3(-8.5f,10f),
                    new Vector3(-8.5f,10f),
                    new Vector3(5f,16.1f), 
                    new Vector3(15f,8.1f), 
                    new Vector3(SceneStats.RightEdge,0)
                };
        public Vector3[] topMid = new Vector3[]
       {
                    SceneStats.TopMid, 
                    new Vector3(0.866f*14f+2,14f),
                    new Vector3(7.75f+2,9.75f),
                    new Vector3(0.866f*4f+2,4f),
                    new Vector3(0.866f*4f+2,4f),
                    new Vector3(-7.75f-4,-1f), 
                    new Vector3(0.866f*-9,-5f), 
                    new Vector3(0,SceneStats.BottomEdge)
                };
        public Vector3[] midLeft = new Vector3[]
       {
                    SceneStats.MidLeft, 
                    new Vector3(-23f,9.81f), 
                    new Vector3(-20.9f,19.1f), 
                    new Vector3(-16.4f,10.8f), 
                    new Vector3(-16.4f,10.8f),  
                    new Vector3(-10.2f,-2f), 
                    new Vector3(-1.5f,-8.4f), 
                    new Vector3(6.3f,-2f), 
                    new Vector3(6.3f,-2f), 
                    new Vector3(11f,9.8f), 
                    new Vector3(16.1f,10.3f), 
                    SceneStats.MidRight
                };
        public Vector3[] midRight = new Vector3[]
       {
                    SceneStats.MidRight, 
                    new Vector3(23f,9.81f), 
                    new Vector3(20.9f,19.1f), 
                    new Vector3(16.4f,10.8f), 
                    new Vector3(16.4f,10.8f),  
                    new Vector3(10.2f,-2f), 
                    new Vector3(1.5f,-8.4f), 
                    new Vector3(-6.3f,-2f), 
                    new Vector3(-6.3f,-2f), 
                    new Vector3(-11f,9.8f), 
                    new Vector3(-16.1f,10.3f), 
                    SceneStats.MidLeft
                };

	public Vector3[] bottomLeft = new Vector3[]{ 
		new Vector3(-27.85f,-9.24f,0f), 
		new Vector3(-21.75f,-7.06f,0f), 
		new Vector3(-17.92f,-5.98f,0f), 
		new Vector3(-15.62f,-5.22f,0f), 
		new Vector3(-15.62f,-5.22f,0f), 
		new Vector3(-10.21f,-4.82f,0f), 
		new Vector3(-6.84f,1.76f,0f), 
		new Vector3(-8.85f,4.85f,0f), 
		new Vector3(-8.85f,4.85f,0f), 
		new Vector3(-12.33f,10.75f,0f), 
		new Vector3(-4.62f,16.54f,0f), 
		new Vector3(0.63f,13.76f,0f), 
		new Vector3(0.63f,13.76f,0f), 
		new Vector3(5.06f,16.81f,0f), 
		new Vector3(13.11f,15.43f,0f), 
		new Vector3(13.69f,11.13f,0f), 
		new Vector3(13.69f,11.13f,0f), 
		new Vector3(17.15f,7.91f,0f), 
		new Vector3(21.05f,3.41f,0f), 
		new Vector3(23.85f,0.1f,0f), 
		new Vector3(23.85f,0.1f,0f), 
		new Vector3(27.87f,-4.586f,0f), 
		new Vector3(25.86f,-2.243f,0f), 
		new Vector3(30.55f,-7.71f,0f) 
	};
	public Vector3[] bottomRight = new Vector3[]{ 
		new Vector3(30.55f,-7.71f,0f), 
		new Vector3(25.86f,-2.243f,0f), 
		new Vector3(27.87f,-4.586f,0f), 
		new Vector3(23.85f,0.1f,0f), 
		new Vector3(23.85f,0.1f,0f), 
		new Vector3(21.05f,3.41f,0f), 
		new Vector3(17.15f,7.91f,0f), 
		new Vector3(13.69f,11.13f,0f), 
		new Vector3(13.69f,11.13f,0f), 
		new Vector3(13.11f,15.43f,0f), 
		new Vector3(5.06f,16.81f,0f), 
		new Vector3(0.63f,13.76f,0f), 
		new Vector3(0.63f,13.76f,0f), 
		new Vector3(-4.62f,16.54f,0f), 
		new Vector3(-12.33f,10.75f,0f), 
		new Vector3(-8.85f,4.85f,0f), 
		new Vector3(-8.85f,4.85f,0f), 
		new Vector3(-6.84f,1.76f,0f), 
		new Vector3(-10.21f,-4.82f,0f), 
		new Vector3(-15.62f,-5.22f,0f), 
		new Vector3(-15.62f,-5.22f,0f), 
		new Vector3(-17.92f,-5.98f,0f), 
		new Vector3(-21.75f,-7.06f,0f), 
		new Vector3(-27.85f,-9.24f,0f) 
	};


    }
