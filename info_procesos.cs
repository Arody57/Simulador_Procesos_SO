using Simulacion_Procesos.InformacionProcesso;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyectoDisney_.variable_Global;


namespace Simulacion_Procesos
{
    public partial class info_procesos : Form
    {
       
        gridAsignationProcess FormPrincipal;
        //int idItem = variableGlobal.id_proceso = 1;
        public info_procesos(gridAsignationProcess FormInit)
        {
            FormPrincipal = FormInit;
            InitializeComponent();
        }

        private void changeOptionProcess(object sender, EventArgs e)
        {
            int index = cmbProcess.SelectedIndex;
            btnAdd.Enabled = true;
            switch (index)
            {
                case 0:
                    txtMemory.Text = "0.05";
                    txtCpu.Text = "10";
                    txtTime.Text = "60";
                    break;
                case 1:
                    txtMemory.Text = "10";
                    txtCpu.Text = "20";
                    txtTime.Text = "45";
                    break;
                case 2:
                    txtMemory.Text = "50";
                    txtCpu.Text = "30";
                    txtTime.Text = "30";
                    break;
                case 3:
                    txtMemory.Text = "1024";
                    txtCpu.Text = "70";
                    txtTime.Text = "120";
                    break;
                case 4:
                    txtMemory.Text = "256";
                    txtCpu.Text = "50";
                    txtTime.Text = "30";
                    break;
                case 5:
                    txtMemory.Text = "2048";
                    txtCpu.Text = "85";
                    txtTime.Text = "120";
                    break;
                default:
                    break;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
         
            info_process_grid newItem = new info_process_grid(variableGlobal.id_proceso++, cmbProcess.SelectedItem.ToString(), float.Parse(txtMemory.Text), Convert.ToInt32(txtCpu.Text), Convert.ToInt32(txtTime.Text));
            FormPrincipal.setDataGrid(newItem);
            Dispose();
        }

    }
}
