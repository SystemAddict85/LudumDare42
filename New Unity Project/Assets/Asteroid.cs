using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Asteroid : Shootable
{

    public Sprite[] asteroidSprites;  

    public void Awake()
    {
        var randSpriteIndex = Random.Range(0, asteroidSprites.Length);
        Debug.Log(randSpriteIndex);
        GetComponent<SpriteRenderer>().sprite = asteroidSprites[randSpriteIndex];
        GetComponent<Animator>().SetInteger("index", randSpriteIndex);

        var randSize = Random.Range(2f, 4f);
        transform.localScale *= randSize;

        totalLife += (randSize / 2f);
    }   
    


}
