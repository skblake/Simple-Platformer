using System.Collections;
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
    public Colors lens = Colors.Red;
    private Color[,] palettes = new Color[7, 9];
    private Camera cam;
    void Start()
    {
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
        palettes[1, 0] = makeColor(0, 0, 0); //ground
        palettes[1, 1] = makeColor(0, 0, 0); //red
        palettes[1, 2] = makeColor(0, 0, 0); //orange
        palettes[1, 3] = makeColor(241, 215, 88); //yellow
        palettes[1, 4] = makeColor(0, 0, 0); //green
        palettes[1, 5] = makeColor(0, 0, 0); //blue
        palettes[1, 6] = makeColor(0, 0, 0); //purple
        palettes[1, 7] = makeColor(221, 33, 49); //player
        palettes[1, 8] = makeColor(221, 33, 49); //background

        ////////////// YELLOW ///////////////
        palettes[3, 0] = makeColor(0, 0, 0); //ground
        palettes[3, 1] = makeColor(221, 33, 49); //red
        palettes[3, 2] = makeColor(0, 0, 0); //orange
        palettes[3, 3] = makeColor(0, 0, 0); //yellow
        palettes[3, 4] = makeColor(0, 0, 0); //green
        palettes[3, 5] = makeColor(0, 0, 0); //blue
        palettes[3, 6] = makeColor(0, 0, 0); //purple
        palettes[3, 7] = makeColor(241, 215, 88); //player
        palettes[3, 8] = makeColor(241, 215, 88); //background
    }
    public void setColor (SpriteRenderer sr, Color c) => sr.color = c;

    public Color makeColor (float r, float g, float b) => new Color (r/255, g/255, b/255);
}