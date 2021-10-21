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
    public partial class info_procesos : Form
    {
       
        gridAsignationProcess FormPrincipal;
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
                    txtMemory.Text = "0.0005";
                    txtCpu.Text = "10";
                    txtTime.Text = "60";
                    break;
                case 1:
                    txtMemory.Text = "0.01";
                    txtCpu.Text = "20";
                    txtTime.Text = "45";
                    break;
                case 2:
                    txtMemory.Text = "0.05";
                    txtCpu.Text = "30";
                    txtTime.Text = "30";
                    break;
                case 3:
                    txtMemory.Text = "1";
                    txtCpu.Text = "70";
                    txtTime.Text = "120";
                    break;
                case 4:
                    txtMemory.Text = "0.256";
                    txtCpu.Text = "50";
                    txtTime.Text = "30";
                    break;
                case 5:
                    txtMemory.Text = "2";
                    txtCpu.Text = "85";
                    txtTime.Text = "120";
                    break;
                default:
                    break;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            List<string> el = new List<string>();

            el.Add(cmbProcess.SelectedItem.ToString());
            el.Add((float.Parse(txtMemory.Text)).ToString());
            el.Add((Convert.ToInt32(txtCpu.Text)).ToString());
            el.Add((Convert.ToInt32(txtTime.Text)).ToString());
                FormPrincipal.setDataGrid(el);
                Dispose();
        }
    }
}
