using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
	[HideInInspector] public PlayerInput playerInput;
	[HideInInspector] public InputAction leftClick;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		leftClick = playerInput.actions["ClickInput"];
	}
}
