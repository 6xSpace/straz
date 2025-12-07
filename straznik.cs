using Godot;
using System;

public partial class straznik : Node2D
{
	bool klikniety = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_area_2d_mouse_entered(){
		//var node2 = node;
		//var sprite = GetNode<Sprite2D>("Sprite2D");
		//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik_klik.png");
		//sprite.Texture = obrazek;
	}
	
	public void _on_area_2d_input_event(Node viewport, InputEvent @event, long shapeIdx)
	{
		//GD.Print(@event);
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				GD.Print("Lewy klik!");
				// tutaj zrób co chcesz
				if (klikniety == false){
					klikniety = true;
					var sprite = GetNode<Sprite2D>("Sprite2D");
					var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik_klik.png");
					sprite.Texture = obrazek;
				} else {
					klikniety = false;
					var sprite = GetNode<Sprite2D>("Sprite2D");
					var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
					sprite.Texture = obrazek;
				}
			}
		}
	}
}
