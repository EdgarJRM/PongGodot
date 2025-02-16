using Godot;

public partial class Opponent : CharacterBody2D
{
	private const float Speed = 600.0f;
	private Ball ball; // Reference to the ball

	public override void _Ready()
	{
		// Look for the ball inside the ganme
		ball = GetParent().FindChild("Ball") as Ball;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (ball == null)
			return; // If the ball has not been found, go out

		Vector2 direction = new Vector2(0, GetDirection());
		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private int GetDirection()
	{
		// makes it move in the direction of the ball
		if (Mathf.Abs(ball.Position.Y - Position.Y) > 25)
		{
			return ball.Position.Y > Position.Y ? 1 : -1;
		}
		return 0;
	}
}
