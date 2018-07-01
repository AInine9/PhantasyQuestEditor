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
        private int panelID;

        public Form1() {
            InitializeComponent();

            tabPage2.MouseDown += new MouseEventHandler(MousePressed);
        }

        public void QuestConnectButtonClicked(object sender, EventArgs e) {
            panelCenterLocation = new Point(
                (questConnectButton.Parent.Location.X + questConnectButton.Parent.Width) / 2,
                (questConnectButton.Parent.Location.Y + questConnectButton.Parent.Height) / 2);

            graphics = tabPage2.CreateGraphics();

            panelID = Int32.Parse(questConnectButton.Parent.Name.Replace("questFlowPanel_ID", ""));
        }

        private void MousePressed(object sender, MouseEventArgs e) {
            if (graphics != null) {
                mouseLocation = new Point(e.X, e.Y);

                graphics.DrawLine(blackPen, panelCenterLocation, mouseLocation);

                graphics = null;

                QuestFlowPanel questFlowPanel = new QuestFlowPanel(mouseLocation, panelID);
                Panel panel = questFlowPanel.getQuestFlowPanel();
                Button connectButton = questFlowPanel.getQuestConnectButton();

                connectButton.Click += new EventHandler(this.QuestConnectButtonClicked);

                panel.Controls.Add(connectButton);

                tabPage2.Controls.Add(panel);
                panel.Controls.Add(connectButton);
                connectButton.BringToFront();
            }
        }
    }
}
