<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/184246703/19.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T830411)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/DXRichEdit_Encryption/MainWindow.xaml) (VB:[MainWindow.xaml](./VB/DXRichEdit_Encryption/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/DXRichEdit_Encryption/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/DXRichEdit_Encryption/MainWindow.xaml.vb))
<!-- default file list end -->

# Document Encryption (Simple Example)

The following sample project shows how to use the RichEditControl for WPF to load and save password-encrypted files. You can specify a password and an encryption type on the left pane and export the result to DOCX or DOC format. When a user re-opens the file with a new password, the [RichEditControl.EncryptedFilePasswordRequested](https://docs.devexpress.com/WPF/DevExpress.Xpf.RichEdit.RichEditControl.EncryptedFilePasswordRequested) and [RichEditControl.EncryptedFilePasswordCheckFailed](https://docs.devexpress.com/WPF/DevExpress.Xpf.RichEdit.RichEditControl.EncryptedFilePasswordCheckFailed) events occur. If the user cancels the operation or exceeds the number of attempts to enter the password, RichEditControl shows the exception message.

![image](./media/project_image.png)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-richedit-document-encryption&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-richedit-document-encryption&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
