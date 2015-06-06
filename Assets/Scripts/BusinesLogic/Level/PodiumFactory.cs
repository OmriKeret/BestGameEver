﻿using System;
using UnityEngine;
using System.Collections.Generic;

    public class PodiumFactory
    {
        private GameObject podiumPrefab;
        List<GameObject> activePodiums;
        private readonly Vector3 TopLeft = new Vector3(-10.99f, 0f, 0);
        private readonly Vector3 BottomLeft = new Vector3(-10.99f, -7.22f, 0);

        PodiumModel[][] allLevels;

        public PodiumFactory()
        {
            podiumPrefab = Resources.Load("Podium") as GameObject;
            activePodiums = new List<GameObject>();
            allLevels = new PodiumModel[][]{
                new PodiumModel[]{new PodiumModel(0)},
                new PodiumModel[]{new PodiumModel(PodiumPaths.NotMoveing)},
                new PodiumModel[]{new PodiumModel(0)},
                new PodiumModel[]{new PodiumModel(0)},
                new PodiumModel[]{new PodiumModel(0)},
                new PodiumModel[]{new PodiumModel(new Vector3(-10.99f, -7.22f, 0)),new PodiumModel(new Vector3(10.99f, 7.22f, 0))},
                new PodiumModel[]{new PodiumModel(0)},
                new PodiumModel[]{new PodiumModel(new Vector3(-10.99f, -7.22f, 0)),new PodiumModel(new Vector3(10.99f, 7.22f, 0))},
                new PodiumModel[]{new PodiumModel(new Vector3(-10.99f, 7.22f, 0)),new PodiumModel(new Vector3(10.99f, -7.22f, 0))},
                new PodiumModel[]{new PodiumModel(new Vector3(-10.99f, -7.22f, 0)),new PodiumModel(new Vector3(10.99f, 7.22f, 0))},
                new PodiumModel[]{new PodiumModel(new Vector3(-10.99f, 7.22f, 0)),new PodiumModel(new Vector3(10.99f, 7.22f, 0)),new PodiumModel(0)},
            };
        }

        public void SetupNewWave(int i_WaveNumber)
        {
            DestroyLevelPodium();
            //for debug
            if (!(i_WaveNumber < allLevels.Length))
            {
                i_WaveNumber = allLevels.Length-1;
            }
            //end debug
            InitLevelPodium(allLevels[i_WaveNumber]);
        }


        public void InitLevelPodium(PodiumModel[] i_podiumToInit)
        {
            foreach (PodiumModel currentPodiumStats in i_podiumToInit)
            {
                GameObject currentPodium = GameObject.Instantiate(podiumPrefab, currentPodiumStats.location, Quaternion.identity) as GameObject;
                currentPodium.transform.localScale = currentPodiumStats.scale;
                activePodiums.Add(currentPodium);
                currentPodium.GetComponent<PodiumLogic>().Move(currentPodiumStats.path);
            }
        }

        public void DestroyLevelPodium()
        {
            foreach (GameObject currentPodium in activePodiums)
            {
                if (currentPodium != null)
                {
                    currentPodium.GetComponent<PodiumLogic>().downForGood();
                }
            }
        }

        public GameObject createPodium(PodiumModel podiumParams)
        {
            GameObject podium = GameObject.Instantiate(podiumPrefab, podiumParams.location, Quaternion.identity) as GameObject;
            podium.transform.localScale = podiumParams.scale;
            return podium;
        }
    }