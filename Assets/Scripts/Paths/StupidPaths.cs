using System;
using UnityEngine;


    public class StupidPaths
    {
        public Vector3[] topRight = new Vector3[]
                {
                    SceneStats.TopRight, 
                    new Vector3(22.23f,5.3f), 
                    new Vector3(-22.17f,5.14f), 
                    new Vector3(SceneStats.LeftEdge,0)
                };
        public Vector3[] topLeft = new Vector3[]
       {
                    SceneStats.TopLeft, 
                    new Vector3(-22f,5.3f), 
                    new Vector3(22f,5.5f), 
                    new Vector3(SceneStats.RightEdge,0)
                };
        public Vector3[] topMid = new Vector3[]
       {
                    SceneStats.TopMid, 
                    new Vector3(7.75f,9.75f), 
                    new Vector3(-7.75f,0f), 
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
