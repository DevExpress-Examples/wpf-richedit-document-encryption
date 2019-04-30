using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace DXRichEdit_Encryption
{
    public partial class MainWindow : ThemedWindow
    {
        int tryCount = 4;
        RichEditDocumentSource source;
        public MainWindow()
        {
            DataContext = this;
            source = new RichEditDocumentSource("FirstLook.docx", DocumentFormat.OpenXml);
            InitializeComponent();
            RichEditDocumentSource = source;
            InitializeEncryptionOptions();
        }
        public RichEditDocumentSource RichEditDocumentSource
        {
            get { return source; }
            private set { source = value; }
        }

        void InitializeEncryptionOptions()
        {
            passwordEdit.Text = "test";

            string[] array = Enum.GetNames(typeof(EncryptionType));
            encryptionComboBoxEdit.ItemsSource = array;
            encryptionComboBoxEdit.SelectedItem = EncryptionType.Strong.ToString();
        }


        void SaveAs(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfDialog = new Microsoft.Win32.SaveFileDialog();
            sfDialog.Filter = "Word 2007 Document (*.docx)|*.docx|Microsoft Word Document (*.doc*)|*.doc*";
            bool? result = sfDialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;
            else
            {
                string ext = Path.GetExtension(sfDialog.FileName);
                if (ext == ".docx")
                    richEditControl1.SaveDocument(sfDialog.FileName, DocumentFormat.OpenXml);
                else
                    richEditControl1.SaveDocument(sfDialog.FileName, DocumentFormat.Doc);
            }

            string fileName = sfDialog.FileName;

            if (openFileCheckEditBox.IsChecked.HasValue && openFileCheckEditBox.IsChecked.Value)
                richEditControl1.LoadDocument(fileName);
        }


        void passwordChanged(object sender, EditValueChangedEventArgs e)
        {
            richEditControl1.Document.Encryption.Password = passwordEdit.Text;
        }

        private void EncryptionComboBoxEdit_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            richEditControl1.Document.Encryption.Type = (EncryptionType)Enum.Parse(typeof(EncryptionType), encryptionComboBoxEdit.Text);
        }

        private void RichEditControl1_EncryptedFilePasswordRequested(object sender, EncryptedFilePasswordRequestedEventArgs e)
        {
            //Count the amount of attempts to enter the password
            if (tryCount > 0)
            {
                tryCount--;
            }
        }

        private void RichEditControl1_EncryptedFilePasswordCheckFailed(object sender, EncryptedFilePasswordCheckFailedEventArgs e)
        {
            //Analyze the error led to this event
            switch (e.Error)
            {
                case RichEditDecryptionError.PasswordRequired:
                    if (tryCount > 0)
                    {
                        e.TryAgain = true;
                        e.Handled = true;
                        System.Windows.Forms.MessageBox.Show("You did not enter the password!", String.Format(" {0} attempts left", tryCount));
                    }
                    else
                        e.TryAgain = false;

                    break;

                case RichEditDecryptionError.WrongPassword:
                    if (tryCount > 0)
                    {
                        if (System.Windows.Forms.MessageBox.Show("The password is incorrect. Try Again?", string.Format("{0} attempts left", tryCount),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            e.TryAgain = true;
                            e.Handled = true;
                        }
                    }
                    break;
            }

        }
    }

}
