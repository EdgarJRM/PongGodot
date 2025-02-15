using Godot;

public partial class GameManager : Node2D
{
    private int PlayerScore = 0;
    private int OpponentScore = 0;

    private Ball ball;
    private Label ScorePlayer;
    private Label ScoreOpponent;
    private Timer restartTimer;

    public override void _Ready()
    {
        ball = GetNode<Ball>("Ball");
        ScorePlayer = GetNode<Label>("MarcadorPlayer");
        ScoreOpponent = GetNode<Label>("MarcadorOponente");
        restartTimer = GetNode<Timer>("RestartTimer");

        RestartGame();
    }

    public override void _Process(double delta)
    {
        ScorePlayer.Text = PlayerScore.ToString();
        ScoreOpponent.Text = OpponentScore.ToString();
    }

    private void RestartGame()
    {
        GD.Print("RestartGame() llamado"); // <- Imprime cuando se ejecuta RestartGame()

        ball.IsMoving = false;
        ball.Direction = Vector2.Zero;
        ball.Position = new Vector2(960, 540);

        GD.Print("Reiniciando juego...");

        restartTimer.Start(); // Esperar antes de reiniciar la pelota
    }
    
    public void _on_player_bow_body_entered(Node2D body)
    {
        GD.Print("Gol del oponente");
        if (body is Ball)
        {
            GD.Print("Gol del oponente");
            OpponentScore += 1;
            RestartGame();
        }
    }

    public void _on_opponen_bow_body_entered(Node2D body)
    {
        if (body is Ball)
        {
            GD.Print("Gol del jugador");
            PlayerScore += 1;
            RestartGame();
        }
    }

    private void _OnRestartTimerTimeout()
    {
        ball.ResetBall(); // Ahora sí se reinicia el movimiento de la pelota
    }
}
