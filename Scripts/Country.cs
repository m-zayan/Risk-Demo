using System;
using Godot.Collections;
using Godot;
namespace RiskGame.Scripts
{
    public partial class Game
    {
        public int Attacker_Dice, Attacked_Dice;

        public class Country
        {

            public int Active { get; set; } //Should be int 0 or 1 or 2
            public Player owner = new Player();
            public int troops;
            public Texture texture = ResourceLoader.Load("res://Images/GameUI/Number.png") as Texture;
            public TextureRect numberOfTroops = new TextureRect();
            public RichTextLabel numberOfTroopsTxt = new RichTextLabel();

            public Country()
            {
                DynamicFont font = new DynamicFont();
                DynamicFontData data = new DynamicFontData();
                data.SetFontPath("res://Fonts/good times rg.ttf");
                font.SetFontData(data);
                font.Size = 20;
                font.UseFilter = true;

                numberOfTroopsTxt.RectSize = new Vector2(30, 30);
                numberOfTroopsTxt.AddFontOverride("normal_font",font);
                numberOfTroopsTxt.PushAlign(RichTextLabel.Align.Center);

                numberOfTroops.Texture = texture;
                owner = null;
                troops = 0;
            }
        }
            public void LoseCountry(Player newone,ref Country enemy, int sol)
            {
            
                enemy.owner = newone;
                enemy.troops = sol;
                enemy.numberOfTroopsTxt.Text = (sol.ToString());
            enemy.numberOfTroops.Modulate = newone.color.Lightened(0.3f);
            Rest();
            __Init__AttackAnimation();

            }
        public bool Attack(ref Country enemy, ref Country country, int x = 1)
        {
            Sprite sprite1 = (GetNode("sliderImage") as Sprite);
            Button AtackBTN = (GetNode("sliderImage/Attackk") as Button);
            bool res = false;
            GD.Print($"enter number of troops to fight from 1 to {country.troops - 1}");
            ////////////should take the input from gui


            GD.Print("x = " + x.ToString());
            System.Random rand = new System.Random();
            for (int i = 0; i < x && enemy.troops > 0 && country.troops >= 2; i++)
            {
                Attacker_Dice = rand.Next(1, 6);
                Attacked_Dice = rand.Next(1, 6);

                RollDice();

                GD.Print($"Attacker Dice: {Attacker_Dice} , Attacked Dice {Attacked_Dice} ");
                if (Attacker_Dice > Attacked_Dice)
                {
                    enemy.troops--;
                    enemy.numberOfTroopsTxt.Text = (enemy.troops.ToString());
                }
                else
                {
                    country.troops--;
                    enemy.numberOfTroopsTxt.Text = (country.troops.ToString());
                }
            }



            if (enemy.troops == 0)
            {

                GD.Print($"player {country.owner.id} has take country from player {enemy.owner.id}");
                int f;

                GD.Print("how many toops you want to transport");
                ////////////should take input from gui
                f = 1;

                enemy.owner.countries--;
                LoseCountry(country.owner, ref enemy, f);
                res = true;
                country.troops -= f;
                country.numberOfTroopsTxt.Text = (country.troops.ToString());
                country.owner.countries++;
            }
            sprite1.Hide();
            AtackBTN.Hide();
            ReCalculateTroops();
            Rest();
            if(country.owner.countries==42)
            {
                GetTree().Quit();
            }
            return res;
        }
    }

}
