using UnityEngine;
using System.Collections;

public static class Easing {
    //@t is the current time (or position) of the tween. This can be seconds or frames, steps, seconds, ms, whatever – as long as the unit is the same as is used for the total time.
    //@b is the beginning value of the property.
    //@c is the change between the beginning and destination value of the property.
    //@d is the total time of the tween.
    //recive a value between  0 - 1 and returns a value between 0 - 1
    public static float easeInOut(float currentTime, float totalTweenTime)
    {
        float alpha = 2f;
        float x = currentTime / totalTweenTime;
        if (x > 1)
        {
            x = 1;
        }
        float xPowered = Mathf.Pow(x, alpha);

        return xPowered / (xPowered + Mathf.Pow((1 - x),alpha));
    }

}
