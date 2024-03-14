using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventChannel eventChannel;

    private void OnEnable()
    {
        eventChannel.OnEventRaised.AddListener(OnEventRaised);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
