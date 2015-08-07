using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventListener : MonoBehaviour {

    public delegate void VoidEvent();
    public Dictionary<EventTypes, VoidEvent> Listener;
    #region Events definition
    public static event VoidEvent onTest;
    #endregion

    void Awake()
    {
        Listener = new Dictionary<EventTypes, VoidEvent>();
        //Add here all the events
        Listener.Add(EventTypes.TestEvent, onTest);
    }

}
