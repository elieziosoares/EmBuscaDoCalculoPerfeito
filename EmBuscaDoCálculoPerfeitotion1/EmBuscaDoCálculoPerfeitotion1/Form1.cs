using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmBuscaDoCálculoPerfeitotion1
{
    public partial class Form1 : Form
    {
        Bitmap[] imgsNumb;
        Bitmap[] imgsOp;
        OperationCard[] OpCards;
        NumericCard[] NumCards;
        object[] positionsNum, positionsOp;
            
        NumericCard mouseNum = null;
        OperationCard mouseOp = null;
        Boolean releaseOut = false;

        public Form1()
        {
            InitializeComponent();

            imgsNumb = new Bitmap[8];
            imgsNumb[0] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._1;
            imgsNumb[1] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._2;
            imgsNumb[2] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._3;
            imgsNumb[3] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._4;
            imgsNumb[4] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._5;
            imgsNumb[5] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._6;
            imgsNumb[6] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._7;
            imgsNumb[7] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources._8;

            imgsOp = new Bitmap[4];
            imgsOp[0] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources.plus;
            imgsOp[1] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources.sub;
            imgsOp[2] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources.mult;
            imgsOp[3] = global::EmBuscaDoCálculoPerfeitotion1.Properties.Resources.div;

            //inicia as cartas
            OpCards = new OperationCard[6];
            NumCards = new NumericCard[8];
            this.InitCards();

            //instancia as casas possíveis para números
            positionsNum = new object[16];//x1, x2, y1, y2
            positionsNum[0] = new int[] { 207, 249, 70, 136 };
            positionsNum[1] = new int[] { 207, 249, 181, 247 };
            positionsNum[2] = new int[] { 207, 249, 292, 358 };
            positionsNum[3] = new int[] { 207, 249, 403, 469 };
            positionsNum[4] = new int[] { 319, 361, 70, 136 };
            positionsNum[5] = new int[] { 319, 361, 181, 247 };
            positionsNum[6] = new int[] { 319, 361, 292, 358 };
            positionsNum[7] = new int[] { 319, 361, 403, 469 };
            positionsNum[8] = new int[] { 431, 473, 70, 136 };
            positionsNum[9] = new int[] { 431, 473, 181, 247 };
            positionsNum[10] = new int[] { 431, 473, 292, 358 };
            positionsNum[11] = new int[] { 431, 473, 403, 469 };
            positionsNum[12] = new int[] { 543, 585, 70, 136 };
            positionsNum[13] = new int[] { 543, 585, 181, 247 };
            positionsNum[14] = new int[] { 543, 585, 292, 358 };
            positionsNum[15] = new int[] { 543, 585, 403, 469 };

            //instancia as casas possíveis para operações
            positionsOp = new object[12];//x1, x2, y1, y2  57; 33
            positionsOp[0] = new int[] { 200, 257, 142, 175 };
            positionsOp[1] = new int[] { 200, 257, 253, 286 };
            positionsOp[2] = new int[] { 200, 257, 364, 397 };
            positionsOp[3] = new int[] { 312, 369, 142, 175 };
            positionsOp[4] = new int[] { 312, 369, 253, 286 };
            positionsOp[5] = new int[] { 312, 369, 364, 397 };
            positionsOp[6] = new int[] { 424, 471, 142, 175 };
            positionsOp[7] = new int[] { 424, 471, 253, 286 };
            positionsOp[8] = new int[] { 424, 471, 364, 397 };
            positionsOp[9] = new int[] { 536, 593, 142, 175 };
            positionsOp[10] = new int[] { 536, 593, 253, 286 };
            positionsOp[11] = new int[] { 536, 593, 364, 397 };
        }

        private void InitCards()
        {
            OpCards[0] = new OperationCard(42, 366, imgsOp[0]);
            OpCards[1] = new OperationCard(42, 404, imgsOp[1]);
            OpCards[2] = new OperationCard(42, 443, imgsOp[2]);
            OpCards[3] = new OperationCard(108, 366, imgsOp[2]);
            OpCards[4] = new OperationCard(108, 404, imgsOp[1]);
            OpCards[5] = new OperationCard(108, 443, imgsOp[0]);

            NumCards[0] = new NumericCard(64, 60, imgsNumb[0]);
            NumCards[1] = new NumericCard(64, 132, imgsNumb[1]);
            NumCards[2] = new NumericCard(64, 204, imgsNumb[2]);
            NumCards[3] = new NumericCard(64, 276, imgsNumb[3]);
            NumCards[4] = new NumericCard(108, 60, imgsNumb[4]);
            NumCards[5] = new NumericCard(108, 132, imgsNumb[5]);
            NumCards[6] = new NumericCard(108, 204, imgsNumb[6]);
            NumCards[7] = new NumericCard(108, 276, imgsNumb[7]);

            foreach (NumericCard p in NumCards)
                this.Controls.Add(p);
            foreach (OperationCard p in OpCards)
                this.Controls.Add(p);
        }

        
        /// <summary>
        /// Controla o arrastar das cartas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDrag_Tick(object sender, EventArgs e)
        {
            //há alguma carta sendo manuseada?
            if (mouseNum == null && mouseOp == null) //caso não haja, verifique novamente.
            {
                //descobrindo que carta está sendo arrastada
                foreach (NumericCard nc in NumCards)
                    if (nc.dragging)
                        mouseNum = nc;
                foreach (OperationCard oc in OpCards)
                    if (oc.dragging)
                        mouseOp = oc;
            }
            

            if (mouseNum != null) // há carta numérica sendo manuseada?
            {
                if (releaseOut) //há e foi liberada
                {
                    mouseNum.resetPosition(); //reinicia posição e variável dragging
                    mouseNum = null;           //libera variável
                    releaseOut = false;
                    return;
                }
                mouseNum.changePosition(MousePosition.X - this.Location.X-15, MousePosition.Y - this.Location.Y-15);
                mouseNum.BringToFront();
            }
            else if (mouseOp != null)// há carta de operação sendo manuseada?
            {
                if (releaseOut)
                {
                    mouseOp.resetPosition();
                    mouseOp = null;
                    releaseOut = false;
                    return;
                }
                mouseOp.changePosition(MousePosition.X - this.Location.X-20, MousePosition.Y - this.Location.Y-20);
                mouseOp.BringToFront();
            }
            return;
        }

        /// <summary>
        /// Regula liberação das cartas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //verificar se foi solto em cima de uma das caixas válidas
            
            //x e y do local do release
            int x = e.X-this.Location.X, y= e.Y-this.Location.Y;
            int[] pointsNum, pointsOp;

            if (mouseNum != null)
            {
                //o local de release é dentro de algum dos locais possíveis para número?
                for (int i = 0; i < positionsNum.Length; i++)
                {
                    pointsNum = (int[])positionsNum[i];
                    if (x > pointsNum[0] && x < pointsNum[1] && y > pointsNum[2] && y < pointsNum[3])
                    {
                        mouseNum.AllocatePosition(pointsNum[0], pointsNum[2]);
                        mouseNum = null;
                        return;
                    }
                }
            }

            if (mouseOp != null)
            {
                //o local de release é dentro de algum dos locais possíveis para operação?
                for (int i = 0; i < positionsOp.Length; i++)
                {
                    pointsOp = (int[])positionsOp[i];
                    if (x > pointsOp[0] && x < pointsOp[1] && y > pointsOp[2] && y < pointsOp[3])
                    {
                        mouseOp.AllocatePosition(pointsOp[0], pointsOp[2]);
                        mouseOp = null;
                        return;
                    }
                }
            }
            releaseOut = true;
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
