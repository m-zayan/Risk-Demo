using Godot;
using System;

public class Options_Page : Node2D
{

    public override void _Ready()
    {

    }


    private void _on_Button_pressed()
    {
        if (Input.IsActionPressed("LM"))
        {
            GetTree().ChangeScene("res://Scenes/StartingMenu.tscn");
        }

    }

}