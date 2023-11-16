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

    //Turning
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
    public TextMeshProUGUI shieldText;

    [Header("Combat")]
    public int HP;
    public int MaxHP;
    public float shootRange;
    public Transform enemyTargetSpot;
    public float aimingSize;
    public float aimingDist;
    public int shield;
    public int MaxShield;
    public float shieldRegainTime;
    private float currentShieldTime;
    //Not Fully Implemented
    private bool invincible;
    public float invincibleTime;
    private float timeSpentInvincible;
    public Transform targetTransform;
    public Transform barrelTransform;
    public int dmg;
    public float timeBetweenFire;
    public float timeSinceLastFire;

    public EnemyShip target;

    [Header("Shooting Visual")]
    public TrailRenderer bulletTrail;


    //How many things is the ship touching
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        MaxShield = shieldRankValues[shieldRank];
        shield = shieldRankValues[shieldRank];
        shieldText.text = "Shield: " + shield.ToString() + "/" + MaxShield.ToString();

        dmg = gunDamageValues[gunRank];
        timeBetweenFire = gunDelayValues[gunRank];

        HPText.text = "HP: " + HP.ToString() + "/" + MaxHP.ToString();
    }

    private void Update()
    {
        //Control Thrust with Scroll Wheel
        currentThrust += Input.mouseScrollDelta.y * (engineRank + 1);
        if (currentThrust < minThrust) currentThrust = minThrust;
        else if (currentThrust > maxThrust) currentThrust = maxThrust;
        thrustText.text = currentThrust.ToString() + " km/h";

        //Test Damage
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDMG(1);
        }

        if(currentShieldTime > 0)
        {
            currentShieldTime -= Time.deltaTime;
        }
        else
        {
            if(shield < MaxShield)
            {
                RegainShield(1);
                currentShieldTime = shieldRegainTime;
            }
        }

        //WIP testing guns
        //Targetting Icon Control
        if(target != null)
        {
            targetingIcon.rectTransform.position = camera.WorldToScreenPoint(target.transform.position);
        }
        else
        {
            targetingIcon.rectTransform.position = targetGoBackTo.rectTransform.position;
        }
        targetingIcon.fillAmount = 1 - (timeSinceLastFire / timeBetweenFire);
        //Fire Rate and Shooting
        if (Input.GetButton("Fire"))
        {
            if(timeSinceLastFire <= 0)
            {
                timeSinceLastFire = timeBetweenFire;
                TrailRenderer tempTrail = Instantiate(bulletTrail);
                if(target != null)
                {
                    barrelTransform.LookAt(target.transform.position);
                    RaycastHit hit;
                    if (Physics.Raycast(barrelTransform.position, barrelTransform.forward, out hit, shootRange))
                    {
                        StartCoroutine(SpawnTrailHit(barrelTransform, tempTrail, hit));
                    }
                    else
                    {
                        StartCoroutine(SpawnTrailMiss(barrelTransform, tempTrail, shootRange));
                    }
                }
                else
                {
                    barrelTransform.rotation = transform.rotation;
                    StartCoroutine(SpawnTrailMiss(barrelTransform, tempTrail, shootRange));
                }
            }
            else
            {
                timeSinceLastFire -= Time.deltaTime;
            }
        }
        else if(timeSinceLastFire > 0)
        {
            timeSinceLastFire -= Time.deltaTime;
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

        //Targetting System
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

    //The Ship Takes Damage. Still need to implement shield which recharges before hull takes damage which doesn't easily heal
    public void TakeDMG(int dmg)
    {
        currentShieldTime = shieldRegainTime;
        if(shield >= dmg)
        {
            shield -= dmg;
        }
        else
        {
            dmg -= shield;
            shield = 0;
            HP -= dmg;
        }
        shieldText.text = "Shield: " + shield.ToString() + "/" + MaxShield.ToString();
        HPText.text = "HP: " + HP.ToString() + "/" + MaxHP.ToString();
        if (HP <= 0)
        {
            HP = 0;
            HPText.text = "HP: " + HP.ToString() + "/" + MaxHP.ToString();
            Destroy(gameObject);
        }
    }

    public void RegainShield(int amount)
    {
        shield += amount;
        if(shield > MaxShield)
        {
            shield = MaxShield;
        }
        shieldText.text = "Shield: " + shield.ToString() + "/" + MaxShield.ToString();
    }
    
    //Crash
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    //Uncrash
    private void OnCollisionExit(Collision collision)
    {
        collisions--;
        if(collisions <= 0)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    //Used by Upgrade Manager. Lacks Persistence
    public void Upgrade(UpgradeableComponent toUpgrade)
    {
        switch (toUpgrade)
        {
            case UpgradeableComponent.shield:
                shieldRank++;
                MaxShield = shieldRankValues[shieldRank];
                shield = shieldRankValues[shieldRank];
                shieldText.text = "Shield: " + shield.ToString() + "/" + MaxShield.ToString();
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

    //show bullet trail
    private IEnumerator SpawnTrailHit(Transform startPos, TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos.position, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;
        if(target!= null)
            target.Die();
        //Instantiate(propertyExplosion, hit.point, Quaternion.LookRotation(hit.point));
        Destroy(trail.gameObject, trail.time);
    }

    //show bullet trail
    private IEnumerator SpawnTrailMiss(Transform startPos, TrailRenderer trail, float ShootRange)
    {
        float time = 0;
        Vector3 pos = startPos.position + (startPos.forward * ShootRange / 2);
        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos.position, pos, time);
            time += trail.time;
            yield return null;
        }
        trail.transform.position = pos;
        Destroy(trail.gameObject, trail.time);
    }

}
