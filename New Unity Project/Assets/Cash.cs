using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : Singleton<Cash> {


    private int totalCash = 0;

    public override void Awake()
    {
        base.Awake();
        UpdateCash(100);
    }
    public void UpdateCash(int change)
    {
        totalCash += change;
        GetComponent<TMPro.TextMeshProUGUI>().text = totalCash.ToString();
    }
}
