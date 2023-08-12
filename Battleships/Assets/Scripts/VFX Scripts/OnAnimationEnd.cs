using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationEnd : MonoBehaviour  {

    [SerializeField] private Animation ENDAnimation;
    public void DestroyParent() {
        Destroy(this.gameObject);
    }
    public void StartENDAnimation() {
       // ENDAnimation.Play();
    }
}
