using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InputManagerChemistry : MonoBehaviour {

    public Transform anchor;        // get the transform of the OculusGo Controller device
    public GameObject indicatorObj; // get the object to use to indicate the proposed teleportation spot
    public GameObject player;
    public static UnityAction onTriggerDown = null;
    public static UnityAction onTouchpadDown = null;
    public static UnityAction onBackDown = null;
    private Vector2 _touchValue;    // touchpad value to indicate direction
	public GameObject prefabProton;
	public GameObject prefabElectron;
    public float xBoundsLeft;
    public float xBoundsRight;
    public float yBoundsForward;
    public float yBoundsBackward;
    public float zBoundsUp;
    public float zBoundsDown;
    private int currentSeed = 42;
    private void Awake()
    {
        InputManagerChemistry.onTriggerDown += TriggerDown;
        InputManagerChemistry.onTouchpadDown += TouchpadDown;
        InputManagerChemistry.onBackDown += BackDown;
    }
    private void OnDestroy()
    {
        InputManagerChemistry.onTriggerDown -= TriggerDown;
        InputManagerChemistry.onTouchpadDown -= TouchpadDown;
        InputManagerChemistry.onBackDown -= BackDown;
    }
    // Use this for initialization
    void Start () {
        indicatorObj.SetActive(false);  // indicator is invisible unless the pointer intersects the ground
        ParticleObject.xBoundsLeft = xBoundsLeft;
        ParticleObject.xBoundsRight = xBoundsRight;
        ParticleObject.yBoundsBackward = yBoundsBackward;
        ParticleObject.yBoundsForward = yBoundsForward;
        ParticleObject.zBoundsDown = zBoundsDown;
        ParticleObject.zBoundsUp = zBoundsUp;
    }
	
	// Update is called once per frame
	void Update () {

        // check for user input: primary trigger 
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (onTriggerDown != null)
                onTriggerDown();
        }

        // check for user input: primary touchpad
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (onTouchpadDown != null) { 
                // touchpad has been pressed save value in local variable and execute function
                _touchValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
                onTouchpadDown();
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            if (onBackDown != null)
                onBackDown();
        }
    }

    // function to call when user pulls trigger
    private void TriggerDown()
    {
        currentSeed += 1;
        UnityEngine.Random.InitState(currentSeed);
        GameObject myMesh;
        if (UnityEngine.Random.value > 0.5f){
            myMesh = GameObject.Instantiate(prefabProton, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		}
		else{
            myMesh = GameObject.Instantiate(prefabElectron, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		}
        Debug.Log("Object instantiated");

        CreatureManagerChemistry.AddParticle(myMesh);
    }

    private void TouchpadDown()
    {
        bool direction = (_touchValue.x > 0.0f) ? true : false;
		Debug.Log("direction: " + direction);
		CreatureManagerChemistry.setMagneticField(direction);		
    }

    private void BackDown(){
        CreatureManagerChemistry.deleteEverything();
    }
}