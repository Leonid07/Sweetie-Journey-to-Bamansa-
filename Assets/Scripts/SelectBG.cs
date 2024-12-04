using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBG : MonoBehaviour
{
    public Image imageThisBG;
    public Image imageChield;
    public int id;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeImageBG);
    }
    public void ChangeImageBG()
    {
        imageThisBG.sprite = imageChield.sprite;
        DataManager.InstanceData.idGackGround = id;
        DataManager.InstanceData.SaveBackGround();
    }
}
