using System.Windows.Forms;

namespace PhantasyQuestEditor.fileManager {
    class Exporter {
        private SaveFileDialog dialog;
        private Form1 form;

        public Exporter(SaveFileDialog dialog, Form form) {
            this.dialog = dialog;
            this.form = (Form1)form;
        }

        public void ExportQuest() {
            System.IO.Stream stream;
            stream = dialog.OpenFile();
            if (stream != null) {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
                writer.Write("Name: " + form.getQuestNameLabel().Text);
                writer.Close();
                stream.Close();

                MessageBox.Show("クエストをエクスポートしました");
            }
        }
    }
}
