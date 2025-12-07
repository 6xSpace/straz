using Godot;
using System;

public partial class PustePole : Node2D
{
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
		var sprite = GetNode<Sprite2D>("Sprite2D");
		var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
		sprite.Texture = obrazek;
	}
	
	public void _on_area_2d_mouse_exited(){
		var sprite = GetNode<Sprite2D>("Sprite2D");
		//var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
		sprite.Texture = null;
	}
}
