using System;
using System.Drawing;
using System.Windows.Forms;

namespace PhantasyQuestEditor {
    class QuestFlowPanel {
        private FlowLayoutPanel panel;
        private Button button;
        private CheckedListBox events;
        private CheckedListBox conditions;
        private ComboBox speaker;
        private TextBox sentence;
        private Label number, nextConversationNumber;

        public QuestFlowPanel(Point mouseLocation, int ID, EventHandler clickedEvent, EventHandler panelClickedEvent) {
            panel = new FlowLayoutPanel();
            button = new Button();
            events = new CheckedListBox();
            conditions = new CheckedListBox();
            speaker = new ComboBox();
            sentence = new TextBox();
            number = new Label();
            nextConversationNumber = new Label();
            // 
            // questFlowPanel
            // 
            panel.BackColor = Color.DarkSalmon;
            panel.Controls.Add(button);
            panel.Controls.Add(speaker);
            panel.Controls.Add(sentence);
            panel.Controls.Add(events);
            panel.Controls.Add(conditions);
            panel.Controls.Add(number);
            panel.Controls.Add(nextConversationNumber);
            panel.Location = mouseLocation;
            panel.Name = "questFlowPanel";
            panel.Size = new Size(230, 196);
            panel.TabIndex = 0;
            panel.Click += new EventHandler(panelClickedEvent);
            // 
            // questEvents
            // 
            events.FormattingEnabled = true;
            events.Name = "questEvents";
            events.HorizontalScrollbar = true;
            events.ScrollAlwaysVisible = true;
            events.Size = new Size(106, 60);
            events.TabIndex = 1;
            // 
            // questConditions
            // 
            conditions.FormattingEnabled = true;
            conditions.Name = "questConditions";
            conditions.HorizontalScrollbar = true;
            conditions.ScrollAlwaysVisible = true;
            conditions.Size = new Size(109, 60);
            conditions.TabIndex = 2;
            // 
            // questSpeaker
            // 
            speaker.FormattingEnabled = true;
            speaker.Name = "questSpeaker";
            speaker.Items.AddRange(new object[] {
            "player"});
            speaker.Size = new Size(99, 20);
            speaker.TabIndex = 6;
            // 
            // questSentence
            // 
            sentence.Multiline = true;
            sentence.Name = "questSentence";
            sentence.ScrollBars = ScrollBars.Vertical;
            sentence.Size = new Size(221, 72);
            sentence.TabIndex = 0;
            // 
            // questConnectButton
            // 
            button.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (128)));
            button.Name = "questConnectButton";
            button.Size = new Size(75, 25);
            button.TabIndex = 3;
            button.Text = "分岐させる";
            button.UseVisualStyleBackColor = true;
            button.Click += new EventHandler(clickedEvent);
            button.Show();
            // 
            // conversationNumber
            // 
            number.AutoSize = true;
            number.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (128)));
            number.Location = new System.Drawing.Point(102, 175);
            number.Name = "conversationNumber";
            number.Size = new System.Drawing.Size(15, 15);
            number.TabIndex = 4;
            number.Text = ID.ToString();
            // 
            // nextConversationNumber
            // 
            nextConversationNumber.AutoSize = true;
            nextConversationNumber.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (128)));
            nextConversationNumber.Location = new System.Drawing.Point(123, 175);
            nextConversationNumber.Name = "nextConversationNumber";
            nextConversationNumber.Size = new System.Drawing.Size(45, 15);
            nextConversationNumber.TabIndex = 5;
            nextConversationNumber.Text = "Next: ";


            panel.ResumeLayout(false);
            panel.PerformLayout();
        }

        public FlowLayoutPanel getQuestFlowPanel() {
            return this.panel;
        }

        public Button getQuestConnectButton() {
            return this.button;
        }

        public CheckedListBox getEventsList() {
            return this.events;
        }
    }
}
