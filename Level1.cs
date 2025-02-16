using Godot;

public partial class Level1 : Node2D
{
	private int PlayerScore = 0;
	private int OpponentScore = 0;
	private Ball ball;
	private Label ScorePlayer;
	private Label ScoreOpponent;
	private Timer restartTimer;
	private bool isRestarting = false; // Evita múltiples reinicios

	public override void _Ready()
	{
		ball = GetNode<Ball>("Ball");
		ScorePlayer = GetNode<Label>("ScorePlayer");
		ScoreOpponent = GetNode<Label>("ScoreOpponent");
		restartTimer = GetNode<Timer>("RestartTimer");

		restartTimer.Timeout += _OnRestartTimerTimeout; // Conectar señal correctamente
		RestartGame();
	}

	public override void _PhysicsProcess(double delta)
	{
		ScorePlayer.Text = PlayerScore.ToString();
		ScoreOpponent.Text = OpponentScore.ToString();
	}

	private void RestartGame()
	{
		if (isRestarting) return; // Evita múltiples llamadas seguidas
		isRestarting = true; 

		GD.Print("RestartGame() llamado");  
		ball.IsMoving = false;
		ball.Direction = Vector2.Zero;
		ball.Velocity = Vector2.Zero;  // Detener completamente
		ball.Position = new Vector2(960, 540);
		
		restartTimer.Stop(); // Asegurar que el temporizador no esté corriendo
		restartTimer.Start(); // Iniciar temporizador para reinicio
	}

	private void _OnRestartTimerTimeout()
	{
		isRestarting = false; // Permitir nuevos reinicios
		GD.Print("Reiniciando pelota...");
		ball.IsMoving = true;
		ball.ResetBall(); 
		GD.Print("Reinicisefasfwerwerando pelota...");
	}

	public void _on_opponen_bow_body_entered(Node2D body)
	{
		if (body is Ball && !isRestarting)
		{
			GD.Print("Gol del oponente");
			OpponentScore += 1;
			RestartGame();
		}
	}

	public void _on_player_bow_body_entered(Node2D body)
	{
		if (body is Ball && !isRestarting)
		{
			GD.Print("Gol del jugador");
			PlayerScore += 1;
			RestartGame();
		}
	}
}
