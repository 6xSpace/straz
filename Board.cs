using Godot;
using System;

public partial class Board : Node2D
{
	int[][] table = 
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
	//0-puste, 1-domek, 2-straznik, 3-bandyta
	
	public void dodajDomek(int x, int y){
		this.table[y][x] = 1;
		//GD.Print(this.table);
	}
	public void dodajStraznika(int x, int y){
		this.table[y][x] = 2;
	}
	public void dodajBandyte(int x, int y){
		this.table[y][x] = 3;
	}
	
	public void wypisz(){
		foreach(var i in this.table)
		{
			//foreach(var y in i){
				//GD.Print(y.ToString());
			//}
			GD.Print("", string.Join(", ", i));
			
		}
	}
}
