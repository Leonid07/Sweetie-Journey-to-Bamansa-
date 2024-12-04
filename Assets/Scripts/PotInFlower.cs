using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotInFlower : MonoBehaviour
{
    public Image flower;
    public Image pot;

    public Button buttonWater;
    public Button buttonFertilize;

    public TMP_Text textWater;
    public TMP_Text textFertilize;

    public int countFirst = 3;
    public int countSecond = 6;
    public int countThird = 8;

    public int counterWater;
    public int counterFertilize;

    private void Start()
    {
        buttonWater.onClick.AddListener(Water);
        buttonFertilize.onClick.AddListener(Fertilize);

        textWater.text = "Water 0/3";
        textFertilize.text = "Fertilize 0/3";
    }

    private void Water()
    {
        // Проверяем, что Water не превысил текущий лимит
        if ((counterWater < countFirst) ||
            (counterWater < countSecond && counterFertilize >= countFirst) ||
            (counterWater < countThird && counterFertilize >= countSecond))
        {
            counterWater++;
            SoundManager.InstanceSound.soundWatelFlower.Play();
            CheckAction(); // Проверка на выполнение условий
            UpdateWaterText();
        }
    }

    private void Fertilize()
    {
        // Проверяем, что Fertilize не превысил текущий лимит
        if ((counterFertilize < countFirst) ||
            (counterFertilize < countSecond && counterWater >= countFirst) ||
            (counterFertilize < countThird && counterWater >= countSecond))
        {
            counterFertilize++;
            SoundManager.InstanceSound.soundFertilize.Play();
            CheckAction(); // Проверка на выполнение условий
            UpdateFertilizeText();
        }
    }

    private void UpdateWaterText()
    {
        if (counterWater < countFirst)
        {
            textWater.text = $"Water {counterWater}/{countFirst}";
        }
        else if (counterWater < countSecond)
        {
            textWater.text = $"Water {counterWater}/{countSecond}";
        }
        else if (counterWater < countThird)
        {
            textWater.text = $"Water {counterWater}/{countThird}";
        }
        else
        {
            textWater.text = "Water Complete!";
        }
    }

    private void UpdateFertilizeText()
    {
        if (counterFertilize < countFirst)
        {
            textFertilize.text = $"Fertilize {counterFertilize}/{countFirst}";
        }
        else if (counterFertilize < countSecond)
        {
            textFertilize.text = $"Fertilize {counterFertilize}/{countSecond}";
        }
        else if (counterFertilize < countThird)
        {
            textFertilize.text = $"Fertilize {counterFertilize}/{countThird}";
        }
        else
        {
            textFertilize.text = "Fertilize Complete!";
        }
    }

    private void CheckAction()
    {
        if (counterWater == countFirst && counterFertilize == countFirst)
        {
            countProgress++;
            Progress();
        }
        else if (counterWater == countSecond && counterFertilize == countSecond)
        {
            countProgress++;
            Progress();
        }
        else if (counterWater == countThird && counterFertilize == countThird)
        {
            countProgress++;
            Progress();
        }
    }

    [Header("Цветок посажен")]
    public int countProgress = 0;
    public int typeSeed = 7;

    // 0 пустой горшок
    // 1 есть семячко
    // 2 есть расточек
    // 3 есть расточек первой степени
    // 4 росток можно вытаскивать

    public void Progress()
    {
        switch (countProgress)
        {
            case 0:
                buttonWater.gameObject.SetActive(false);
                buttonFertilize.gameObject.SetActive(false);

                counterFertilize = 0;
                counterWater = 0;

                textFertilize.text = $"Fertilize {counterFertilize}/{countFirst}";
                textWater.text = $"Water {counterWater}/{countFirst}";

                break;

            case 1:
                buttonWater.gameObject.SetActive(true);
                buttonFertilize.gameObject.SetActive(true);

                break;

            case 2:
                buttonWater.gameObject.SetActive(true);
                buttonFertilize.gameObject.SetActive(true);
                flower.gameObject.SetActive(true);
                flower.sprite = PanelManager.InstancePanel.spriteZeroState;
                break;

            case 3:
                buttonWater.gameObject.SetActive(true);
                buttonFertilize.gameObject.SetActive(true);
                flower.gameObject.SetActive(true);
                flower.sprite = PanelManager.InstancePanel.spriteFirstState[typeSeed];
                break;

            case 4:
                buttonWater.gameObject.SetActive(true);
                buttonFertilize.gameObject.SetActive(true);
                flower.gameObject.SetActive(true);
                flower.sprite = PanelManager.InstancePanel.spriteSecondState[typeSeed];

                PanelManager.InstancePanel.panelUpFlower.gameObject.SetActive(true);
                PanelManager.InstancePanel.panelUpFlower.imageFlower = flower;
                PanelManager.InstancePanel.panelUpFlower.isBuyLocation = typeSeed;

                gameObject.AddComponent<Canvas>();
                GetComponent<Canvas>().overrideSorting = true;
                GetComponent<Canvas>().sortingOrder = 1;
                PanelManager.InstancePanel.canvasPot = GetComponent<Canvas>();

                break;
        }
    }
}