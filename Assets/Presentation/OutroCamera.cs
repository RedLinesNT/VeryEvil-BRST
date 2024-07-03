using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VEvil.GameLogic.Cameras.Gameplay;

public class OutroCamera : MonoBehaviour
{
    public Transform _nexus = null;
    [SerializeField] private GameCamera _cam = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
