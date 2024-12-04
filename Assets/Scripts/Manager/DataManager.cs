using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager InstanceData { get; private set; }
    private void Awake()
    {
        if (InstanceData != null && InstanceData != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int coin;
    public string idCoin = "idCoin";

    public TMP_Text[] textCoin;

    [Header("инвентарь для заднего фона")]
    public ButtonShopLocation[] buttonShopLocations;
    public Transform[] selectBG;

    [Header("Главный задний фон")]
    public int idGackGround;
    public string stringIdBackGround;
    public Image imageBG;

    [Header("Инвентарь для горшка")]
    public Transform contentInventorySport;

    public Sprite[] spriteSpot;
    public int[] countSpot;
    public string[] idCountSpot;

    public GameObject prefabSpot;

    [Header("Инвентарь для семян")]
    public Transform contentInventorySeed;

    public Sprite[] spriteSeed;
    public int[] countSeed;
    public string[] idCounterSeed;

    public GameObject prefabSeed;

    [Header("Инвентарь для цветов")]
    public int[] countFlower;
    public string[] idCountFlower;
    public GameObject prefabFlower;

    [Header("Панель горшков на главном экране и сохранение")]
    public GameObject contentPotInFlower;
    public GameObject prefabPotInFlower;

    public List<string> idPotInFlower = new List<string>();

    private void Start()
    {
        SetIdCountSpot();
        SetIdCountSeed();
        SetIdCountFlower();

        LoadCoin();
        AddCoinToText();

        LoadBackGround();
        CheckShopLocations();

        LoadCountSpot();
        CreateSpotInContent();

        LoadCountSeed();
        CreateSeedInContent();

        LoadCountFlower();
        CreateFlowerInContent();

        LoadPotInFlower();

        LoadCountMaxPage();
    }
    // добавляет в список значения
    public void SetIdCountPotInFlower()
    {
        if (idPotInFlower.Count > 0)
        {
            if (int.TryParse(idPotInFlower[idPotInFlower.Count - 1], out int lastValue))
            {
                idPotInFlower.Add((lastValue + 1).ToString());
            }
        }
        else
        {
            idPotInFlower.Add("1111");
        }
    }
    public void SavePotInFlower()
    {
        for (int i = 0; i < idPotInFlower.Count; i++)
        {
            foreach (Transform child in contentPotInFlower.transform)
            {
                PlayerPrefs.SetInt(idPotInFlower[i], child.GetComponent<PotInFlower>().countProgress);
                PlayerPrefs.Save();
            }
        }
    }
    public void LoadPotInFlower()
    {
        if (PlayerPrefs.HasKey("1111"))
        {
            idPotInFlower.Add("1111");
            for (int i = 0; i < idPotInFlower.Count; i++)
            {
                if (PlayerPrefs.HasKey(idPotInFlower[i]))
                {
                    SetIdCountPotInFlower();
                    for (int j = 0; j < countSpot.Length; j++)
                    {
                        if (countSpot[j] > 0)
                        {
                            for (int k = 0; k < countSpot[j]; k++)
                            {
                                GameObject go = Instantiate(prefabPotInFlower, contentPotInFlower.transform);
                                go.GetComponent<PotInFlower>().countProgress = PlayerPrefs.GetInt(idPotInFlower[i]);
                                go.GetComponent<PotInFlower>().pot.sprite = spriteSpot[j];
                            }
                        }
                    }
                }
            }
            idPotInFlower.RemoveAt(idPotInFlower.Count - 1);
        }
    }
    public void SetIdCountSpot()
    {
        int count = 0;
        for (int i = 0; i < idCountSpot.Length; i++)
        {
            idCountSpot[i] = (count + 1000).ToString();
            count++;
        }
    }
    public void SetIdCountSeed()
    {
        int count = 0;
        for (int i = 0; i < idCounterSeed.Length; i++)
        {
            idCounterSeed[i] = (count + 10000).ToString();
            count++;
        }
    }
    public void SetIdCountFlower()
    {
        int count = 0;
        for (int i = 0; i < idCountFlower.Length; i++)
        {
            idCountFlower[i] = (count + 100000).ToString();
            count++;
        }
    }
    public void SaveCountSpot()
    {
        for (int i = 0; i < countSpot.Length; i++)
        {
            PlayerPrefs.SetInt(idCountSpot[i], countSpot[i]);
            PlayerPrefs.Save();
        }
    }
    public void SaveCountSeed()
    {
        for (int i = 0; i < countSeed.Length; i++)
        {
            PlayerPrefs.SetInt(idCounterSeed[i], countSeed[i]);
            PlayerPrefs.Save();
        }
    }
    public void SaveCountFlower()
    {
        for (int i = 0; i < countFlower.Length; i++)
        {
            PlayerPrefs.SetInt(idCountFlower[i], countFlower[i]);
            PlayerPrefs.Save();
        }
    }
    public void LoadCountSpot()
    {
        for (int i = 0; i < countSpot.Length; i++)
        {
            if (PlayerPrefs.HasKey(idCountSpot[i]))
            {
                countSpot[i] = PlayerPrefs.GetInt(idCountSpot[i]);
            }
        }
    }
    public void LoadCountSeed()
    {
        for (int i = 0; i < countSeed.Length; i++)
        {
            if (PlayerPrefs.HasKey(idCounterSeed[i]))
            {
                countSeed[i] = PlayerPrefs.GetInt(idCounterSeed[i]);
            }
        }
    }
    public void LoadCountFlower()
    {
        for (int i = 0; i < countFlower.Length; i++)
        {
            if (PlayerPrefs.HasKey(idCountFlower[i]))
            {
                countFlower[i] = PlayerPrefs.GetInt(idCountFlower[i]);
            }
        }
    }
    public void CreateSpotInContent()
    {
        for (int i = 0; i < spriteSpot.Length; i++)
        {
            if (countSpot[i] > 0)
            {
                for (int j = 0; j < countSpot[i]; j++)
                {
                    GameObject go = Instantiate(prefabSpot, contentInventorySport);///////////////////////////////////////////////////////////////
                    go.GetComponent<Image>().sprite = spriteSpot[i];
                }
            }
        }
    }
    public void CreateSeedInContent()
    {
        for (int i = 0; i < spriteSeed.Length; i++)
        {
            if (countSeed[i] > 0)
            {
                for (int j = 0; j < countSeed[i]; j++)
                {
                    GameObject go = Instantiate(prefabSeed, contentInventorySeed);
                    go.GetComponent<Image>().sprite = spriteSeed[i];
                    go.GetComponent<Seed>().numberFlower = i;
                }
            }
        }
    }
    public void CreateFlowerInContent()
    {
        for (int i = 0; i < PanelManager.InstancePanel.spriteSecondState.Length; i++)
        {
            if (countFlower[i] > 0)
            {
                for (int j = 0; j < countFlower[i]; j++)
                {
                    GameObject go = Instantiate(prefabFlower, contentInventorySeed);
                    go.GetComponent<Image>().sprite = PanelManager.InstancePanel.spriteSecondState[i];
                    go.GetComponent<Flower>().numberFlower = i;
                }
            }
        }
    }
    public void CheckShopLocations()
    {
        for (int i = 0; i < buttonShopLocations.Length; i++)
        {
            if (buttonShopLocations[i].isBuyLocation == 1)
            {
                selectBG[i].gameObject.SetActive(true);
            }
            else
            {
                selectBG[i].gameObject.SetActive(false);
            }
        }
    }
    public void SaveBackGround()
    {
        PlayerPrefs.SetInt(stringIdBackGround, idGackGround);
        PlayerPrefs.Save();
    }
    public void LoadBackGround()
    {
        if (PlayerPrefs.HasKey(stringIdBackGround))
        {
            idGackGround = PlayerPrefs.GetInt(stringIdBackGround);
            imageBG.sprite = selectBG[idGackGround].GetComponent<SelectBG>().imageChield.sprite;
        }
    }
    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey(idCoin))
        {
            coin = PlayerPrefs.GetInt(idCoin);
            AddCoinToText();
        }
    }
    public void AddCoinToText()
    {
        foreach (TMP_Text text in textCoin)
        {
            text.text = coin.ToString();
        }
    }
    public void SaveCoin()
    {
        PlayerPrefs.SetInt(idCoin, coin);
        PlayerPrefs.Save();
    }
    [Header("Сохранение количества страниц в списке")]
    public string IDcountMaxPage = "IDcountMaxPage";
    public Swipe swipePanel;
    public void SaveCountMaxPage()
    {
        PlayerPrefs.SetInt(IDcountMaxPage, swipePanel.maxPage);
        PlayerPrefs.Save();
    }
    public void LoadCountMaxPage()
    {
        if (PlayerPrefs.HasKey(IDcountMaxPage))
        {
            swipePanel.maxPage = PlayerPrefs.GetInt(IDcountMaxPage);
        }
    }
}