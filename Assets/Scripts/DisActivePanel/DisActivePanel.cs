using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisActivePanel : MonoBehaviour
{
    public float time = 1;
    public void Start()
    {
        StartCoroutine(timer());
    }
    public IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
