using UnityEngine;
using Leap;
using System.Collections;

public class RaycastHighlighter : MonoBehaviour {

    private Frame frame;
    private Rigidbody grabbedObject;
    private Vector3 prevUserHandPos;
    private Vector3 currUserHandPos;
    private Vector3 hitPosition;

    private GameObject temp;
    private Vector3 tempHitPoint;
    private GameObject[] destructibles;

    public string SceneName;
    public Vector3 ForcePush;
    public Vector3 ForcePull;
    public Vector3 ForceGripLeft;
    public Vector3 ForceGripRight;
    public Vector3 ForceGripUp;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    // Use this for initialization
    void Start ()
    {
        destructibles = GameObject.FindGameObjectsWithTag("MedExplosive");
    }
    
    // Update is called once per frame
    void Update ()
    {
        foreach (GameObject destructible in destructibles)
        {
            (destructible.GetComponent("Halo") as Behaviour).enabled = false;
        }

        UpdatePointerRaycast();

        if (Input.GetKeyUp(KeyCode.R))
        {
            Application.LoadLevel(SceneName);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            Application.LoadLevel("Menu");
        }
    }

    void ForceUnleashed(Vector3 initialPos, Vector3 currentPos)
    {
        /*
        if (Vector3.Distance(currentPos, initialPos) > 30)
        {
            Vector3 forceGrip = (currentPos - initialPos).normalized * 400;
            grabbedObject.isKinematic = false;
            grabbedObject.AddForceAtPosition(transform.TransformDirection(forceGrip), hitPosition, ForceMode.Force);
            grabbedObject = null;
        }
        */

        
        if (currentPos.x < initialPos.x - xOffset)
        {
            grabbedObject.isKinematic = false;
            grabbedObject.AddForceAtPosition(transform.TransformDirection(ForceGripLeft), hitPosition, ForceMode.Force);
            grabbedObject = null;
        }
        else if (currentPos.x > initialPos.x + xOffset)
        {
            grabbedObject.isKinematic = false;
            grabbedObject.AddForceAtPosition(transform.TransformDirection(ForceGripRight), hitPosition, ForceMode.Force);
            grabbedObject = null;
        }
        else if (currentPos.z > initialPos.z + zOffset)
        {
            grabbedObject.isKinematic = false;
            grabbedObject.AddForceAtPosition(transform.TransformDirection(ForcePush), hitPosition, ForceMode.Force);
            grabbedObject = null;
        }
        else if (currentPos.z < initialPos.z - zOffset)
        {
            grabbedObject.isKinematic = false;
            grabbedObject.AddForceAtPosition(transform.TransformDirection(ForcePull), hitPosition, ForceMode.Force);
            grabbedObject = null;
        }
        else if (currentPos.y > initialPos.y + yOffset)
        {
            grabbedObject.isKinematic = false;
            grabbedObject.AddForceAtPosition(transform.TransformDirection(ForceGripUp), hitPosition, ForceMode.Force);
            grabbedObject = null;
        }
    }

    bool isHandOpen()
    {
        return GameObject.Find("right").GetComponent<UnityHand>().hand.Fingers.Count >= 3;
    }

    void UpdatePointerRaycast()
    {
        GameObject rightPalm = GameObject.Find("rightHand");
        Vector3 rightPalmPosition = rightPalm.transform.position;
        UnityHand rightHand = GameObject.Find("right").GetComponent<UnityHand>(); // Get right hand

        Ray pointer = new Ray(rightPalmPosition, transform.TransformDirection(Vector3.forward));

        Debug.DrawRay(rightPalmPosition, transform.TransformDirection(Vector3.forward), Color.cyan);

        RaycastHit hit;

        if (grabbedObject != null)
        {
            currUserHandPos = LeapInputEx.Frame.Hands.Rightmost.PalmPosition.ToUnity();
            ForceUnleashed(prevUserHandPos, currUserHandPos);
        }

        if (Physics.Raycast(pointer, out hit))
        {
            if (hit.collider.tag == "Explosive" || hit.collider.tag == "MedExplosive")
            {
                temp = hit.transform.gameObject;
                tempHitPoint = hit.point;
                Debug.Log("Gotcha " + temp.name);

                (temp.GetComponent("Halo") as Behaviour).enabled = true;

                if (isHandOpen()) // Open the hand, only palm remains
                {
                    Debug.Log("Grabbed " + temp.name);
                    grabbedObject = temp.rigidbody;
                    hitPosition = tempHitPoint;
                    prevUserHandPos = LeapInputEx.Frame.Hands.Rightmost.PalmPosition.ToUnity();
                }
            }
        }
    }

    void UpdatePalmRaycast()
    {
        Hand hand = GameObject.Find("right").GetComponent<UnityHand>().hand; // Get right hand
        Vector3 normal = -hand.PalmNormal.ToUnity();
        Vector3 forward = hand.Direction.ToUnity();

        // Rotation of hands
        //transform.rotation = settings.leapPosOffset.rotation;
        //transform.rotation *= Quaternion.LookRotation(new Vector3(forward.x, forward.y, forward.z), new Vector3(normal.x, normal.y, normal.z));

        Vector3 pivot = GameObject.Find("rightHand").transform.position;

        Debug.DrawRay(pivot, transform.TransformDirection(forward), Color.yellow);
    }
}
