using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager InstancePanel { get; private set; }
    private void Awake()
    {
        if (InstancePanel != null && InstancePanel != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstancePanel = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [Header("Управление для панели магазина")]
    public Button buttonSpots;
    public Button buttonSeeds;
    public Button buttonLocation;

    public GameObject panelSpotsShop;
    public GameObject panelSeedsShop;
    public GameObject panelLocationShop;

    public GameObject panelApplySeed;
    public PanelUpFlower panelUpFlower;

    private void Start()
    {
        buttonSpots.onClick.AddListener(()=> 
        {
            GoToSpotsShop();
        });
        buttonSeeds.onClick.AddListener(() =>
        {
            GoToSeedsShop();
        });
        buttonLocation.onClick.AddListener(() =>
        {
            GoToLocationShop();
        });

        buttonOpenOrder.onClick.AddListener(CreateOrderAndOpenPanel);
    }

    public void GoToSpotsShop()
    {
        panelSpotsShop.SetActive(true);
    }
    public void GoToSeedsShop()
    {
        panelSeedsShop.SetActive(true);
    }
    public void GoToLocationShop()
    {
        panelLocationShop.SetActive(true);
    }

    [Header("Цветы")]
    public Sprite spriteZeroState;
    public Sprite[] spriteFirstState;
    public Sprite[] spriteSecondState;

    [Header("Горшок)))")]
    public Canvas canvasPot;

    [Header("Панель для обработки заказов")]
    public GameObject prefabOrder;
    public Transform transformContent;

    [Space(10)]

    public GameObject panelOrder;
    public Button buttonOpenOrder;
    public int countOrder = 6;

    public void CreateOrderAndOpenPanel()
    {
        panelOrder.SetActive(true);

        for (int i = 0; i < countOrder; i++)
        {
            if (transformContent.childCount != countOrder)
            {
                GameObject go = Instantiate(prefabOrder, transformContent);
                go.GetComponent<Order>().CheckOrders();
            }
        }
    }
}