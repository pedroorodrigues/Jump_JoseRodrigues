using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    [SerializeField] private Image Bar;
    //public float waitTime = 30.0f;
    private float jumpBar;

    public void Jump_Bar(float charge)
    {
        jumpBar += charge;
    }
    private void Update()
    {
        Debug.Log(jumpBar);
        //jumpBar.fillAmount = Mathf.Clamp01(90f);
        //Bar.fillAmount -= 1.0f / waitTime * Time.deltaTime;
    }
}   
