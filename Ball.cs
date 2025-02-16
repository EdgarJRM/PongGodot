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
		_timer = GetParent().GetNodeOrNull<Timer>("RestartTimer");
		_audioCollision = GetNodeOrNull<AudioStreamPlayer>("AudioBallCollide");

		ResetBall();
	}

	public void ResetBall()
	{
		Speed = 600f;

		// Genera una dirección aleatoria asegurando que no sea demasiado vertical
		float angle = (float)(_random.NextDouble() * Math.PI * 2); // Ángulo aleatorio entre 0 y 2π
		Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

		// Asegurar que el movimiento inicial no sea casi vertical (para evitar rebotes repetitivos)
		if (Math.Abs(Direction.Y) < 0.2f)
		{
			Direction.Y += 0.2f * Math.Sign(Direction.Y);
		}

		Direction = Direction.Normalized();
		IsMoving = true;
		//_timer.Stop();
	}

	public override void _PhysicsProcess(double delta){
		if (IsMoving){
			Velocity = Direction * Speed; // Asignar la velocidad correctamente

			var collide = MoveAndCollide(Velocity * (float)delta);

			if (collide != null) {  // Verificar si hubo colisión{
				if (_audioCollision != null)  // Verificar que el nodo de sonido exista
				{
					_audioCollision.Play();
				}
				Direction = Direction.Bounce(collide.GetNormal()); // Rebotar correctamente
				GD.Print($"Bounced! New Direction: {Direction}");  
			}
		}
	}

	private void _OnRestartTimerTimeout()
	{
		ResetBall();
	}
}
