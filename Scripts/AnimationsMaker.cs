using Godot;
using System.Collections.Generic;
using Godot.Collections;

namespace RiskGame.Scripts
{
    public partial class Game
    {

            public void __initZoom()
            {

                try
                {
                    AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
                    Animation animation = ZCamera.GetAnimation("Zoom");

                    int idx = 0;
                    float value = 1.0f;
                    for (int i = 0; i < 22; i++)
                    {
                        animation.TrackSetKeyValue(idx, i, new Vector2(value, value));
                       // GD.Print(animation.TrackGetKeyValue(idx, i));
                        value -= (1 / 60f);
                    }
                }
                catch
                {
                    GD.Print("Null Reference Error");
                }

            }
        public void Zoom(Vector2 pos, string name)
        {

            AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
            Animation animation = ZCamera.GetAnimation("Zoom");
            Vector2 camera_pos = (GetNode("ZCamera/Camera2D") as Camera2D).Position;
            GD.Print(camera_pos);
            float distance = pos.DistanceTo(camera_pos);
            Vector2 speed = new Vector2(distance / 22, distance / 22);
            Vector2 dir = pos.DirectionTo(camera_pos);

            Vector2 newPos = camera_pos;

            for (int i = 0; i < 22; i++)
            {
                animation.TrackSetKeyValue(1, i, newPos);
                newPos -= (dir * speed);
            }
            ZCamera.Play("Zoom");

        }

        public Animation AddAnimation(int name)
            {
                Animation animation = new Animation();
                int index = 0;
                foreach (int neighbor in map[name])
                {
					try{
                    if (countries[neighbor].owner.id != countries[name].owner.id)
                    {
                        animation.AddTrack(Animation.TrackType.Value);
                        animation.TrackSetPath(index, $"{(neighbor)}:modulate");

                        animation.SetLength(2.4f);
                        animation.SetStep(0.6f);
                        animation.SetLoop(true);
                        animation.TrackInsertKey(index, 0, new Color());
                        animation.TrackSetKeyValue(index, 0,countries[neighbor].owner.color);// Owner Color Not Found

                        animation.TrackInsertKey(index, 0.6f, new Color());
                        animation.TrackSetKeyValue(index, 1, Colors.Black);

                        animation.TrackInsertKey(index, 1.2f, new Color());
                        animation.TrackSetKeyValue(index, 2, countries[neighbor].owner.color);


                        animation.TrackInsertKey(index, 1.8f, new Color());
                        animation.TrackSetKeyValue(index, 3, Colors.Black);

                        animation.TrackInsertKey(index, 2.4f, new Color());
                        animation.TrackSetKeyValue(index, 4, countries[neighbor].owner.color);
                        index++;
                    }

                }
                catch(System.Exception e)
                {
                    GD.Print("Null Reference Error Please Check Ready Function Note");
                }
            }
            return animation;
        }
            public void __Init__AttackAnimation()
            {
                AnimationPlayer AttackPlayer = GetNode("Attack") as AnimationPlayer;



                for (int name = 0; name < 42; name++)
                {
                    AttackPlayer.AddAnimation(name.ToString(), AddAnimation(name));
               
                }
            }
        public void AttackAnimation(string name)
        {
            AnimationPlayer animation = GetNode("Attack") as AnimationPlayer;
            animation.Play(name);
        }
        public void ChangeMode(string Mode)
        {
            string[] Modes = { "Draft", "Attack", "Fortify" };

            Texture Active = ResourceLoader.Load("res://Images/GameUI/Layer.png") as Texture;
            Texture None = ResourceLoader.Load("res://Images/GameUI/Layer2.png") as Texture;

            RichTextLabel modeTextLabel = GetNode("OnReady/Mode") as RichTextLabel;
            modeTextLabel.Text = Mode;



            foreach(string mode in Modes)
            {
                TextureRect ModeMarker = GetNode($"OnReady/{mode}Marker") as TextureRect;
                if(mode==Mode)
                    ModeMarker.SetTexture(Active);

                else
                    ModeMarker.SetTexture(None);
            }



        }

        public void PlayerTurn(string Player)
        {
            RichTextLabel PlayerLabel = GetNode("OnReady/PlayerId") as RichTextLabel;
            PlayerLabel.Text = Player;
        }

        public void RollDice()
        {
            AnimationPlayer player = GetNode("DicePlayer") as AnimationPlayer;


            player.Play("Dice");
        }

        public void ShowDiceValue(int For1, int For2)
        {
            Sprite sprite1 = GetNode("DicePlayer/Dice") as Sprite;
            Sprite sprite2 = GetNode("DicePlayer/Dice2") as Sprite;


            sprite1.SetFrame(For1 - 1);
            sprite2.SetFrame(For2 - 1);
        }
        ////////////////////////////////Sound/////////////////////////////////////////
        public void ClikedSound()
        {
            AnimationPlayer sound = GetNode("OnReady") as AnimationPlayer;

            sound.Play("Click");
        }
        public void GameSound(bool play)
        {
            AnimationPlayer sound = GetNode("OnReady") as AnimationPlayer;

            AudioStreamPlayer audio = GetNode("OnReady/Game") as AudioStreamPlayer;


            AudioStream Stream1 = ResourceLoader.Load("res://Sound/Heartbeat Suspense/hbs.wav") as AudioStream;
            AudioStream Stream2 = ResourceLoader.Load("res://Sound/Battle-Field-Sound.wav") as AudioStream;
            if (play)
            {
                audio.SetStream(Stream2);
            }
            else
            {
                audio.SetStream(Stream1);
            }

            sound.Play("Battle-Field");
        }


        /////////////////////////////// Colors ////////////////////////////////////////
        public void ToLight(string name)
        {
            if (players[countries[int.Parse(name)].owner.id].color == Colors.DarkRed)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.Red;
            }
            else if (players[countries[int.Parse(name)].owner.id].color == Colors.DarkGreen)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.Green;


            }
            else if (players[countries[int.Parse(name)].owner.id].color == Colors.DarkBlue)
            {

                players[countries[int.Parse(name)].owner.id].color = Colors.Blue;

            }
            Brush(name);
        }
        public void ToDark(string name)
        {

            if (players[countries[int.Parse(name)].owner.id].color == Colors.Red)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.DarkRed;


            }
            else if (players[countries[int.Parse(name)].owner.id].color == Colors.Green)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.DarkGreen;


            }
            else if (countries[int.Parse(name)].owner.color == Colors.Blue)
            {
                players[countries[int.Parse(name)].owner.id].color = Colors.DarkBlue;


            }
            Brush(name);
        }
        public void Brush(string name)
        {
            Sprite mySprite = (GetNode((name)) as Sprite);
            try
            {
                mySprite.Modulate = players[countries[int.Parse(name)].owner.id].color;
                mySprite.Update();
                //GD.Print("7a7a");
            }
            catch { GD.Print("Country not found, please add it first"); }
        }

        public void Rest()
        {

            AnimationPlayer ZCamera = GetNode("ZCamera") as AnimationPlayer;
            ZCamera.Stop();
            AnimationPlayer animation = GetNode("Attack") as AnimationPlayer;
            animation.Stop(false);
            for (int i = 0; i < 42; i++)
            {

                ToDark(i.ToString());

            }
        }

    }

}
