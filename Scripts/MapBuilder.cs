using System;
using System.Collections.Generic;
using Godot;
namespace RiskGame.Scripts
{
    public partial class Game
    {
        //fills the map logically
        private void FillMap()
        {
            // 1 ,2, 35     
            map[0] = new List<int>() { 1, 2, 35 };
            //0, 2 ,3 ,12           
            map[1] = new List<int>() { 0, 2, 3, 12 };
            //0 ,1, 3, 4          
            map[2] = new List<int>() { 0, 1, 3, 4 };
            //1, 2 ,4 ,6 ,5 ,12
            map[3] = new List<int>() { 1, 2, 4, 6, 5, 12 };
            //2 ,3, 7, 6          
            map[4] = new List<int>() { 2, 3, 7, 6 };
            //6 ,3, 12          
            map[5] = new List<int>() { 6, 3, 12 };
            //5 ,3, 4, 7           
            map[6] = new List<int>() { 5, 3, 4, 7 };
            //4 ,6, 8          
            map[7] = new List<int>() { 4, 6, 8 };
            //7 ,9, 10           
            map[8] = new List<int>() { 7, 9, 10 };
            //8 ,10, 11           
            map[9] = new List<int>() { 8, 10, 11 };
            //8, 9 ,11, 22           
            map[10] = new List<int>() { 8, 9, 11, 22 };
            //9, 10
            map[11] = new List<int>() { 9, 10 };
            //1 ,3 ,5, 13
            map[12] = new List<int>() { 1, 3, 5, 13 };
            //12,14,15
            map[13] = new List<int>() { 12, 14, 15 };
            //13,15,16,17
            map[14] = new List<int>() { 13, 15, 16, 17 };
            //13,16,18,14
            map[15] = new List<int>() { 13, 16, 18, 14 };
            //14,15,17,18,19
            map[16] = new List<int>() { 14, 15, 17, 18, 19 };
            //14,16,19,22
            map[17] = new List<int>() { 14, 16, 19, 22 };
            //15,16,19,20,28,27
            map[18] = new List<int>() { 15, 16, 19, 20, 28, 27 };
            //16,17,18,20,21
            map[19] = new List<int>() { 16, 17, 18, 20, 21 };
            //18,19,21,23,28,29
            map[20] = new List<int>() { 18, 19, 21, 23, 28, 29 };
            //19,20,23,22
            map[21] = new List<int>() { 19, 20, 23, 22 };
            //17,10,21,23,24
            map[22] = new List<int>() { 17, 10, 21, 23, 24 };
            //21,22,24,25,26,20
            map[23] = new List<int>() { 21, 22, 24, 25, 26, 20 };
            //22,23,25
            map[24] = new List<int>() { 22, 23, 25 };
            //23,24,26
            map[25] = new List<int>() { 23, 24, 26 };
            //23,25
            map[26] = new List<int>() { 23, 25 };
            //18,28,30,31
            map[27] = new List<int>() { 18, 28, 30, 31 };
            //18,27,30,20,29
            map[28] = new List<int>() { 18, 27, 30, 20, 29 };
            //20,28,30,37
            map[29] = new List<int>() { 20, 28, 30, 37 };
            //27,28,29,34,37,31
            map[30] = new List<int>() { 27, 28, 29, 34, 37, 31 };
            //27,30,34,33,32
            map[31] = new List<int>() { 27, 30, 34, 33, 32 };
            //31,33,35
            map[32] = new List<int>() { 31, 33, 35 };
            //31,32,35,34
            map[33] = new List<int>() { 31, 32, 35, 34 };
            //31,33,36,30,35
            map[34] = new List<int>() { 31, 33, 36, 30, 35 };
            //32,33,36,0,34
            map[35] = new List<int>() { 32, 33, 36, 0, 34 };
            //35,34
            map[36] = new List<int>() { 35, 34 };
            //29,30,38
            map[37] = new List<int>() { 29, 30, 38 };
            //37,39,40
            map[38] = new List<int>() { 37, 39, 40 };
            //38,40,41
            map[39] = new List<int>() { 38, 40, 41 };
            //41,38,39
            map[40] = new List<int>() { 41, 38, 39 };
            //40,39
            map[41] = new List<int>() { 40, 39 };


        }

    }
}
