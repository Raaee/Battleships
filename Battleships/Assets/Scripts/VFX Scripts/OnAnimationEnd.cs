using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationEnd : MonoBehaviour  {

    [SerializeField] private Animation ENDAnimation;
    public bool isTurnPrefab = false;
    public void DestroyParent() {
        Destroy(this.gameObject);
    }
    public void StartENDAnimation() {
       // ENDAnimation.Play();
    }

    private void Start()
    {
        if(isTurnPrefab)
        {

        }
    }
}
