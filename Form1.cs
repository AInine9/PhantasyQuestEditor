﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhantasyQuestEditor {
    public partial class Form1 : Form {

        private Point startLocation, endLocation;
        private Pen blackPen = new Pen(Color.Black, 5);
        private Graphics graphics;

        public Form1() {
            InitializeComponent();

            tabPage2.MouseDown += new MouseEventHandler(MousePressed);
        }

        public void QuestConnectButtonClicked(object sender, EventArgs e) {
            Control clickedButton = (Control)sender;
            Control parentPanel = clickedButton.Parent;

            startLocation = tabPage2.PointToClient(clickedButton.PointToScreen(clickedButton.Location));

            graphics = tabPage2.CreateGraphics();
        }

        private void ExportEvent(object sender, EventArgs e) {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = this.questNameLabel.Text + ".yml";
            dialog.InitialDirectory = @"C:\";
            dialog.Filter = "YAMLファイル(*.yml)|*.yml|すべてのファイル(*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Title = "保存先";
            dialog.RestoreDirectory = true;
             if (dialog.ShowDialog() == DialogResult.OK) {
                ExportQuest(dialog);
            }
        }

        public void ExportQuest(SaveFileDialog dialog) {
            System.IO.Stream stream;
            stream = dialog.OpenFile();
            if (stream != null) {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
                writer.Write("Name: " + this.questNameLabel.Text);
                writer.Close();
                stream.Close();
            }
        }

        private void MousePressed(object sender, MouseEventArgs e) {
            Control pressedControl = (Control)sender;
            String name = pressedControl.Name;

            if (graphics != null && name.Equals("tabPage2")) {
                endLocation = new Point(e.X, e.Y);

                graphics.DrawLine(blackPen, startLocation, endLocation);

                graphics.Dispose();
                graphics = null;

                int maxConversationID = 0;
                Control[] controls = tabPage2.Controls.Find("conversationNumber", true);
                
                foreach (Control control in controls) {
                    if (Int32.Parse(control.Text) > maxConversationID) {
                        maxConversationID = Int32.Parse(control.Text);
                    }
                }

                int nextConversationID = maxConversationID + 1;

                QuestFlowPanel questFlowPanel = new QuestFlowPanel(endLocation, nextConversationID, this.QuestConnectButtonClicked);
                FlowLayoutPanel panel = questFlowPanel.getQuestFlowPanel();

                tabPage2.Controls.Add(panel);
            }
        }
    }
}
