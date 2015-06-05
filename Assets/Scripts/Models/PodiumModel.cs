using System;
using UnityEngine;

    public struct PodiumModel
    {
        public Vector3 location;
        public Vector3 scale;
        public Vector3[] path;

        //Static podium
        public PodiumModel(Vector3 i_location)
        {
            location = i_location;
            scale = new Vector3(0.45f, 0.45f, 0.66367f);
            path = PodiumPaths.NotMoveing;
        }

        public PodiumModel(Vector3 i_Location, Vector3[] i_Path)
        {
            location = i_Location;
            scale = new Vector3(0.45f, 0.45f, 0.66367f);
            path = i_Path;
        }

        public PodiumModel(Vector3[] i_Path)
        {
            location = new Vector3(1.99f, -7.22f, 0);
            scale = new Vector3(0.45f, 0.45f, 0.66367f);
            path = i_Path;

        }

        
        public PodiumModel(int i)
        {
            location = new Vector3(1.99f,-7.22f,0);
            scale = new Vector3(0.45f, 0.45f, 0.66367f);
            path = PodiumPaths.NotMoveing;
        }
        
    }
