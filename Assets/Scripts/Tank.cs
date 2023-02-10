using System.Collections;
using UnityEngine;

public class Tank : Base_Controller
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedBoost = 8f;
    [SerializeField] private float speedRotation = 50f;
    [SerializeField] private float boostDuration = 2f;
    [SerializeField] private GameUI ammo;
    [SerializeField] private Material mBase;
    [SerializeField] private Material mHead;
    [SerializeField] private Material mBaseBoost;
    [SerializeField] private Material mHeadBoost;
    [SerializeField] private Renderer tankBase;
    [SerializeField] private Renderer tankHead;
                     public float maxBullets = 10f;
                     private float minBullets;
                     public bool isBoosted;

    private void Update()
    {
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
        tankBase.material = mBaseBoost;
        tankHead.material = mHeadBoost;
        yield return new WaitForSeconds(boostDuration);
        tankBase.material = mBase;
        tankHead.material = mHead;
        speed = tmpSpeed;
        isBoosted = false;
    }
}