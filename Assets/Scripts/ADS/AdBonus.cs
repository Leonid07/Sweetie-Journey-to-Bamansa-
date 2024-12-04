using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdBonus : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string adPlacementIdIOS = "Rewarded_iOS";
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null;

    public Button buttonWatchAdsPlusGold;
    public GameObject panelRewardADS;

    private void Awake()
    {
#if UNITY_IOS
        _adUnitId = adPlacementIdIOS;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }

    private void Start()
    {
        LoadAd(); // ��������� ������� ��� ������
        buttonWatchAdsPlusGold.onClick.AddListener(ShowAdAndReward);
    }
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
    public void ShowAdAndReward()// ��������� ����� �� 1000
    {
        Advertisement.Show(_adUnitId, this);
        LoadAd();
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            DataManager.InstanceData.coin += 2500;
            DataManager.InstanceData.SaveCoin();
            DataManager.InstanceData.AddCoinToText();
            panelRewardADS.SetActive(false);
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {

    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad Loaded: {placementId}");
    }
}