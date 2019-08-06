using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 runtimeValue;

    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }
    public void OnBeforeSerialize() { }

}
