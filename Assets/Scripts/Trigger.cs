using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{  
    public string hintColor;
    public GameObject linkedHint;
    public GameObject manager;
    SpriteRenderer hintRenderer;

    private PlatformManager.Colors type;
    private PlatformManager _manager;

    void Start() {
        hintRenderer = linkedHint.GetComponent<SpriteRenderer>();
        _manager = manager.GetComponent<PlatformManager>();
        type = _manager.setType(hintColor);
    }

    void OnTriggerEnter2D(Collider2D activator) {
        if (type.CompareTo(_manager.lens) != 0) {
            hintRenderer.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D activator) {
        hintRenderer.enabled = false;
    }
}