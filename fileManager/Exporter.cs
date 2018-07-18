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
                List<Control> controls = new List<Control>();
                controls.AddRange(tabpage2.Controls.Find("questFlowPanel", true));
                controls.Reverse();

                foreach (Control control in controls) {
                    FlowLayoutPanel questFlowPanel = (FlowLayoutPanel) control;
                    Control[] questSentenceControls = questFlowPanel.Controls.Find("questSentence", false);
                    TextBox questSentence = (TextBox) questSentenceControls[0];

                    Control[] questEventsControls = questFlowPanel.Controls.Find("questEvents", false);
                    CheckedListBox questEvents = (CheckedListBox) questEventsControls[0];

                    Control[] questConditionsControls = questFlowPanel.Controls.Find("questConditions", false);
                    CheckedListBox questConditions = (CheckedListBox) questConditionsControls[0];

                    Control[] questSpeakerControls = questFlowPanel.Controls.Find("questSpeaker", false);
                    ComboBox questSpeaker = (ComboBox) questSpeakerControls[0];

                    Control[] conversationNumberControls = questFlowPanel.Controls.Find("conversationNumber", false);
                    Label conversationNumberLabel = (Label) conversationNumberControls[0];

                    Control[] nextConversationNumberControls = questFlowPanel.Controls.Find("nextConversationNumber", false);
                    Label nextConversationNumberLabel = (Label) nextConversationNumberControls[0];

                    string sentence = questSentence.Text;

                    List<int> eventNumbers = new List<int>();
                    for (int i = 0; i < questEvents.Items.Count; i++) {
                        if (questEvents.GetItemChecked(i)) {
                            eventNumbers.Add(i + 1);
                        }
                    }

                    List<int> conditionNumbers = new List<int>();
                    for (int i = 0; i < questConditions.Items.Count; i++) {
                        if (questConditions.GetItemChecked(i)) {
                            conditionNumbers.Add(i + 1);
                        }
                    }

                    string speaker = null;
                    speaker = questSpeaker.SelectedItem.ToString();
                    string speakerID = null;

                    if (!speaker.Equals("player")) {
                        foreach (DataRow row in form.getNPCDataTable().Rows) {
                            if (row.ItemArray[0].ToString().Equals(speaker)) {
                                speakerID = row.ItemArray[1].ToString();
                                break;
                            }
                        }
                    }

                    string conversationNumber = conversationNumberLabel.Text;

                    List<int> nextConversationNumbers = new List<int>();
                    if (nextConversationNumberLabel.Text.Contains(",")) {
                        //exist next conversation
                        string[] strings = nextConversationNumberLabel.Text.Replace("Next: ", "").Remove(0, 1).Split(',');
                        foreach (string s in strings) {
                            if (s.Equals("")) break;
                            nextConversationNumbers.Add(int.Parse(s));
                        }
                    }

                    if (speaker.Equals("player")) {
                        writer.Write("  - player>" + sentence);
                    }
                    else {
                        writer.Write("  - " + speakerID + ">" + sentence);
                    }

                    if (eventNumbers.Count != 0) {
                        writer.Write("; events:");
                        for (int i = 0; i < eventNumbers.Count; i++) {
                            if (i == 0) {
                                writer.Write(eventNumbers[0]);
                            }
                            else {
                                writer.Write(", " + eventNumbers[i]);
                            }
                        }
                    }

                    if (conditionNumbers.Count != 0) {
                        writer.Write("; conditions:");
                        for (int i = 0; i < conditionNumbers.Count; i++) {
                            if (i == 0) {
                                writer.Write(conditionNumbers[0]);
                            }
                            else {
                                writer.Write(", " + conditionNumbers[i]);
                            }
                        }
                    }

                    if (nextConversationNumbers.Count != 0) {
                        if (nextConversationNumbers.Count == 1) {
                            //next conversation speaker is a NPC
                            writer.Write("; next:" + nextConversationNumbers[0].ToString());
                        }
                        else {
                            //next conversation speaker is a player
                            writer.Write("; reply:");
                            for (int i = 0; i < nextConversationNumbers.Count; i++) {
                                if (i == 0) {
                                    writer.Write(nextConversationNumbers[0]);
                                }
                                else {
                                    writer.Write(", " + nextConversationNumbers[i]);
                                }
                            }
                        }
                    }

                    writer.WriteLine("");
                }

                writer.WriteLine("StartPoints:");
                foreach (DataRow row in form.getStartPointsDataTable().Rows) {
                    string startPoint = row[0].ToString();
                    writer.WriteLine("  - " + startPoint);
                }

                writer.WriteLine("NPCs:");
                foreach (DataRow row in form.getNPCDataTable().Rows) {
                    string npcName = row.ItemArray[0].ToString();
                    string npcID = row.ItemArray[1].ToString();
                    writer.WriteLine("  - " + npcName + ":" + npcID);
                }

                writer.WriteLine("Objectives:");
                foreach (DataRow row in form.getObjectiveDataTable().Rows) {
                    string objective = row[0].ToString();
                    writer.WriteLine("  - " + objective);
                }

                writer.WriteLine("Events:");
                foreach (DataRow row in form.getEventDataTable().Rows) {
                    string eventString = row[0].ToString();
                    writer.WriteLine("  - " + eventString);
                }

                writer.WriteLine("Conditions:");
                foreach (DataRow row in form.getConditionDataTable().Rows) {
                    string condition = row[0].ToString();
                    writer.WriteLine("  - " + condition);
                }

                writer.WriteLine("Journals:");
                foreach (DataRow row in form.getJournalDataTable().Rows) {
                    string journal = row[0].ToString();
                    writer.WriteLine("  - " + journal);
                }

                writer.Close();
                stream.Close();

                MessageBox.Show("クエストをエクスポートしました");
            }
        }
    }
}
