using System;
using UnityEngine;

    public class PodiumFactory
    {
        GameObject podiumPrefab;

        public PodiumFactory()
        {
            podiumPrefab = Resources.Load("Podium") as GameObject;
        }

        public GameObject createPodium(Vector2 i_Location, Vector3 i_Scale)
        {
            GameObject podium = GameObject.Instantiate(podiumPrefab, i_Location, Quaternion.identity) as GameObject;
            podium.transform.localScale = i_Scale;
            return podium;
        }

        public GameObject createPodium(Vector2 i_Location)
        {
            return createPodium(i_Location, new Vector3(0.66367f, 0.66367f, 0.66367f));
        }

        public GameObject createPodium()
        {
            return createPodium(new Vector2(1.99f, -7.22f));
        }
    }