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
        Cola.Cola QeueueInterrupt = new Cola.Cola();
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
        //Ingresa a la grid interrupt
        public void enqueueGridInterrupt()
        {
            int lengthProc = QueueRunning.NumeroElementos;

                if (gridRunning.RowCount > 0)
                {
                    MessageBox.Show("Moviendo cola " + gridRunning.CurrentRow.Selected + "  a estado Interrumpido ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridRunning.Rows.RemoveAt(gridRunning.CurrentRow.Index);
                    info_process_grid index;
                    index = (info_process_grid)QueueRunning.Pop();

                    string[] items = index.ToString().Split(',');
                    gridInterrupt.Rows.Add(items);


                    QeueueInterrupt.Push(index);
                    timerInter.Start();
                }
            
        }
        //sale de la grid interrupt hacia grid ready 
        public void dequeueInterruptProcess()
        {
            int length = QeueueInterrupt.NumeroElementos;
            for (int a = 0; a< length; a++)
            {
                if (gridInterrupt.RowCount > 0)
                {
                    MessageBox.Show("Moviendo cola " + gridInterrupt.CurrentRow.Selected + "  a estado Ready ", "Informacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    gridInterrupt.Rows.RemoveAt(gridInterrupt.CurrentRow.Index);
                    info_process_grid indexProces;
                    indexProces = (info_process_grid)QeueueInterrupt.Pop();

                    string[] itemProcess = indexProces.ToString().Split(',');
                    
                    gridReady.Rows.Add(itemProcess);
                    QueueReady.Push(indexProces);
                }
                timer3.Stop();
                dequeueEnqueueProcess1();
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

        private void tmrDev_Tick(object sender, EventArgs e)//
        {
            int aux = Convert.ToInt32(mls.Text);
            aux = aux+1;

            //Milisegudos
            mls.Text = aux.ToString();

            if (aux == 5) //segundos en estado NEW para pasar a ready y runing 
            {
                tmrDev.Stop(); 
                dequeueEnqueueProcess();
                mls.Text = "0";
                aux = 0;
            }
           
        }
        private void timer2_Tick(object sender, EventArgs e)/////RUNING
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
                bool enqueueProcess = variableGlobal.count == 2 ? true : false;

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
        private void timerInter_Tick(object sender, EventArgs e)
        {
            int AuxiliarTime = Convert.ToInt32(mls.Text);
            AuxiliarTime = AuxiliarTime + 1;

            //Milisegudos
            mls.Text = AuxiliarTime.ToString();

            if (AuxiliarTime == 6)
            {
                timerInter.Stop();
                //120
                string timeProcess = variableGlobal.inProcess[3];
                int auxInProgress = AuxiliarTime; //60
                int auxProcess = Convert.ToInt32(timeProcess);
                variableGlobal.calcRest = auxProcess - auxInProgress;

                dequeueInterruptProcess();

                mls.Text = "0";
                AuxiliarTime = 0;
            }
        }


        private void gridAsignationProcess_FormClosing(object sender, FormClosingEventArgs e)

     



            private void gridAsignationProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDev.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timerInter.Enabled = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
                tmrDev.Enabled = false;
            if (gridRunning.SelectedRows.Count > 0)
            {

                timer2.Enabled = false;
                timer3.Enabled = false;
                //string value = gridRunning.CurrentRow.Cells["Nombre"].Value.ToString();
                //Desde la grid, hacer que pase a la grid nueva de interrupt dandole un tiempo random de espera y luego que encole a Ready
                mls.Text = "0";
                enqueueGridInterrupt();
                timer2.Enabled = false;
            }
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void mls_Click(object sender, EventArgs e)
        {

        }
    }
}
