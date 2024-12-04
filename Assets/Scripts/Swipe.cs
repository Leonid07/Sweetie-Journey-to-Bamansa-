using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Swipe : MonoBehaviour, IEndDragHandler
{
    public int maxPage;
    public int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    public RectTransform levelPagesRect;

    [SerializeField] float tweenTime;

    float dragThreshould;

    public Button buttonLeft;
    public Button buttonRight;

    private void Start()
    {
        buttonLeft.onClick.AddListener(() =>
        {
            Previous();
            StartCoroutine(MovePage());
        });
        buttonRight.onClick.AddListener(() =>
        {
            Next();
            StartCoroutine(MovePage());
        });
        buttonLeft.gameObject.SetActive(false);

        StartCoroutine(SizeContenPanel());
    }

    public IEnumerator SizeContenPanel()
    {
        yield return new WaitForSeconds(0.1f);
        if (maxPage == 1)
        {
            yield break;
        }
        else
        {
            // Убедимся, что pivot установлен в 0, чтобы расширение шло в одну сторону
            levelPagesRect.pivot = new Vector2(0, 0.5f);

            // Увеличиваем ширину
            levelPagesRect.sizeDelta = new Vector2((levelPagesRect.sizeDelta.x + 1264.296f * maxPage) - 1264.296f, levelPagesRect.sizeDelta.y);

            currentPage = 1;
            targetPos = levelPagesRect.localPosition;
            dragThreshould = Screen.width / 15;
        }
    }
    //-1244.591
    public void NewSizePanel()
    {
        if (maxPage != 0)
        {
            // Убедимся, что pivot установлен в 0, чтобы расширение шло в одну сторону
            levelPagesRect.pivot = new Vector2(0, 0.5f);

            // Увеличиваем ширину
            levelPagesRect.sizeDelta = new Vector2(levelPagesRect.sizeDelta.x + 1264.296f, levelPagesRect.sizeDelta.y);
        }
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            StartCoroutine(MovePage());
            buttonLeft.gameObject.SetActive(true);
            if (currentPage == maxPage)
            {
                buttonRight.gameObject.SetActive(false);
            }
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            StartCoroutine(MovePage());
            buttonRight.gameObject.SetActive(true);
            if (currentPage == 1)
            {
                buttonLeft.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator MovePage()
    {
        Vector3 startPos = levelPagesRect.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < tweenTime)
        {
            levelPagesRect.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / tweenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelPagesRect.localPosition = targetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            StartCoroutine(MovePage());
        }
    }
}