using Godot;
using System;
using System.Collections.Generic;

public partial class PustePole : Node2D
{
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
		//var node2 = node;
		var sprite = GetNode<Sprite2D>("Sprite2D");
		var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
		
		
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		//GD.Print(tablica);
		GD.Print(string.Join(", ", tablica.tychNieCzysc));
		
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
		
		if (tablica.szukacSciezki){
			string temp = this.Name;
			char iii = temp[4];
			string ii = Convert.ToString(iii);
			int i = Convert.ToInt32(ii);
			char yyy = temp[5];
			string yy = Convert.ToString(yyy);
			int y = Convert.ToInt32(yy);
			
			var szukajacy = GetNode<straznik>("/root/Node2D/"+tablica.szukajacy[0]);
			
			if (szukajacy.tablicaSciezki[i][y] != 9999){
				sekwencja.Add(this.Name);
				szukaj(szukajacy.tablicaSciezki, i, y, szukajacy.tablicaSciezki[i][y]);
				
				foreach (var item in sekwencja){
					var pole = GetNode<Sprite2D>("/root/Node2D/"+item+"/Sprite2D");
					pole.Texture = obrazek;
				}
			}
			
			
			
		}
		sprite.Texture = obrazek;
		
		//GD.Print(string.Join(", ", sekwencja));
		
		
	}
	
	public void szukaj(dynamic obszar, int pole_i, int pole_y, int licznik){
		if (licznik >= 0 && obszar[pole_i+1][pole_y] == licznik-1){
			int temp = pole_i+1;
			sekwencja.Add("pole"+temp+pole_y);
			szukaj(obszar, pole_i+1, pole_y, licznik-1);
		} else if (licznik >= 0 && obszar[pole_i-1][pole_y] == licznik-1){
			int temp = pole_i-1;
			sekwencja.Add("pole"+temp+pole_y);
			szukaj(obszar, pole_i-1, pole_y, licznik-1);
		} else if (licznik >= 0 && obszar[pole_i][pole_y-1] == licznik-1){
			int temp = pole_y-1;
			sekwencja.Add("pole"+pole_i+temp);
			szukaj(obszar, pole_i, pole_y-1, licznik-1);
		} else if (licznik >= 0 && obszar[pole_i][pole_y+1] == licznik-1){
			int temp = pole_y+1;
			sekwencja.Add("pole"+pole_i+temp);
			szukaj(obszar, pole_i, pole_y+1, licznik-1);
		}
		
		
	}
	
	public void _on_area_2d_mouse_exited(){
		var sprite = GetNode<Sprite2D>("Sprite2D");
		sekwencja.Clear();
	}
	
	public void _on_area_2d_input_event(Node viewport, InputEvent @event, long shapeIdx){
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				
				var tablica = GetNode<Board>("/root/Node2D/tableNode");
				tablica.szukacSciezki = false;
				if (!tablica.wylaczKlikanie && tablica.szukajacy.Count != 0 && sekwencja.Count > 0){
					GD.Print("weszło!");
					tablica.wylaczKlikanie = true;
					//////////////////////////////////////////////////////////////
					//ABSOLUTNE TYPY REFERENCYJNE RIGHT THERE
					foreach (string item in sekwencja){
						tablica.tychNieCzysc.Add(item);
					}
					//GD.Print(tablica.szukajacy[0]);
					var szukajacy = GetNode<straznik>("/root/Node2D/"+tablica.szukajacy[0]);
					szukajacy.otrzymajSekwencje(sekwencja);
					
					var timer = GetNode<Timer>("/root/Node2D/Timer");
					
					var tata = GetNode<Main>("/root/Node2D");
					if (!tata.paused){
						timer.Start();
					}
				}
				
			}
		}
	}
}
