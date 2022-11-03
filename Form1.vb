Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Imports System.Reflection
Imports System.Text
Imports Microsoft.Win32
Imports System.IO.Ports
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
' thread-safe calling for thread_hide    


Public Class Form1
    Dim Contador As Integer = 0
    Delegate Sub Change()
    Private Declare Function InternetGetConnectedState Lib "wininet.dll" (ByRef lpdwFlags As Int32, ByVal dwReserved As Int32) As Boolean
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SerialPort1.Close()
        SerialPort1.PortName = "com3"
        SerialPort1.BaudRate = "9600"
        SerialPort1.DataBits = 8
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.Handshake = Handshake.None
        SerialPort1.Encoding = System.Text.Encoding.Default
        SerialPort1.Open()
    End Sub

    Private Sub dataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Try
            Dim mydata As String = ""

            mydata = SerialPort1.ReadExisting()
            If txtOutput.InvokeRequired Then
                txtOutput.Invoke(DirectCast(Sub() txtOutput.Text &= "fuga detectada,Atender" + vbNewLine, MethodInvoker))
                Contador = Contador + 1
                If Contador > 3 Then
                    Contador = 0
                    Dim smtp As New SmtpClient
                    Dim message As New MailMessage
                    Dim username As String
                    Dim password As String
                    Dim destination As String
                    Dim subject As String
                    Dim lngFlags As Integer
                    username = "infoarqui1226@gmail.com"
                    password = "slelmmkyiscwbhnj"
                    destination = "mjavier1226@gmail.com"
                    subject = "HAY UNA FUGA, ATENDER PERO YA HIJUEPUTA"
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    smtp.Port = 587
                    smtp.Credentials = New Net.NetworkCredential(username, password)
                    message.To.Add(destination)
                    message.From = New MailAddress(username)
                    message.Subject = subject
                    message.Body = "Se han Registrado 3 Fugas , ATENDER URGENTEMENTE MIERDA"
                    If InternetGetConnectedState(lngFlags, 0) Then
                        ' checks if internet connection is available or not
                        smtp.Send(message)
                        MessageBox.Show("Alerta Enviada")
                        ' CheckBox1.Checked = False
                    Else txtOutput.Text = mydata + "" + vbNewLine + " net connection not available" + vbNewLine + ""
                    End If
                End If

            Else
                txtOutput.Text &= mydata
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If InStr(txtOutput.Text, "Exceeded") Then
            'CheckBox1.Checked = True
        End If
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'CheckBox1.Checked
        If 1 = True Then
            Dim smtp As New SmtpClient
            Dim message As New MailMessage
            Dim username As String
            Dim password As String
            Dim destination As String
            Dim subject As String
            Dim lngFlags As Integer
            username = "infoarqui@gmail.com"
            password = "mariojavier/01"
            destination = "mjavier1226@gmail.com"
            subject = "HAY UNA FUGA, ATENDER PERO YA HIJUEPUTA"
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True
            smtp.Port = 587
            smtp.Credentials = New Net.NetworkCredential(username, password)
            message.To.Add(destination)
            message.From = New MailAddress(username)
            message.Subject = subject
            message.Body = txtOutput.Text
            If InternetGetConnectedState(lngFlags, 0) Then
                ' checks if internet connection is available or not
                smtp.Send(message)
                txtOutput.Text = ""
                ' CheckBox1.Checked = False
            Else txtOutput.Text = txtOutput.Text + "" + vbNewLine + " net connection not available" + vbNewLine + ""
            End If
        End If
    End Sub
End Class
