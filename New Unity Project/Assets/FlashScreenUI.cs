using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreenUI : Singleton<FlashScreenUI> {    

    public enum FlashColor { RED, GREEN, BLUE, WHITE };
    private Image image;

    private float flashTime = 0f;
    private float currentTime = 0f;
    [SerializeField]
    private float step = 0f;
    private Color32 currentColor;

    [SerializeField]
    private Color32 red;
    [SerializeField]
    private Color32 blue;
    [SerializeField]
    private Color32 green;
    [SerializeField]
    private Color32 white;

    private bool flashStarted = false;

    public override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {

        if (flashStarted)
        {
            currentTime += Time.deltaTime;
            var alpha = image.color.a - step * Time.deltaTime;
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            if(currentTime >= flashTime)
            {
                
                image.enabled = false;
                flashStarted = false;
            }
        }
	}

    public void FlashScreen(FlashColor color, float time)
    {
        switch (color)
        {
            case FlashColor.RED:
                currentColor = red;
                break;
            case FlashColor.GREEN:
                currentColor = green;
                break;
            case FlashColor.BLUE:
                currentColor = blue;
                break;
            case FlashColor.WHITE:
                currentColor = white;
                break;
        }

        flashTime = time;
        currentTime = 0f;

        step = 1f / time;

        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        image.enabled = true;      
        flashStarted = true;

    }
}
