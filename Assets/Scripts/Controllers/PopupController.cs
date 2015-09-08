using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupController : MonoBehaviour {

    PopupLogic logic;
    EventListener listener;
    public Sprite MissionStart, MissionEnd;
    Dictionary<PopupType, Sprite> bgType;

    void Awake()
    {
        listener = EventListener.instance;
    }

	// Use this for initialization
	void Start () {
        logic = this.GetComponent<PopupLogic>();
        initBg();
        listener.Listener[EventTypes.FinishedMission] += InvokePopup;
	}

    void initBg()
    {
        bgType = new Dictionary<PopupType, Sprite>();
        bgType.Add(PopupType.StartMission, MissionStart);
        bgType.Add(PopupType.EndMission, MissionEnd);
    }

    public void InvokePopup(params System.Object[] obj)
    {
        if (obj.Length != 2||(!(obj[0] is string)||!(obj[1] is PopupType)))
        {
            Debug.LogError(string.Format("Popup gets 2 parameters - messege,PopupType. Got {0}, {1}",obj[0],obj[1]));
            return;
        }

        InvokePopup(obj[0].ToString(), (PopupType)obj[1]);
    }

    public void InvokePopup(string messege, PopupType type)
    {
        logic.showPopup(messege, bgType[type]);
    }

    void Update()
    {
        //For testing only
        if (Input.GetKeyDown(KeyCode.A))
        {
            listener.Listener[EventTypes.FinishedMission].Invoke("Scratch your balls", PopupType.StartMission);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            listener.Listener[EventTypes.FinishedMission].Invoke("Scratch your balls", PopupType.EndMission);
        }
    }
	
}
