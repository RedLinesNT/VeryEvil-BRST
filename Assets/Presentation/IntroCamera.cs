using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VEvil.GameLogic.Buildings;

public class IntroCamera : MonoBehaviour
{
    [SerializeField] private GameObject _gameCamera;
    [SerializeField] private Nexus[] _nexus;

    public void SwitchCamera()
    {
        if (_gameCamera != null) _gameCamera.SetActive(true);

        if (_nexus.Length > 0) // Only use if nexus is in auto generation
        {
            for (int i = 0; i < _nexus.Length; i++)
            {
                _nexus[i].enabled = true;
            }
        } 

        Destroy(gameObject);
    }
}
