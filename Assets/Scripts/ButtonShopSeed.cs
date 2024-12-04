using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShopSeed : MonoBehaviour
{
    public int isBuyLocation = 0;
    public Image imageSpot;

    public int price;

    public GameObject seedPrefab;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(BuyLocation);
    }
    public void BuyLocation()
    {
        if (DataManager.InstanceData.coin >= price)
        {
            DataManager.InstanceData.coin -= price;
            DataManager.InstanceData.SaveCoin();
            DataManager.InstanceData.AddCoinToText();

            CreateSeed();

            DataManager.InstanceData.countSeed[isBuyLocation] += 1;
            DataManager.InstanceData.SaveCountSeed();
            SoundManager.InstanceSound.soundBuy.Play();
        }
    }
    public void CreateSeed()
    {
        GameObject go = Instantiate(seedPrefab, DataManager.InstanceData.contentInventorySeed);
        go.GetComponent<Image>().sprite = imageSpot.sprite;
        go.GetComponent<Seed>().numberFlower = isBuyLocation;
    }
}