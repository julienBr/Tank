using System.Collections;
using UnityEngine;

public class Base_Controller : MonoBehaviour
{ 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected Transform[] bulletSpawnPosition;
    [SerializeField] private GameObject turretHead;
    [SerializeField] private Transform _transform;
    [SerializeField] public float pv;
    [SerializeField] public float nbBullets;
                     protected bool isAlreadyFiring;
    [SerializeField] public AppDatas choice;

    protected void UpdatePv()
    {
        pv--;
        if (pv == 0)
        {
            Destroy(gameObject);
        }
    }
    
    protected void Fire()
    {
        if (nbBullets > 0)
        {
            if (!isAlreadyFiring)
            {
                isAlreadyFiring = true;
                StartCoroutine(FireDelay());
            }
        }
    }

    private IEnumerator FireDelay()
    {
        nbBullets--;
        foreach(Transform spawn in bulletSpawnPosition) Instantiate(bulletPrefab, spawn.position, spawn.rotation);
        yield return new WaitForSeconds(0.5f);
        isAlreadyFiring = false;
    }
    
    protected void RotateToTarget(Vector3 targetPos)
    {
        _transform.position = new Vector3(targetPos.x, turretHead.transform.position.y, targetPos.z);
        turretHead.transform.LookAt(_transform);
    }
}