using Godot;

public partial class Level1 : Node2D
{
	private int PlayerScore = 0;
	private int OpponentScore = 0;
	private Ball ball;
	private Label ScorePlayer;
	private Label ScoreOpponent;
	private Timer restartTimer;
	private bool isRestarting = false; // Avoid multiple reboots

	public override void _Ready(){
		// Reference is made to objects
		ball = GetNode<Ball>("Ball");
		ScorePlayer = GetNode<Label>("ScorePlayer");
		ScoreOpponent = GetNode<Label>("ScoreOpponent");
		restartTimer = GetNode<Timer>("RestartTimer");
		
		restartTimer.Timeout += _OnRestartTimerTimeout; // Connect the timer signal correctly.
		RestartGame();
	}

	public override void _PhysicsProcess(double delta){
		// the result contained in the labels is captured
		ScorePlayer.Text = PlayerScore.ToString();
		ScoreOpponent.Text = OpponentScore.ToString();
	}

	private void RestartGame(){
		if (isRestarting) return; // Avoid multiple calls in a row
		isRestarting = false; 

		GD.Print("RestartGame() llamado");  
		ball.IsMoving = false;
		ball.Direction = Vector2.Zero;
		ball.Velocity = Vector2.Zero;  // Stop completely
		ball.Position = new Vector2(960, 540);
		ball.ResetBall(); 
		restartTimer.Stop(); // Make sure the timer is not running
		restartTimer.Start(); // Start timer for reboot
	}

private void _OnRestartTimerTimeout(){
	isRestarting = false; // Allow new restarts
	GD.Print("Reiniciando pelota...");
	ball.IsMoving = true;
	ball.ResetBall(); 
}
	
	public void _on_opponen_bow_body_entered(Node2D body){
		// Record if the ball goes off the board, record score and restart the game
		if (body is Ball && !isRestarting){
			GD.Print("Gol del oponente");
			OpponentScore += 1;
			RestartGame();
		}
	}

	public void _on_player_bow_body_entered(Node2D body){
		// Record if the ball goes off the board, record score and restart the game
		if (body is Ball && !isRestarting){
			GD.Print("Gol del jugador");
			PlayerScore += 1;
			RestartGame();
		}
	}
}
