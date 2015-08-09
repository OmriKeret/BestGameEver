using UnityEngine;
using System.Collections;

public class PopupController : MonoBehaviour {

    PopupLogic logic;
    EventListener listener;

    void Awake()
    {
        listener = EventListener.instance;
    }

	// Use this for initialization
	void Start () {
        logic = this.GetComponent<PopupLogic>();
        
        listener.Listener[EventTypes.FinishedMission] += logic.showPopup;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            listener.Listener[EventTypes.FinishedMission].Invoke("Popup motherfucker!");
        }
	}
}
