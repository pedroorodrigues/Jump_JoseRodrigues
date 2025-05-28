using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    [SerializeField] private Image Bar;
    public float waitTime = 30.0f;
    private float jumpBar;

    private void Awake()
    {
        jumpBar = 0;
    }

    public void Jump_Bar(float charge)
    {
        jumpBar = charge;
        StartCoroutine(JumpCycle());

    }
    private void Update()
    {
        Debug.Log(jumpBar);
        Bar.fillAmount = Mathf.MoveTowards(jumpBar, 10f, Time.deltaTime);
    }
    private IEnumerator JumpCycle()
    {
        yield return new WaitForSeconds(5); 
    }
}   
