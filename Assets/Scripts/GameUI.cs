using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject tank;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float maxHealth = 100f;
    private float minHealth;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private GameObject bulletImage;
    [SerializeField] private Transform canvas;
    private float nbBullets;
    public List<GameObject> listImage;
    
    private void Awake()
    {
        nbBullets = tank.GetComponent<Tank>().nbBullets;
        for (int i = 1; i <= nbBullets; i++)
        {
            listImage.Add(Instantiate(bulletImage, new Vector3(i * 25f, 25f, 0f), Quaternion.identity, canvas));
        }
    }
    
    private void Update()
    {
        UpdateScore();
    }
    
    private void UpdateScore()
    {
        tank.GetComponent<Tank>().pv = Mathf.Clamp(tank.GetComponent<Tank>().pv, minHealth, maxHealth);
        scoreText.text = tank.GetComponent<Tank>().pv + " / " + maxHealth;
        healthBarImage.fillAmount = tank.GetComponent<Tank>().pv / maxHealth;
    }
    
    public void LoseAmmo() 
    {
        Destroy(listImage.Last());
        listImage.RemoveAt(listImage.Count-1);
    }
}
