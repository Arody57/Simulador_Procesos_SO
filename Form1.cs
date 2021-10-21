using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Simulacion_Procesos
{
    public partial class gridAsignationProcess : Form
    {
        info_procesos Dialogo1;
        int processId = 1;
        //Creacion de colas para el proceso del programa
        Cola.Cola QueueProgress = new Cola.Cola();
        Cola.Cola QueueReady = new Cola.Cola();
        Cola.Cola QueueRunning = new Cola.Cola();
        Cola.Cola QueueWait = new Cola.Cola();
        Cola.Cola QueueFinal = new Cola.Cola();
        public gridAsignationProcess()
        {
            InitializeComponent();
        }

        private void procesosInicialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogo1 = new info_procesos(this);
            Dialogo1.Show();
        }
        public void setDataGrid(List<string> process)
        {
            process.Insert(0, (processId++).ToString());
            string[] arr = process.ToArray();

            gridProcess.Rows.Add(arr);

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void asignarProcesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogo1 = new info_procesos(this);
            Dialogo1.Show();
        }
    }
}
