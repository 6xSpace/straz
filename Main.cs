using Godot;
using System;

public partial class Main : Node2D
{
	public double tempo = 1;
	public bool paused = false;
	public int tura = 0;
	public int punkty = 0;
	public int hp = 3;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("chuj");
		var table = new Board();
		table.Name = "tableNode";
		//table.tychNieCzysc.Add("chuj");
		AddChild(table);
		//table.dodajDomek(2, 1);
		//table.dodajDomek(2, 2);
		//table.dodajDomek(1, 2);
		
		table.wypisz();
		table.generuj(this);
		
		//foreach(var i in table.table)
		//{
			//foreach(var y in i){
				//GD.Print(y.ToString());
			//}
			//GD.Print("", string.Join(", ", i));
			//
		//}
		
		var napis = GetNode<RichTextLabel>("napisTura");
		napis.Text = "Tura 0";
		
		var napis2 = GetNode<RichTextLabel>("napisTempo");
		napis2.Text = "tura co: 1s";
		
		var napis3 = GetNode<RichTextLabel>("napisPunkty");
		napis3.Text = "Punkty: "+this.punkty;
		
		var napis4 = GetNode<RichTextLabel>("napisHP");
		napis4.Text = "HP: "+this.hp;
		//napis.Color = "black";
		
		//var domek = new Sprite2D();
		//var obrazek = GD.Load<Texture2D>("res://asstets/placeholder_domek.png");
		//domek.Texture = obrazek;
		//this.AddChild(domek);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	public void _on_timer_timeout(){
		//GD.Print("CHUUUUJ");
		var table = GetNode<Board>("tableNode");
		//GD.Print(table.szukacSciezki);
		if (!table.szukacSciezki){
			this.zmianaTury();
			
		}
	}
	
	public void wypisz(){
		GD.Print("chuj");
	}
	
	public void zmianaTury(){
		tura++;
		var napis = GetNode<RichTextLabel>("napisTura");
		napis.Text = "Tura "+tura;
		
		var table = GetNode<Board>("tableNode");
		table.tura();
		if (tura%7 == 1){
			table.dodajCywila();
		}
		if (tura%15 == 0){
			table.dodajBandyte();
		}
		
		if (this.hp == 0){
			var timer = GetNode<Timer>("/root/Node2D/Timer");
			timer.Stop();
			var ekran = GetNode<Sprite2D>("ekranKonca");
			ekran.Visible = true;
			var napisKonca = GetNode<RichTextLabel>("napisKonca");
			napisKonca.Visible = true;
			napisKonca.Text = "Koniec! twój wynik: " + this.punkty;
			
			this.MoveChild(ekran, -1);
			this.MoveChild(napisKonca, -1);
		}
		
	}
	
	public void dodajPunkt(){
		this.punkty += 1;
		var napis3 = GetNode<RichTextLabel>("napisPunkty");
		napis3.Text = "Punkty: "+this.punkty;
	}
	
	public void odejmijHP(){
		this.hp -= 1;
		var napis3 = GetNode<RichTextLabel>("napisHP");
		napis3.Text = "HP: "+this.hp;
	}
	
	public void _on_area_2d_input_event(Node viewport, InputEvent @event, long shapeIdx){
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				zmianaTury();
			}
		}
	}
	
	public void _on_area_2d_input_event_szybciej(Node viewport, InputEvent @event, long shapeIdx){
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				if (this.tempo > 0.25 && !this.paused){
					this.tempo -= 0.25;
					var timer = GetNode<Timer>("/root/Node2D/Timer");
					timer.WaitTime = tempo;
					var napis2 = GetNode<RichTextLabel>("napisTempo");
					napis2.Text = "tura co: "+this.tempo+"s.";
				}
				
			}
		}
	}
	
	public void _on_area_2d_input_event_wolniej(Node viewport, InputEvent @event, long shapeIdx){
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				if (this.tempo < 5 && !this.paused){
					this.tempo += 0.25;
					var timer = GetNode<Timer>("/root/Node2D/Timer");
					timer.WaitTime = tempo;
					var napis2 = GetNode<RichTextLabel>("napisTempo");
					napis2.Text = "tura co: "+this.tempo+"s.";
				}
				
			}
		}
	}
	
	public void _on_area_2d_input_event_pauza(Node viewport, InputEvent @event, long shapeIdx){
		if (@event is InputEventMouseButton mb)
		{
			if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
			{
				if (this.paused){
					this.paused = false;
					var timer = GetNode<Timer>("/root/Node2D/Timer");
					timer.Start();
					var napis2 = GetNode<RichTextLabel>("napisTempo");
					napis2.Text = "tura co: "+this.tempo+"s.";
				} else {
					this.paused = true;
					var timer = GetNode<Timer>("/root/Node2D/Timer");
					timer.Stop();
					var napis2 = GetNode<RichTextLabel>("napisTempo");
					napis2.Text = "pauza";
				}
				
			}
		}
	}
}
