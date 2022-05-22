using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : MonoBehaviour
{

    public Slider sliderval;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderval.value = Player.currEXP / Player.requiredEXPforNextLevel;
        if (Player.currEXP >= Player.requiredEXPforNextLevel)
        {
            Debug.Log("Level Up");
            Player.currEXP -= Player.requiredEXPforNextLevel;
            Player.currLevel ++;
            Player.requiredEXPforNextLevel = (float)(Player.requiredEXPforNextLevel * Player.requiredEXPforNextLevel * 0.012);
            Debug.Log(Player.requiredEXPforNextLevel);
        }
    }
}
