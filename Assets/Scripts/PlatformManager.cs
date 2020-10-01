using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages platform types and color changes. 
//USAGE: Put on an empty GameObject.
public class PlatformManager : MonoBehaviour
{
    public ArrayList platforms = new ArrayList();
    public GameObject player;
    public GameObject cam_obj;

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

    private Color brightRed, darkRed, brightOrange, redOrange, yellowOrange,
        brightYellow, darkYellow, brightGreen, brightBlue, darkBlue, brightPurple;

    public Colors lens = Colors.Red; //initial bg color
    private Color[,] palettes = new Color[7, 9]; //2D array of colors for each lens
    private Camera cam;
    private bool initializing = true;
    void Start()
    {
        //////// BASIC COLORS ////////
         //purpleRed;
         brightRed = makeColor(221, 33, 49);
         darkRed = makeColor(52, 7, 11);
         brightOrange = makeColor(242, 115, 58);
         redOrange = makeColor(230, 61, 29);
         yellowOrange = makeColor (242, 189, 88);
         brightYellow = makeColor(241, 215, 88);
         darkYellow = makeColor(199, 144, 11);
         //yellowGreen;
         brightGreen = makeColor (127, 220, 109);
         //blueGreen;
         brightBlue = makeColor(73, 183, 249);
         darkBlue = makeColor(10, 50, 91);
         //purpleBlue;
         brightPurple = makeColor(152, 51, 228);

        cam = cam_obj.GetComponent<Camera>();
        //changeColor(lens);
        makePalettes();
    }

    void Update()
    {   
        //Listens for numeric key input
        if (initializing)
            changeColor(lens);
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
            initializing = false;
            changeColor(Colors.Red); //RED
        } else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
            initializing = false;
            changeColor(Colors.Yellow); //YELLOW
        } else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
            initializing = false;
            changeColor(Colors.Blue);
        }
    }
    
    //Changes stage color
    private void changeColor(Colors c) {
        if (lens != c) { //skips the whole method if the color hasn't changed
            Colors oldLens = lens; //save previous color
            lens = c; //update color
            Debug.Log(lens);
            foreach (Platform p in platforms) {
                p.setColor(palettes[(int)lens, (int)p.type]); //set new platform colors
                if (p.type.CompareTo(oldLens) == 0) {
                    p.setVisible(true); //show platforms that were hidden
                } else if (p.type.CompareTo(c) == 0) {
                    p.setVisible(false); //hide platforms that match lens
                }
            }
            setColor(player.GetComponent<SpriteRenderer>(), palettes[(int)oldLens, 7]);
            cam.backgroundColor = palettes[(int)lens, 8];
        }
    }

    //initializes color palette array
    private void makePalettes () {
        /////////////// RED ////////////////
        palettes[1, 0] = darkRed; //ground
        palettes[1, 1] = makeColor(0, 0, 0); //red
        palettes[1, 2] = redOrange; //orange
        palettes[1, 3] = brightOrange; //yellow
        palettes[1, 4] = makeColor(0, 0, 0); //green
        palettes[1, 5] = brightPurple; //blue
        palettes[1, 6] = brightPurple; //purple
        palettes[1, 7] = brightRed; //player
        palettes[1, 8] = brightRed; //background

        /*////////////// ORANGE ///////////////
        palettes[2, 0] = makeColor(180, 130, 9); //ground
        palettes[2, 1] = redOrange; //red
        palettes[2, 2] = makeColor(0, 0, 0); //orange
        palettes[2, 3] = yellowOrange; //yellow
        palettes[2, 4] = makeColor(0, 0, 0); //green
        palettes[2, 5] = makeColor(0, 0, 0); //blue
        palettes[2, 6] = makeColor(0, 0, 0); //purple
        palettes[2, 7] = brightOrange; //player
        palettes[2, 8] = brightOrange; //background*/

        ////////////// YELLOW ///////////////
        palettes[3, 0] = darkYellow; //ground
        palettes[3, 1] = brightOrange; //red
        palettes[3, 2] = yellowOrange; //orange
        palettes[3, 3] = makeColor(0, 0, 0); //yellow
        palettes[3, 4] = brightGreen; //green
        palettes[3, 5] = brightGreen; //blue
        palettes[3, 6] = makeColor(0, 0, 0); //purple
        palettes[3, 7] = brightYellow; //player
        palettes[3, 8] = brightYellow; //background

        /////////////// BLUE ////////////////
        palettes[5, 0] = darkBlue; //ground
        palettes[5, 1] = brightPurple; //red
        palettes[5, 2] = darkBlue; //orange
        palettes[5, 3] = brightGreen; //yellow
        palettes[5, 4] = brightGreen; //green
        palettes[5, 5] = makeColor(0, 0, 0); //blue
        palettes[5, 6] = brightPurple; //purple
        palettes[5, 7] = brightBlue; //player
        palettes[5, 8] = brightBlue; //background
    }
    //setter for spriterenderer colors
    public void setColor (SpriteRenderer sr, Color c) => sr.color = c;

    //called by the Platform class to push a platform to the ArrayList.
    public void addToList(Platform p) => platforms.Add(p);

    //simplifies color initialization
    public Color makeColor (float r, float g, float b) => new Color (r/255, g/255, b/255);

    //converts a string to a Colors enum.
    public Colors setType (string t) {
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