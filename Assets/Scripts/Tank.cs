using System;
using System.Collections;
using UnityEngine;

public class Tank : Base_Controller
{
    [SerializeField] private float speed;
    [SerializeField] private float speedBoost;
    [SerializeField] private float speedRotation = 50f;
    [SerializeField] private float boostDuration = 2f;
    [SerializeField] private GameUI ammo;
                     public float maxBullets;
                     private float minBullets;
                     public bool isBoosted;

    private void Start()
    {
        maxBullets = choice.actualDifficulty.tank.maxBullets;
        nbBullets = choice.actualDifficulty.tank.nbBullets;
        pv = choice.actualDifficulty.tank.pv;
        speed = choice.actualDifficulty.tank.speed;
        speedBoost = choice.actualDifficulty.tank.speedBoost;
    }

    private void Update()
    {
        choice.actualDifficulty.tank.actualTankNbBullets = nbBullets;
        choice.actualDifficulty.tank.actualTankPv = pv;
        nbBullets = Mathf.Clamp(nbBullets, minBullets, maxBullets);
        transform.Translate(0.0f, 0.0f, Input.GetAxis("Vertical")*speed*Time.deltaTime);
        transform.Rotate(0.0f,Input.GetAxis("Horizontal")*speedRotation*Time.deltaTime, 0.0f);
        AimtoTarget();
        if (Input.GetMouseButton(0))
        {
            if (nbBullets > 0)
            {
                if (!isAlreadyFiring && !ammo.isAlreadyReload)
                {
                    Fire();
                    ammo.LoseAmmo();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ammo.Reload();
        }
    }

    private void AimtoTarget()
    {
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(tempRay, out RaycastHit hit))
        {
            RotateToTarget(hit.point);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boost") && !isBoosted)
        {
            isBoosted = true;
            StartCoroutine(Boost());
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            UpdatePv();
        }
    }

    private IEnumerator Boost()
    {
        float tmpSpeed = speed;
        speed = speedBoost;
        yield return new WaitForSeconds(boostDuration);
        speed = tmpSpeed;
        isBoosted = false;
    }
}