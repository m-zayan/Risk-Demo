using Godot;
using System.Collections.Generic;
using Godot.Collections;
namespace RiskGame.Scripts
{

    public partial class Game : Sprite
    {
        //organize turns
        Country mycontry, enmey;
        private int startLevel = 0, coun1, coun2;
        private int selectedTroobs;
        private int turn;
        private int selected = -1;
        private List<int> enemies;
        //graph of the game
        private  List<int>[] map = new List<int>[42];
        private List<int>[] content = new List<int>[6];
        private Country[] countries = new Country[42];
        private Player[] players = new Player[3];
        private Player curplayer;
        private int mode = 0;


        public void ChangeTxtTroops(Player player,int troops)
        {

            RichTextLabel notUsedTroopsLabel = GetNode("OnReady/PlayerName") as RichTextLabel;

        
            notUsedTroopsLabel.Text = "Player Not Used Troops : " + player.notusedTroops.ToString();
          
        }
        public override void _Ready()
        { 
            
            /* 
             **************************************************************************************************
             * Please be careful of functions dependencies So we can't swap the places of animation functions**
             * ************************************************************************************************            
            */
            turn = 0;
            curplayer = new Player();
            Fillcont();
            FillMap();
            InitPlayers();
            InitCountries();
            Disturb();
            __Init__AttackAnimation();
            __initZoom();
            DrawTroops();

            StartTurn();
            ReCalculateTroops();

        }


        public void Fillcont()
        {

            content[0] = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 12 };
            //0, 2 ,3 ,12           
            content[1] = new List<int>() { 8, 9, 10, 11 };
            //0 ,1, 3, 4          
            content[2] = new List<int>() { 13, 14, 15, 16, 17, 18, 19 };
            //1, 2 ,4 ,6 ,5 ,12
            content[3] = new List<int>() { 21, 22, 23, 24, 25, 26 };
            //2 ,3, 7, 6          
            content[4] = new List<int>() { 20, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };
            //6 ,3, 12          
            content[5] = new List<int>() { 38, 39, 40, 41 };
        }

        public void DrawTroops()
        {

            var Scene = GetNode(".");
            
            for( int index = 0; index < 42; index++)
            {
                countries[index].numberOfTroops.RectPosition = (GetNode(index.ToString()) as Sprite).GetPosition() - new Vector2(25,25);
                countries[index].numberOfTroops.Modulate = countries[index].owner.color.Lightened(0.3f);

                countries[index].numberOfTroopsTxt.RectPosition = (GetNode(index.ToString()) as Sprite).GetPosition() - new Vector2(15, 15);
                Scene.AddChild(countries[index].numberOfTroops);

                Scene.AddChild(countries[index].numberOfTroopsTxt);
            }
            countries[38].numberOfTroops.RectPosition += new Vector2(45, 0);
            countries[38].numberOfTroopsTxt.RectPosition += new Vector2(45, 0);

            countries[27].numberOfTroops.RectPosition += new Vector2(-15, 0);
            //countries[27].numberOfTroopsTxt.RectPosition += new Vector2(45, 0);
        }

        private void CheckMap()
        {
            for (int i = 0; i < 42; i++)
            {
                for (int ii = 0; ii < map[i].Count; ii++)
                {
                    bool found = false;
                    for (int iii = 0; iii < map[map[i][ii]].Count; iii++)
                    {
                        if (map[map[i][ii]][iii] == i)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        System.Console.WriteLine("{0},{1}", i, map[i][ii]);
                    }
                }
            }
        }
        private void InitPlayers()
        {
            for (int i = 0; i < 3; i++)
            {
                players[i] = new Player();
                players[i].id = i;
            }
            players[0].color = Colors.DarkGreen;
            players[1].color = Colors.DarkRed;
            players[2].color = Colors.DarkBlue;
        }
        private void InitCountries()
        {
            for (int i = 0; i < 42; i++)
            {
                countries[i] = new Country();
            }
        }
        private void Disturb()
        {
            System.Random rand = new System.Random();
            bool[] arr = new bool[42];
            List<int>[] arr2 = new List<int>[3];
            for (int i = 0; i < 3; i++)
            {
                arr2[i] = new List<int>();
            }
            for (int i = 0; i < 2; i++)
            {
                while (players[i].countries < 14)
                {
                    int f = rand.Next(0, 41);
                    // System.Console.WriteLine(f);
                    if (arr[f] == false)
                    {
                        arr[f] = true;
                        AddCountry(i, f.ToString());
                        arr2[i].Add(f);
                    }

                }
            }

            for (int i = 0; i < 42; i++)
            {
                if (arr[i] == false)
                {
                    arr[i] = true;
                    AddCountry(2, i.ToString());
                    arr2[2].Add(i);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int ii = 0; ii < arr2[i].Count; ii++)
                {
                    int f = rand.Next(0, players[i].notusedTroops / 3);
                    countries[arr2[i][ii]].troops += f;
                    players[i].notusedTroops -= f;
                    if (players[i].notusedTroops == 0) break;
                }
                if (players[i].notusedTroops > 0)
                {
                    while (players[i].notusedTroops > 0)
                    {
                        int f = rand.Next(0, 13);
                        countries[arr2[i][f]].troops += 1;
                        players[i].notusedTroops -= 1;
                        
                    }
                }
            }

        }
        public void ReCalculateTroops()
        {
            for (int i = 0; i < 42; i++)
            {
                countries[i].numberOfTroopsTxt.Text = countries[i].troops.ToString();
            }
        }
        public void AddCountry(int id, string name)
        {
            countries[int.Parse(name)].owner = players[id];
            countries[int.Parse(name)].troops++;
            countries[int.Parse(name)].numberOfTroopsTxt.Text=(countries[int.Parse(name)].troops.ToString());
            players[id].countries++;
            players[id].notusedTroops--;
            ToDark(name);
            //GD.Print(id);

        }


  private void Draft(int a, int numberOfTroops=1)
        {
            ChangeTxtTroops(players[turn], players[turn].notusedTroops);
            HSlider slider = (GetNode("sliderImage/soldireSlider") as HSlider);
            Label label = (GetNode("sliderImage/Label") as Label);
            Label label1 = (GetNode("Label2") as Label);
            Sprite sprite1 = (GetNode("sliderImage") as Sprite);
            if (startLevel == 0)
            {
                slider.MinValue = 0;
                slider.MaxValue = players[turn % 3].notusedTroops;
                sprite1.Show();
                label1.Text = players[turn % 3].notusedTroops.ToString();
                startLevel++;
            }
            else if(startLevel==1)
            {

                // GD.Print("enter  num of troops ");
                //////////should take the number from gui
                int b = 1;
                b = numberOfTroops;

                // b = int.Parse(System.Console.ReadLine());
                if (b <= players[turn % 3].notusedTroops && countries[a].owner == players[turn % 3])
                {
                    players[turn % 3].notusedTroops -= b;

                    ChangeTxtTroops(players[turn], players[turn].notusedTroops);
                    // players[turn % 3].usedTroops += b;
                    countries[a].troops += b;
                    sprite1.Hide();
                    startLevel = 0;
                    label1.Text = players[turn % 3].notusedTroops.ToString();
                    if (players[turn % 3].notusedTroops == 0)
                    {
                        label1.Hide();
                        startLevel = -1;
                    }
                }
                countries[a].numberOfTroopsTxt.Text = (countries[a].troops.ToString());
                countries[b].numberOfTroopsTxt.Text = (countries[b].troops.ToString());
            }

        }
        private void Attack(int Attacker, int Attacked)
        {

            GD.Print("Attack");

            if (countries[Attacked].owner != curplayer)
                if (Attack(ref countries[Attacked], ref countries[Attacker]))
                {
                    Brush(Attacked.ToString());
                    for (int i = 0; i < 5; i++)
                    {
                        bool b = false;
                        for (int j = 0; j < content[i].Count; j++)
                        {
                            if (Attacked == content[i][j])
                            {
                                players[turn % 3].card[i]++;
                                if (players[turn % 3].card[i] == 3)
                                {
                                    players[turn % 3].hasCard++;

                                }
                                b = true;
                                break;
                            }

                        }
                        if (b)
                            break;
                    }


                }


        }
        //searches for all countries 1 if you want friend one,0 if you want enimies
        private void ViewAllCities(int state)
        {
            bool[] vis = new bool[42];
            for (int i = 0; i < 42; i++) vis[i] = false;
            DFS(0, vis, state);
        }
        private void DFS(int cur, bool[] vis, int state)
        {
            if (vis[cur] == true) return;
            vis[cur] = true;
            if (state == 1 && countries[cur].owner == players[turn % 3])
            {
                //run any function
                GD.Print(cur);
                ToLight(cur.ToString());
            }
            else if (state == 0 && countries[cur].owner != players[turn % 3])
            {
                System.Console.Write("{0} ", cur);
                //run any function
            }
            for (int i = 0; i < map[cur].Count; i++)
            {
                DFS(map[cur][i], vis, state);
            }
        }
        private void Fortify(int a, int b, int c = 1)
        {
            Sprite sprite1 = (GetNode("sliderImage") as Sprite);
            Button AtackBTN = (GetNode("sliderImage/Attackk") as Button);
            /////////should read the input from gui

            GD.Print($"enter num of troops from 1 to {countries[a].troops - 1}");
            /////////should read the input from gui


            if (countries[a].troops - c > 1)
            {
                countries[a].troops -= c;
                countries[b].troops += c;
            }
            sprite1.Hide();
            AtackBTN.Hide();
            countries[a].numberOfTroopsTxt.Text = countries[a].troops.ToString(); 
            countries[b].numberOfTroopsTxt.Text = countries[b].troops.ToString();
        }
        private void SelectFriends(int cur)
        {
            bool[] vis = new bool[42];
            for (int i = 0; i < 42; i++) vis[i] = false;
            Tracelands(cur, vis);
        }
        private void Tracelands(int cur, bool[] vis)
        {
            if (vis[cur] == true) return;
            vis[cur] = true;
            // System.Console.Write("{0} ", cur);
            ToLight(cur.ToString());
            for (int i = 0; i < map[cur].Count; i++)
            {
                if (countries[map[cur][i]].owner == players[turn])
                {
                    Tracelands(map[cur][i], vis);
                }
            }
        }
        private void StartTurn()
        {
            ViewAllCities(1);
        
            GD.Print("Draft");
            int newtroops = (players[turn % 3].countries) / 3;
            GD.Print($"Player{turn % 3},has new{newtroops}troops");
            PlayerTurn($"Player {turn % 3}");
            for (int i = 0; i < 5; i++)
            {
                if ((players[turn % 3].card[i]) >= 3)
                {

                    newtroops *= players[turn % 3].hasCard;
                    players[turn % 3].card[i] -= 3;
                    break;

                }

            }

            players[turn % 3].notusedTroops += newtroops;
           //Rest();
            ChangeTxtTroops(players[turn], players[turn].notusedTroops);

        }
        private void _on_Button_pressed()
        {

            ClikedSound();

            if (this.mode == 0 && players[this.turn].notusedTroops == 0)
            {

                this.mode = 1;

                ChangeMode("Attack");

                Rest();

            }
            else if (this.mode == 1)
            {

                this.mode = 2;

                ChangeMode("Fortify");

                selected = -1;
                Rest();


            }
            else if (this.mode == 2)
            {

                this.mode = 0;

                ChangeMode("Draft");

                this.turn = (this.turn + 1) % 3;
                Rest();

                StartTurn();
            }

        }
        private void _on_Area2D_input_event(object viewport, object @event, int shape_idx, string name)
        {

            if (Input.IsActionPressed("LM") && this.mode == 0 && countries[int.Parse(name)].owner == players[turn])
            {
                ClikedSound();

                Draft(int.Parse(name), selectedTroobs);

                GD.Print($"still {players[turn].notusedTroops}");
            }
            else if (Input.IsActionPressed("LM") && this.mode == 1)
            {
                ClikedSound();
                startLevel = 0;
                if (selected == -1)
                {
                    if (countries[int.Parse(name)].owner == players[turn])
                    {
                        selected = int.Parse(name);
                        ToLight(name);
                        for (int i = 0; i < map[int.Parse(name)].Count; i++)
                        {
                            if (countries[map[int.Parse(name)][i]].owner != players[turn])
                            {

                                // ToLight(map[int.Parse(name)][i].ToString());
                                // Zoom(GetGlobalMousePosition(), name);
                                AttackAnimation(name);
        
                            }
                        }
                    }
                }
                else
                {
                    Rest();
  
                    bool allow = false;
                    int n = int.Parse(name);
                    for (int i = 0; i < map[selected].Count; i++)
                    {
                        if (n == map[selected][i])
                        {
                            allow = true;
                            break;
                        }
                    }
                    if (countries[int.Parse(name)].owner == players[turn])
                    {
                        Rest();

                        selected = -1;
                    }
                    else
                    {
                        if (allow)
                        {
                            HSlider slider = (GetNode("sliderImage/soldireSlider") as HSlider);
                            Label label = (GetNode("sliderImage/Label") as Label);
                            Sprite sprite1 = (GetNode("sliderImage") as Sprite);
                            Button AtackBTN = (GetNode("sliderImage/Attackk") as Button);
                            label.Text = "1";
                            slider.MinValue = 1;
                            slider.MaxValue = countries[selected].troops - 1;
                            sprite1.Show();
                            AtackBTN.Show();
                            slider.TickCount = 1;
                            mycontry = countries[selected];
                            enmey = countries[n];

                        }

                        else Rest();
                    }

                }

            }
          

            else if (Input.IsActionPressed("LM") && this.mode == 2)
            {
                ClikedSound();
                int n = int.Parse(name);
                if (selected == -1)
                {
                    if (countries[n].owner == players[turn])
                    {
                        selected = n;


                        SelectFriends(n);
                    }
                }
                else
                {
                    if (n == selected || countries[n].owner != players[turn])
                    {
                        selected = -1;
                        Rest();
                    }
                    else
                    {
                        HSlider slider = (GetNode("sliderImage/soldireSlider") as HSlider);
                        Label label = (GetNode("sliderImage/Label") as Label);
                        Sprite sprite1 = (GetNode("sliderImage") as Sprite);
                        Button AtackBTN = (GetNode("sliderImage/travelBTN") as Button);
                        label.Text = "1";
                        slider.MinValue = 1;
                        slider.MaxValue = countries[selected].troops - 1;
                        sprite1.Show();
                        AtackBTN.Show();
                        slider.TickCount = 1;
                        coun1 = selected;
                        coun2 = n;

                    }
                }
            }

        }


        private void _on_DicePlayer_animation_finished(string anim_name)
        {
            AnimationPlayer player = GetNode("DicePlayer") as AnimationPlayer;
            player.Stop();
            ShowDiceValue(Attacker_Dice, Attacked_Dice);
        }

        private void _on_soldireSlider_value_changed(int x)
        {
			try
			{
            HSlider slider = (GetNode("sliderImage/soldireSlider") as HSlider);
            Label label = (GetNode("sliderImage/Label") as Label);
            if (mode == 0)
            {

                slider.MinValue = 0;
                slider.MaxValue = players[turn % 3].notusedTroops;


            }
            label.Text = x.ToString();
            selectedTroobs = x;
			}
			catch
			{
				/// hello world  
								
			}
        }
        private void _on_Attackk_pressed(bool b)
        {
            if (b)
            {
                HSlider slider = (GetNode("sliderImage/soldireSlider") as HSlider);
                // selectedTroobs = slider.TickCount;
                GD.Print(selectedTroobs.ToString());
                Attack(ref enmey, ref mycontry, selectedTroobs);
            }
        }

        private void _on_travelBTN_pressed(bool b)
        {
            if (b)
            {
                HSlider slider = (GetNode("sliderImage/soldireSlider") as HSlider);
                // selectedTroobs = slider.TickCount;
                GD.Print(slider.TickCount.ToString());
                Fortify(coun1, coun2, selectedTroobs);
            }
        }

    }

}

