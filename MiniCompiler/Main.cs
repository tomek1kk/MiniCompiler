
using System;
using System.IO;
using System.Collections.Generic;
using GardensPoint;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class Compiler
{
    public static string boolVal;
    public static int errors = 0;

    public static List<string> source;
    public static Dictionary<string, string> symbolTable = new Dictionary<string, string>();
    
    public static int Main(string[] args)
    {
        string file;
        FileStream source;
        Console.WriteLine("\nMini programming language compiler - Gardens Point");
        if (args.Length >= 1)
            file = args[0];
        else
        {
            Console.Write("\nsource file:  ");
            file = Console.ReadLine();
        }
        try
        {
            var sr = new StreamReader(file);
            string str = sr.ReadToEnd();
            sr.Close();
            Compiler.source = new System.Collections.Generic.List<string>(str.Split(new string[] { "\r\n" }, System.StringSplitOptions.None));
            source = new FileStream(file, FileMode.Open);
        }
        catch (Exception e)
        {
            Console.WriteLine("\n" + e.Message);
            return 1;
        }
        Scanner scanner = new Scanner(source);
        Parser parser = new Parser(scanner);
        Console.WriteLine();
        sw = new StreamWriter(file + ".il");
        GenProlog();
        parser.Parse();
        GenEpilog();
        sw.Close();
        source.Close();
        if (errors == 0)
            Console.WriteLine("  compilation successful\n");
        else
        {
            int e = errors > 1 ? errors - 1 : errors;
            Console.WriteLine($"\n  {e} errors detected\n");
            File.Delete(file + ".il");
        }
        return errors == 0 ? 0 : 2;
    }

    public static void EmitCode(string instr = null)
    {
        sw.WriteLine(instr);
    }

    public static void EmitCode(string instr, params object[] args)
    {
        sw.WriteLine(instr, args);
    }
    public static string NewTemp()
    {
        return string.Format($"t{nr}");
    }
    public static string AddIfTemp()
    {
        labelStack.Push(++nr);
        return string.Format($"if{nr}");
    }
    public static string GetIfTemp()
    {
        int n = labelStack.Pop();
        return string.Format($"if{n}");
    }
    public static string AddElseTemp()
    {
        elseLabelStack.Push(++nrE);
        return string.Format($"else{nrE}");
    }
    public static string GetElseTemp()
    {
        int n = elseLabelStack.Pop();
        return string.Format($"else{n}");
    }
    public static string AddWhileTemp()
    {
        whileLabelStack.Push(++nrW);
        return string.Format($"while{nrW}");
    }
    public static string GetWhileTemp()
    {
        int n = whileLabelStack.Pop();
        return string.Format($"while{n}");
    }
    public static string AddParTemp()
    {
        parStack.Push(++nrP);
        return string.Format($"par{nrP}");
    }
    public static string GetParTemp()
    {
        if (parStack.Count == 0)
            return string.Format($"parx{nrPx++}");
        int n = parStack.Pop();
        return string.Format($"par{n}");
    }

    public static string CheckParTemp()
    {
        if (parStack.Count == 0)
            return string.Format($"parx{nrPx}");
        return string.Format($"par{parStack.Peek()}");
    }
    
    private static int nr = 0;
    private static int nrE = 0;
    private static int nrW = 0;
    private static int nrP = 0;
    public static int nrPx = 0;

    private static Stack<int> labelStack = new Stack<int>();
    private static Stack<int> elseLabelStack = new Stack<int>();
    private static Stack<int> whileLabelStack = new Stack<int>();
    private static Stack<int> parStack = new Stack<int>();

    private static StreamWriter sw;

    private static void GenProlog()
    {
        EmitCode(".assembly extern mscorlib { }");
        EmitCode(".assembly mini { }");
        EmitCode(".method static void main()");
        EmitCode("{");
        EmitCode(".entrypoint");
        EmitCode(".maxstack 64");
        EmitCode(".try");
        EmitCode("{");
        EmitCode();

        EmitCode("// prolog");
        EmitCode(".locals init ( float64 __temp )");
    }

    private static void GenEpilog()
    {
        EmitCode("leave EndMain");
        EmitCode("}");
        EmitCode("catch [mscorlib]System.Exception");
        EmitCode("{");
        EmitCode("callvirt instance string [mscorlib]System.Exception::get_Message()");
        EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
        EmitCode("leave EndMain");
        EmitCode("}");
        EmitCode("EndMain: ret");
        EmitCode("}");
    }

}
