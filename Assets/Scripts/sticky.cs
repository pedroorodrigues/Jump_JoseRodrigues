using UnityEngine.Events;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    private bool playerIsInWall;
    [SerializeField] private UnityEvent OnPlayerStaySticky = new UnityEvent();
    [SerializeField] private UnityEvent OnPlayerUnSticky = new UnityEvent();
    [SerializeField] private UnityEvent OnPlayerHitSticky = new UnityEvent();


    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            playerIsInWall = true;
            OnPlayerHitSticky?.Invoke();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!playerIsInWall)
        {
            return;

        }
        OnPlayerStaySticky?.Invoke();


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnPlayerUnSticky?.Invoke();
        playerIsInWall = false;

    }
}
