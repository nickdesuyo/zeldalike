﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalObject : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }

}
