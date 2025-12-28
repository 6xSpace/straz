using Godot;
using System;

public partial class Cywil : Node2D
{
	
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
	
	public void szukaj(){
		
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
	
	public static void malyPathfinding(dynamic obszar, int pole_i, int pole_y, int licznik){
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
