using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Tank tank;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private GameObject isBoostedImage;
    [SerializeField] private GameObject bulletImage;
    [SerializeField] private Transform canvas;
                     private float minHealth;
                     private float nbBullets;
                     public List<GameObject> listImage;
                     public bool isAlreadyReload;
    
    private void Awake()
    {
        maxHealth = tank.choice.actualDifficulty.tank.maxHealth;
        isBoostedImage.SetActive(false);
        for (int i = 0; i < tank.nbBullets; i++)
        {
            listImage.Add(Instantiate(bulletImage, new Vector3(i * 50f, 0f, 0f), Quaternion.identity, canvas));
        }
    }
    
    private void Update()
    {
        UpdateScore();
        displayBoost();
    }
    
    private void UpdateScore()
    {
        tank.pv = Mathf.Clamp(tank.pv, minHealth, maxHealth);
        scoreText.text = tank.pv + " / " + maxHealth;
        healthBarImage.fillAmount = tank.pv / maxHealth;
    }
    
    public void LoseAmmo() 
    {
        Destroy(listImage.Last());
        listImage.RemoveAt(listImage.Count-1);
    }

    public void Reload()
    {
        float remainingBullet = tank.maxBullets - tank.nbBullets;
        if (remainingBullet > 0)
        {
            if (!isAlreadyReload)
            {
                isAlreadyReload = true;
                StartCoroutine(WaitReload());
            }
        }
    }
    
    private IEnumerator WaitReload()
    {
        for (int i = (int)tank.nbBullets; i < tank.maxBullets; i++) 
        {
            listImage.Add(Instantiate(bulletImage, new Vector3(i * 50f, 0f, 0f), Quaternion.identity, canvas));
            yield return new WaitForSeconds(0.5f);
        }
        tank.nbBullets = tank.maxBullets;
        isAlreadyReload = false;
    }

    private void displayBoost()
    {
        isBoostedImage.SetActive(tank.isBoosted);
    }
}