using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ManaManager : MonoBehaviour
{
    
    public Slider sliderval;
    
    public TextMeshProUGUI manaText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderval.value = Player.currMana / Player.maxMana;
        manaText.text = Player.currMana.ToString() + " / " + Player.maxMana.ToString();
    }
}
