using System;
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
    public Image targetingIcon;
    public Image targetGoBackTo;
    public TextMeshProUGUI HPText;

    [Header("Combat")]
    public int HP;
    public int MaxHP;
    public int shield;
    public int MaxShield;
    private bool invincible;
    public float invincibleTime;
    private float timeSpentInvincible;
    public Transform targetTransform;
    public Transform barrelTransform;
    public int dmg;
    public float timeBetweenFire;
    public float timeSinceLastFire;
    public float shootRange;
    public Transform enemyTargetSpot;
    public float aimingSize;
    public float aimingDist;

    public EnemyShip target;

    private int collisions = 0;

    [Header("Upgrades")]
    public int shieldRank;
    public int[] shieldRankValues;
    public int engineRank;
    public int[] engineRankValues;
    public int gunRank;
    public int[] gunDamageValues;
    public float[] gunDelayValues;
    public int radarRank;
    public enum UpgradeableComponent { shield, engine, gun, radar };
    //ShipUpgradeManager shipUpgradeManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HPText.text = HP.ToString() + "/" + MaxHP.ToString();
        //shipUpgradeManager = FindObjectOfType<ShipUpgradeManager>();
    }

    private void Update()
    {
        //Control Thrust with Scroll Wheel
        currentThrust += Input.mouseScrollDelta.y * (engineRank + 1);
        if (currentThrust < minThrust) currentThrust = minThrust;
        else if (currentThrust > maxThrust) currentThrust = maxThrust;
        thrustText.text = currentThrust.ToString() + " km/h";

        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDMG(1);
        }

        if(target != null)
        {
            targetingIcon.rectTransform.position = camera.WorldToScreenPoint(target.transform.position);
            if (Input.GetButton("Fire"))
            {
                target.Die();
            }
        }
        else
        {
            targetingIcon.rectTransform.position = targetGoBackTo.rectTransform.position;
        }
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
        target = null;
        if(Physics.SphereCast(camera.transform.position, aimingSize, camera.transform.forward, out sphereHit, aimingDist, LayerMask.GetMask("Enemy")))
        {
            if (sphereHit.collider.gameObject.CompareTag("EnemyShip"))
            {
                target = sphereHit.collider.gameObject.GetComponent<EnemyShip>();
                //target.Die();
            }
        }

        //Ship Go Forward
        rb.velocity = (transform.forward * currentThrust) + (transform.up * elevation);

    }

    public void TakeDMG(int dmg)
    {
        HP -= dmg;
        HPText.text = HP.ToString() + "/" + MaxHP.ToString();
        if (HP <= 0)
        {
            HP = 0;
            HPText.text = HP.ToString() + "/" + MaxHP.ToString();
            Destroy(gameObject);
        }
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

    public void Upgrade(UpgradeableComponent toUpgrade)
    {
        switch (toUpgrade)
        {
            case UpgradeableComponent.shield:
                shieldRank++;
                MaxShield = shieldRankValues[shieldRank];
                shield = shieldRankValues[shieldRank];
                break;
            case UpgradeableComponent.engine:
                engineRank++;
                maxThrust = engineRankValues[engineRank];
                minThrust = -engineRankValues[engineRank]/2;
                break;
            case UpgradeableComponent.gun:
                gunRank++;
                dmg = gunDamageValues[gunRank];
                timeBetweenFire = gunDelayValues[gunRank];
                break;
            case UpgradeableComponent.radar:
                radarRank++;
                break;
        }
    }

}
