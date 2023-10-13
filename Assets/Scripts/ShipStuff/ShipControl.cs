using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour
{

    private Rigidbody rb;
    //public GameObject CameraObject;
    //public Renderer visual;

    [Header("FLIGHT NUMBERS")]
    public float minThrust;
    public float maxThrust;
    private float currentThrust;

    public float pitch;
    public float pitchSensitivity;
    public float yaw;
    public float yawSensitivity;
    public float elevation;
    public float elevationSensitivity;

    public float roll;
    public float turnRoll;
    public float turnRollAmount;
    public float rollSensitivity;
    private float currentRoll;

    [Header("UI")]
    public TextMeshProUGUI thrustText;
    public Camera camera;
    public Image targettingIcon;

    [Header("Combat")]
    public int HP;
    public int MaxHP;
    private bool invincible;
    public float invincibleTime;
    private float timeSpentInvincible;
    public Transform targetTransform;
    public Transform barrelTransform;
    public float timeBetweenFire;
    public float timeSinceLastFire;
    public float shootRange;
    public Transform enemyTargetSpot;
    public float aimingSize;
    public float aimingDist;

    private int collisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Control Thrust with Scroll Wheel
        currentThrust += Input.mouseScrollDelta.y * 2;
        if (currentThrust < minThrust) currentThrust = minThrust;
        else if (currentThrust > maxThrust) currentThrust = maxThrust;
        thrustText.text = currentThrust.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Rotate Ship with WS AD and QE
        pitch = Input.GetAxis("Vertical") * pitchSensitivity;
        yaw = Input.GetAxis("Horizontal") * yawSensitivity;
        elevation = Input.GetAxis("Elevation") * elevationSensitivity;
        if (Input.GetButton("Roll"))
        {
            roll = -Input.GetAxis("Roll") * rollSensitivity;
            currentRoll = transform.eulerAngles.z;
        }
        else
        {
            roll = 0;
        }
        transform.Rotate(pitch, yaw, roll);

        RaycastHit sphereHit;
        if(Physics.SphereCast(camera.transform.position, aimingSize, camera.transform.forward, out sphereHit, aimingDist))
        {
            if (sphereHit.collider.gameObject.CompareTag("EnemyShip"))
            {
                EnemyShip target = sphereHit.collider.gameObject.GetComponent<EnemyShip>();
                //target.Die();
            }
        }

        if (Input.GetButton("Fire"))
        {

        }

        //Ship Go Forward
        rb.velocity = (transform.forward * currentThrust) + (transform.up * elevation);

    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions--;
        if(collisions <= 0)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

}
