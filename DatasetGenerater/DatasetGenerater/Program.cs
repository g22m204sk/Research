using System;
using System.Collections.Generic;
namespace DatasetGenerater
{
    class Program
    {
        static void Main(string[] args)
        {
            Generater g = new Generater();

            for (int i = 0; i < 100; i++)
                Console.WriteLine(g.GenerateVariableName());
        }
    }


    class Generater
    {
        string[] _variableType = new string[]
       {
            "int",
            "float",
            "double",
            "char",
            "string",
            "bool",
            "var",
            "object"
       };
        string[] _oprator = new string[]
        {
            "=",
            "+=",
            "-=",
            "*=",
            "/=",
        };
        string[] _variableHead = new string[]
        {
            "_",
            "a",
            "A"
        };
        string charas = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public void Generate(int datasize)
        {

            List<string> dataset = new List<string>();
            Random random = new Random();
            for (int i = 0; i < _variableType.Length; i++)
            {
                for (int j = 0; j < datasize; j++)
                {
                    string name = GenerateVariableName();

                    int zero = random.Next(0, 100);
                    int set = random.Next(0, 10);
                    int value = 0;
                    switch (i)
                    {
                        //intの時
                        case 0:
                            
                            //10分の1の確立で0初期化
                            if (zero % 10 != 0) value = random.Next(-10000, 10000);
                            
                            
                            if (set > 7)
                            {
                                dataset.Add("int " + name + ";");
                                dataset.Add("整数型変数" + name+"を宣言");
                            }
                            else
                            {
                                dataset.Add("int " + name + "=" + value.ToString() + ";");
                                dataset.Add("整数型変数" + name+"に"+value.ToString()+"を代入");
                            }
                            break;

                        //float と　doubleの時
                        case 1:
                        case 2:
                            double value_f = 0;
                            bool DorF = random.Next(0,100) % 2 == 0;
                            if (zero % 10 != 0) value_f = random.Next(-10000, 10000) + random.NextDouble() ;
                            if (set > 7)
                            {
                                dataset.Add((DorF ? "double " : "float ") + name + ";");
                                dataset.Add("実数型変数" + name + "を宣言");
                            }
                            else
                            {
                                dataset.Add((DorF ? "double " : "float ") + name + "=" + value_f.ToString() + ";");
                                dataset.Add("実数型変数" + name + "に" + value_f.ToString() + "を代入");
                            }
                            break;
                    }
                }
            }
        }
        //ちゃんとした英単語を変数名につけるように
        public string GenerateVariableName()
        {
            string result = "";

            Random random = new Random();
            float headNum = random.Next(0, 101);
            if (headNum % 3 == 0) result += "_";

            //変数名の長さは１０文字以下
            float nameLen = random.Next(1, 10);

            for (int i = 0; i < nameLen; i++)
            {
                if (i == 0)
                {
                    if (headNum % 3 == 1)
                        result += charas[random.Next(0, 26)];
                    if (headNum % 3 == 2)
                        result += charas[random.Next(26, 53)];
                }
                else result += charas[random.Next(0, charas.Length)];
            }

            return result;
        }
    }
}
