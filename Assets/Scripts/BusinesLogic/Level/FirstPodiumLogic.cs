using UnityEngine;
using System.Collections;
    class FirstPodiumLogic : PodiumLogic
    {
        public void initPodium(GameObject i_Podium)
        {
            podium = i_Podium;
            originalLocation = podium.transform.position;
            downLocation = originalLocation - new Vector3(0, 20, 0);
        }
    }
