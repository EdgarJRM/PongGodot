using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	private const float SPEED = 400.0f;

	public override void _PhysicsProcess(double delta){
		double direction = Input.GetAxis("ui_up", "ui_down", );
		Velocity = new Vector2(Velocity.x, direction * SPEED);

		MoveAndSlide();
	}
}
