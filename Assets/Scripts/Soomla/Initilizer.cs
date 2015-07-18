using UnityEngine;
using System.Collections;
using Soomla.Store;

public class Initilizer : MonoBehaviour {

    public static Initilizer init;

    void Start()
    {
        if (init == null)
        {
            DontDestroyOnLoad(gameObject);
            SoomlaStore.Initialize(new SoomlaPurhcableItem());
            init = this;
        }
        else if (init != this)
        {
            Destroy(gameObject);
        }
    }
}
