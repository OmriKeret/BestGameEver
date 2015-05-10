using UnityEngine;
using System.Collections;
    class FirstPodiumLogic : PodiumLogic
    {
        public void initPodium(GameObject i_Podium)
        {
            podium = i_Podium;
            originalLocation = podium.transform.position;
            downLocation = originalLocation - new Vector3(0, 40, 0);
        }

        protected void startGoUp()
        {
            goingDown = false;
            firstJump = true;
            secondJump = true;
            LeanTween.cancel(podium, false);
            resetRotation();
            LeanTween.move(podium, originalLocation, timeToComeBackUp).setDelay(timeToWaitDown);
            //Destroy(podium);
        }
    }
