using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("chuj");
		var table = new Board();
		table.dodajDomek(1, 1);
		table.wypisz();
		table.generuj(this);
		
		//var domek = new Sprite2D();
		//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek.png");
		//domek.Texture = obrazek;
		//this.AddChild(domek);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
