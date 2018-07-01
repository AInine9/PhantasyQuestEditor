﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhantasyQuestEditor {
    class QuestFlowPanel {

        public QuestFlowPanel(Point mouseLocation, int ID) {
            panel = new FlowLayoutPanel();
            button = new Button();
            events = new CheckedListBox();
            conditions = new CheckedListBox();
            speaker = new ListBox();
            sentence = new TextBox();

            ID = ID + 1;
            // 
            // questFlowPanel
            // 
            panel.BackColor = Color.DarkSalmon;
            panel.Controls.Add(sentence);
            panel.Controls.Add(events);
            panel.Controls.Add(conditions);
            panel.Controls.Add(speaker);
            panel.Location = mouseLocation;
            panel.Name = "questFlowPanel_ID" + ID;
            panel.Size = new Size(230, 196);
            panel.TabIndex = 0;
            // 
            // questEvents
            // 
            events.FormattingEnabled = true;
            events.Name = "questEvents" + ID;
            events.ScrollAlwaysVisible = true;
            events.Size = new Size(106, 60);
            events.TabIndex = 1;
            // 
            // questConditions
            // 
            conditions.FormattingEnabled = true;
            conditions.Name = "questConditions" + ID;
            conditions.ScrollAlwaysVisible = true;
            conditions.Size = new Size(109, 60);
            conditions.TabIndex = 2;
            // 
            // questSpeaker
            // 
            speaker.FormattingEnabled = true;
            speaker.ItemHeight = 12;
            speaker.Name = "questSpeaker" + ID;
            speaker.ScrollAlwaysVisible = true;
            speaker.Size = new Size(93, 16);
            speaker.TabIndex = 1;
            // 
            // questSentence
            // 
            sentence.Multiline = true;
            sentence.Name = "questSentence" + ID;
            sentence.ScrollBars = ScrollBars.Vertical;
            sentence.Size = new Size(221, 72);
            sentence.TabIndex = 0;
            // 
            // questConnectButton
            // 
            button.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (128)));
            button.Name = "questConnectButton" + ID;
            button.Size = new Size(75, 25);
            button.TabIndex = 3;
            button.Text = "分岐させる";
            button.UseVisualStyleBackColor = true;
            button.Show();


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

        private FlowLayoutPanel panel;
        private Button button;
        private CheckedListBox events;
        private CheckedListBox conditions;
        private ListBox speaker;
        private TextBox sentence;
    }
}