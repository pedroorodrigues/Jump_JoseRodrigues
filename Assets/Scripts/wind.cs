using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class wind : MonoBehaviour
{
    [SerializeField] private UnityEvent OnWind = new UnityEvent();
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnWind?.Invoke();
    }
}
