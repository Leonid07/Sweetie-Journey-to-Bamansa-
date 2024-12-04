using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public int numberFlower;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendSeed);
    }
    public void SendSeed()
    {
        PanelManager.InstancePanel.panelApplySeed.SetActive(true);

        PanelManager.InstancePanel.panelApplySeed.GetComponent<PanelApplySeed>().buttonYes.onClick.RemoveAllListeners();
        PanelManager.InstancePanel.panelApplySeed.GetComponent<PanelApplySeed>().buttonNo.onClick.RemoveAllListeners();

        PanelManager.InstancePanel.panelApplySeed.GetComponent<PanelApplySeed>().buttonYes.onClick.AddListener(Yes);
        PanelManager.InstancePanel.panelApplySeed.GetComponent<PanelApplySeed>().buttonNo.onClick.AddListener(No);
    }
    public void Yes()
    {
        foreach (Transform child in DataManager.InstanceData.contentPotInFlower.transform)
        {
            if (child.GetComponent<PotInFlower>().countProgress ==  0)
            {
                child.GetComponent<PotInFlower>().countProgress = 1;
                child.GetComponent<PotInFlower>().typeSeed = numberFlower;
                child.GetComponent<PotInFlower>().Progress();
                PanelManager.InstancePanel.panelApplySeed.SetActive(false);

                DataManager.InstanceData.countSeed[numberFlower] -= 1;

                DataManager.InstanceData.SaveCountSeed();

                DataManager.InstanceData.SavePotInFlower();
                Destroy(gameObject);
                return;
            }
            else
            {
                Debug.Log("нет доступных горшков");
            }
        }
    }
    public void No()
    {
        PanelManager.InstancePanel.panelApplySeed.SetActive(false);
    }
}