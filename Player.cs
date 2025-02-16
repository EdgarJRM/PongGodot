using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float SPEED = 600.0f;

	public override void _PhysicsProcess(double delta)
	{
		// Get input from up/down S or W controls (keys)
		float direction = Input.GetAxis("up", "down");

		// Modify the speed on the Y axis, but leave the speed on the X axis intact
		Velocity = new Vector2(0, direction * SPEED);

		// Move the player with modified speed
		MoveAndSlide();
	}
}
