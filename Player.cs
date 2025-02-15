using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private const float SPEED = 400.0f;

    public override void _PhysicsProcess(double delta)
    {
        // Obtener la entrada de los controles (teclas) izquierda/derecha
        float direction = Input.GetAxis("up", "down");

        // Modificar la velocidad en el eje Y, pero dejar la velocidad en el eje X intacta
        Velocity = new Vector2(0, direction * SPEED);

        // Mover al jugador con la velocidad modificada
        MoveAndSlide();
    }

    public override void _Ready()
    {
        GD.Print("Hola mundo");
    }
}