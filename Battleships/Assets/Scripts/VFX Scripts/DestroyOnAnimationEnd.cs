using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour  {


    public void DestroyParent() {
        var c =FindObjectOfType<Canvas>();
        GameObject parent = gameObject.transform.parent.gameObject;
      
        Debug.Log("right here doofus", gameObject);
        //Debug.Break();
        //Destroy(parent);
    }
}
