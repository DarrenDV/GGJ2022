using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentData 
{
    public Vector3 position;
    public Quaternion rotation;
    public bool hasShot;
    public bool hasMeleed;

    public MomentData(Vector3 _pos, Quaternion _rot, bool _hasShot, bool _hasMeleed)
    {
        position = _pos;
        rotation = _rot;
        hasShot = _hasShot;
        hasMeleed = _hasMeleed;
    }
}
