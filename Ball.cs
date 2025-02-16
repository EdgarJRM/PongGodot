using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	public float Speed = 600f;
	public Vector2 Direction = Vector2.Zero;
	public bool IsMoving = false;
	private Random _random = new Random();
	
	private AudioStreamPlayer _audioCollision;
	private Timer _timer;

	public override void _Ready()
	{
		// Reference is made to objects
		_timer = GetParent().GetNodeOrNull<Timer>("RestartTimer");
		_audioCollision = GetNodeOrNull<AudioStreamPlayer>("AudioBallCollide");

		ResetBall();
	}

	public void ResetBall()
	{
		Speed = 1100f;

		// Generates a random direction ensuring it is not too vertical
		float angle = (float)(_random.NextDouble() * Math.PI * 5); // Random angle between 0 and 5π
		Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

		// Ensure that the initial movement is not almost vertical (to avoid repetitive bounces)
		if (Math.Abs(Direction.Y) < 0.2f)
		{
			Direction.Y += 0.2f * Math.Sign(Direction.Y);
		}

		Direction = Direction.Normalized();
		IsMoving = true;
		_timer.Stop();
	}

	public override void _PhysicsProcess(double delta){
		if (IsMoving){
			Velocity = Direction * Speed; // Assigning speed correctly

			var collide = MoveAndCollide(Velocity * (float)delta);

			if (collide != null) {  // Check for collision
				if (_audioCollision != null)  // Verify that the sound node exists
				{
					_audioCollision.Play();
				}
				Direction = Direction.Bounce(collide.GetNormal()); // Bounce correctly
				GD.Print($"Bounced! New Direction: {Direction}");  
			}
		}
	}

	private void _OnRestartTimerTimeout()
	{
		ResetBall();
	}
}
