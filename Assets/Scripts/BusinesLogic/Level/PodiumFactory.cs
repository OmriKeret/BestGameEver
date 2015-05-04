using System;
using UnityEngine;
using System.Collections.Generic;

    public class PodiumFactory
    {
        private GameObject podiumPrefab;
        List<GameObject> activePodiums;

        PodiumModel[][] allLevels;

        public PodiumFactory()
        {
            podiumPrefab = Resources.Load("Podium") as GameObject;
            activePodiums = new List<GameObject>();
            allLevels = new PodiumModel[][]{
                new PodiumModel[]{new PodiumModel(0), new PodiumModel(new Vector3(-3,-7.22f,0))},
                new PodiumModel[]{new PodiumModel(0), new PodiumModel(new Vector3(-9,-7.22f,0))},
                new PodiumModel[]{new PodiumModel(0), new PodiumModel(new Vector3(-16,-7.22f,0))},
                new PodiumModel[]{new PodiumModel(0), new PodiumModel(new Vector3(-22,-7.22f,0))}
            };
        }

        public void SetupNewWave(int i_WaveNumber)
        {
            DestroyLevelPodium();
            //for debug
            if (!(i_WaveNumber < allLevels.Length))
            {
                i_WaveNumber = 0;
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
            }
        }

        public void DestroyLevelPodium()
        {
            foreach (GameObject currentPodium in activePodiums)
            {
                GameObject.Destroy(currentPodium);
            }
        }

        public GameObject createPodium(PodiumModel podiumParams)
        {
            GameObject podium = GameObject.Instantiate(podiumPrefab, podiumParams.location, Quaternion.identity) as GameObject;
            podium.transform.localScale = podiumParams.scale;
            return podium;
        }
    }