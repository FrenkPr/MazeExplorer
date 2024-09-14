using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Mngr : Singleton<HUD_Mngr>
{
    [HideInInspector] public int numCollectiblesFound;
    private float timePlayCount;

    // Start is called before the first frame update
    void Start()
    {
        numCollectiblesFound = 0;
        timePlayCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.isPauseMenuActive)
        {
            return;
        }

        timePlayCount += Time.deltaTime;

        int hours = (int)(timePlayCount / 3600);
        int minutes = (int)(timePlayCount / 60) % 60;
        int secs = (int)timePlayCount % 60;

        UI_Utilities.Instance.TextSprites["TimePlayCount"].text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + secs.ToString("00");
    }
}
