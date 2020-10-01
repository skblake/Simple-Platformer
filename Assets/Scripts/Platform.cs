using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{ 
    public PlatformManager manager;
    //public string typeName;
    public PlatformManager.Colors type;
    public bool visible = true;
    private SpriteRenderer s_renderer;
    private PlatformEffector2D platformEffector;
    private BoxCollider2D box_collider;
   
    void Start() {
        s_renderer = GetComponent<SpriteRenderer>();
        platformEffector = GetComponent<PlatformEffector2D>();
        box_collider = GetComponent<BoxCollider2D>();
        manager = FindObjectOfType<PlatformManager>();
        manager.addToList(this);
     }

     public void setVisible (bool v) =>
         visible = s_renderer.enabled = platformEffector.enabled = box_collider.enabled = v;

     public void setColor (Color c) => s_renderer.color = c;
}