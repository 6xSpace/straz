using Godot;
using System;

public partial class Board : Node2D
{
	int[][] table = 
	//[[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,],
	//[0,0,0,0,0,0,0,0,0,0,]] ;
	[[1,1,1,1,1,1,1,1,1,1,],
	[1,0,0,0,0,0,0,0,0,1,],
	[1,0,0,0,0,0,0,0,0,1,],
	[1,0,0,0,0,0,0,0,0,1,],
	[1,0,0,0,0,0,3,0,0,1,],
	[1,0,0,0,0,0,0,0,0,1,],
	[1,0,0,0,0,2,0,0,0,1,],
	[1,0,0,0,0,0,0,0,0,1,],
	[1,0,0,0,0,0,0,0,0,1,],
	[1,1,1,1,1,1,1,1,1,1,]] ;
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
	
	public void generuj(dynamic node){
		for (int i = 0; i<this.table.Length; i++)
		{
			for (int y = 0; y<this.table[i].Length; y++){
				if (this.table[i][y] == 1){
					GD.Print(node);
					//var domek = GetNode<Area2D>("domek");
					var domek1 = GD.Load<PackedScene>("res://domek.tscn");
					var domeczek = domek1.Instantiate<Node2D>();
					//AddChild(domek);
					//domek.Position = new Vector2(10, 15);
					
					//var parent = GetNode<Node2D>("Node2D");
					////var node2 = node;
					//var domek = new Sprite2D();
					//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek.png");
					//domek.Texture = obrazek;
					node.AddChild(domeczek);
					GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				}else if (this.table[i][y] == 2){
					GD.Print(node);
					var domek1 = GD.Load<PackedScene>("res://straznik.tscn");
					var domeczek = domek1.Instantiate<Node2D>();
					node.AddChild(domeczek);
					GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				}else if (this.table[i][y] == 3){
					GD.Print(node);
					var domek1 = GD.Load<PackedScene>("res://bandyta.tscn");
					var domeczek = domek1.Instantiate<Node2D>();
					node.AddChild(domeczek);
					GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				}
			}
			
		}
	}
}
