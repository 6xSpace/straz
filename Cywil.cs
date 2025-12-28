using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
//using Random;

public partial class Cywil : Node2D
{
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
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.szukaj();
		foreach(var chuj in this.tablicaSciezki)
		{
			GD.Print("", string.Join(", ", chuj));
		}
		//GD.Print("", string.Join(", ", this.cele));
		Random rand = new Random();
		string[] cel = [cele.ElementAt(rand.Next(0, cele.Count)).Key, cele.ElementAt(rand.Next(0, cele.Count)).Key];
		GD.Print(cel[0]);
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
		GD.Print(this.tablicaSciezki);
	}
	
	public int[][] pathfinding(dynamic obszar){
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
		GD.Print(pole_i);
		if (obszar.Length > pole_i+2 && obszar[0].Length > pole_y+1 && pole_i > 0 && pole_y >0){
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
			
			if (obszar[pole_i+1][pole_y] == 3001){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+(pole_i+1)+pole_y;
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					GD.Print("taki klucz już jest");
				}
				
			} 
			if (obszar[pole_i-1][pole_y] == 3001){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+(pole_i-1)+pole_y;
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					GD.Print("taki klucz już jest");
				}
			} 
			if (obszar[pole_i][pole_y+1] == 3001){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+pole_i+(pole_y+1);
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					GD.Print("taki klucz już jest");
				}
			} 
			if (obszar[pole_i][pole_y-1] == 3001){
				try{
					string temp1 = "pole"+pole_i+pole_y;
					string temp2 = "pole"+pole_i+(pole_y-1);
				
					this.cele.Add(temp1, temp2);
				}
				catch (ArgumentException)
				{
					GD.Print("taki klucz już jest");
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
}
