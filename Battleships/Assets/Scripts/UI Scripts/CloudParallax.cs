using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudParallax : MonoBehaviour
{
    private float speed = 6f;
    private float offscreen = -66f;
    private Vector3 originalPos;
    private void Start()
    {
        originalPos = transform.position;
    }


    private void Update()
    {
        //amount to move cloud
       var amtToMove = Time.deltaTime * speed;
        //move enemy
        transform.Translate(Vector3.left * amtToMove);


      
        //respawn with random Y
        if (transform.localPosition.x <= offscreen)
        {

            speed = Random.Range(5f, 7f);
            var newPos = new Vector3(originalPos.x + 5, Random.Range(-10f, 20f), originalPos.z);
            transform.position = newPos;
        }
    }
}
