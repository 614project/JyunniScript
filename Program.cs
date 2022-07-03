using System;
using System.IO;

public partial class Program
{
    static bool stop = false;
    static bool error = false;
    static List<Variable> Variables = new();
    static List<string> Names = new();

    static void Main(string[] cmd)
    {
        if (cmd.Length == 0)
        {
            Inter();
            return;
        }
        if (!File.Exists(cmd[0]))
        {
            Console.Write("존재하지 않는 파일 경로입니다.");
            return;
        }
        Scripter(File.ReadAllLines(cmd[0]));
        Console.ReadKey();
    }

    static void Scripter(string[] text)
    {
        if (text.Length == 0) return;
        foreach(string dcr in text)
        {
            Runner(dcr);
            if (stop || error) return;
        }
    }

    static void Runner(string inputtext)
    {
        if ((inputtext = inputtext.Trim()).Length == 0) return;
        List<string> c = new();
        string p = inputtext;
        int rint = 0, k =0;
        float d=0;
        while (p.Length != 0)
        {
            if ((rint = p.IndexOf(' ')) == -1)
            {
                if (p.Length != 0) c.Add(p);
                break;
            }
            c.Add(p.Substring(0, rint));
            p = p.Substring(rint).TrimStart();
        }
        if (c.Count == 0 || c[0].Length == 0) return;
        bool CheckDuobleDot()
        {
            if ((rint = inputtext.IndexOf(':')) == -1)
            {
                Console.WriteLine("문자열 시작어 ':' 가 없습니다.");
                error = true;
                return true;
            }
            return false;
        }
        bool CheckLength(int index,string? message = null,bool er = true)
        {
            if (c.Count == index || c[index] == "")
            {
                if (message != null)Console.WriteLine(message);
                error = er;
                return true;
            }
            return false;
        }
        bool InputVariable(out string outstr)
        {
            string str = inputtext;
            outstr = "Error";
            str = str.Substring(++rint).Replace("\\n", "\n").Replace("\\.", "\\c").Replace("\\[","\\s").Replace("\\]","\\e");
            int o = 0,u = 0;
            List<int> point = new();
            List<string> value = new();
            string t;
            while ((o = str.IndexOf("[")) != -1)
            {
                if (
                    str.Length <= o + 1 ||
                    (u = (t = str.Substring(o + 1)).IndexOf(']'))
                    == -1
                    )
                {
                    Console.WriteLine("변수 이름의 끝은 ']' 로 끝나야 합니다.");
                    error = true;
                    return true;
                }
                if (GetVariable(t.Substring(0,u),out t))
                {
                    return true;
                }
                point.Add(o);
                value.Add(t);
                str = str.Remove(o, u + 2);
            }
            for (int i = point.Count - 1;i>=0;i--)
            {
                str =  str.Insert(point[i], value[i]);
            }
            outstr = str.Replace("\\s", "[").Replace("\\e", "]").Replace("\\c","\\");
            return false;
        }
        bool GetVariable(string varname,out string str)
        {
            str = "Error";
            int index;
            if (varname == "")
            {
                Console.WriteLine("'[' 와 ']' 사이 안에 변수 이름을 적어야 합니다.");
                error = true;
                return true;
            }
            if ((index = Names.IndexOf(varname)) == -1)
            {
                Console.WriteLine($"'{varname}' 은 없는 변수명입니다.");
                error = true;
                return true;
            }
            str = Variables[index].Source.ToString();
            return false;
        }
        if (c[0].StartsWith('['))
        {
            if ((rint = c[0].IndexOf(']')) == -1)
            {
                Console.WriteLine("변수 이름의 끝은 ']' 로 끝나야 합니다.");
                error = true;
                return;
            }
            p = c[0].Substring(1, rint - 1);
            if ((k = Names.IndexOf(p)) == -1)
            {
                Console.WriteLine($"'{p}' 는 없는 변수명입니다.");
                error = true;
                return;
            }
            switch (Variables[k].Type)
            {
                case 's':
                    if (CheckDuobleDot()) return;
                    if (InputVariable(out p)) return;
                    Variables[k].Source = p;
                    break;
                case 'i':
                    if (CheckLength(1, "변수에 넣을 값을 입력해야 합니다.")) return;
                    if (!int.TryParse(c[1], out rint))
                    {
                        Console.WriteLine("변수에 넣을 값이 정수(int)가 아닙니다.");
                        error = true;
                        return;
                    }
                    Variables[k].Source = rint;
                    break;
                case 'f':
                    if (CheckLength(1, "변수에 넣을 값을 입력해야 합니다.")) return;
                    if (!float.TryParse(c[1], out d))
                    {
                        Console.WriteLine("변수에 넣을 값이 정수(int)가 아닙니다.");
                        error = true;
                        return;
                    }
                    Variables[k].Source = d;
                    break;
                default:
                    Console.WriteLine($"'{p}' 는 잘못된 타입을 가진 변수형입니다.");
                    error = true;
                    break;
            }
            return;
        }
        switch (c[0].ToLower())
        {
            case "print":
            case "write":
                if (CheckDuobleDot()) return;
                if (InputVariable(out p)) return;
                Console.Write(p);
                return;
            case "println":
            case "writeline":
                if (CheckDuobleDot()) return;
                if (InputVariable(out p)) return;
                Console.WriteLine(p);
                return;
            case "stop":
            case "exit":
                stop = true;
                return;
            case "help":
                if (CheckLength(1))
                {
                    Console.Write(const_helplist);
                }
                else Helper(c[1]);
                return;
            case "clear":
                Console.Clear();
                return;
            case "wait":
                if (CheckLength(1, "'wait' 뒤에 지연할 숫자를 입력해야합니다.")) return;
                
                if (!float.TryParse(c[1],out d))
                {
                    Console.WriteLine("입력된 값이 정수 또는 실수가 아닙니다.");
                    error = true;
                    return;
                }
                if (d < 0)
                {
                    Console.WriteLine("입력된 값이 0 미만의 음수입니다.");
                    error = true;
                    return;
                }
                Thread.Sleep((int)d * 1000);
                return;
            case "delay":
                if (CheckLength(1, "'delay' 뒤에 지연할 숫자를 입력해야합니다.")) return;
                if (!int.TryParse(c[1], out rint))
                {
                    Console.WriteLine("입력된 값이 정수가 아닙니다.");
                    error = true;
                    return;
                }
                if (rint<0)
                {
                    Console.WriteLine("입력된 값이 0 미만의 음수입니다.");
                    error = true;
                    return;
                }
                Thread.Sleep(rint);
                return;
            case "int":
                if (CheckLength(1, "'int' 뒤에 변수 이름을 적어주세요.")) return;
                if (c[1].IndexOfAny("!@#$%^&*()+-=~`|\\<>,.?/;:'\"[]{}".ToArray()) != -1)
                {
                    Console.WriteLine("'_'(언더바) 를 제외한 모든 특수기호는 변수 이름으로 사용할수 없습니다.");
                    error = true;
                    return;
                }
                if (Names.IndexOf(c[1]) != -1)
                {
                    Console.WriteLine($"'{c[1]}' 의 이름을 가진 변수가 이미 존재합니다.");
                    error = true;
                    return;
                }
                if (CheckLength(2,null,false))
                {
                    Variables.Add(new('i', 0));
                    Names.Add(c[1]);
                } else
                {
                    if (!int.TryParse(c[2],out rint))
                    {
                        Console.WriteLine("변수에 넣을 값이 정수(int)가 아닙니다.");
                        error = true;
                        return;
                    }
                    Variables.Add(new('i', rint));
                    Names.Add(c[1]);
                }
                return;
            case "string":
                if (CheckLength(1, "'string' 뒤에 변수 이름을 적어주세요.")) return;
                if (c[1].IndexOfAny("!@#$%^&*()+-=~`|\\<>,.?/;:'\"[]{}".ToArray()) != -1)
                {
                    Console.WriteLine("'_'(언더바) 를 제외한 모든 특수기호는 변수 이름으로 사용할수 없습니다.");
                    error = true;
                    return;
                }
                if (Names.IndexOf(c[1]) != -1)
                {
                    Console.WriteLine($"'{c[1]}' 의 이름을 가진 변수가 이미 존재합니다.");
                    error = true;
                    return;
                }
                if (CheckLength(2, null, false))
                {
                    Variables.Add(new('s', string.Empty));
                    Names.Add(c[1]);
                }
                else
                {
                    if (CheckDuobleDot()) return;
                    if (InputVariable(out p)) return;
                    Variables.Add(new('s', p));
                    Names.Add(c[1]);
                }
                return;
            case "read":
                if (CheckLength(1, "입력을 받을 변수명을 입력하세요.")) return;
                if ((k = Names.IndexOf(c[1])) == -1)
                {
                    Console.WriteLine($"'{c[1]}' 는 없는 변수명입니다.");
                    error = true;
                    return;
                }
                if (Variables[k].Type != 's')
                {
                    Console.WriteLine("문자열(string) 변수만 입력을 받을수 있습니다.");
                    error = true;
                    return;
                }
                Variables[k].Source = Console.ReadLine();
                break;
            case "free":
                if (CheckLength(1, "제거할 변수명을 입력하세요.")) return;
                if ((k = Names.IndexOf(c[1])) == -1)
                {
                    Console.WriteLine($"'{c[1]}' 는 없는 변수명입니다.");
                    error = true;
                    return;
                }
                Names.RemoveAt(k);
                Variables.RemoveAt(k);
                break;

            default:
                Console.WriteLine($"'{c[0]}'은 알수없는 명령어입니다.");
                error = true;
                return;
        }
    }

    static void Inter()
    {
        Console.WriteLine("Jyunni Script [" + const_version + "]\n(c) 614Project. All rights reserved.\n'help' 를 입력해 명령어 목록을 확인해보세요.\n'stop' 또는 'exit' 를 입력해 스크립터를 종료할수 있습니다.");
        do
        {
            Console.Write("\n>>>");
            Runner(Console.ReadLine());
            if (stop) return;
        } while (true);
    }
}

class Variable
{
    public char Type = 'B';
    public object Source = false;
    public Variable()
    {

    }
    public Variable(char type, object source)
    {
        Type = type;
        Source = source;
    }
}