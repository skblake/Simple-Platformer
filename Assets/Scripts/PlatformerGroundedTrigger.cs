using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGroundedTrigger : MonoBehaviour
{
    public PlatformerCharacter myCharacter; 
        // Assign in the Inspector. This connects this script to 
        // PlatformerCharacter.cs and allows it to access public variables in that
        // class. 

    // Runs every frame there is an object inside this trigger
    void OnTriggerStay2D (Collider2D activator) {
        myCharacter.isGrounded = true;
    }
    
    // Runs on the first frame when an object leaves the trigger
    void OnTriggerExit2D (Collider2D activator) {
        myCharacter.isGrounded = false;
    }
}
