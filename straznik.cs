using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class straznik : Node2D
{
	bool klikniety = false;
	List<string> sekwencja = new List<string>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_area_2d_mouse_entered(){
		
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
			{
				for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
					//GD.Print(tablica.table[pole_i][pole_y]);
					if (tablica.table[pole_i][pole_y] == 0){
						if (!tablica.tychNieCzysc.Contains("pole"+pole_i+pole_y)){
							var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
						
							pole.Texture = null;
						}
					}
				}
						
			}
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
				var tablica = GetNode<Board>("/root/Node2D/tableNode");
				GD.Print(tablica);
				tablica.wypisz();
				
				//object kopia = tablica.table.Clone();
				//tablica.tablicaSciezki = tablica.table;
				
				//TO JEST ULTRA KURA GŁUPIE, JEBAĆ TYPY REFERENCYJNE ISTG
				///////////////////////////////////////////////////////////
				for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
				{
					for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
						//GD.Print(tablica.table[pole_i][pole_y]);
						tablica.tablicaSciezki[pole_i][pole_y] = tablica.table[pole_i][pole_y];
					}
							
				}
				//int[][] doZwrocenia = this.pathfinding(tablica.tablicaSciezki);
				//tablica.tablicaSciezki = doZwrocenia;
				//tablica.szukacSciezki = true;
				//tablica.wypisz();
				
				this.pathfinding(tablica.tablicaSciezki);
				
				tablica.wypisz();
				/////////////////////////////////////////////////////////////////////
				
				
				if (klikniety == false){
					klikniety = true;
					tablica.szukacSciezki = true;
					tablica.szukajacy = this.Name;
					var sprite = GetNode<Sprite2D>("Sprite2D");
					var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik_klik.png");
					sprite.Texture = obrazek;
				} else {
					klikniety = false;
					tablica.szukacSciezki = false;
					tablica.szukajacy = null;
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
	
	public void otrzymajSekwencje(List<string> sekwencja){
		GD.Print(string.Join(", ", sekwencja));
		this.sekwencja = sekwencja;
		//poruszaj();
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		klikniety = false;
		tablica.szukacSciezki = false;
		//tablica.szukajacy = null;
		//var sprite = GetNode<Sprite2D>("Sprite2D");
		//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
		//sprite.Texture = obrazek;
	}
	
	public void poruszaj(){
		GD.Print(this.sekwencja.Last());
	}
}
