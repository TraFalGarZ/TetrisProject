using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;


namespace Tetris
{
    public partial class Form1 : Form
    {
        bool Form_activated;
        int[,] feldstatus;
        int[] currBlock;
        int currBlockID;
        int[] currPanelrow;
        int[] currPanelcol;
        ArrayList PanelList = new ArrayList();
        Color[] FarbenFeld = {Color.Red,Color.Yellow, Color.Green, Color.Blue,Color.Cyan, Color.Magenta, Color.Black,Color.White};
        const int Leer = -1;
        const int Rand = -2;
        Random rand = new Random();
        
        public Form1()
        {
            InitializeComponent();

            erstellen();
           // erstellen_test();
           // Form_activated = true;
           //nächstesPanel(rand.Next(0,7));
            nächstesPanel(0);
            
        }


        private void erstellen()
        {
            int i,j;
            int xAchse = 0, yAchse = 0;
            tableLayoutPanel1.Visible = false;
            panel_out.Visible = false;
            label_countdown.Visible = false;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();  
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.ColumnCount = 0;   
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            
            Austausch.anz_row= 20;
            Austausch.anz_col=  11;

            //x   /-/  y
            //col /-/ row
            //i   /-/  j
            feldstatus = new int[Austausch.anz_col, Austausch.anz_row+1]; //Feld eig von -1 bis maximale Anzahl+1
            for (j = 0; j < Austausch.anz_row; j++)
            {
                //ganze rechte Spalte wird als Rand gesetzt
                feldstatus[0, j] = -2;
                //Mittleres Feld ist leer
                for (i = 1; i < Austausch.anz_col-1; i++)
                {
                    feldstatus[i, j] = -1;
                }
                //linke Spalte wird als Rand gesetzt
                feldstatus[ Austausch.anz_col-1,j] = -2;           //-2 = Rand / -1 = leer
            }
            //ganze Untere Reihe des Spielfeldes wird als Rand gesetzt
            for (i = 0; i < Austausch.anz_col; i++)
            {
                feldstatus[i,Austausch.anz_row] = -2;
            }

            int row_index; 
            int col_index;
            row_index = 0;
            col_index = 0;          
             
             for (i = 0; i < Austausch.anz_col; i++)
             {
                  col_index = AddTableCol();
             }

             for (i = 0; i < Austausch.anz_row; i++)
             {
                 row_index = AddTableRow();

                 for (j = 0; j < Austausch.anz_col; j++)
                 {
                     xAchse = j;
                     yAchse = Austausch.anz_row - (i+1);
                     Panel Panel1 = new Panel();
                     Panel1.Name = "panelx" + xAchse+"y"+yAchse;
                     Panel1.BorderStyle = BorderStyle.FixedSingle;
                     Panel1.BackColor = Color.Transparent;
                     Panel1.Margin = new Padding(0, 0, 0, 0);
                    // tableLayoutPanel1.Controls.Add(Panel1, j, row_index);
                     
                 }
             }
             panel_out.Size = new Size(tableLayoutPanel1.Size.Width + 10, tableLayoutPanel1.Size.Height + 10);
             
        }

        private int AddTableRow()
        {
            int index = this.tableLayoutPanel1.RowCount;
            this.tableLayoutPanel1.RowCount++;
            RowStyle style = new RowStyle(SizeType.Absolute);
            style.Height = 30;
            tableLayoutPanel1.RowStyles.Add(style);            
            return index;
        }

        private int AddTableCol()
        {
            int index = this.tableLayoutPanel1.ColumnCount;
            this.tableLayoutPanel1.ColumnCount++;
            ColumnStyle style = new ColumnStyle(SizeType.Absolute);
            style.Width = 30;
            tableLayoutPanel1.ColumnStyles.Add(style);
            return index;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            panel_out.Visible = true;
            tableLayoutPanel1.Visible = true;

            timer_fall.Start();
            this.ActiveControl = null;
            
           
        }

        //Pfeiltasten überschreiben
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                
                return true; //<-- Um zu verhindern, dass Focus weitergereicht wird
            }
            else if (keyData == Keys.Down)
            {
                
                return true; //<-- Um zu verhindern, dass Focus weitergereicht wird
            }
            if (keyData == Keys.Right)
            {
                return true;
            }
            if (keyData == Keys.Left)
            {
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer_block_down.Enabled = false;
            timer_fall.Enabled = false;
            this.ActiveControl = null;
        }

        //Methode Blöcke fallen
        private void timer_block_down_Tick(object sender, EventArgs e)
        {
            int i, j, z;
            string panelName;
            Color colorsave;
            Panel panelControl;
            Panel panelControl_under;
            bool block_under=false;            


            if (Form_activated == true)
            {
                
                for (i = 0; i < Austausch.anz_col; i++)
                {
                    for (j = 0; j < Austausch.anz_row; j++)
                    {
                        panelName = "panelx" + i + "y" + j;
                        panelControl = (Panel)tableLayoutPanel1.Controls.Find(panelName, false).FirstOrDefault();

                        if (panelControl.BackColor == Color.Red)
                        {
                            z = j - 1;
                            if (z >= 0)
                            {
                                panelName = "panelx" + i + "y" + z;
                                panelControl_under = (Panel)tableLayoutPanel1.Controls.Find(panelName, false).FirstOrDefault();

                                if (panelControl_under.BackColor != Color.Red)
                                {
                                    colorsave = panelControl.BackColor;
                                    panelControl.BackColor = Color.Transparent;
                                    panelControl_under.BackColor = colorsave;
                                }
                            }
                        }
                    }
                } //End For-Schleife
            }// End If-Schleife
          
        }

        //Methode Testblock erstellen
        private void erstellen_test()
        {
            int i, j;
            string panelName;
            Panel panelControl;

            for (i = 0; i < (Austausch.anz_col - (Austausch.anz_col - 2)); i++)
            {
                for (j = Austausch.anz_row-1; j > (Austausch.anz_row - 3); j--)
                {
                    panelName = "panelx" + i + "y" + j;
                    panelControl = (Panel)tableLayoutPanel1.Controls.Find(panelName, false).FirstOrDefault();
                    panelControl.BackColor = Color.Red;
                    panelControl.BorderStyle = BorderStyle.FixedSingle;

                }
            }
            
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            int i, j, z;
            string panelName;
            Color colorsave;
            Panel panelControl;
            Panel panelControl_right;
            
            switch (e.KeyCode)
            {
                case Keys.Down:

                    //Hier einfügen was passieren soll wenn Pfeil nach unten gedrückt wird

                    break;
                case Keys.Right:

                     //Hier einfügen was passieren soll wenn Rechte Pfeiltaste gedrückt wird 

                    break;
                case Keys.Up:

                    //Hier einfügen was passieren soll wenn Pfeil nach oben gedrückt wird

                    break;
                case Keys.Left:

                    //Hier einfügen was passieren soll wenn Linke Pfeiltaste gedrückt wird

                    break;
            }

            
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            Form_activated = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Form_activated = true;
        }

        private void Panel_erstellen(int form)
        {
            int x;

            Panel[] Panel1 = new Panel[4];
            for (x = 0; x < 4; x++)
            {
                Panel1[x] = new Panel();
                Panel1[x].BorderStyle = BorderStyle.FixedSingle;
                Panel1[x].Margin = new Padding(0, 0, 0, 0);

            }

            switch (form)
            {
                case 0:
                    //Quadrat
                    for (x = 0; x < 4; x++)
                    {
                        Panel1[x].BackColor = Color.Red;
                        Panel1[x].Name = "Q" + x;
                    }

                    tableLayoutPanel1.Controls.Add(Panel1[0], (Austausch.anz_col / 2) - 1, 0);
                    feldstatus[(Austausch.anz_col / 2) - 1, 0] = 0;
                    tableLayoutPanel1.Controls.Add(Panel1[1], (Austausch.anz_col / 2), 0);
                    feldstatus[(Austausch.anz_col / 2), 0] = 0;
                    tableLayoutPanel1.Controls.Add(Panel1[2], (Austausch.anz_col / 2) - 1, 1);
                    feldstatus[(Austausch.anz_col / 2) - 1, 1] = 0;
                    tableLayoutPanel1.Controls.Add(Panel1[3], (Austausch.anz_col / 2), 1);
                    feldstatus[(Austausch.anz_col / 2), 1] = 0;                    
                    break;

                case 1:
                    //T-Block
                    for (x = 0; x < 4; x++)
                    {
                        Panel1[x].Name = "T" + x;
                        Panel1[x].BackColor = Color.Blue;
                    }

                    tableLayoutPanel1.Controls.Add(Panel1[0], (Austausch.anz_col / 2) - 1, 0);
                    feldstatus[(Austausch.anz_col / 2) - 1, 0] = 1;
                    tableLayoutPanel1.Controls.Add(Panel1[1], (Austausch.anz_col / 2), 0);
                    feldstatus[(Austausch.anz_col / 2), 0] = 1;
                    tableLayoutPanel1.Controls.Add(Panel1[2], (Austausch.anz_col / 2) + 1, 0);
                    feldstatus[(Austausch.anz_col / 2) + 1, 0] =1;
                    tableLayoutPanel1.Controls.Add(Panel1[3], (Austausch.anz_col / 2), 1);
                    feldstatus[(Austausch.anz_col / 2), 1] = 1;
                    break;

                case 2:
                    //L-Block


                    break;



            }
        }

        private void nächstesPanel(int form)
        {
            int Farbe;
            int index;
            Panel[] p = new Panel[4];
            Farbe = rand.Next(0, 8);
            currBlock = new int[4];
            
            currPanelrow = new int[4];
            currPanelcol = new int[4];
            for (index = 0; index < 4; index++)
            {
                p[index] = new Panel();
                PanelList.Add(p[index]);
                p[index].Size = new Size(30, 30);
                p[index].BorderStyle = BorderStyle.FixedSingle;
                p[index].Margin = new Padding(0, 0, 0, 0);
                p[index].BackColor = FarbenFeld[Farbe];
                currBlock[index] = PanelList.Count - 1;
                p[index].Name = "panel" + currBlock[index];
                Debug.Print(Convert.ToString(p[index].Name));
            }

            switch (form)
            {
                case 0:
                    //Würfel
                    currPanelrow[0] = 0;
                    currPanelcol[0] = (Austausch.anz_col / 2)-1;
                    currPanelrow[1] = 0;
                    currPanelcol[1] = (Austausch.anz_col / 2);
                    currPanelrow[2] = 1;
                    currPanelcol[2] = (Austausch.anz_col / 2)-1;
                    currPanelrow[3] = 1;
                    currPanelcol[3] = (Austausch.anz_col / 2);
                    currBlockID = 0;
                 break;

                case 1:
                    //L-Block
                     currPanelrow[0] = 1;
                     currPanelcol[0] = (Austausch.anz_col / 2) +1;
                     currPanelrow[1] = 0;
                     currPanelcol[1] = (Austausch.anz_col / 2)+1;
                     currPanelrow[2] = 1;
                     currPanelcol[2] = (Austausch.anz_col / 2);
                     currPanelrow[3] = 1;
                     currPanelcol[3] = (Austausch.anz_col / 2)-1;
                     currBlockID = 1;
                 break;

                case 2:
                 //J-Block
                     currPanelrow[0] = 1;
                     currPanelcol[0] = (Austausch.anz_col / 2) + 1;
                     currPanelrow[1] = 1;
                     currPanelcol[1] = (Austausch.anz_col / 2);
                     currPanelrow[2] = 1;
                     currPanelcol[2] = (Austausch.anz_col / 2)-1;
                     currPanelrow[3] = 0;
                     currPanelcol[3] = (Austausch.anz_col / 2)-1;
                     currBlockID = 2;
                 break;

                 case 3:
                 //S-Block
                     currPanelrow[0] = 1;
                     currPanelcol[0] = (Austausch.anz_col / 2) - 1;
                     currPanelrow[1] = 1;
                     currPanelcol[1] = (Austausch.anz_col / 2);
                     currPanelrow[2] = 0;
                     currPanelcol[2] = (Austausch.anz_col / 2);
                     currPanelrow[3] = 0;
                     currPanelcol[3] = (Austausch.anz_col / 2)+1;
                     currBlockID = 3;
                 break;

                case 4:
                    //Z-Block
                     currPanelrow[0] = 0;
                     currPanelcol[0] = (Austausch.anz_col / 2)-1 ;
                     currPanelrow[1] = 0;
                     currPanelcol[1] = (Austausch.anz_col / 2);
                     currPanelrow[2] = 1;
                     currPanelcol[2] = (Austausch.anz_col / 2);
                     currPanelrow[3] = 1;
                     currPanelcol[3] = (Austausch.anz_col / 2)+1;
                     currBlockID = 4;
                 break;
                 
                case 5:
                    //T-Block
                     currPanelrow[0] = 0;
                     currPanelcol[0] = (Austausch.anz_col / 2)-1;
                     currPanelrow[1] = 0;
                     currPanelcol[1] = (Austausch.anz_col / 2);
                     currPanelrow[2] = 0;
                     currPanelcol[2] = (Austausch.anz_col / 2)+1;
                     currPanelrow[3] = 1;
                     currPanelcol[3] = (Austausch.anz_col / 2);
                     currBlockID = 5;
                 break;

                case 6:
                 //I-Block
                 currPanelrow[0] = 0;
                 currPanelcol[0] = (Austausch.anz_col / 2) - 2;
                 currPanelrow[1] = 0;
                 currPanelcol[1] = (Austausch.anz_col / 2)-1;
                 currPanelrow[2] = 0;
                 currPanelcol[2] = (Austausch.anz_col / 2) ;
                 currPanelrow[3] = 0;
                 currPanelcol[3] = (Austausch.anz_col / 2)+1;
                 currBlockID = 6;
                 break;

                case 7:
                    //Test
                 //Würfel
                 currPanelrow[3] = 0;
                 currPanelcol[3] = (Austausch.anz_col / 2) - 1;
                 currPanelrow[2] = 0;
                 currPanelcol[2] = (Austausch.anz_col / 2);
                 currPanelrow[1] = 1;
                 currPanelcol[1] = (Austausch.anz_col / 2) - 1;
                 currPanelrow[0] = 0;
                 currPanelcol[0] = (Austausch.anz_col / 2);
                 currBlockID = 0;
                 break;


                default:
                    
                break;
            }
           

            for (index = 0; index < 4; index++)
            {
                tableLayoutPanel1.Controls.Add(p[index], currPanelcol[index], currPanelrow[index]);  
                feldstatus[currPanelcol[index],currPanelrow[index]] = Farbe; 
            }


            //feldstatus ausgeben 
            int i, j;
            Debug.WriteLine("___________________________________________");
            for (i = 0; i <= Austausch.anz_row; i++)
            {
                for (j = 0; j < Austausch.anz_col; j++)
                {
                    Debug.Write(feldstatus[j, i] + "\t");
                }
                Debug.WriteLine("");
            }
 
        }

        private void timer_fall_Tick(object sender, EventArgs e)
        {
            int i,j;
            int kleinsterBlockwert;
            int groessterBlockwert;
            int[] colblocks = new int[4];
            int unterster_col_block = -1;
            ArrayList lst_doppelte_werte = new ArrayList(); //liste der werte die beim herunterfallen nicht beachtet werden sollen [index des Blocks]

            if (Form_activated == true)
            {
                //Größe des Blocks bestimmen
                kleinsterBlockwert = Austausch.anz_col;
                for (i = 0; i < 4; i++)
                {
                    if (currPanelcol[i] < kleinsterBlockwert)
                    {
                        kleinsterBlockwert = currPanelcol[i];
                    }
                }

                groessterBlockwert = 0;
                for (i = 0; i < 4; i++)
                {
                    if (currPanelcol[i] > groessterBlockwert)
                    {
                        groessterBlockwert = currPanelcol[i];
                    }
                }

                for (i = 0; i < 4; i++)
                {
                    colblocks[i] = -1;
                }
                for (i = 0; i < 4; i++)
                {
                    for (j = 0; j < 4; j++)
                    {
                        if (currPanelcol[i] == colblocks[j])
                        {
                            if (lst_doppelte_werte.Count == 0)
                            {
                                lst_doppelte_werte.Add(i);
                                lst_doppelte_werte.Add(j);
                            }
                            else
                            {
                                if ((!(lst_doppelte_werte.Contains(i))) && currPanelcol[i] == currPanelcol[(int)lst_doppelte_werte[0]])
                                {
                                    lst_doppelte_werte.Add(i);

                                }
                                else
                                {
                                    if ((!(lst_doppelte_werte.Contains(i))) || (!(lst_doppelte_werte.Contains(j))))
                                    {
                                        lst_doppelte_werte.Add(i);
                                        lst_doppelte_werte.Add(j);
                                    }
                                }

                            }


                        }
                    }
                    colblocks[i] = currPanelcol[i];
                }

                //ermitteln welches Panel tiefer liegt
                bool isgleiche_col = true;
                int gleiche_col = 0;
                for (i = 0; i < lst_doppelte_werte.Count; i++)
                {
                    if (i == 0)
                    {
                        gleiche_col = currPanelcol[(int)lst_doppelte_werte[i]];
                    }
                    else
                    {
                        if (currPanelcol[(int)lst_doppelte_werte[i]] != gleiche_col)
                        {
                            isgleiche_col = false;
                        }
                    }

                }
                //--------------------------------------------------------------------------------------
                //funktioniert - noch testen
                bool ugef = false;
                if (isgleiche_col == true)
                {

                    for (i = 0; i < lst_doppelte_werte.Count; i++)
                    {
                        if (currPanelrow[(int)lst_doppelte_werte[i]] > unterster_col_block)
                        {
                            unterster_col_block = currPanelrow[(int)lst_doppelte_werte[i]];
                        }
                    }
                    for (i = 0; i < lst_doppelte_werte.Count; i++)
                    {
                        if (currPanelrow[(int)lst_doppelte_werte[i]] == unterster_col_block && ugef == false)
                        {
                            unterster_col_block = (int)lst_doppelte_werte[i];
                            ugef = true;
                        }
                    }
                    lst_doppelte_werte.Remove(unterster_col_block);
                }
                //---------------------------------------------------------------------------------------
                if (isgleiche_col == false)
                {

                    int z1 = -1, z2 = -1;
                    int ucolz1 = -1, ucolz2 = -1;
                    for (i = 0; i < lst_doppelte_werte.Count; i++)
                    {
                        Debug.Print("lstdoppelte_werte[i] = " + (int)lst_doppelte_werte[i]);
                        Debug.Print("currPanelcol = " + currPanelcol[(int)lst_doppelte_werte[i]]);
                        if (i == 0)
                        {
                            z1 = currPanelcol[(int)lst_doppelte_werte[i]];
                        }
                        else
                        {
                            if (currPanelcol[(int)lst_doppelte_werte[i]] == z1)
                            {
                            }
                            else
                            {
                                z2 = currPanelcol[(int)lst_doppelte_werte[i]];
                            }
                        }
                    }
                    for (i = 0; i < lst_doppelte_werte.Count; i++)
                    {
                        if (currPanelcol[(int)lst_doppelte_werte[i]] == z1)
                        {
                            if (currPanelrow[(int)lst_doppelte_werte[i]] > ucolz1)
                            {
                                ucolz1 = currPanelrow[(int)lst_doppelte_werte[i]];
                            }
                        }
                        if (currPanelcol[(int)lst_doppelte_werte[i]] == z2)
                        {
                            if (currPanelrow[(int)lst_doppelte_werte[i]] > ucolz2)
                            {
                                ucolz2 = currPanelrow[(int)lst_doppelte_werte[i]]; //0 - (müssten 1 sein)
                            }
                            Debug.Print("currpanelrow = " + currPanelrow[(int)lst_doppelte_werte[i]]);
                            Debug.Print("ucolz2 = " + ucolz2);
                        }
                    }

                    bool tgef1 = false;
                    bool tgef2 = false;
                    for (i = 0; i < lst_doppelte_werte.Count; i++)
                    {
                        if (currPanelcol[(int)lst_doppelte_werte[i]] == z1)
                        {
                            if (currPanelrow[(int)lst_doppelte_werte[i]] == ucolz1 && tgef1 == false)
                            {
                                ucolz1 = (int)lst_doppelte_werte[i];
                                tgef1 = true;
                            }
                        }

                        if (currPanelcol[(int)lst_doppelte_werte[i]] == z2)
                        {
                            if (currPanelrow[(int)lst_doppelte_werte[i]] == ucolz2 && tgef2 == false)
                            {
                                ucolz2 = (int)lst_doppelte_werte[i];
                                tgef2 = true;
                            }
                        }

                    }
                    lst_doppelte_werte.Remove(ucolz1);
                    lst_doppelte_werte.Remove(ucolz2);
                }
                //------------------------------------------------------------------------------------------------------

                for (i = 0; i < 4; i++)
                {
                    Debug.Print("[" + i + "] = [" + currPanelcol[i] + "][" + currPanelrow[i] + "]");
                }
                for (i = 0; i < lst_doppelte_werte.Count; i++)
                {
                    Debug.Print("------" + Convert.ToString(lst_doppelte_werte[i]));
                }




                /* Falls es nicht mehr weiter geht */
                bool blocku_besetzt = false;
                for (i = 0; i < 4; i++)
                {
                    if (!(lst_doppelte_werte.Contains(i)))
                    {
                        if (feldstatus[currPanelcol[i], currPanelrow[i] + 1] != -1)
                        {
                            blocku_besetzt = true;
                        }
                    }
                }

                //Überprüfung - besetzter Block unterhalb
                if (blocku_besetzt == true)
                {
                    Debug.Print("Block kann nicht weiter fallen");
                }

                if (blocku_besetzt == false)
                {
                    for (i = PanelList.Count - 1; i >= PanelList.Count - 4; i--) //Achtung : i kann auch größer 4 sein bei mehreren Blöcken
                    {
                        //Block löschen
                        Panel panel_1 = (Panel)tableLayoutPanel1.Controls.Find("panel" + i, false).FirstOrDefault();
                        tableLayoutPanel1.Controls.Remove(panel_1);


                    }
                    for (i = 0; i < 4; i++)
                    {
                        feldstatus[currPanelcol[i], currPanelrow[i]] = -1;
                    }

                    for (i = PanelList.Count - 1; i >= PanelList.Count - 4; i--)
                    {
                        //Block einen nach unten verschieben
                        Panel panel_1 = (Panel)PanelList[i];

                        currPanelrow[i] = currPanelrow[i] + 1;
                        tableLayoutPanel1.Controls.Add(panel_1, currPanelcol[i], currPanelrow[i]);
                    }
                    for (i = 0; i < 4; i++)
                    {
                        feldstatus[currPanelcol[i], currPanelrow[i]] = 1;       //<-- Beispiel / eig müsste hier die Block_ID stehen bzw die ID der Farbe
                    }
                    
                }

            }
//------------------------------------------------------------------------------------
            //Feldstatus Feld Ausgabe
            Debug.WriteLine("___________________________________________");
            for (i = 0; i <= Austausch.anz_row; i++)
            {
                for (j = 0; j <Austausch.anz_col; j++)
                {
                    Debug.Write(feldstatus[j, i] + "\t");
                }
                Debug.WriteLine("");
            }
//-------------------------------------------------------------------------------------
            
        }

    }
}
