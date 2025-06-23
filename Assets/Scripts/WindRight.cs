using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class windRight : MonoBehaviour
{
    [SerializeField] private UnityEvent OnWindRigth = new UnityEvent();
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnWindRigth?.Invoke();
    }
}
