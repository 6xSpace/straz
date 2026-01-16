using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
//using Random;

public partial class Cywil : Node2D
{
	List<string> sekwencja = new List<string>();
	Dictionary<string, string> cele = new Dictionary<string, string>();
	public int[][] tablicaSciezki = 
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
	
	string[] cel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		this.tablicaSciezki = tablica.tablicaSciezki.Clone() as int[][];
		
		this.szukaj();
		//foreach(var chuj in this.tablicaSciezki)
		//{
			//GD.Print("", string.Join(", ", chuj));
		//}
		//GD.Print("", string.Join(", ", this.cele));
		//GD.Print(cele.Count);
		
		if (cele.Count > 1){
			Random rand = new Random();
			int randtemp = rand.Next(0, cele.Count-1);
			this.cel = [cele.ElementAt(randtemp).Key, cele.ElementAt(randtemp).Value];
		} else if (cele.Count == 1){
			this.cel = [cele.ElementAt(0).Key, cele.ElementAt(0).Value];
		} else {
			GD.Print("idk chuj zesraj sie lmao");
			return;
		}
		
		
		//GD.Print(this.cel[1]);
		//GD.Print(this.cel[0]);
		//GD.Print("", string.Join(", ", cele));
		
		var pole_cel = GetNode<Sprite2D>("/root/Node2D/"+cel[1]+"/Sprite2D");
		var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek_cel.png");
		pole_cel.Texture = obrazek;
		
		
		string temp = this.cel[0];
		char iii = temp[4];
		string ii = Convert.ToString(iii);
		int i = Convert.ToInt32(ii);
		char yyy = temp[5];
		string yy = Convert.ToString(yyy);
		int y = Convert.ToInt32(yy);
		sekwencja.Add(this.cel[0]);
		//GD.Print(ii, yy);
		this.zrobSekwencje(this.tablicaSciezki, i, y, this.tablicaSciezki[i][y]);
		//GD.Print("", string.Join(", ", sekwencja));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void szukaj(){
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
		{
			for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
				this.tablicaSciezki[pole_i][pole_y] = tablica.table[pole_i][pole_y];
			}
		}
		this.pathfinding(this.tablicaSciezki);
		//GD.Print(this.tablicaSciezki);
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
	
	public int[][] pathfinding(dynamic obszar){
		GD.Print(this.Name);
		string temp = this.Name;
		char iii = temp[4];
		string ii = Convert.ToString(iii);
		
		int i = Convert.ToInt32(ii);
		char yyy = temp[5];
		string yy = Convert.ToString(yyy);
		int y = Convert.ToInt32(yy);
		malyPathfinding(obszar, i, y, 1);

		for (int pole_i = 0; pole_i<obszar.Length; pole_i++)
		{
			for (int pole_y = 0; pole_y<obszar[pole_i].Length; pole_y++){
				if (obszar[pole_i][pole_y] == 0){
					obszar[pole_i][pole_y] = 9999;
				}
			}
		}
		
		return obszar;
	}
	
	public void malyPathfinding(dynamic obszar, int pole_i, int pole_y, int licznik){
		//GD.Print(pole_i);
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
			
			//////////
			//GD.Print(obszar.Length);
			//GD.Print(pole_i);
			
			if (obszar[pole_i+1][pole_y] == 3001 && licznik > 3){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+(pole_i+1)+pole_y;
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					//GD.Print("taki klucz już jest");
				}
				
			} 
			if (obszar[pole_i-1][pole_y] == 3001 && licznik > 3){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+(pole_i-1)+pole_y;
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					//GD.Print("taki klucz już jest");
				}
			} 
			if (obszar[pole_i][pole_y+1] == 3001 && licznik > 3){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+pole_i+(pole_y+1);
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					//GD.Print("taki klucz już jest");
				}
			} 
			if (obszar[pole_i][pole_y-1] == 3001 && licznik > 3){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+pole_i+(pole_y-1);
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					//GD.Print("taki klucz już jest");
				}
			}
			
			//////////////
			
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
	
	public void skosIBok(int bok_i, int bok_y, int skos_i, int skos_y){
		var cel = GetNode<PustePole>("/root/Node2D/pole"+bok_i+bok_y);
		
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
			
		string temp = this.Name;
		char iii = temp[4];
		string ii = Convert.ToString(iii);
		int ja_i = Convert.ToInt32(ii);
		char yyy = temp[5];
		string yy = Convert.ToString(yyy);
		int ja_y = Convert.ToInt32(yy);
		
		var tempPosition = this.Position;
		var tempName = this.Name;
		var tempName2 = cel.Name;

		this.Position = cel.Position;
		cel.Position = tempPosition;

		cel.Name = "chuj";
		this.Name = tempName2;
		cel.Name = tempName;

		var tempID = tablica.table[ja_i][ja_y];
		tablica.table[ja_i][ja_y] = tablica.table[bok_i][bok_y];
		tablica.table[bok_i][bok_y] = tempID;

		this.sekwencja.Add("pole"+skos_i+skos_y);
	}
	
	public void krokWTyl(int tyl_i, int tyl_y){
		var cel = GetNode<PustePole>("/root/Node2D/pole"+tyl_i+tyl_y);
		
		var tablica = GetNode<Board>("/root/Node2D/tableNode");
			
		string temp = this.Name;
		char iii = temp[4];
		string ii = Convert.ToString(iii);
		int ja_i = Convert.ToInt32(ii);
		char yyy = temp[5];
		string yy = Convert.ToString(yyy);
		int ja_y = Convert.ToInt32(yy);
		
		var tempPosition = this.Position;
		var tempName = this.Name;
		var tempName2 = cel.Name;
								
		this.Position = cel.Position;
		cel.Position = tempPosition;
								
		cel.Name = "chuj";
		this.Name = tempName2;
		cel.Name = tempName;
								
		var tempID = tablica.table[ja_i][ja_y];
		tablica.table[ja_i][ja_y] = tablica.table[tyl_i][tyl_y];
		tablica.table[tyl_i][tyl_y] = tempID;
								
								
		this.sekwencja.Add(tempName);
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
			
			if (tablica.table[cel_i][cel_y] != 3002 && tablica.table[cel_i][cel_y] != 3004){
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
					bool gdziesWeszlo = false;
					if (ja_i < cel_i){
						if (tablica.table[ja_i][ja_y+1] == 0 && tablica.table[ja_i+1][ja_y+1] == 0){
							this.skosIBok(ja_i, ja_y+1, ja_i+1, ja_y+1);
							gdziesWeszlo = true;
						} else if (tablica.table[ja_i][ja_y-1] == 0 && tablica.table[ja_i+1][ja_y-1] == 0){
							this.skosIBok(ja_i, ja_y-1, ja_i+1, ja_y-1);
							gdziesWeszlo = true;
						}
					} else if (ja_i > cel_i){
						if (tablica.table[ja_i][ja_y+1] == 0 && tablica.table[ja_i-1][ja_y+1] == 0){
							this.skosIBok(ja_i, ja_y+1, ja_i-1, ja_y+1);
							gdziesWeszlo = true;
						} else if (tablica.table[ja_i][ja_y-1] == 0 && tablica.table[ja_i-1][ja_y-1] == 0){
							this.skosIBok(ja_i, ja_y-1, ja_i-1, ja_y-1);
							gdziesWeszlo = true;
						}
					} else if (ja_y < cel_y){
						if (tablica.table[ja_i+1][ja_y] == 0 && tablica.table[ja_i+1][ja_y+1] == 0){
							this.skosIBok(ja_i+1, ja_y, ja_i+1, ja_y+1);
							gdziesWeszlo = true;
						} else if (tablica.table[ja_i-1][ja_y] == 0 && tablica.table[ja_i-1][ja_y+1] == 0){
							this.skosIBok(ja_i-1, ja_y, ja_i-1, ja_y+1);
							gdziesWeszlo = true;
						}
					} else if (ja_y > cel_y){
						if (tablica.table[ja_i+1][ja_y] == 0 && tablica.table[ja_i+1][ja_y-1] == 0){
							this.skosIBok(ja_i+1, ja_y, ja_i+1, ja_y-1);
							gdziesWeszlo = true;
						} else if (tablica.table[ja_i-1][ja_y] == 0 && tablica.table[ja_i-1][ja_y-1] == 0){
							this.skosIBok(ja_i-1, ja_y, ja_i-1, ja_y-1);
							gdziesWeszlo = true;
						}
					}
					
					if (!gdziesWeszlo){
						if (ja_i < cel_i){
							this.krokWTyl(ja_i-1, ja_y);
						} else if (ja_i > cel_i){
							this.krokWTyl(ja_i+1, ja_y);
						} else if (ja_y < cel_y){
							this.krokWTyl(ja_i, ja_y-1);
						} else if (ja_y > cel_y){
							this.krokWTyl(ja_i, ja_y+1);
						} 
							
					}
				}
				
			} else {
				var pole_cel = GetNode<Sprite2D>("/root/Node2D/"+cel[1]+"/Sprite2D");
				var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek.png");
				pole_cel.Texture = obrazek;
				var tablica = GetNode<Board>("/root/Node2D/tableNode");
				tablica.cywileDeathList.Add(this);
				
				string temp = this.Name;
				char iii = temp[4];
				string ii = Convert.ToString(iii);
				int ja_i = Convert.ToInt32(ii);
				char yyy = temp[5];
				string yy = Convert.ToString(yyy);
				int ja_y = Convert.ToInt32(yy);
				
				tablica.table[ja_i][ja_y] = 0;
				this.QueueFree();
				
				var node = GetNode<Node2D>("/root/Node2D");
				var domek1 = GD.Load<PackedScene>("res://puste_pole.tscn");
				var domeczek = domek1.Instantiate<PustePole>();
				domeczek.Name = "pole"+ja_i+ja_y;
				node.AddChild(domeczek);
				//GD.Print(domeczek);
				domeczek.Position = new Vector2(30*ja_i, 30*ja_y);
			}
		}
		
		void _on_area_2d_mouse_entered(){
			GD.Print(this.Name);
		}
}
