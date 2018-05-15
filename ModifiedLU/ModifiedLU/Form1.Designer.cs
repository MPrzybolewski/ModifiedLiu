using System.Drawing;
using System.Windows.Forms;

namespace ModifiedLU
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Font drawFont = new System.Drawing.Font("Arial", 8);
            SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            int i = 0;
            int maxX = 0;
            using (Pen selPen = new Pen(Color.Blue))
            {
                foreach (Node node in Program.ListOfNodes)
                {
                    i++;
                    foreach(int positionX in node.PositionsInMachineLine)
                    {
                        if(maxX < positionX*30)
                        {
                            maxX = positionX;
                        }

                        Rectangle rectangleForNode = new Rectangle(positionX*30, 0, 30, 20);
                        g.DrawRectangle(selPen, rectangleForNode);

                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        e.Graphics.DrawString("Z" + i, this.Font, Brushes.Black, rectangleForNode, sf);
                    }
                }

                for(int j = 0; j <= maxX; j++)
                {
                    Rectangle rectangleForMachineLine = new Rectangle(j * 30, 30, 30, 20);
                    g.DrawRectangle(selPen, rectangleForMachineLine);
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(j.ToString(), this.Font, Brushes.Black, rectangleForMachineLine, sf);
                }
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion
    }
}

