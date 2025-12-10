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
				//GD.Print(tablica);
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
		//this.sekwencja = sekwencja;
		foreach (var item in sekwencja){
			this.sekwencja.Add(item);
		}
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
		//GD.Print(this.sekwencja);
		GD.Print("", string.Join(", ", this.sekwencja));
		if (this.sekwencja.Count > 0){
			//GD.Print(this.Name);
			
			//GD.Print(this.sekwencja.Last());
			//GD.Print(this.Position);
			
			string temp = this.Name;
			char iii = temp[4];
			string ii = Convert.ToString(iii);
			int ja_i = Convert.ToInt32(ii);
			char yyy = temp[5];
			string yy = Convert.ToString(yyy);
			int ja_y = Convert.ToInt32(yy);
			
			temp = this.sekwencja.Last();
			iii = temp[4];
			ii = Convert.ToString(iii);
			int cel_i = Convert.ToInt32(ii);
			yyy = temp[5];
			yy = Convert.ToString(yyy);
			int cel_y = Convert.ToInt32(yy);
			
			var cel = GetNode<PustePole>("/root/Node2D/"+this.sekwencja.Last());
			var obrazek = GetNode<Sprite2D>("/root/Node2D/"+this.sekwencja.Last()+"/Sprite2D");
			obrazek.Texture = null;
			
			//GD.Print(cel.Name);
			
			var tablica = GetNode<Board>("/root/Node2D/tableNode");
			var tempPosition = this.Position;
			var tempName = this.Name;
			var tempName2 = cel.Name;
			
			this.Position = cel.Position;
			cel.Position = tempPosition;
			
			cel.Name = "chuj";
			this.Name = tempName2;
			cel.Name = tempName;
			GD.Print(this.Name);
			
			var tempID = tablica.table[ja_i][ja_y];
			
			////////////////////////////////////////////////////
			//GD.Print(tempID);
			tablica.table[ja_i][ja_y] = tablica.table[cel_i][cel_y];
			tablica.table[cel_i][cel_y] = tempID;
			
			tablica.tychNieCzysc.RemoveAt(this.sekwencja.Count - 1);
			this.sekwencja.RemoveAt(this.sekwencja.Count - 1);
			
			if (this.sekwencja.Count == 0){
				var sprite = GetNode<Sprite2D>("Sprite2D");
				var obrazek1 = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
				sprite.Texture = obrazek1;
			}
			//GD.Print(ja_i, ja_y, cel_i, cel_y);
			//GD.Print(this.Name);
			//GD.Print(cel.Name);
			tablica.wypisz();
		}
		
	}
}
