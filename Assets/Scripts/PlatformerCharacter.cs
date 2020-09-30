using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE: Character controller with basic movement and jumping.
// USAGE: Put this on a player object with a 2D collider and Rigidbody2D.
public class PlatformerCharacter : MonoBehaviour
{
    public bool isGrounded; // Set by PlatformerGroundedTrigger.cs
    public float moveSpeed = 15f;
    public float jumpPower = 20f;
        // This is the jump force. Remember that with physics, it's better to err
        // on the side of larger numbers.
    Rigidbody2D myRigidBody; // Stores a reference to the Rigidbody.
    float inputHorizontal;
    bool isJumping = false;
        // We're storing these movement variables here so they can be used by both 
        // Update() and FixedUpdate(). 
    void Start() {

        myRigidBody = GetComponent<Rigidbody2D>();
        // This line caches a reference to the Rigidbody component. You'll see this 
        // pattern often in example code online. We're going to be using Rigidbody
        // a lot in a character controller, and it is both faster and more 
        // CPU-efficient to access this in a variable instead of typing
        // GetComponent<Rigidbody2D>() over and over. 
    }

    //////////////////////////// UPDATE FUNCTIONS ////////////////////////////////
    // In the past, we've used just Update(), but in this class we need two     //
    // update functions: Update() and FixedUpdate().                            //
    // Update() -                            FixedUpdate() -                    //
    //  * Fires every RENDERING frame         * Fires every PHYSICS frame, at a //
    //  * Processes input                       fixed timestep                  //
    //  * Should be used for most things      * All physics code should go here //
    // When you're working with physics in unity, you MUST separate the two     //
    // functions or you'll get weird glitches.                                  //
    //////////////////////////////////////////////////////////////////////////////

    void Update() { // Stores player inputs for use in FixedUpdate(). 
        ///////////////////////////// INPUT CODE /////////////////////////////////
        
        ////// LEFT & RIGHT //////
        inputHorizontal = Input.GetAxis("Horizontal");
            // RETURNS -1f: left  |  0f: no movement  |  1f: right              
            // Remember that Input.GetAxis() uses a VIRTUAL JOYSTICK. This means 
            // you don't have to change your code for different input methods
            // (controller, WASD, arrow keys, etc), the virtual joystick manages 
            // different input methods. You can see the virtual joysticks in the 
            // Unity editor under Edit > Project Settings > Input Manager. The 
            // Horizontal input is at the top of the list. If you expand it, you
            // can see the button mappings and other properties of the joystick.
            // This lets you set up basic controller support, but just note that 
            // this stuff is kind of old (circa 2010) and hasn't been updated 
            // much––it's going to be obsolete by next year, if it isn't already.
            // You should probably be using the new input system instead, but 
            // that's a little more complicated, and most online tutorials cover
            // the older input system. You'll also see this kind of input system 
            // in basically every game engine. 

        ///// SPACE TO JUMP //////
        if (Input.GetButtonDown("Jump") && isGrounded)  {
            // You could use GetKey(), but we're using the virtual joystick 
            // instead. If you go back to the Input Manager, you'll see that
            // a ways below Horizontal on the list is an entry for Jump, which 
            // shows what keys the action maps to. 
            isJumping = true;
        }
    }

    void FixedUpdate() { 

        ////////////////////////////// MOVEMENT //////////////////////////////////

        // Previously, we were using AddForce(), which works well for things like
        // cars, but for a player-character it's a lot easier to just set the 
        // velocity directly. 
        myRigidBody.velocity = new Vector2 (
            inputHorizontal * moveSpeed,
                // If we don't multiply inputHorizontal by something, the 
                // character will move super slowly. Declaring a public tuning
                // variable makes it easy to playtest this. 
            myRigidBody.velocity.y
                // We want to preserve the vertical velocity in case we're jumping 
                // or falling, so we'll just pass the existing y velocity back in.
            ); 

        /////////////////////PROPERTIES & VARIABLES IN C# ////////////////////////
        //                                                                      //
        // If you put this line up above:                                       //
        //     >> myRigidBody.velocity.x = inputHorizontal                      //
        // it would throw this error:                                           //
        //     >> Cannot modify the return value of 'Rigidbody2D.velocity'      //
        //        because it is not a variable [Assembly-CSharp]csharp(cs1612). //
        //                                                                      //
        // That's because velocity is a PROPERTY, a combination of a variable   //
        // and a method. A property is a public pair of getter and setter       //
        // methods, usually for accessing or modifying a corresponding private  // 
        // variable or field (called a BACKING FIELD). A lot of C# properties   //
        // just pass the input directly through and are functionally pretty     //
        // much the same as a public variable.                                  //
        //                                                                      //
        // Those might be declared like this:                                   //
        // class Example {                                                      //
        //      private string var; // private field to protect                 //
        //      public string Var { // property to access the field             //
        //          get { return var; }                                         //
        //          set { var = value; } // sets var to whatever is passed in   //
        //      }                                                               //
        // }                                                                    //
        //                                                                      //
        // You could put code between the brackets above––for example, you      //
        // could have the setter throw an error if value is out of bounds, or   // 
        // you could process the input in some way before storing it in var.    //
        // A property has just two methods: get and set. For properties like    //
        // the above that pass the private value straight through, you can use  //
        // shorthand instead to declare them (AUTO-IMPLEMENTED PROPERTIES):     //
        //                                                                      //
        //      public string Var // property                                   //
        //      { get; set; }                                                   //
        //                                                                      //
        // Properties can be read-write (get & set), read-only (get), or        //
        // write-only (set). Here is an example class that declares a property  //
        // as an EXPRESSION-BODIED MEMBER and uses it more functionally:        //
        //                                                                      //
        // public class Person {                                                //
        //      private string _firstName;                                      //
        //      private string _lastName;                                       //
        //                                                                      //
        //      public Person (string first, string last) {                     //
        //          _firstName = first;                                         //
        //          _lastName = last;                                           //
        //      }                                                               //
        //      // Declare property as expression-bodied member                 //
        //      // (one-line method)                                            //
        //      public string Name => $"{_firstName} {_lastName}";              //
        // }                                                                    //
        //                                                                      //
        // public class Example {                                               //
        //      public static void Main() {                                     //
        //                                                                      //
        //          var person = new Person ("Taylor", "Swift");                //
        //          Debug.Log(person.Name);                                     //
        //      }                                                               //
        // }                                                                    //
        // OUTPUT: Taylor Swift                                                 //
        //                                                                      //
        //  ////////////////////EXPRESSION-BODIED MEMBERS/////////////////////  //
        //  // This is a more readable way of writing one-line methods,     //  //
        //  // including properties, constructors, finalizers, and          //  //
        //  // indexers. They consist of a single expression that returns a //  //
        //  // value whose type matches the method's return type or, for    //  //
        //  // methods that return void, perform some operation:            //  //
        //  //                                                              //  //
        //  //  * public override ToString() => $"{fname} {lname}".Trim();  //  //
        //  //  * public void DisplayName() => Console.WriteLine(ToString());/  //
        //  //                                                              //  //
        //  // This one creates a read-only property:                       //  //
        //  //  * private string locationName;                              //  //
        //  //  * public string Name => locationName;                       //  //
        //  //                                                              //  //
        //  // And this one is a constructor:                               //  //
        //  //  * public class Location {                                   //  //
        //  //  *       private string locationName;                        //  //
        //  //  *                                                           //  //
        //  //  *       // class has a single parameter, name, which it     //  //
        //  //  *       // assigns to the Name property                     //  //
        //  //  *       public Location (string name) => Name = name        //  //
        //  //  *                                                           //  //
        //  //  *       //also uses expression-bodied method declarations   //  //
        //  //  *       public string Name {                                //  //
        //  //  *           get => locationName;                            //  //
        //  //  *           set => locationName = value;                    //  //
        //  //  *       }                                                   //  //
        //  //  * }                                                         //  //
        //  //////////////////////////////////////////////////////////////////  //
        //                                                                      //
        //////////////////////////////////////////////////////////////////////////

        ////////////////////////////////// JUMP //////////////////////////////////

        if (isJumping) {
            myRigidBody.velocity = new Vector2 (
                myRigidBody.velocity.x,  // We're passing this directly back in
                                         // because horizontal movement is
                                         // handled above.
                jumpPower);
            isJumping = false;
        }

        // The above code works for jump, but it feels really floaty. There are
        // a few ways you can increase the gravity so that jump feels more 
        // responsive: 
        //      1 - Go to Edit > Project Settings > Physics 2D and change the 
        //          global gravity for your entire project. Robert does not 
        //          recommend doing it this way.
        //      2 - Add something like this in the code: 
        //              else {
        //                  myRigidBody.velocity += Physics2D.gravity;
        //              }
        //          That'll add extra gravity when you're not jumping and pull
        //          you down more––but Robert doesn't recommend this either
        //          because it's a little bit hacky. 
        //      3 - Just modify the Gravity Scale property of the Rigidbody2D 
        //          component. For example, if we want to make gravity 5 times 
        //          stronger, we just type in "5". 
        // Increasing the gravity makes the jump feel snappier but it also means
        // the jump needs more power to reach the same height. 
    }
}