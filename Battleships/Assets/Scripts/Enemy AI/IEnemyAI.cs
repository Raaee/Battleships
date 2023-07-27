using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IEnemyAI :MonoBehaviour
{
    public abstract Vector2 DetermineNextLocation();
}
