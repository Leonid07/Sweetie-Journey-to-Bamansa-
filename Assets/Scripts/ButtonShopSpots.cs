using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShopSpots : MonoBehaviour
{
    public int isBuyLocation = 0;
    public Image imageSpot;

    public int price;

    public GameObject spotPrefab;

    public Swipe swipePanel;

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
            //DataManager.InstanceData.CheckShopLocations();

            CreateSpot();
            CreatePot();

            // save pot
            DataManager.InstanceData.countSpot[isBuyLocation] += 1;
            swipePanel.maxPage++;
            DataManager.InstanceData.SaveCountSpot();

            DataManager.InstanceData.SetIdCountPotInFlower();
            DataManager.InstanceData.SavePotInFlower();
            DataManager.InstanceData.SaveCountMaxPage();

            SoundManager.InstanceSound.soundBuy.Play();

            swipePanel.NewSizePanel();
        }
    }
    public void CreateSpot()
    {
        GameObject go = Instantiate(spotPrefab, DataManager.InstanceData.contentInventorySport);
        go.GetComponent<Image>().sprite = imageSpot.sprite;
    }

    public void CreatePot()
    {
        GameObject go = Instantiate(DataManager.InstanceData.prefabPotInFlower, DataManager.InstanceData.contentPotInFlower.transform);
        go.GetComponent<PotInFlower>().pot.GetComponent<Image>().sprite = imageSpot.sprite;
        go.GetComponent<PotInFlower>().Progress();
    }
}