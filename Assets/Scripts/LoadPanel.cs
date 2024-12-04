using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public float timer = 2f;
    public RectTransform rotatingImage; // UI объект, который нужно вращать
    public float rotationSpeed = 100f; // Скорость вращения в градусах/сек
    private void Start()
    {
        StartCoroutine(qwe());
    }
    public IEnumerator qwe()
    {
        float elapsedTime = 0f;

        // Вращаем объект в течение заданного времени
        while (elapsedTime < timer)
        {
            rotatingImage.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Ждём следующий кадр
        }

        // Отключаем объект после завершения вращения
        gameObject.SetActive(false);
    }
}
