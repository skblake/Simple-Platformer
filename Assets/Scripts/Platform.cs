using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Platform : MonoBehaviour
{ 
    public PlatformManager manager;
    //public string typeName;
    public PlatformManager.Colors type;
    public bool visible = true;
    void Start() {
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

     public void setVisible (bool v) => visible = v;
}
