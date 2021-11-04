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
		//Pat para crear una carpeta dentro del proyecto en ejecucion bin/debug
		public Log(string Path)
		{
			this.Path = Path;
		}
		//Se añade data al archivo, se crea el nombre automaticamente con la fecha 
		public void Add(string logger)
		{
			CreateDirectory();
			var dates = DateTime.Now;
			var Date = dates.Date.ToString("dd-MM-yyyy");
			string nombre = String.Format("log_{0}_Proceso_finalizado", Date);
			string cadena = "";//sustitutir por cola
			//string nombre cola
			//string nombre proceso
			cadena += DateTime.Now + logger + Environment.NewLine;
			StreamWriter file = new StreamWriter(Path + "/" + nombre, true);
			file.Write(cadena);
			file.Close();
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







