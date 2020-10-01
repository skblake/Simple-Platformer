using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public ArrayList platforms = new ArrayList();
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
    void Start()
    {
        changeColor(lens);
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
            }
        }
    }

    private Color[] getPalette(Colors c) {
        return new Color[5];
    }
}