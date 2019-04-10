using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;    

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DriveInfo[] u = DriveInfo.GetDrives();
            foreach (DriveInfo unidad in u)
            {
                try
                {
                    listBox1.Items.Add(unidad.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error leyendo los discos"+ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            //String unidades = listBox2.SelectedIndex.ToString();
            String unidades = listBox1.SelectedItem.ToString();
            //listBox2.Items.Add(unidades);
            DriveInfo d = new DriveInfo(unidades);
            listBox2.Items.Add("Disco: "+d.Name+"\n");
            if (d.IsReady)
            {
                listBox2.Items.Add("espacio ocupado (GB): " + (d.TotalSize - d.AvailableFreeSpace) / 1024 / 1024 / 1024 + "\n");
                listBox2.Items.Add("espacio disponible (GB): " + (d.TotalFreeSpace) / 1024 / 1024 / 1024 + "\n");
                listBox2.Items.Add("espacio total (GB): " + (d.TotalSize) / 1024 / 1024 / 1024 + "\n");
            }
            listBox2.Items.Add("tipo de disco utilizado" + d.DriveType.ToString());

            treeView1.Nodes.Clear();
            if (d.IsReady)
            {
                DirectoryInfo dir = new DirectoryInfo(unidades);
                foreach (DirectoryInfo sub in dir.GetDirectories())
                {
                    treeView1.Nodes.Add(sub.Name);
                }

                foreach (FileInfo file in dir.GetFiles())
                {
                    TreeNode nodo = new TreeNode();
                    nodo.Text = file.Name;
                    nodo.ForeColor = Color.Blue;
                    treeView1.Nodes.Add(nodo);
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String Dir = "C:/test";
            String arch = "C:/prueba.txt";
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
            else
            {
                MessageBox.Show("Existe el directorio");
            }

            if (!File.Exists(arch))
            {
                File.Create(arch);
            }
            else
            {
                MessageBox.Show("Existe el archivo");
            }
        }
    }
}
