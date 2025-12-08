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
		sprite.Texture = obrazek;
		
		var tablica = GetNode<Board>("/root/Node2D/@Node2D@2");
		//GD.Print(tablica);
		if (tablica.szukacSciezki){
			string temp = this.Name;
			char iii = temp[4];
			string ii = Convert.ToString(iii);
			int i = Convert.ToInt32(ii);
			//GD.Print(iii);
			//GD.Print(i);
			char yyy = temp[5];
			string yy = Convert.ToString(yyy);
			int y = Convert.ToInt32(yy);
			//GD.Print(y);
			//GD.Print(tablica.table[i][y]);
			sekwencja.Add(this.Name);
			szukaj(tablica.tablicaSciezki, i, y, tablica.table[i][y]);
			GD.Print(string.Join(", ", sekwencja));
			
			
			//for (int i = 0; i<this.table.Length; i++)
			//{
					//for (int y = 0; y<this.table[i].Length; y++){
						//if (tablica.table[i][y] == 0){
							//
						//}
					//}
						//
				//}
			foreach (var item in sekwencja){
				//GD.Print(item);
				var pole = GetNode<Sprite2D>("/root/Node2D/"+item+"/Sprite2D");
				//GD.Print(pole);
				pole.Texture = obrazek;
			}
		}
	}
	
	public void szukaj(dynamic obszar, int pole_i, int pole_y, int licznik){
		//GD.Print(obszar[pole_i+1][pole_y]);
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
		//var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
		sprite.Texture = null;
		//foreach (var item in sekwencja){
		////GD.Print(item);
			//var pole = GetNode<Sprite2D>("/root/Node2D/"+item+"/Sprite2D");
			////GD.Print(pole);
			//pole.Texture = null;
		//}
		sekwencja.Clear();
	}
}
