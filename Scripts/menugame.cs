using Godot;
using System;

public class menugame : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
 public AudioStreamPlayer2D A = new AudioStreamPlayer2D();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
      
 }
	public void _on_Button_pressed()
	{
	   GD.Print("done");
	   GetTree().ChangeScene("res://pagegame.tscn");
	
	}
	public void _on_Button2_pressed()
    {
   GetTree().ChangeScene("res://optionpage.tscn");
    }
	public void _on_Button3_pressed()
{
        GetTree().Quit();
}

}












