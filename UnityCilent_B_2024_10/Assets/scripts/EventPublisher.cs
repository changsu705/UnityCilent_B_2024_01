using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public EventChannel eventChannel;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            eventChannel.RaiseEvent();
        }
    }
}
