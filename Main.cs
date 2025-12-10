using Godot;
using System;

public partial class Main : Node2D
{
	public int tura = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("chuj");
		var table = new Board();
		table.Name = "tableNode";
		//table.tychNieCzysc.Add("chuj");
		AddChild(table);
		table.dodajDomek(2, 1);
		table.dodajDomek(2, 2);
		table.dodajDomek(1, 2);
		
		table.wypisz();
		table.generuj(this);
		
		var napis = GetNode<RichTextLabel>("napisTura");
		napis.Text = "Tura 0";
		//napis.Color = "black";
		
		//var domek = new Sprite2D();
		//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek.png");
		//domek.Texture = obrazek;
		//this.AddChild(domek);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void wypisz(){
		GD.Print("chuj");
	}
	
	public void zmianaTury(){
		tura++;
		var napis = GetNode<RichTextLabel>("napisTura");
		napis.Text = "Tura "+tura;
		
		var table = GetNode<Board>("tableNode");
		table.tura();
	}
	
	public void _on_area_2d_input_event(Node viewport, InputEvent @event, long shapeIdx){
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				zmianaTury();
			}
		}
	}
}
