using PhantasyQuestEditor.fileManager;
using System;
using System.Data;
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
                Exporter exporter = new Exporter(dialog, this);

                exporter.ExportQuest();
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

        public TextBox getQuestNameLabel() {
            return this.questNameLabel;
        }

        public DataGridView getNpcListDataView() {
            return this.npcListDataView;
        }

        public DataGridView getStartPointListView() {
            return this.startPointListView;
        }

        public TabPage getTabPage2() {
            return this.tabPage2;
        }

        public DataGridView getObjectiveDataListView() {
            return this.objectiveDataListView;
        }

        public DataGridView getEventDataListView() {
            return this.eventDataListView;
        }

        public DataGridView getConditionDataListView() {
            return this.conditionDataListView;
        }

        public DataGridView getJournalDataListView() {
            return this.journalDataListView;
        }

        public DataTable getNPCDataTable() {
            return dataSet1.NPCDataTable;
        }

        public DataTable getObjectiveDataTable() {
            return dataSet11.ObjectiveDataTable;
        }

        public DataTable getEventDataTable() {
            return dataSet13.EventDataTable;
        }

        public DataTable getConditionDataTable() {
            return dataSet12.ConditionDataTable;
        }

        public DataTable getJournalDataTable() {
            return dataSet14.JournalDataTable;
        }

        public DataTable getStartPointsDataTable() {
            return dataSet1.StartPointDataTable;
        }

        private void EventListEdited(object sender, DataGridViewCellEventArgs e) {
            int changedRow = e.RowIndex;

            if (changedRow == -1) return;

            string eventString = eventDataListView.Rows[changedRow].Cells[0].Value.ToString();

            foreach (Control control in tabPage2.Controls.Find("questEvents", true)) {
                CheckedListBox eventList = (CheckedListBox) control;
                eventList.Items.Add(eventString);
            }
        }

        private void ConditionListEdited(object sender, DataGridViewCellEventArgs e) {
            int changedRow = e.RowIndex;

            if (changedRow == -1) return;

            string conditionString = conditionDataListView.Rows[changedRow].Cells[0].Value.ToString();

            foreach (Control control in tabPage2.Controls.Find("questConditions", true)) {
                CheckedListBox conditionList = (CheckedListBox) control;
                conditionList.Items.Add(conditionString);
            }
        }

        private void NPCListEdited(object sender, DataGridViewCellEventArgs e) {
            int changedRow = e.RowIndex;

            if (changedRow == -1) return;

            string npcString = npcListDataView.Rows[changedRow].Cells[0].Value.ToString();

            if (e.ColumnIndex == 1) return;

            foreach (Control control in tabPage2.Controls.Find("questSpeaker", true)) {
                ComboBox npcList = (ComboBox) control;
                npcList.Items.Add(npcString);
            }
        }
    }
}
