using proyectoDisney_.variable_Global;
using Simulacion_Procesos.InformacionProcesso;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Simulacion_Procesos
{
    public partial class gridAsignationProcess : Form
    {
        info_procesos Dialogo1;

        //Creacion de colas para el proceso del programa
        Cola.Cola QueueProgress = new Cola.Cola();
        Cola.Cola QueueNew = new Cola.Cola();
        Cola.Cola QueueReady = new Cola.Cola();
        Cola.Cola QueueRunning = new Cola.Cola();
        Cola.Cola QueueWait = new Cola.Cola();
        Cola.Cola QueueFinal = new Cola.Cola();
        public string[] arr;
        public gridAsignationProcess()
        {
            InitializeComponent();
            variableGlobal.count = 0;
        }

        private void procesosInicialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogo1 = new info_procesos(this);
            Dialogo1.Show();
        }
        public void setDataGrid(info_process_grid process)
        {
            //Agregando a cola de procesos
            QueueProgress.Push(process);
            string[] itemProcess = process.ToString().Split(',');
            //gridProcess.Rows.Add(itemProcess);
            gridProcess.Rows.Insert(0, itemProcess);
            gridProcess.ClearSelection();
            gridProcess.Rows[0].Selected = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            creacionProceso();
            tmrDev.Start();
            
        }

        private void asignarProcesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogo1 = new info_procesos(this);
            Dialogo1.Show();
        }
        /// <summary>
        /// GRID NUEVO
        /// </summary>
        public void creacionProceso()
        {
            int LengthProcess = QueueProgress.NumeroElementos;
            variableGlobal.count = 1;

            for (int i = 0; i < LengthProcess; i++)
            {
                if (gridProcess.RowCount > 0)
                {
                    //gridProcess.Rows.RemoveAt(gridProcess.CurrentRow.Index);
                    MessageBox.Show("Moviendo cola " + gridProcess.CurrentRow.Index + "  a estado NEW ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridProcess.Rows.Remove(gridProcess.CurrentRow);
                    info_process_grid indexProces;
                    indexProces = (info_process_grid)QueueProgress.Pop();
                    string[] itemProcess = indexProces.ToString().Split(',');
                    gridNew.Rows.Add(itemProcess);
                    QueueNew.Push(indexProces);
                }
            }
        }
        /// <summary>
        /// GRID READY
        /// </summary>
        public void dequeueEnqueueProcess()
        {
            int lengthProcess = QueueNew.NumeroElementos;

                if (gridNew.RowCount > 0)
                {
                    MessageBox.Show("Moviendo cola " + gridNew.CurrentRow.Selected + "  a estado Ready ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridNew.Rows.RemoveAt(gridNew.CurrentRow.Index);
                    info_process_grid indexProces;
                    indexProces = (info_process_grid)QueueNew.Pop();

                    string[] itemProcess = indexProces.ToString().Split(',');
                    variableGlobal.inProcess = itemProcess;
                    gridReady.Rows.Add(itemProcess);
                    QueueReady.Push(indexProces);

                }
            dequeueEnqueueProcess1();
        }
        /// <summary>
        /// Grid RUNNING
        /// </summary>
        public void dequeueEnqueueProcess1()
        {
            int lengthProcess = QueueReady.NumeroElementos;
            for (int i = 0; i < lengthProcess; i++)
            {
                if (gridReady.RowCount > 0)
                {
                    MessageBox.Show("Moviendo cola " + gridReady.CurrentRow.Selected + "  a estado Running ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridReady.Rows.RemoveAt(gridReady.CurrentRow.Index);
                    info_process_grid indexProces;
                    indexProces = (info_process_grid)QueueReady.Pop();

                    string[] itemProcess = indexProces.ToString().Split(',');
                    gridRunning.Rows.Add(itemProcess);

                    QueueRunning.Push(indexProces);
                        timer2.Start();

                }
            }
        }

        public void dequeueWaitProcess()
        {
            int proLength = QueueWait.NumeroElementos;
            if (gridWait.RowCount > 0)
            {
                MessageBox.Show("Moviendo cola " + gridWait.CurrentRow.Selected + "  a estado Ready ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                gridWait.Rows.RemoveAt(gridWait.CurrentRow.Index);
                info_process_grid indexProc;
                indexProc = (info_process_grid)QueueWait.Pop();

                string[] item = indexProc.ToString().Split(',');
                gridReady.Rows.Add(item);
                QueueReady.Push(indexProc);
            }
            timer3.Stop();
            dequeueEnqueueProcess1();
        }
        /// <summary>
        /// Moviendo a cola Final
        /// </summary>
        public void EnqueueFinished()
        {
            mls.Text = "0";
            int vLengthPro = QueueRunning.NumeroElementos;
            for (int i = 0;  i< vLengthPro; i++)
            {
                if (gridRunning.RowCount > 0)
                {
                    MessageBox.Show("Moviendo cola " + gridRunning.CurrentRow.Selected + "  a estado Finalizado ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridRunning.Rows.RemoveAt(gridRunning.CurrentRow.Index);
                }
            }
            info_process_grid indexP;
            indexP = (info_process_grid)QueueRunning.Pop();

            string[] itemEl = indexP.ToString().Split(',');
            gridFinal.Rows.Add(itemEl);
            QueueFinal.Push(itemEl);
            MessageBox.Show("Proceso finalizado correctamente", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void tmrDev_Tick(object sender, EventArgs e)
        {
            int aux = Convert.ToInt32(mls.Text);
            aux = aux+1;

            //Milisegudos
            mls.Text = aux.ToString();

            if (aux == 5) //segundos
            {
                tmrDev.Stop(); 
                dequeueEnqueueProcess();
                mls.Text = "0";
                aux = 0;
            }
           
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            int n1 = Convert.ToInt32(mls.Text);
            n1 = n1 + 1;

            mls.Text = n1.ToString();
            if (n1 == 10) //60
            {
                timer2.Stop();
                string timeProcess = variableGlobal.inProcess[3];
                int auxInProgress = n1;
                int auxProcess = Convert.ToInt32(timeProcess);
                bool enqueueProcess = variableGlobal.count == 3 ? true : false;

                if (enqueueProcess)
                    EnqueueFinished();
                else
                EnqueueWaiting();
                variableGlobal.count = variableGlobal.count + 1;

            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            int devAux = Convert.ToInt32(mls.Text);
            devAux = devAux + 1;

            //Milisegudos
            mls.Text = devAux.ToString();

            if (devAux == 30)
            {
                tmrDev.Stop();
              //120
                string timeProcess = variableGlobal.inProcess[3];
                int auxInProgress = devAux; //60
                int auxProcess = Convert.ToInt32(timeProcess);
                variableGlobal.calcRest = auxProcess - auxInProgress;

                    dequeueWaitProcess();

                //dequeueWaitProcess();
                mls.Text = "0";
                devAux = 0;
            }
        }

        private void gridAsignationProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDev.Enabled = false;
            timer2.Enabled = false;
        }

        public void EnqueueWaiting()
        {
            mls.Text = "0";
            int lengthProcess = QueueRunning.NumeroElementos;
            for (int i = 0; i < lengthProcess; i++)
            {
                if (gridRunning.RowCount > 0)
                {
                    MessageBox.Show("Moviendo cola " + gridRunning.CurrentRow.Selected + "  a estado Waiting ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridRunning.Rows.RemoveAt(gridRunning.CurrentRow.Index);
                }
            }
            info_process_grid indexProces;
            indexProces = (info_process_grid)QueueRunning.Pop();

            string[] itemProcess = indexProces.ToString().Split(',');
            gridWait.Rows.Add(itemProcess);
            QueueWait.Push(indexProces);
            timer3.Start();
        }
    }
}
