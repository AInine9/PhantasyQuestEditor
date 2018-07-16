using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PhantasyQuestEditor.fileManager {
    class Exporter {
        private SaveFileDialog dialog;
        private Form1 form;

        public Exporter(SaveFileDialog dialog, Form form) {
            this.dialog = dialog;
            this.form = (Form1) form;
        }

        public void ExportQuest() {
            System.IO.Stream stream;
            stream = dialog.OpenFile();

            TabPage tabpage2 = form.getTabPage2();

            if (stream != null) {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);

                writer.WriteLine("Name: " + form.getQuestNameLabel().Text);

                writer.WriteLine("Conversation:");
                Control[] controls = tabpage2.Controls.Find("questFlowPanel", true);

                foreach (Control control in controls) {
                    FlowLayoutPanel questFlowPanel = (FlowLayoutPanel) control;
                    Control[] questSentenceControls = questFlowPanel.Controls.Find("questSentence", false);
                    TextBox questSentence = (TextBox) questSentenceControls[0];

                    Control[] questEventsControls = questFlowPanel.Controls.Find("questEvents", false);
                    CheckedListBox questEvents = (CheckedListBox) questEventsControls[0];

                    Control[] questConditionsControls = questFlowPanel.Controls.Find("questConditions", false);
                    CheckedListBox questConditions = (CheckedListBox) questEventsControls[0];

                    Control[] questSpeakerControls = questFlowPanel.Controls.Find("questSperaker", false);
                    ListBox questSpeaker = (ListBox) questEventsControls[0];

                    Control[] conversationNumberControls = questFlowPanel.Controls.Find("conversationNumber", false);
                    Label conversationNumberLabel = (Label) conversationNumberControls[0];

                    Control[] nextConversationNumberControls = questFlowPanel.Controls.Find("nextConversationNumber", false);
                    Label nextConversationNumberLabel = (Label) nextConversationNumberControls[0];

                    string sentence = questSentence.Text;

                    List<int> eventNumbers = new List<int>();
                    for (int i = 0; i > questEvents.Items.Count; i++) {
                        if (questEvents.GetItemChecked(i)) {
                            eventNumbers.Add(i);
                        }
                    }

                    List<int> conditionNumbers = new List<int>();
                    for (int i = 0; i < questConditions.Items.Count; i++) {
                        if (questConditions.GetItemChecked(i)) {
                            conditionNumbers.Add(i);
                        }
                    }

                    string speaker = null;
                    speaker = questSpeaker.SelectedItem.ToString();
                    string speakerID = null;
                    DataTable npcDataTable = form.getNPCDataTable();
                    foreach (DataRow row in npcDataTable.Rows) {
                        if (row.ItemArray[0].ToString().Equals(speaker)) {
                            speakerID = row.ItemArray[1].ToString();
                            break;
                        }
                    }

                    string conversationNumber = conversationNumberLabel.Text;

                    List<int> nextConversationNumbers = new List<int>();
                    string[] strings = nextConversationNumberLabel.Text.Replace("Next: ,", "").Split(',');
                    foreach (string s in strings) {
                        nextConversationNumbers.Add(int.Parse(s));
                    }

                    writer.WriteLine("  -" + speakerID + ">" + sentence);

                    if (eventNumbers != null) {
                        writer.Write("; events:");
                        foreach (int number in eventNumbers) {
                            writer.Write(number + ", ");
                        }
                    }

                    if (conditionNumbers != null) {
                        writer.Write("; conditions:");
                        foreach (int number in conditionNumbers) {
                            writer.Write(number + ", ");
                        }
                    }

                    if (speaker.Equals("player")) {
                        writer.Write("; next:" + nextConversationNumbers[0]);
                    }
                    else {
                        writer.Write("; reply:");
                        foreach (int i in nextConversationNumbers) {
                            writer.Write(i + ", ");
                        }
                    }
                }

                writer.Close();
                stream.Close();

                MessageBox.Show("クエストをエクスポートしました");
            }
        }
    }
}
