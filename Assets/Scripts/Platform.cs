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
        //setType(typeName);
        manager.addToList(this);
     }

     public void setType(string t) {
         switch (t) {
             case "Ground":
                type = PlatformManager.Colors.Ground;
             break;

             case "Red":
                type = PlatformManager.Colors.Red;
                //setVisible(false);
             break;

             case "Orange":
                type = PlatformManager.Colors.Orange;
             break;

             case "Yellow":
                type = PlatformManager.Colors.Yellow;
             break;
             
             case "Green":
                type = PlatformManager.Colors.Green;
             break;

             case "Blue":
                type = PlatformManager.Colors.Blue;
             break;

             case "Purple":
                type = PlatformManager.Colors.Purple;
             break;

             default:
                Debug.Log("Type spelled incorrectly!");
             break;
         }
     }

     public void setVisible (bool v) =>
         visible = s_renderer.enabled = platformEffector.enabled = box_collider.enabled = v;

     public void setColor (Color c) => s_renderer.color = c;
}