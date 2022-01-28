using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentData 
{
    public Vector3 position;
    public Quaternion rotation;
    public bool hasShot;

    public MomentData(Vector3 _pos, Quaternion _rot, bool _hasShot)
    {
        position = _pos;
        rotation = _rot;
        hasShot = _hasShot;
    }
}
