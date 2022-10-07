using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{

    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;


    void InitiateEvent(InteractableStateArgs state)
    {
        switch (state.NewInteractableState)
        {
            case InteractableState.ProximityState:
                proximityEvent.Invoke();    
                break;
            case InteractableState.ContactState:
                contactEvent.Invoke();
                break;
            case InteractableState.ActionState:
                actionEvent.Invoke();
                break;
            default:
                defaultEvent.Invoke();
                break;
        }
        
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
