Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Forms

Namespace DXRichEdit_Encryption

    Public Partial Class MainWindow
        Inherits ThemedWindow

        Private tryCount As Integer = 4

        Private source As RichEditDocumentSource

        Public Sub New()
            DataContext = Me
            source = New RichEditDocumentSource("FirstLook.docx", DocumentFormat.OpenXml)
            Me.InitializeComponent()
            RichEditDocumentSource = source
            InitializeEncryptionOptions()
        End Sub

        Public Property RichEditDocumentSource As RichEditDocumentSource
            Get
                Return source
            End Get

            Private Set(ByVal value As RichEditDocumentSource)
                source = value
            End Set
        End Property

        Private Sub InitializeEncryptionOptions()
            Me.passwordEdit.Text = "test"
            Dim array As String() = [Enum].GetNames(GetType(EncryptionType))
            Me.encryptionComboBoxEdit.ItemsSource = array
            Me.encryptionComboBoxEdit.SelectedItem = EncryptionType.Strong.ToString()
        End Sub

        Private Sub SaveAs(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim sfDialog As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog()
            sfDialog.Filter = "Word 2007 Document (*.docx)|*.docx|Microsoft Word Document (*.doc*)|*.doc*"
            Dim result As Boolean? = sfDialog.ShowDialog()
            If Not result.HasValue OrElse Not result.Value Then
                Return
            Else
                Dim ext As String = Path.GetExtension(sfDialog.FileName)
                If Equals(ext, ".docx") Then
                    Me.richEditControl1.SaveDocument(sfDialog.FileName, DocumentFormat.OpenXml)
                Else
                    Me.richEditControl1.SaveDocument(sfDialog.FileName, DocumentFormat.Doc)
                End If
            End If

            Dim fileName As String = sfDialog.FileName
            If Me.openFileCheckEditBox.IsChecked.HasValue AndAlso Me.openFileCheckEditBox.IsChecked.Value Then Me.richEditControl1.LoadDocument(fileName)
        End Sub

        Private Sub passwordChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Me.richEditControl1.Document.Encryption.Password = Me.passwordEdit.Text
        End Sub

        Private Sub EncryptionComboBoxEdit_EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Me.richEditControl1.Document.Encryption.Type = CType([Enum].Parse(GetType(EncryptionType), Me.encryptionComboBoxEdit.Text), EncryptionType)
        End Sub

        Private Sub RichEditControl1_EncryptedFilePasswordRequested(ByVal sender As Object, ByVal e As EncryptedFilePasswordRequestedEventArgs)
            'Count the amount of attempts to enter the password
            If tryCount > 0 Then
                tryCount -= 1
            End If
        End Sub

        Private Sub RichEditControl1_EncryptedFilePasswordCheckFailed(ByVal sender As Object, ByVal e As EncryptedFilePasswordCheckFailedEventArgs)
            'Analyze the error led to this event
            Select Case e.Error
                Case RichEditDecryptionError.PasswordRequired
                    If tryCount > 0 Then
                        e.TryAgain = True
                        e.Handled = True
                        Forms.MessageBox.Show("You did not enter the password!", String.Format(" {0} attempts left", tryCount))
                    Else
                        e.TryAgain = False
                    End If

                Case RichEditDecryptionError.WrongPassword
                    If tryCount > 0 Then
                        If Forms.MessageBox.Show("The password is incorrect. Try Again?", String.Format("{0} attempts left", tryCount), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Forms.DialogResult.Yes Then
                            e.TryAgain = True
                            e.Handled = True
                        End If
                    End If

            End Select

            e.Handled = True
        End Sub

        Private Sub RichEditControl1_DecryptionFailed(ByVal sender As Object, ByVal e As DecryptionFailedEventArgs)
            Forms.MessageBox.Show(e.Exception.Message.ToString())
        End Sub
    End Class
End Namespace
