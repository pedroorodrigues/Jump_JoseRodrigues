using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    [SerializeField]private UnityEvent OnPlayerHitWall = new UnityEvent();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPlayerHitWall?.Invoke();
        }
    }
}
