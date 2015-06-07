using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PodiumFactory
    {
        private GameObject podiumPrefab;
        List<GameObject> activePodiums;
        private readonly Vector3 Mid = new Vector3(0f, -17.22f, 0);
        private readonly Vector3 TopLeft = new Vector3(-10.99f, -10f, 0);
        private readonly Vector3 BottomLeft = new Vector3(-10.99f, -17.22f, 0);
        private readonly Vector3 TopRight = new Vector3(10.99f, -10f, 0);
        private readonly Vector3 BottomRight = new Vector3(10.99f, -17.22f, 0);

        private const int Put2Podiums = 10;
        private const int Put3Podiums = 15;
        

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

        public void SetupNewWave(WaveLogic i_Wave)
        {
            DestroyLevelPodium();
            int[] sides = {0, 0, -1};//-1 since the "End" is always in the right
            foreach (EnemyLocation location in i_Wave.Locations)
            {
                if ((int) location < 4)
                {
                    sides[2]++;
                }
                else if ((int)location>4)
                {
                    sides[0]++;
                }
                else
                {
                    sides[1]++;
                }
            }

            List<PodiumModel> podiumsToInit = new List<PodiumModel>();

            int max = MathUtils.Max(sides);
            if (sides[0] == sides[2] || sides[1] == max)
            {
                podiumsToInit.Add(new PodiumModel(Mid));   
            }
            else if (sides[0] == max)
            {
                podiumsToInit.Add(new PodiumModel(TopLeft));
                if (MathUtils.SumOfArray(sides) >= Put2Podiums)
                {
                    podiumsToInit.Add(new PodiumModel(BottomRight));
                }
            }
            else
            {
                podiumsToInit.Add(new PodiumModel(TopRight));
                if (MathUtils.SumOfArray(sides) >= Put2Podiums)
                {
                    podiumsToInit.Add(new PodiumModel(BottomLeft));
                }
            }

            InitLevelPodium(podiumsToInit.ToArray());
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