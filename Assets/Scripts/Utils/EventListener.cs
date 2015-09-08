using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventListener : MonoBehaviour {


    private static EventListener _instance;

    public static EventListener instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EventListener>();
            }
            return _instance;
        }
    }

    public delegate void GenericEvent(params System.Object[] obj);
    public Dictionary<EventTypes, GenericEvent> Listener;
    #region Events definition
    public event GenericEvent onTest;
    public event GenericEvent onWaveOver;
    public event GenericEvent onGameOver;
    public event GenericEvent onFinishedMission;
    public event GenericEvent onPlayerGotHit;
    #endregion

    void Awake()
    {

        Listener = new Dictionary<EventTypes, GenericEvent>();
        #region Add events to the Listener
        Listener.Add(EventTypes.TestEvent, onTest);
        Listener.Add(EventTypes.WaveOver, onWaveOver);
        Listener.Add(EventTypes.GameOver, onGameOver);
        Listener.Add(EventTypes.FinishedMission, onFinishedMission);
        Listener.Add(EventTypes.PlayerGotHit, onPlayerGotHit);
        #endregion
        
    }

}
