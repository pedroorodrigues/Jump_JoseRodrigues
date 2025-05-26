using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Camara : MonoBehaviour
{

    [SerializeField] private Transform _target;

    void Update()
    {
        if (_target != null)
        {
            transform.position = new Vector3(transform.position.x, _target.position.y + 3, -10);
        }
    }
}

