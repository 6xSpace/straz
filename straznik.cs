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
				
				//DOBIERANIE SIE DO BOARDA
				var tablica = GetNode<Board>("/root/Node2D/@Node2D@2");
				GD.Print(tablica);
				tablica.wypisz();
				
				this.pathfinding(tablica.getter());
				/////////////////////////////////////////////////////////////////////
				
				
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
	public int[][] pathfinding(dynamic obszar){
		GD.Print(this.Name);
		string temp = this.Name;
		char iii = temp[4];
		string ii = Convert.ToString(iii);
		int i = Convert.ToInt32(ii);
		GD.Print(iii);
		GD.Print(i);
		char yyy = temp[5];
		string yy = Convert.ToString(yyy);
		int y = Convert.ToInt32(yy);
		GD.Print(y);
		malyPathfinding(obszar, i, y, 1);
		//malyPathfinding(obszar, i, y, 1);
		//malyPathfinding(obszar, i, y, 1);
		//malyPathfinding(obszar, i, y, 1);
		
		
		
		foreach(var chuj in obszar)
		{
			//foreach(var y in i){
				//GD.Print(y.ToString());
			//}
			GD.Print("", string.Join(", ", chuj));
			
		}
		
		return obszar;
	}
	public static void malyPathfinding(dynamic obszar, int pole_i, int pole_y, int licznik){
		if (obszar[pole_i+1][pole_y] == 0 || (obszar[pole_i+1][pole_y] > licznik && obszar[pole_i+1][pole_y] < 3000)){
			obszar[pole_i+1][pole_y] = licznik;
			//malyPathfinding(obszar, pole_i+1, pole_y, licznik+1);
		} 
		if (obszar[pole_i-1][pole_y] == 0 || (obszar[pole_i-1][pole_y] > licznik && obszar[pole_i-1][pole_y] < 3000)){
			obszar[pole_i-1][pole_y] = licznik;
			//malyPathfinding(obszar, pole_i-1, pole_y, licznik+1);
		} 
		if (obszar[pole_i][pole_y+1] == 0 || (obszar[pole_i][pole_y+1] > licznik && obszar[pole_i][pole_y+1] < 3000)){
			obszar[pole_i][pole_y+1] = licznik;
			//malyPathfinding(obszar, pole_i, pole_y+1, licznik+1);
		} 
		if (obszar[pole_i][pole_y-1] == 0 || (obszar[pole_i][pole_y-1] > licznik && obszar[pole_i][pole_y-1] < 3000)){
			obszar[pole_i][pole_y-1] = licznik;
			//malyPathfinding(obszar, pole_i, pole_y-1, licznik+1);
		}
		
		
		if (obszar[pole_i+1][pole_y] == licznik){
			malyPathfinding(obszar, pole_i+1, pole_y, licznik+1);
		}
		if (obszar[pole_i-1][pole_y] == licznik){
			malyPathfinding(obszar, pole_i-1, pole_y, licznik+1);
		}
		if (obszar[pole_i][pole_y-1] == licznik){
			malyPathfinding(obszar, pole_i, pole_y-1, licznik+1);
		}
		if (obszar[pole_i][pole_y+1] == licznik){
			malyPathfinding(obszar, pole_i, pole_y+1, licznik+1);
		}
	}
}
