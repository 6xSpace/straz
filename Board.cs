using Godot;
using System;
using System.Collections.Generic;

public partial class Board : Node2D
{

	public bool wylaczKlikanie = false;


	public int[][] table = 
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
	[[3001,3001,3001,3001,3001,3001,3001,3001,3001,3001,],
	[3001,0,0,0,0,0,0,0,0,3001,],
	[3001,0,0,0,0,0,0,0,0,3001,],
	[3001,0,0,3004,0,0,0,0,0,3001,],
	[3001,0,0,0,0,0,3003,0,0,3001,],
	[3001,0,0,0,0,0,0,0,0,3001,],
	[3001,0,0,0,0,3002,0,0,0,3001,],
	[3001,0,0,0,0,0,0,0,0,3001,],
	[3001,0,3002,0,0,0,0,0,0,3001,],
	[3001,3001,3001,3001,3001,3001,3001,3001,3001,3001,]] ;
	//0-puste, 3001-domek, 3002-straznik, 3003-bandyta, 3004-cywil
	
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
	
	public bool szukacSciezki = false;
	
	public List<string> szukajacy = new List<string>();
	
	public List<string> tychNieCzysc = new List<string>();
	
	public List<straznik> straznicy = new List<straznik>();
	
	public void dodajDomek(int x, int y){
		this.table[y][x] = 3001;
		//GD.Print(this.table);
		
		//GD.Print("Moja ścieżka: " + GetPath());              // ścieżka tego węzła
	//if (GetParent() != null)
		//GD.Print("Parent: " + GetParent().GetPath() + " typ: " + GetParent().GetType().Name);

	}
	public void dodajStraznika(int x, int y){
		this.table[y][x] = 3002;
	}
	public void dodajBandyte(int x, int y){
		this.table[y][x] = 3003;
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
	
	public int[][] getter(){
		return table;
	}
	
	public void tura(){
		//GD.Print("chuj");
		foreach (var straznik in this.straznicy){
			straznik.poruszaj();
		}
	}
	
	public void generuj(dynamic node){
		for (int i = 0; i<this.table.Length; i++)
		{
			for (int y = 0; y<this.table[i].Length; y++){
				if (this.table[i][y] == 3001){
					//GD.Print(node);
					//var domek = GetNode<Area2D>("domek");
					var domek1 = GD.Load<PackedScene>("res://domek.tscn");
					var domeczek = domek1.Instantiate<Domek>();
					//AddChild(domek);
					//domek.Position = new Vector2(10, 15);
					
					//var parent = GetNode<Node2D>("Node2D");
					////var node2 = node;
					//var domek = new Sprite2D();
					//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek.png");
					//domek.Texture = obrazek;
					domeczek.Name = "pole"+i+y;
					node.AddChild(domeczek);
					//GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				}else if (this.table[i][y] == 3002){
					//GD.Print(node);
					var domek1 = GD.Load<PackedScene>("res://straznik.tscn");
					var domeczek = domek1.Instantiate<straznik>();
					domeczek.Name = "pole"+i+y;
					node.AddChild(domeczek);
					//GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
					this.straznicy.Add(domeczek);
					GD.Print("", string.Join(", ", this.straznicy));
					//foreach (var straznik in this.straznicy){
						//straznik.poruszaj();
					//}
					
				}else if (this.table[i][y] == 3003){
					//GD.Print(node);
					var domek1 = GD.Load<PackedScene>("res://bandyta.tscn");
					var domeczek = domek1.Instantiate<Bandyta>();
					domeczek.Name = "pole"+i+y;
					node.AddChild(domeczek);
					//GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				} else if (this.table[i][y] == 3004){
					//GD.Print(node);
					var domek1 = GD.Load<PackedScene>("res://Cywil.tscn");
					var domeczek = domek1.Instantiate<Cywil>();
					domeczek.Name = "pole"+i+y;
					node.AddChild(domeczek);
					//GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				} else if (this.table[i][y] == 0){
					//GD.Print(node);
					var domek1 = GD.Load<PackedScene>("res://puste_pole.tscn");
					var domeczek = domek1.Instantiate<PustePole>();
					domeczek.Name = "pole"+i+y;
					node.AddChild(domeczek);
					//GD.Print(domeczek);
					domeczek.Position = new Vector2(30*i, 30*y);
					
				}
			}
			
		}
	}
}
