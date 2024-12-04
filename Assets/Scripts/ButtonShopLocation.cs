using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShopLocation : MonoBehaviour
{
    public string idButtonLocation;
    public int isBuyLocation = 0;

    private void Awake()
    {
        idButtonLocation = gameObject.name;
        LoadLocation();
        CheckIsBuy();
        GetComponent<Button>().onClick.AddListener(BuyLocation);
    }
    public void BuyLocation()
    {
        if (DataManager.InstanceData.coin >= 1000)
        {
            isBuyLocation++;
            DataManager.InstanceData.coin -= 1000;
            DataManager.InstanceData.SaveCoin();
            DataManager.InstanceData.AddCoinToText();
            DataManager.InstanceData.CheckShopLocations();
            CheckIsBuy();
            SoundManager.InstanceSound.soundBuy.Play();
        }
    }
    public void CheckIsBuy()
    {
        if (isBuyLocation == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            SaveLocation();
        }
    }
    public void SaveLocation()
    {
        PlayerPrefs.SetInt(idButtonLocation, isBuyLocation);
        PlayerPrefs.Save();
    }
    public void LoadLocation()
    {
        if (PlayerPrefs.HasKey(idButtonLocation))
        {
            isBuyLocation = PlayerPrefs.GetInt(idButtonLocation);
        }
    }
}