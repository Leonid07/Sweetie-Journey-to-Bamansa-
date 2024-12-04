using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PanelUpFlower : MonoBehaviour
{
    public Button thisButton;

    public GameObject panelFingers_1;
    public GameObject panelFingers_2;
    public GameObject panelFingers_3;
    public GameObject panelFingers_4;

    int count = 0;

    public Image imageFlower;
    public int isBuyLocation = 0;

    private void Start()
    {
        thisButton = GetComponent<Button>();

        thisButton.onClick.AddListener(NextTab);
    }

    public void NextTab()
    {
        count++;
        switch (count)
        {
            case 1:
                panelFingers_1.SetActive(false);
                panelFingers_2.SetActive(true);
                break;
            case 2:
                panelFingers_2.SetActive(false);
                panelFingers_3.SetActive(true);
                break;
            case 3:
                panelFingers_3.SetActive(false);
                panelFingers_4.SetActive(true);
                break;
            case 4:
                panelFingers_4.SetActive(false);
                panelFingers_1.SetActive(true);

                CreateFlower();

                StartCoroutine(AnimateToCenterAndBack());
                count = 0;
                break;
        }
    }
    public void CreateFlower()
    {
        GameObject go = Instantiate(DataManager.InstanceData.prefabFlower, DataManager.InstanceData.contentInventorySeed);
        go.GetComponent<Image>().sprite = imageFlower.sprite;
    }

    [Header("Анимация сорванного цветка")]
    public RectTransform uiObject; // UI объект для перемещения
    public GameObject targetObject; // Объект, который активируется
    public float animationDuration = 1.0f; // Длительность анимации

    private Vector3 initialPosition; // Исходное положение UI объекта
    private Vector3 targetPosition; // Положение центра экрана

    private IEnumerator AnimateToCenterAndBack()
    {
        uiObject = PanelManager.InstancePanel.canvasPot.GetComponent<PotInFlower>().flower.GetComponent<RectTransform>();

        initialPosition = uiObject.position;

        targetPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            uiObject.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        uiObject.position = targetPosition;

        targetObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        //elapsedTime = 0f;
        //while (elapsedTime < animationDuration)
        //{
            //uiObject.position = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / animationDuration);
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}

        DataManager.InstanceData.countFlower[isBuyLocation] += 1;
        DataManager.InstanceData.SaveCountFlower();

        PanelManager.InstancePanel.canvasPot.sortingOrder = 0;

        PanelManager.InstancePanel.canvasPot.GetComponent<PotInFlower>().countProgress = 0;
        PanelManager.InstancePanel.canvasPot.GetComponent<PotInFlower>().typeSeed = 7;

        DataManager.InstanceData.SavePotInFlower();

        Destroy(PanelManager.InstancePanel.canvasPot.gameObject.GetComponent<Canvas>());
        PanelManager.InstancePanel.canvasPot.GetComponent<PotInFlower>().Progress();
        PanelManager.InstancePanel.canvasPot = null;

        targetObject.SetActive(false);
        uiObject.gameObject.SetActive(false);
        uiObject.position = initialPosition;
        gameObject.SetActive(false);
    }
}