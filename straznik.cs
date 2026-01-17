using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class straznik : Node2D
{
	bool klikniety = false;
	List<string> sekwencja = new List<string>();
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
				var tablica = GetNode<Board>("/root/Node2D/tableNode");
				
				//TO JEST ULTRA KURA GŁUPIE, JEBAĆ TYPY REFERENCYJNE ISTG
				for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
				{
					for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
						this.tablicaSciezki[pole_i][pole_y] = tablica.table[pole_i][pole_y];
					}
				}
				this.pathfinding(this.tablicaSciezki);
				
					if (this.sekwencja.Count == 0){
						if (klikniety == false){
						klikniety = true;
						tablica.szukacSciezki = true;
						if (tablica.szukajacy.Count == 0){
							tablica.szukajacy.Add(this.Name);
						} else {
							tablica.szukajacy[0] = this.Name;
						}
						var sprite = GetNode<Sprite2D>("Sprite2D");
						var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik_klik.png");
						sprite.Texture = obrazek;
						
						var timer = GetNode<Timer>("/root/Node2D/Timer");
						if (!timer.IsStopped()){
							timer.Stop();
							
						}
						
						tablica.wylaczKlikanie = false;
					} else {
						klikniety = false;
						tablica.szukacSciezki = false;
						tablica.szukajacy.RemoveAt(0);
						var sprite = GetNode<Sprite2D>("Sprite2D");
						var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
						sprite.Texture = obrazek;
						
						var timer = GetNode<Timer>("/root/Node2D/Timer");
						var tata = GetNode<Main>("/root/Node2D");
						if (!tata.paused){
							timer.Start();
							
						}
						
						tablica.wylaczKlikanie = true;
						
					}
				}
				
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
	public static void malyPathfinding(dynamic obszar, int pole_i, int pole_y, int licznik){
		if (obszar[pole_i+1][pole_y] == 0 || (obszar[pole_i+1][pole_y] > licznik && obszar[pole_i+1][pole_y] < 3000) || obszar[pole_i+1][pole_y] == 3003){
			obszar[pole_i+1][pole_y] = licznik;
		} 
		if (obszar[pole_i-1][pole_y] == 0 || (obszar[pole_i-1][pole_y] > licznik && obszar[pole_i-1][pole_y] < 3000 || obszar[pole_i-1][pole_y] == 3003)){
			obszar[pole_i-1][pole_y] = licznik;
		} 
		if (obszar[pole_i][pole_y+1] == 0 || (obszar[pole_i][pole_y+1] > licznik && obszar[pole_i][pole_y+1] < 3000 || obszar[pole_i][pole_y+1] == 3003)){
			obszar[pole_i][pole_y+1] = licznik;
		} 
		if (obszar[pole_i][pole_y-1] == 0 || (obszar[pole_i][pole_y-1] > licznik && obszar[pole_i][pole_y-1] < 3000 || obszar[pole_i][pole_y-1] == 3003)){
			obszar[pole_i][pole_y-1] = licznik;
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
		//GD.Print(string.Join(", ", sekwencja));
		foreach (var item in sekwencja){
			this.sekwencja.Add(item);
		}

		var tablica = GetNode<Board>("/root/Node2D/tableNode");
		klikniety = false;
		tablica.szukacSciezki = false;
	}
	
	public void poruszaj(){
		
		
		//GD.Print(this.sekwencja);
		//GD.Print("straznik z "+this.Name);
		//GD.Print("", string.Join(", ", this.sekwencja));
		if (this.sekwencja.Count > 0){
			
			var tablica = GetNode<Board>("/root/Node2D/tableNode");
			
			//for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
			//{
				//for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
					////GD.Print(tablica.table[pole_i][pole_y]);
					//if (tablica.table[pole_i][pole_y] == 0){
						//if (tablica.tychNieCzysc.Contains("pole"+pole_i+pole_y)){
							//var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
							//var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
							//pole.Texture = obrazek;
						//}
					//}
				//}
						//
			//}
			
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
			
			if (tablica.table[cel_i][cel_y] != 3002 && tablica.table[cel_i][cel_y] != 3004 && tablica.table[cel_i][cel_y] != 3003){
				var cel = GetNode<PustePole>("/root/Node2D/"+this.sekwencja.Last());
					var obrazek = GetNode<Sprite2D>("/root/Node2D/"+this.sekwencja.Last()+"/Sprite2D");
					obrazek.Texture = null;
					
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
					
					////////////////////////////////////////////////////
					//GD.Print(tempID);
					tablica.table[ja_i][ja_y] = tablica.table[cel_i][cel_y];
					tablica.table[cel_i][cel_y] = tempID;
					
					//tablica.tychNieCzysc.RemoveAt(this.sekwencja.Count - 1);
					List<string> templist = new List<string>();
					templist.Add(this.sekwencja[(this.sekwencja.Count - 1)]);
					//GD.Print(this.sekwencja[this.sekwencja.Count-1]);
					//GD.Print("", string.Join(", ", tablica.tychNieCzysc));
					var duplicates = tablica.tychNieCzysc.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key);
					
					tablica.tychNieCzysc = tablica.tychNieCzysc.Except(templist).ToList();
					//GD.Print("", string.Join(", ", tablica.tychNieCzysc));
					//GD.Print("", string.Join(", ", duplicates));
					//if (duplicates.Contains(this.sekwencja[this.sekwencja.Count-1])){
						//tablica.tychNieCzysc.Add(this.sekwencja[this.sekwencja.Count-1]);
					//}
					//var duplicates = tablica.tychNieCzysc.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key);
					
					foreach (var item in duplicates){
						tablica.tychNieCzysc.Add(item);
					}
					
					//GD.Print(this.sekwencja[this.sekwencja.Count-1]);
					//GD.Print("", string.Join(", ", tablica.tychNieCzysc));
					this.sekwencja.RemoveAt(this.sekwencja.Count - 1);
					
					if (this.sekwencja.Count == 0){
						var sprite = GetNode<Sprite2D>("Sprite2D");
						var obrazek1 = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
						sprite.Texture = obrazek1;
						
						tablica.wylaczKlikanie = false;
						if (tablica.szukajacy.Count > 0){
							tablica.szukajacy.RemoveAt(0);
							
						}
					}
					//tablica.wypisz();
				} else if (tablica.table[cel_i][cel_y] != 3002) {
					//GD.Print("stoję! moja sekwencja: ");
					//GD.Print("", string.Join(", ", this.sekwencja));
					
					//rozwiązanie typu bumper
					//zderzenie wyłącza strażnikom ruch
					
					//foreach (var item in tablica.straznicy){
						//if (item.Name == this.sekwencja.Last()){
							//item.poruszaj();
							//item.stop = true;
							////this.poruszaj();
						//}
					//}
					var duplicates = tablica.tychNieCzysc.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key);
					
					foreach (var item in tablica.straznicy){
						if (this.sekwencja.Count != 0){
							if (item.Name == this.sekwencja.Last()){
								
							if (item.sekwencja.Count() == 0){
								tablica.tychNieCzysc = tablica.tychNieCzysc.Except(this.sekwencja).ToList();
								foreach (var item1 in duplicates){
									tablica.tychNieCzysc.Add(item1);
									
								}
								this.sekwencja.Clear();
								var sprite = GetNode<Sprite2D>("Sprite2D");
								var obrazek1 = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
								sprite.Texture = obrazek1;
									
								tablica.wylaczKlikanie = false;
								if (tablica.szukajacy.Count > 0){
									tablica.szukajacy.RemoveAt(0);
								}
							} else if (this.Name == item.sekwencja.Last()){
								
								tablica.tychNieCzysc = tablica.tychNieCzysc.Except(this.sekwencja).ToList();
								tablica.tychNieCzysc = tablica.tychNieCzysc.Except(item.sekwencja).ToList();
								
								
								this.sekwencja.Clear();
								var sprite = GetNode<Sprite2D>("Sprite2D");
								var obrazek1 = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
								sprite.Texture = obrazek1;
									
								tablica.wylaczKlikanie = false;
								if (tablica.szukajacy.Count > 0){
									tablica.szukajacy.RemoveAt(0);
								}
								
								item.sekwencja.Clear();
								var sprite2 = GetNode<Sprite2D>("/root/Node2D/" + item.Name + "/Sprite2D");
								//var obrazek2 = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
								sprite2.Texture = obrazek1;
									
								foreach (var item1 in duplicates){
									tablica.tychNieCzysc.Add(item1);
								}
									
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
							}
							//item.poruszaj();
							//item.stop = true;
							//this.poruszaj();
						}
						}
						
					}
					//this.sekwencja.Clear();
					//var sprite = GetNode<Sprite2D>("Sprite2D");
					//var obrazek1 = GD.Load<Texture2D>("res://asstets/placeholder_straznik.png");
					//sprite.Texture = obrazek1;
						//
					//tablica.wylaczKlikanie = false;
					//if (tablica.szukajacy.Count > 0){
						//tablica.szukajacy.RemoveAt(0);
					//}
					
					
				} else if (tablica.table[cel_i][cel_y] != 3004){
					GD.Print("czekaj!");
				} else if (tablica.table[cel_i][cel_y] != 3003){
					GD.Print("dorwać go!");
					var bandyta = GetNode<Bandyta>("/root/Node2D/pole"+cel_i+cel_y);
					bandyta.QueueFree();
				}
				
				//GD.Print("to ja! strażnik z "+this.Name);
				//GD.Print("zostawiam po sobie tychnieczysc w takim stanie: ");
				//GD.Print("", string.Join(", ", tablica.tychNieCzysc));
				
				for (int pole_i = 0; pole_i<tablica.table.Length; pole_i++)
			{
				for (int pole_y = 0; pole_y<tablica.table[pole_i].Length; pole_y++){
					//GD.Print(tablica.table[pole_i][pole_y]);
					if (tablica.table[pole_i][pole_y] == 0){
						if (tablica.tychNieCzysc.Contains("pole"+pole_i+pole_y)){
							var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
							var obrazek = GD.Load<Texture2D>("res://asstets/puste_klik.png");
							pole.Texture = obrazek;
						} else {
							var pole = GetNode<Sprite2D>("/root/Node2D/pole"+pole_i+pole_y+"/Sprite2D");
							pole.Texture = null;
						}
					}
				}
						
			}
			}
			
			
	
	}
}
