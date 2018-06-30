using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhantasyQuestEditor {
    public partial class Form1 : Form {

        private Point panelCenterLocation, mouseLocation;
        private Pen blackPen = new Pen(Color.Black, 5);
        private Graphics graphics;

        public Form1() {
            InitializeComponent();

            this.tabPage2.MouseDown += new MouseEventHandler(MousePressed);
        }

        private void QuestConnectButtonClicked(object sender, EventArgs e) {
            this.panelCenterLocation = new Point(
                (this.questConnectButton.Parent.Location.X + this.questConnectButton.Parent.Width) / 2,
                (this.questConnectButton.Parent.Location.Y + this.questConnectButton.Parent.Height) / 2);

            this.graphics = this.tabPage2.CreateGraphics();
        }

        private void MousePressed(object sender, MouseEventArgs e) {
            if (graphics != null) {
                this.mouseLocation = new Point(e.X, e.Y);

                this.graphics.DrawLine(blackPen, panelCenterLocation, mouseLocation);

                this.graphics = null;
            }
        }
    }
}
