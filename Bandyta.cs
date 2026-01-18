using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Bandyta : PustePole
{
	List<string> sekwencja = new List<string>();
	public string? ofiara = null;
	public int aktualnyDystans = 1000;
	public int delay = 0;
	int[][] tablicaSciezki = 
	[[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,],
	[0,0,0,0,0,0,0,0,0,0,]] ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void kazdaTura(){
		
		if (this.delay == 0){
			this.delay = 1;
			this.sekwencja.Clear();
			this.ofiara = null;
			this.aktualnyDystans = 1000;
			var tablica = GetNode<Board>("/root/Node2D/tableNode");
			//this.tablicaSciezki = tablica.table.Clone() as int[][];
			for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
			{
				for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
					this.tablicaSciezki[pole_i][pole_y] = tablica.table[pole_i][pole_y];
				}
			}
			
			//foreach(var chuj in this.tablicaSciezki)
			//{
				//GD.Print("", string.Join(", ", chuj));
			//}
			
			this.pathfinding(this.tablicaSciezki);
			
			
			
			GD.Print(this.ofiara);
			
			if (this.ofiara != null){
				char iii = ofiara[4];
				string ii = Convert.ToString(iii);
				int i = Convert.ToInt32(ii);
				char yyy = ofiara[5];
				string yy = Convert.ToString(yyy);
				int y = Convert.ToInt32(yy);
				sekwencja.Add(ofiara);
				//GD.Print(ii, yy);
				this.zrobSekwencje(this.tablicaSciezki, i, y, this.tablicaSciezki[i][y]);
				
				//GD.Print("", string.Join(", ", this.sekwencja));
				
				this.poruszaj();
			}
		} else {
			this.delay--;
		}
		
		
	}
	
	public void zrobSekwencje(dynamic obszar, int pole_i, int pole_y, int licznik){ //1 do 1 funkcja szukaj z puste pole
		bool gdziesWeszlo = false;
		if (pole_i+1 < obszar.Length && !gdziesWeszlo){
			//GD.Print(licznik);
			if (licznik >= 0 && obszar[pole_i+1][pole_y] == licznik-1){
				//GD.Print("WCHODZI22222");
				int temp = pole_i+1;
				sekwencja.Add("pole"+temp+pole_y);
				zrobSekwencje(obszar, pole_i+1, pole_y, licznik-1);
				gdziesWeszlo = true;
			}
		}
		if (pole_i > 0 && !gdziesWeszlo){
			if (licznik >= 0 && obszar[pole_i-1][pole_y] == licznik-1){
				int temp = pole_i-1;
				sekwencja.Add("pole"+temp+pole_y);
				zrobSekwencje(obszar, pole_i-1, pole_y, licznik-1);
				gdziesWeszlo = true;
			}
		}
		if (pole_y+1 < obszar[0].Length && !gdziesWeszlo){
			if (licznik >= 0 && obszar[pole_i][pole_y+1] == licznik-1){
				int temp = pole_y+1;
				sekwencja.Add("pole"+pole_i+temp);
				zrobSekwencje(obszar, pole_i, pole_y+1, licznik-1);
				gdziesWeszlo = true;
			}
		}
		if (pole_y > 0 && !gdziesWeszlo){
			if (licznik >= 0 && obszar[pole_i][pole_y-1] == licznik-1){
				int temp = pole_y-1;
				sekwencja.Add("pole"+pole_i+temp);
				zrobSekwencje(obszar, pole_i, pole_y-1, licznik-1);
				gdziesWeszlo = true;
			}
		}
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
	
	public void malyPathfinding(dynamic obszar, int pole_i, int pole_y, int licznik){
		//foreach(var chuj in obszar)
		//{
			//GD.Print("", string.Join(", ", chuj));
		//}
		if (obszar.Length > pole_i+1 && obszar[0].Length > pole_y+1 && pole_i > 0 && pole_y >0){
			if (obszar[pole_i+1][pole_y] == 0 || (obszar[pole_i+1][pole_y] > licznik && obszar[pole_i+1][pole_y] < 3000)){
			obszar[pole_i+1][pole_y] = licznik;
			} 
			if (obszar[pole_i-1][pole_y] == 0 || (obszar[pole_i-1][pole_y] > licznik && obszar[pole_i-1][pole_y] < 3000)){
				obszar[pole_i-1][pole_y] = licznik;
			} 
			if (obszar[pole_i][pole_y+1] == 0 || (obszar[pole_i][pole_y+1] > licznik && obszar[pole_i][pole_y+1] < 3000)){
				obszar[pole_i][pole_y+1] = licznik;
			} 
			if (obszar[pole_i][pole_y-1] == 0 || (obszar[pole_i][pole_y-1] > licznik && obszar[pole_i][pole_y-1] < 3000)){
				obszar[pole_i][pole_y-1] = licznik;
			}
		
			if (this.aktualnyDystans > licznik){
				if (obszar[pole_i+1][pole_y] == 3004){
					this.ofiara = "pole"+(pole_i+1)+pole_y;
					this.tablicaSciezki[pole_i+1][pole_y] = licznik;
					this.aktualnyDystans = licznik;
					//return;
				} else if (obszar[pole_i-1][pole_y] == 3004){
					this.ofiara = "pole"+(pole_i-1)+pole_y;
					this.tablicaSciezki[pole_i-1][pole_y] = licznik;
					this.aktualnyDystans = licznik;
					//return;
				} else if (obszar[pole_i][pole_y+1] == 3004){
					this.ofiara = "pole"+pole_i+(pole_y+1);
					this.tablicaSciezki[pole_i][pole_y+1] = licznik;
					this.aktualnyDystans = licznik;
					//return;
				} else if (obszar[pole_i][pole_y-1] == 3004){
					this.ofiara = "pole"+pole_i+(pole_y-1);
					this.tablicaSciezki[pole_i][pole_y-1] = licznik;
					this.aktualnyDystans = licznik;
					//return;
				}
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
	
	public int[][] pathfinding(dynamic obszar){
		//GD.Print(this.Name);
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
		malyPathfinding(obszar, i, y, 1);

		for (int pole_i = 0; pole_i<obszar.Length; pole_i++)
		{
			for (int pole_y = 0; pole_y<obszar[pole_i].Length; pole_y++){
				//GD.Print(tablica.table[pole_i][pole_y]);
				if (obszar[pole_i][pole_y] == 0){
					obszar[pole_i][pole_y] = 9999;
				}
			}
		}
		
		//foreach(var chuj in obszar)
		//{
			//GD.Print("", string.Join(", ", chuj));
		//}
		
		return obszar;
	}
	
	public void poruszaj(){
		if (this.sekwencja.Count > 0){
			
			var tablica = GetNode<Board>("/root/Node2D/tableNode");
			
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
			
			if (tablica.table[cel_i][cel_y] != 3002 && tablica.table[cel_i][cel_y] != 3003 && tablica.table[cel_i][cel_y] != 3004){
				var cel = GetNode<PustePole>("/root/Node2D/"+this.sekwencja.Last());
				//var obrazek = GetNode<Sprite2D>("/root/Node2D/"+this.sekwencja.Last()+"/Sprite2D");
				//obrazek.Texture = null;
					
				//var tablica = GetNode<Board>("/root/Node2D/tableNode");
				var tempPosition = this.Position;
				var tempName = this.Name;
				var tempName2 = cel.Name;
					
				this.Position = cel.Position;
				cel.Position = tempPosition;
					
				cel.Name = "chuj";
				this.Name = tempName2;
				cel.Name = tempName;
				//GD.Print(this.Name);
					
				var tempID = tablica.table[ja_i][ja_y];
				tablica.table[ja_i][ja_y] = tablica.table[cel_i][cel_y];
				tablica.table[cel_i][cel_y] = tempID;
					
					
				this.sekwencja.RemoveAt(this.sekwencja.Count - 1);
					
				
				//tablica.wypisz();
				} else if (tablica.table[cel_i][cel_y] == 3004){
					
					GD.Print("lmao get robbd");
					var cywil = GetNode<Cywil>("/root/Node2D/pole"+cel_i+cel_y);
					cywil.Name = "fsdfasdf";
					cywil.QueueFree();
					
					tablica.cywileDeathList.Add(cywil);
					
					var node = GetNode<Main>("/root/Node2D");
					var domek1 = GD.Load<PackedScene>("res://puste_pole.tscn");
					var domeczek = domek1.Instantiate<PustePole>();
					domeczek.Name = "pole"+cel_i+cel_y;
					node.AddChild(domeczek);
					//GD.Print(domeczek);
					domeczek.Position = new Vector2(30*cel_i, 30*cel_y);
					tablica.table[cel_i][cel_y] = 0;
					
					node.odejmijHP();
					
					this.poruszaj();
				}
		}
	}
}
