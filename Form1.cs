using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhantasyQuestEditor {
    public partial class Form1 : Form {

        private Point startLocation, endLocation;
        private Pen blackPen = new Pen(Color.Black, 5);
        private Graphics graphics;
        private Control[] nextConversationLabel;
        private Bitmap bitmap;

        public Form1() {
            InitializeComponent();

            pictureBox.MouseDown += new MouseEventHandler(MousePressed);

            bitmap = new Bitmap(1920, 1080);
            pictureBox.Image = bitmap;
        }

        public void QuestConnectButtonClicked(object sender, EventArgs e) {
            Control clickedButton = (Control) sender;
            Control parentPanel = clickedButton.Parent;
            nextConversationLabel = clickedButton.Parent.Controls.Find("nextConversationNumber", false);

            startLocation = pictureBox.PointToClient(clickedButton.PointToScreen(clickedButton.Location));

            graphics = Graphics.FromImage(pictureBox.Image);
        }

        public void PanelClicked(object sender, EventArgs e) {
            Control panel = (Control) sender;
            panel.BringToFront();
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
            Control pressedControl = (Control) sender;
            String name = pressedControl.Name;

            if (graphics != null && name.Equals("pictureBox")) {
                endLocation = new Point(e.X, e.Y);

                graphics.DrawLine(blackPen, startLocation, endLocation);
                pictureBox.Refresh();

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

                QuestFlowPanel questFlowPanel = new QuestFlowPanel(endLocation, nextConversationID, this.QuestConnectButtonClicked, this.PanelClicked);
                FlowLayoutPanel panel = questFlowPanel.getQuestFlowPanel();

                tabPage2.Controls.Add(panel);
                panel.BringToFront();

                nextConversationLabel[0].Text = nextConversationLabel[0].Text + "," + nextConversationID;
                nextConversationLabel = null;
            }
        }
    }
}
