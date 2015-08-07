using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventListener : MonoBehaviour {

    public delegate void VoidEvent();
    public Dictionary<EventTypes, VoidEvent> Listener;
    #region Events definition
    public static event VoidEvent onTest;
    public static event VoidEvent onWaveOver;
    #endregion

    void Awake()
    {
        Listener = new Dictionary<EventTypes, VoidEvent>();
        #region Add events to the Listener
        Listener.Add(EventTypes.TestEvent, onTest);
        Listener.Add(EventTypes.WaveOver, onWaveOver);
        #endregion
        
    }

}
