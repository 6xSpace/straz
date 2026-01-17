using Godot;
using System;

public partial class Bandyta : PustePole
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	//public void _on_area_2d_mouse_entered(){
		//var tablica = GetNode<Board>("/root/Node2D/tableNode");
		//for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
			//{
				//for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
					////GD.Print(tablica.table[pole_i][pole_y]);
					//if (tablica.table[pole_i][pole_y] == 0){
						//if (!tablica.tychNieCzysc.Contains("pole"+pole_i+pole_y)){
							//var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
							//
							//pole.Texture = null;
						//}
					//}
				//}
						//
			//}
			//}
			
	public void _on_area_2d_mouse_entered(){
		//var node2 = node;
		//GD.Print(this.Name);
		var sprite = GetNode<Sprite2D>("Sprite2D");
		var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
		var obrazekBandyta = GD.Load<Texture2D>("res://asstets/placeholder_bandyta_klik.png");
		
		
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		//GD.Print(tablica);
		//GD.Print(string.Join(", ", tablica.tychNieCzysc));
		
		for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
			{
				for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
					//GD.Print(tablica.table[pole_i][pole_y]);
					if (tablica.table[pole_i][pole_y] == 0){
						if (!tablica.tychNieCzysc.Contains("pole"+pole_i+pole_y)){
							var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
							
							pole.Texture = null;
						}
					} else if (tablica.table[pole_i][pole_y] == 3003){
						if (!tablica.tychNieCzysc.Contains("pole"+pole_i+pole_y)){
							var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
							
							pole.Texture = obrazekBandyta;
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
		sprite.Texture = obrazekBandyta;
		
		//GD.Print(string.Join(", ", sekwencja));
		
		
	}
}
