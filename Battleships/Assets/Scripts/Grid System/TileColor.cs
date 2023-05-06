using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour
{

    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private GameObject highlight; 
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(bool isOffset)
    {
        sr.color = isOffset ? offsetColor : baseColor;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
