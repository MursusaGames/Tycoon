//using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmobInterSystem : MonoBehaviour
{
    //private InterstitialAd interstitial;
    //public int pushNumber;

    private void OnEnable()
    {
        //MobileAds.Initialize(initStatus => { });
    }
    void Start()
    {
        //string adUnitInterId = "ca-app-pub-3940256099942544/1033173712";
        // Initialize an InterstitialAd.
        //this.interstitial = new InterstitialAd(adUnitInterId);
        //RequestInterstitial();
    }
    


    private void RequestInterstitial()
    {
        // Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        //this.interstitial.LoadAd(request);        
    }    

   /* public void ShowInterAd()
    {
        pushNumber += 1;
        
        if (pushNumber % 2 == 0 && pushNumber > 3)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
                Invoke(nameof(RequestInterstitial), 0.1f);
            }
        }        
        
    }*/
   

}
