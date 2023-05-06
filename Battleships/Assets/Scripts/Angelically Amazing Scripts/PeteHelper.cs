using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A collection of helper methods that will make doing unity stuff easier, such as transformations, formulas, math and audio stuff etc.
/// Licensed by the angelic Peterson 
/// </summary>
public static class PeteHelper
{
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    /// <summary>
    /// every time we use "new waitforseconds" we are adding more garbage memory into unity, which can decrease performance.
    /// this code caches it so its not making new memory and increases performance 
    /// </summary>
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;
        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }




    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;
    /// <summary>
    /// is the mouse pointer over any UI elements? 
    /// </summary>
    public static bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition,  _results);

        return _results.Count > 0;
    }


    /// <summary>
    /// self explanatory. I hope TX/FL politicians dont cancel me
    /// </summary>
    public static void DeleteChildren(this Transform t)
    {
        foreach (Transform child in t)
        {
            Object.Destroy(child.gameObject);
        }
    }


}
