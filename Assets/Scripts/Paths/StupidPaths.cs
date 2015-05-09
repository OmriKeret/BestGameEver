using System;
using UnityEngine;


    public class StupidPaths
    {
        public Vector3[] topRight = new Vector3[]
                {
                    new Vector3(SceneStats.RightEdge,18f), 
                    new Vector3(23.5f,9f),
                    new Vector3(18f,6f),
                    new Vector3(10.5f,10f),
                    new Vector3(10.5f,10f),
                    new Vector3(-3f,16.1f), 
                    new Vector3(-13f,8.1f), 
                    new Vector3(SceneStats.LeftEdge,0)
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
                    new Vector3(20f,12.81f), 
                    new Vector3(9.9f,17.1f), 
                    new Vector3(SceneStats.RightEdge,0)
                };
        public Vector3[] midRight = new Vector3[]
       {
                    SceneStats.MidRight, 
                    new Vector3(-20f,12.81f), 
                    new Vector3(-9.9f,17.1f), 
                    new Vector3(SceneStats.LeftEdge,0)
                };

        public Vector3[] bottomLeft = new Vector3[]
       {
                    SceneStats.BottomLeft, 
                    new Vector3(SceneStats.LeftEdge+0.25f*SceneStats.Width,SceneStats.TopEdge), 
                    new Vector3(SceneStats.LeftEdge+0.5f*SceneStats.Width,SceneStats.BottomEdge), 
                    SceneStats.MidRight
                };
        public Vector3[] bottomRight = new Vector3[]
       {
                    SceneStats.BottomRight, 
                    new Vector3(SceneStats.LeftEdge+0.75f*SceneStats.Width,SceneStats.TopEdge), 
                    new Vector3(SceneStats.LeftEdge+0.5f*SceneStats.Width,SceneStats.BottomEdge), 
                    SceneStats.MidLeft
                };


    }
