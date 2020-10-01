﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public ArrayList platforms = new ArrayList();
    public GameObject player;
    public GameObject cam_obj;
    public void addToList(Platform p) => platforms.Add(p);

    /* An ENUMERATION TYPE (or ENUM TYPE) is a value type defined by a set of 
     * named constants of the underlying integral numeric type. To define an 
     * enumeration type, use the enum keyword and specify the names of enum 
     * members:
     */     public enum Colors {
                Ground,     //0
                Red,        //1
                Orange,     //2
                Yellow,     //3
                Green,      //4
                Blue,       //5
                Purple      //6
   }/* By default, the associated constant values of enum members are of type int;
     * they start with zero and increase by one following the definition text
     * order. You can explicitly specify any other integral numeric type as an 
     * underlying type of an enumeration type (long, ushort). You can also 
     * explicitly specify the associated content values.
     */

    private Color brightRed, darkRed, brightOrange, redOrange, yellowOrange, darkOrange,
        brightYellow, darkYellow;

    public Colors lens = Colors.Red;
    private Color[,] palettes = new Color[7, 9];
    private Camera cam;
    void Start()
    {
        //////// BASIC COLORS ////////
         brightRed = makeColor(221, 33, 49);
         darkRed = makeColor(52, 7, 11);
         brightOrange = makeColor(242, 115, 58);
         redOrange = makeColor(230, 61, 29);
         yellowOrange = makeColor (242, 189, 88);
         //darkOrange;
         brightYellow = makeColor(241, 215, 88);
         darkYellow = makeColor(199, 144, 11);

        cam = cam_obj.GetComponent<Camera>();
        changeColor(lens);
        makePalettes();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
            changeColor(Colors.Red); //RED
        } else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha2)) {
            changeColor(Colors.Yellow); //YELLOW
        }
    }
    
    private void changeColor(Colors c) {
        if (lens != c) {
            Colors oldLens = lens;
            lens = c;
            Debug.Log(lens);
            foreach (Platform p in platforms) {
                if (p.type.CompareTo(oldLens) == 0) {
                    p.setVisible(true);
                } else if (p.type.CompareTo(c) == 0) {
                    p.setVisible(false);
                }
                p.setColor(palettes[(int)lens, (int)p.type]);
            }
            setColor(player.GetComponent<SpriteRenderer>(), palettes[(int)oldLens, 7]);
            cam.backgroundColor = palettes[(int)lens, 8];
            //cam.backgroundColor = Color.Lerp(cam.backgroundColor, palettes[(int)lens, 8], 1);
        }
    }

    private void makePalettes () {
        /////////////// RED ////////////////
        palettes[1, 0] = darkRed; //ground
        palettes[1, 1] = makeColor(0, 0, 0); //red
        palettes[1, 2] = redOrange; //orange
        palettes[1, 3] = brightOrange; //yellow
        palettes[1, 4] = makeColor(0, 0, 0); //green
        palettes[1, 5] = makeColor(0, 0, 0); //blue
        palettes[1, 6] = makeColor(0, 0, 0); //purple
        palettes[1, 7] = brightRed; //player
        palettes[1, 8] = brightRed; //background

        ////////////// ORANGE ///////////////
        palettes[2, 0] = makeColor(180, 130, 9); //ground
        palettes[2, 1] = redOrange; //red
        palettes[2, 2] = makeColor(0, 0, 0); //orange
        palettes[2, 3] = yellowOrange; //yellow
        palettes[2, 4] = makeColor(0, 0, 0); //green
        palettes[2, 5] = makeColor(0, 0, 0); //blue
        palettes[2, 6] = makeColor(0, 0, 0); //purple
        palettes[2, 7] = brightOrange; //player
        palettes[2, 8] = brightOrange; //background

        ////////////// YELLOW ///////////////
        palettes[3, 0] = darkYellow; //ground
        palettes[3, 1] = brightOrange; //red
        palettes[3, 2] = yellowOrange; //orange
        palettes[3, 3] = makeColor(0, 0, 0); //yellow
        palettes[3, 4] = makeColor(0, 0, 0); //green
        palettes[3, 5] = makeColor(0, 0, 0); //blue
        palettes[3, 6] = makeColor(0, 0, 0); //purple
        palettes[3, 7] = brightYellow; //player
        palettes[3, 8] = brightYellow; //background
    }
    public void setColor (SpriteRenderer sr, Color c) => sr.color = c;

    public Color makeColor (float r, float g, float b) => new Color (r/255, g/255, b/255);

    public Colors typeSet (string t) {
        switch (t) {
             case "Ground":
                return Colors.Ground;
                //break;

             case "Red":
                return Colors.Red;
                //break;

             case "Orange":
                return Colors.Orange;
                //break;

             case "Yellow":
                return Colors.Yellow;
                //break;
             
             case "Green":
                return Colors.Green;
                //break;

             case "Blue":
                return Colors.Blue;
                //break;

             case "Purple":
                return Colors.Purple;
                //break;

             default:
                Debug.Log("Type spelled incorrectly!");
                return Colors.Ground;
        }
    }
}