using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace ROOT
{
    public partial class ExtTool : Form
    {
        public ExtTool()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //creates the list and adds all check boxes to it.
            List<CheckBox> stageList = new List<CheckBox>();
            stageList.Add(checkBox1);
            stageList.Add(checkBox2);
            stageList.Add(checkBox3);
            stageList.Add(checkBox4);
            stageList.Add(checkBox5);
            stageList.Add(checkBox6);
            stageList.Add(checkBox7);
            stageList.Add(checkBox8);
            stageList.Add(checkBox9);
            stageList.Add(checkBox10);
            stageList.Add(checkBox11);
            stageList.Add(checkBox12);
            stageList.Add(checkBox13);
            stageList.Add(checkBox14);
            stageList.Add(checkBox15);
            stageList.Add(checkBox16);
            stageList.Add(checkBox17);
            stageList.Add(checkBox18);
            stageList.Add(checkBox19);
            stageList.Add(checkBox20);
            stageList.Add(checkBox21);
            stageList.Add(checkBox22);
            stageList.Add(checkBox23);
            stageList.Add(checkBox24);
            stageList.Add(checkBox25);
            stageList.Add(checkBox26);
            stageList.Add(checkBox27);
           

            //initializes the stream writer to create a file named whatever was in the textbox.
            StreamWriter stageWrite = new StreamWriter(textBox1.Text);
            

            //puts a block wherever a check was placed.
            for(int x = 0; x < stageList.Count; x++)
            {
                if(stageList[x].Checked == true)
                {
                    if(x%5 == 0 && x<16 || x == 18 || x == 20 || x == 22)
                    {
                        stageWrite.WriteLine("x.");
                    }
                    else if(x == 16 || x == 17)
                    {
                        stageWrite.WriteLine("x.-.");
                    }
                    else if(x == 21)
                    {
                        stageWrite.WriteLine("x.-.-.-.");
                    }
                    else
                    {
                        stageWrite.Write("x.");
                    }
                    
                }
                else
                {
                    if (x % 5 == 0 && x < 16 || x == 18 || x == 20 || x == 22)
                    {
                        stageWrite.WriteLine("-.");
                    }
                    else if (x == 16 || x == 17)
                    {
                        stageWrite.WriteLine("-.-.");
                    }
                    else if (x == 21)
                    {
                        stageWrite.WriteLine("-.-.-.-.");
                    }
                    else
                    {
                        stageWrite.Write("-.");
                    }
                }
                
            }
            stageWrite.Close();
        }
    }
}
