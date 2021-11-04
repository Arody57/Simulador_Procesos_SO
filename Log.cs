using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulacion_Procesos
{
	public class Log
	{
		private string Path = "";

		public Log(String Path)
		{
			this.Path = Path;
		}

		public void Add()
		{
			CreateDirectory();
			string nombre = GetNameFile();
			string cadena = "";//sustitutir por cola
			//string nombre cola
			//string nombre proceso
			cadena += DateTime.Now + "prueba " + Environment.NewLine;
			StreamWriter sw = new StreamWriter(Path + "/" + nombre, true);
			sw.Write(cadena);
			sw.Close();


		}
		private string GetNameFile()
		{
			string nombre = "";
			nombre = "log" + DateTime.Now.Year + " " + DateTime.Now.Month + " " + DateTime.Now + "Log.txt";

			return nombre;
		}


		private void CreateDirectory()
		{
			try
			{
				if (!Directory.Exists(Path))
					Directory.CreateDirectory(Path);

			}
			catch (DirectoryNotFoundException ex)
			{
				throw new Exception(ex.Message);
				
			}
			
		}
	}
}







