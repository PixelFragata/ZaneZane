using System;
using System.Diagnostics;
using System.Reflection;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;

namespace ZZ_ERP.Infra.CrossCutting.Connections.Functions
{
	public class ConsoleEx
	{
	    private static Print SpecificPrint = Print.NormalPrint;

		public static void WriteLine(string line, Print print = Print.NormalPrint)
		{
			/*Pega o metodo anterior na pilha*/
			var frame = new StackFrame(1);
			var method = frame.GetMethod();

		    if (print == Print.SpecificPrint)
		    {
		        SpecificPrint = print;
		    }

		    if (method?.DeclaringType != null)
			{
				var nameSpace = method.DeclaringType.Namespace;
				Console.ResetColor();

			    if (SpecificPrint == Print.SpecificPrint)
			    {
			        if (print == Print.SpecificPrint)
			        {
			            Console.ForegroundColor = ConsoleColor.Magenta;
			        }
			        else
			        {
			            Console.ForegroundColor = ConsoleColor.White;
			        }
			    }else
			    {
			        Console.ForegroundColor = WhoIs(nameSpace);
			    }
			}
			Console.WriteLine(line);
		}
		
		public static void WriteError(string line, Exception e)
		{
			SpecificPrint = Print.SpecificPrint;
			
			/*Pega o metodo anterior na pilha*/
			StackFrame frame = new StackFrame(1,true);
			MethodBase method = frame.GetMethod();
			var lineNumber = frame.GetFileLineNumber();
			if (method?.DeclaringType != null)
			{
				string nameSpace = method.DeclaringType.Namespace;

				Console.ResetColor();
				Console.ForegroundColor = WhoIs(nameSpace);
			}

			//line += "Erro em | " + e;
			line += "Erro em | " + e.StackTrace + " | " + e.Message + "  |  line:  " + lineNumber + e;
			Console.WriteLine(line);
		}
		
		public static void WriteError(Exception e)
		{
			/*Pega o metodo anterior na pilha*/
			SpecificPrint = Print.SpecificPrint;
			StackFrame frame = new StackFrame(1, true);
			MethodBase method = frame.GetMethod();
			var lineNumber = frame.GetFileLineNumber();
			if (method?.DeclaringType != null)
			{
				var nameSpace = method.DeclaringType.Namespace;
				frame = new StackFrame(1, true);
				method = frame?.GetMethod();
				//var line = "Erro em | " + e.StackTrace + " | " + e;
				var line = "Erro em | "  + method?.Name  + " | "+ e.StackTrace + " | " + e.Message + "  |  line:  " + lineNumber + e;
				Console.ResetColor();
				Console.ForegroundColor = WhoIs(nameSpace);
				Console.WriteLine(line);
			}
			else
			{
				var line = "Erro em | " + e.StackTrace + " | " + e;
//				var line = "Erro em | "+ e.StackTrace + " | " + e.Message + "  |  line:  " + lineNumber + e;
				Console.WriteLine(line);
			}
		}

		private static ConsoleColor WhoIs(string sender)
		{
			//adicionar todos aqui
			ConsoleColor consColor = ConsoleColor.Blue;

            if (sender.Contains("ZZ_ERP.DataApplication"))
            {
                consColor = ConsoleColor.Green;
                //Console.Write("Melchior : ");
            }
            else if (sender.Contains("ZZ_ERP.API"))
            {
                consColor = ConsoleColor.Blue;
                //Console.Write("Zacarias : ");
            }
            else if (sender.Contains("MongoRepository"))
            {
                consColor = ConsoleColor.Yellow;
                //Console.Write("MongoRepository : ");
            }
            else if (sender.Contains("Adam"))
            {
                consColor = ConsoleColor.DarkMagenta;
                //Console.Write("Adam : ");
            }
            else if (sender.Contains("Universal"))
            {
                consColor = ConsoleColor.Cyan;
                //Console.Write("Universal : ");
            }
            else if (sender.Contains("Gaspar"))
            {
                consColor = ConsoleColor.Gray;
                //Console.Write("Gaspar : ");
            }
            else if (sender.Contains("Baltasar"))
            {
                consColor = ConsoleColor.Magenta;
                //Console.Write("Baltasar : ");
            }
            else if (sender.Contains("Lucifer")) 
            {
                consColor = ConsoleColor.DarkRed;
                //Console.Write("Lucifer : ");
            }
            else if (sender.Contains("Jezebeth")) 
            {
	            consColor = ConsoleColor.DarkCyan;
	            //Console.Write("Lucifer : ");
            }

			//Thread.Sleep(100);
			return consColor;
		}

	}

   
}
