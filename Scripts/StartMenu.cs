using Godot;
using System;

public class StartMenu : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }


 	public override void _Process(float delta)
  	{
      
  	}

    private void _on_Button_pressed(string button)
    {
        if (button == "Play")
        {
            GetTree().ChangeScene("res://Scenes/Battle-Field.tscn");
        }
        if (button == "Options")
        {
            GetTree().ChangeScene("res://Scenes/Options-Page.tscn");

       
        }
        if (button == "Exit")
        {
            GetTree().Quit();
        }
    }



}


