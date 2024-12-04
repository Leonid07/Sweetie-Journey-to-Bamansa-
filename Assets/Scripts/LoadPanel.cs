using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public float timer = 2f;
    public RectTransform rotatingImage; // UI ������, ������� ����� �������
    public float rotationSpeed = 100f; // �������� �������� � ��������/���
    private void Start()
    {
        StartCoroutine(qwe());
    }
    public IEnumerator qwe()
    {
        float elapsedTime = 0f;

        // ������� ������ � ������� ��������� �������
        while (elapsedTime < timer)
        {
            rotatingImage.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // ��� ��������� ����
        }

        // ��������� ������ ����� ���������� ��������
        gameObject.SetActive(false);
    }
}
