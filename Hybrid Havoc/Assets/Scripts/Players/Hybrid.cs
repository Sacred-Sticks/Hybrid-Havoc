using System;
using Kickstarter.Identification;
using UnityEngine;

public class Hybrid : MonoBehaviour
{
    private IInputReceiver[] inputs;
    private Player player;
    
    private void Awake()
    {
        inputs = GetComponents<IInputReceiver>();
        player = GetComponent<Player>();
    }
}
