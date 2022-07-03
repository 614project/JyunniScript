using System;

public partial class Program
{
    const string const_version = "0.1.4";

    const string const_help_print = "'print :[string]' 또는 'write :[string]' : 원하는 문자열을 출력합니다.";

    const string const_help_println = "'println :[string]' 또는 'writeline :[string]' : 원하는 문자열을 한 줄에 출력합니다.";

    const string const_help_exit = "'stop' 또는 'exit' : Jyunni Script를 종료합니다.";

    const string const_help_help = "'help' [command] : 원하는 명령어에 대한 설명을 출력합니다.";

    const string const_help_clear = "'clear' : 현재 콘솔에 출력된 내용을 모두 지웁니다.";

    const string const_help_wait = "'wait' [float]' : 원하는 값만큼 실행을 지연합니다. 기준은 '1초' 입니다. ('6.14'을 입력하면 6.14초가 지연됩니다.)";

    const string const_help_delay = "'delay [int]' : 원하는 값만큼 실행을 지연합니다. 기준은 '0.001'초 입니다. ('614'를 입력하면 0.614초가 지연됩니다.)";

    const string const_help_int = "'int [string]' 또는 'int [string] [int]' : 새 정수형 변수를 원하는 이름으로 생성합니다. 또는 원하는 값도 넣습니다. (이름은 언더바 '_' 를 제외하고는 특수문자를 쓸수 없습니다.)";
    
    const string const_help_string = "'string [string]' 또는 'string [string] [string]' : 새 문자열 변수를 원하는 이름으로 생성합니다. 또는 원하는 값도 넣습니다. (이름은 언더바 '_' 를 제외하고는 특수문자를 쓸수 없습니다.)";

    const string const_help_read = "'read [문자열 변수의 이름]' : 콘솔에서 입력을 받아 원하는 변수에 저장합니다.";

    const string const_help_free = "'free' [변수의 이름]' : 원하는 변수를 제거합니다.";

    const string const_helplist = $"\n명령어 목록:\n{const_help_print}\n{const_help_println}\n{const_help_clear}\n{const_help_int}\n{const_help_string}\n{const_help_read}\n{const_help_free}\n{const_help_wait}\n{const_help_delay}\n{const_help_exit}\n{const_help_help}\n";

    static void Helper(string cmd)
    {
        switch (cmd) {
            case "print":
            case "write":
                Console.WriteLine(const_help_print);
                break;
            case "println":
            case "writeline":
                Console.WriteLine(const_help_println);
                break;
            case "exit":
            case "stop":
                Console.WriteLine(const_help_exit);
                break;
            case "help":
                Console.WriteLine(const_help_help);
                break;
            case "clear":
                Console.WriteLine(const_help_clear);
                break;
            case "wait":
                Console.WriteLine(const_help_wait);
                break;
            case "delay":
                Console.WriteLine(const_help_delay);
                break;
            case "int":
                Console.WriteLine(const_help_int);
                break;
            case "string":
                Console.WriteLine(const_help_string);
                break;
            case "read":
                Console.WriteLine(const_help_read);
                break;
            case "free":
                Console.WriteLine(const_help_free);
                break;
            default:
                Console.WriteLine($"help: '{cmd}' 는 알수없는 명령어입니다.");
                error = true;
                break;
        }
    }
}