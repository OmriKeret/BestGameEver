using UnityEngine;
using System.Collections;

public interface PhaseEventHandler {

    void handleEvent();
    void next();
    void setNext(PhaseEventHandler next);
    
}
