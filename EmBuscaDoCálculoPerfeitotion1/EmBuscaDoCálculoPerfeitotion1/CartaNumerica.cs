using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EmBuscaDoCálculoPerfeitotion1
{
    public class NumericCard: PictureBox
    {
        System.Drawing.Point initialPonit;
        public Boolean dragging = false;
        public System.Drawing.Point offset;

        public NumericCard(int x, int y)
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Size = new System.Drawing.Size(42, 66);
            this.Location = new System.Drawing.Point(x, y);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.event_MouseDown);
            initialPonit = this.Location;
            this.BringToFront();
        }
        public NumericCard(int x, int y, System.Drawing.Bitmap image)
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Size = new System.Drawing.Size(42, 66);
            this.Location = new System.Drawing.Point(x, y);
            this.Image = image;

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.event_MouseDown);
            initialPonit = this.Location;
            this.BringToFront();
        }

        public void changePosition(System.Drawing.Point position){
             this.Location = position;
        }

        public void changePosition(int x, int y){
             this.Location = new System.Drawing.Point(x, y);
        }

        private void event_MouseDown(object sender, MouseEventArgs e)
        {
            this.dragging = true;
            this.DoDragDrop(this.Image, DragDropEffects.Copy | DragDropEffects.Move);
            offset = e.Location;
        }

        public void resetPosition()
        {
            this.dragging = false;
            this.Location = this.initialPonit;
        }

        public void AllocatePosition(System.Drawing.Point position)
        {
            this.dragging = false;
            this.Location = position;
            this.MouseDown -= this.event_MouseDown;
        }

        public void AllocatePosition(int x, int y)
        {
            this.dragging = false;
            this.Location = new System.Drawing.Point(x,y);
            this.MouseDown -= this.event_MouseDown;
        }
        
    }
}
