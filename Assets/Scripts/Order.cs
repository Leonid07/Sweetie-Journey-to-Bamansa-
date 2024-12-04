using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public Button giveAway;
    public Sprite spriteButtonDisActive;
    public Sprite spriteButtonActive;

    public Image imageFlower;

    public TMP_Text textCountFlower;
    public TMP_Text textCountReward;

    public int countReward;
    public int countFlower;

    public int number;

    private void Start()
    {
        giveAway.onClick.AddListener(GetReward);
    }

    public void GetReward()
    {
        if (countFlower <= DataManager.InstanceData.countFlower[number])
        {
            DataManager.InstanceData.coin += countReward;

            DataManager.InstanceData.SaveCoin();
            DataManager.InstanceData.AddCoinToText();

            DataManager.InstanceData.countFlower[number] -= countFlower;
            DataManager.InstanceData.SaveCountFlower();

            SoundManager.InstanceSound.soundSellFlower.Play();

            Destroy(gameObject);
        }
    }
    public void CheckOrders()
    {
        number = Random.Range(0,6);

        imageFlower.sprite = PanelManager.InstancePanel.spriteSecondState[number];

        countReward = Random.Range(1500,3000);
        countFlower = Random.Range(2,5);

        textCountReward.text = $"+ {countReward}";
        textCountFlower.text = $"{countFlower}";

        if (countFlower <= DataManager.InstanceData.countFlower[number])
        {
            giveAway.GetComponent<Image>().sprite = spriteButtonActive;
        }
        else
        {
            giveAway.GetComponent<Image>().sprite = spriteButtonDisActive;
        }
    }
}