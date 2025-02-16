using Godot;

public partial class Opponent : CharacterBody2D
{
	private const float Speed = 400.0f;
	private Ball ball; // Referencia a la pelota

	public override void _Ready()
	{
		// Buscar la pelota dentro del padre
		ball = GetParent().FindChild("Ball") as Ball;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (ball == null)
			return; // Si la pelota no ha sido encontrada, salir

		Vector2 direction = new Vector2(0, GetDirection());
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private int GetDirection()
	{
		if (Mathf.Abs(ball.Position.Y - Position.Y) > 25)
		{
			return ball.Position.Y > Position.Y ? 1 : -1;
		}
		return 0;
	}
}
