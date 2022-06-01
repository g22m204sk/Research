using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DatasetGenerater
{
    class Program
    {
        static void Main(string[] args)
        {
            Generater g = new Generater();
            VariableGenerater _vGene = new VariableGenerater();

            // _vGene.Output();

            //for (int i = 0; i < 100; i++)
            //     Console.WriteLine(g.GenerateVariableName());


            Console.WriteLine(g.GenerateVar());

            /* var a = g.Generate(10); 
             for(int i = 0; i < a.Count; i++)
             {
                 Console.WriteLine(a[i]);
                 if (i % 2 == 1) Console.WriteLine();
             }*/

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
        Dictionary<VariableType, string> japanese_val = new Dictionary<VariableType, string>()
            {
                { VariableType.Int, "整数型" },
                { VariableType.Float, "実数型" },
                { VariableType.Double, "実数型" },
                { VariableType.Bool, "bool型" },
                { VariableType.Char, "文字型" },
                { VariableType.String, "文字列型" },
                { VariableType.Var , "var型"}
            };
        Dictionary<VariableType, string> english_val = new Dictionary<VariableType, string>() {
                { VariableType.Int , "int" },
                { VariableType.Float , "float"},
                { VariableType.Double,"double"},
                { VariableType.Bool , "bool"},
                { VariableType.Char, "char"},
                { VariableType.String, "string" },
                { VariableType.Var , "var"}
            };
        string charas = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        string[] _variabelNames = new string[0];

        public Generater()
        {
            string file;
            using (StreamReader sr = new StreamReader(@"C:\Users\Koichi_S\Desktop\RESEARCH\variabletName.txt"))
            {
                file = sr.ReadToEnd();
                _variabelNames = file.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public List<string> Generate(int datasize)
        {

            List<string> dataset = new List<string>();
            Random random = new Random();
            int thre = 7;

            for (int i = 0; i < _variableType.Length; i++)
            {
                for (int j = 0; j < datasize; j++)
                {
                    string name = GenerateVariableName();

                    int zero = random.Next(0, 100);
                    int set = random.Next(0, 10);
                    switch (i)
                    {
                        //intの時
                        case 0:

                            // 20%の確率で宣言
                            if (set > thre) dataset.Add(GeneString(VariableType.Int, GeneMode.declaration));
                            else dataset.Add(GeneString(VariableType.Int, GeneMode.assign));
                            break;


                        case 1:
                            if (set > thre) dataset.Add(GeneString(VariableType.Float, GeneMode.declaration));
                            else dataset.Add(GeneString(VariableType.Float, GeneMode.assign));
                            break;
                        case 2:
                            if (set > thre) dataset.Add(GeneString(VariableType.Double, GeneMode.declaration));
                            else dataset.Add(GeneString(VariableType.Double, GeneMode.assign));
                            break;
                        case 3:
                            if (set > thre) dataset.Add(GeneString(VariableType.Bool, GeneMode.declaration));
                            else dataset.Add(GeneString(VariableType.Bool, GeneMode.assign));

                            break;
                        case 4:
                            if (set > thre) dataset.Add(GeneString(VariableType.Char, GeneMode.declaration));
                            else dataset.Add(GeneString(VariableType.Char, GeneMode.assign));

                            break;
                        case 5:
                            if (set > thre) dataset.Add(GeneString(VariableType.String, GeneMode.declaration));
                            else dataset.Add(GeneString(VariableType.String, GeneMode.assign));

                            break;
                        case 6:
                            dataset.Add(GenerateVar());
                            break;
                    }
                }
            }
            return dataset;
        }


        public string GeneString(VariableType val, GeneMode mode)
        {
            Random random = new Random();
            string name = GenerateVariableName();
            string tmp_string = "";
            int zero = random.Next(0, 100);
            if (mode == GeneMode.assign)
            {
                switch (val)
                {
                    case VariableType.Int:

                        int value = random.Next(-10000, 10000);
                        value = zero % 10 == 0 ? 0 : value;
                        tmp_string += ("int " + name + "=" + value.ToString() + ";" + Environment.NewLine);
                        tmp_string += ("整数型変数" + name + "を宣言し" + value.ToString() + "を代入");
                        break;
                    case VariableType.Float:
                        double value_f = random.Next(-10000, 10000) + random.NextDouble();
                        int digit = random.Next(1, 5);
                        value_f = Math.Truncate(value_f * Math.Pow(10, digit)) / Math.Pow(10, digit);
                        value_f = zero % 10 == 0 ? 0 : value_f;
                        tmp_string += ("float " + name + "=" + value_f.ToString() + "f;" + Environment.NewLine);
                        tmp_string += ("実数型変数" + name + "を宣言し" + value_f.ToString() + "を代入");
                        break;
                    case VariableType.Double:
                        double value_d = random.Next(-10000, 10000) + random.NextDouble();
                        int digit_d = random.Next(1, 5);
                        value_d = Math.Truncate(value_d * Math.Pow(10, digit_d)) / Math.Pow(10, digit_d);
                        value_d = zero % 10 == 0 ? 0 : value_d;

                        tmp_string += ("double " + name + "=" + value_d.ToString() + ";" + Environment.NewLine);
                        tmp_string += ("実数型変数" + name + "を宣言し" + value_d.ToString() + "を代入");
                        break;
                    case VariableType.Bool:
                        bool flag = random.Next(0, 100) % 2 == 0;
                        tmp_string += ("bool " + name + "=" + flag.ToString() + ";" + Environment.NewLine);
                        tmp_string += ("bool型変数" + name + "を宣言し" + flag.ToString() + "を代入");
                        break;
                    case VariableType.Char:
                        char value_c = charas[random.Next(0, charas.Length)];
                        value_c = zero % 10 == 0 ? ' ' : value_c;
                        tmp_string += ("char " + name + "='" + value_c.ToString() + "';" + Environment.NewLine);
                        tmp_string += ("文字型変数" + name + "を宣言し" + value_c.ToString() + "を代入");
                        break;
                    case VariableType.String:
                        int len = random.Next(2, 15);
                        string value_s = "";
                        for (int s = 0; s < len; s++, value_s += charas[random.Next(0, charas.Length)]) ;
                        value_s = zero % 10 == 0 ? "" : value_s;
                        tmp_string += ("string " + name + "=\"" + value_s.ToString() + "\";" + Environment.NewLine);
                        tmp_string += ("文字列型変数" + name + "を宣言し" + value_s.ToString() + "を代入");
                        break;
                }
            }
            else
            {
                tmp_string += english_val[val] + " " + name + ";" + Environment.NewLine;
                tmp_string += japanese_val[val] + "型変数" + name + "を宣言";
            }
            return tmp_string;
        }

        public string GenerateVar()
        {
            string s = "";
            Random random = new Random();
            var types = (VariableType[])Enum.GetValues(typeof(VariableType));
            int type_num = random.Next(0, types.Length);
            s = GeneString(types[type_num], GeneMode.assign);
            var ss = s.Split(';');
            ss[0] = ss[0].Replace(english_val[types[type_num]], "var").Replace(Environment.NewLine, "");
            ss[1] = ss[1].Replace(japanese_val[types[type_num]], "var型").Replace(Environment.NewLine, "");

            return ss[0] + ";" + Environment.NewLine + ss[1];
        }

        public string GenerateArray(VariableType type, GeneMode mode)
        {
            Random random = new Random();
            string name = GenerateVariableName();
            int indexSize = random.Next(1, 10);

            string result = "";

            switch (type)
            {
                if (GeneMode.assign == mode)
            {
                 
                    case VariableType.Int:
                        string index = "";
                for (int i = 0; i < indexSize; i++)
                    index += random.Next(-1000, 1000).ToString() + "f,";
                result += "int[] " + name + "=new int[]{" + index + "};" + Environment.NewLine;
                result += "整数型配列" + name + "を宣言し{" + index + "}を代入";
                break;
                    case VariableType.Float:
                        string index_f = "";
                for (int i = 0; i < indexSize; i++)
                {
                    int digit = random.Next(1, 3);
                    float tmp_f = random.Next(-1000, 1000) + random.NextDouble();
                    tmp_f = Math.Floor(tmp_f * Math.Pow(10, digit)) / Math.Pow(10, digit);
                    index_f += tmp_f + ",";
                }
                result += "float[] " + name + "=new float[]{" + indexf + "};" + Environment.NewLine;
                result += "実数型配列" + name + "を宣言し{" + index_f + "を代入";
                break;
                    case VariableType.Double:
                        string index_d = "";
                for (int i = 0; i < indexSize; i++)
                {
                    int digit = random.Next(1, 3);
                    float tmp_d = random.Next(-1000, 1000) + random.NextDouble();
                    tmp_d = Math.Floor(tmp_d * Math.Pow(10, digit)) / Math.Pow(10, digit);
                    index_d += tmp_d + ",";
                }
                result += "double[] " + name + "=new double[]{" + index_d + "};" + Environment.NewLine;
                result += "実数型配列" + name + "を宣言し{" + index_d + "を代入";
                break;
                    case VariableType.Bool:
                        string index_b = "";
                for (int i = 0; i < indexSize; i++)
                    index_b += random.Next(0, 100) % 2 == 0 + ",";
                result += "bool[] " + name + "=new bool[]{" + index_b + "};" + Environment.NewLine;
                result += "bool型配列" + name + "を宣言し{" + index_b + "を代入";
                break;
                    case VariableType.Char:
                        string index_c = "";
                for (int i = 0; i < indexSize; i++)
                    index_c += "'" + charas[random.Next(0, charas.Length)] + "',";
                result += "char[] " + name + "=new char[]{" + index_c + "};" + Environment.NewLine;
                result += "文字型配列" + name + "を宣言し{" + index_c + "を代入";
                break;
                    case VariableType.Char:
                        string index_s = "";
                int string_len = random.Next(1, 10);

                for (int i = 0; i < indexSize; i++)
                {
                    string tmp_s = "";
                    for (int j = 0; j < string_len; j++)
                    {
                        tmp_s += charas[random.Next(0, charas.Length)];
                    }
                    index_s += "\"" + tmp_s + "\",";
                }

                result += "string[] " + name + "=new string[]{" + index_s + "};" + Environment.NewLine;
                result += "文字列型配列" + name + "を宣言し{" + index_s + "を代入";
                break;
                case VariableType.Var:
                   result += GenerateVarArray();
            }
            else
            {
                result += english_val[type] + "[] " + name + ";" + Environment.NewLine;
                result += japanese_val[type] + "型配列" + name + "を宣言";
            }
            return result;
        }
    }
    public string GenerateVarArray()
    {
        string s = "";
        Random random = new Random();
        var types = (VariableType[])Enum.GetValues(typeof(VariableType));
        int type_num = random.Next(0, types.Length);
        s = GenerateArray(types[type_num], GeneMode.assign);
        var ss = s.Split(';');
        ss[0] = ss[0].Replace(english_val[types[type_num]], "var").Replace(Environment.NewLine, "");
        ss[1] = ss[1].Replace(japanese_val[types[type_num]], "var型").Replace(Environment.NewLine, "");
        return ss[0] + ";" + Environment.NewLine + ss[1];
    }
}


//ちゃんとした英単語を変数名につけるように
public string GenerateVariableName()
{
    string result = "";

    Random random = new Random();
    float headNum = random.Next(0, 101);
    if (headNum % 10 == 3) result += "_";

    //変数名の長さは１０文字以下
    float nameLen = random.Next(1, 10);

    float switchrandom = random.Next(0, 100);

    switch (switchrandom % 10)
    {
        case 0:
        case 1:
            for (int i = 0; i < nameLen; i++)
            {
                if (i == 0)
                {
                    if (headNum % 10 == 1)
                        result += charas[random.Next(0, 26)];
                    else if (headNum % 10 == 2)
                        result += charas[random.Next(26, charas.Length - 10)];
                    else
                        result += charas[random.Next(0, charas.Length - 10)];

                }
                else result += charas[random.Next(0, charas.Length)];
            }
            break;
        default:
            result += _variabelNames[random.Next(0, _variabelNames.Length)];
            break;
    }
    if (result == "") result = _variabelNames[random.Next(0, _variabelNames.Length)];

    return result;
}
}

enum VariableType
{
    Int,
    Float,
    Double,
    Bool,
    Char,
    String,
    Var
}

enum GeneMode
{
    assign,
    declaration
}
class VariableGenerater
{
    string[] _variableNames = new string[]{
            "int",
            "float",
            "double",
            "var",
            "object",
            "bool",
        };




    public void Output()
    {
        Search();
        System.Console.WriteLine("完了");
    }

    void Search()
    {

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-z0-9]+$");
        string result = "";
        using (StreamReader sr = new StreamReader(@"C:\Users\Koichi_S\Desktop\Research_UG\pro-jpn1.txt"))
        {
            string dataset = sr.ReadToEnd();
            string[] dataset_sepa = dataset.Split(new char[] { '\n' }, StringSplitOptions.None);
            Console.WriteLine("データ読み込み完了");
            Console.WriteLine("変数名を検索中です");
            for (int i = 0; i < dataset_sepa.Length; i += 2)
            {
                for (int j = 0; j < _variableNames.Length; j++)
                {
                    if (dataset_sepa[i].Contains(_variableNames[j] + " "))
                    {
                        //変数型の後から2個目の空白までを変数名として抽出
                        string tmp = dataset_sepa[i].Remove(0, dataset_sepa[i].IndexOf(_variableNames[j]) + _variableNames[j].Length).TrimStart();
                        try
                        {
                            string tmp_result = "";
                            if (tmp.Contains(" "))
                                tmp_result = tmp.Substring(0, tmp.IndexOf(" ")) + Environment.NewLine;
                            else
                                tmp_result = tmp.Substring(0, tmp.IndexOf(";")) + Environment.NewLine;

                            tmp_result = tmp_result.Length > 20 ? "" : tmp_result;
                            tmp_result = Regex.IsMatch(tmp_result, @"[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]+") ? "" : tmp_result;
                            tmp_result = tmp_result.Replace(",", "").Replace(")", "").Replace(";", "").Replace("//", "").Replace("=", "").Replace("!=", "").Replace("==", "").Replace("!", "").Replace("(", "").Replace(":", "").Replace("[", "").Replace("]", "");
                            tmp_result = tmp_result.Substring(0, 1) == "_" ? tmp_result.Remove(0, 1) : tmp_result;
                            tmp_result = tmp_result.Contains("\"") ? "" : tmp_result;

                            //1文字の変数名は省く
                            result += tmp_result.Length > 3 ? tmp_result : "";
                        }
                        catch { }
                    }
                }
                Console.Write(i + " / " + (dataset_sepa.Length - 1).ToString() + "\r");
            }
        }

        Console.WriteLine(Environment.NewLine + "---------------------重複した変数名を削除します--------------------------");
        //result = result.Replace(",", "").Replace(")", "").Replace(";", "").Replace("//", "").Replace("=", "").Replace("!=", "").Replace("==", "").Replace("!", "").Replace("(", "").Replace("_", "").Replace(":", "");

        string[] _after = DeleteDuplicate(result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));

        Console.WriteLine(Environment.NewLine + "書き出します");
        using (StreamWriter sw = new StreamWriter(@"C:\Users\Koichi_S\Desktop\RESEARCH\variabletName.txt"))
        {
            for (int i = 0; i < _after.Length; i++)
            {
                sw.WriteLine(_after[i]);
                Console.Write(i + " / " + (_after.Length - 1).ToString() + "\r");
            }
        }
        Console.WriteLine(Environment.NewLine + "完了しました");
    }

    string[] DeleteDuplicate(string[] sentences)
    {
        List<string> result = new List<string>();

        bool addFrag = true;
        for (int i = 0; i < sentences.Length; i++)
        {
            addFrag = true;
            for (int j = 0; j < result.Count; j++)
            {
                if (sentences[i] == result[j])
                    addFrag = false;

            }
            if (addFrag)
                result.Add(sentences[i]);
            Console.Write(i + " / " + (sentences.Length - 1).ToString() + "\r");
        }
        return result.ToArray();

    }
}
}

