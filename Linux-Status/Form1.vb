Imports System.Net
Imports System.Text, System.Text.RegularExpressions, System.Security, System.IO, System.Threading

Public Class Form1
    Dim TeleInfo As String
    Dim bottoken As String
    Dim acid As String
    Dim Pos As Point
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        End
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        WindowState = FormWindowState.Minimized
    End Sub
    Public Sub send_message(msg As String)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Try
            Dim a As New WebClient()
            a.DownloadString("https://api.telegram.org/bot" + bottoken + "/sendMessage?chat_id=" + acid + "&text=" + msg)
        Catch ex As Exception
        End Try
    End Sub
    Dim x As Integer = 0
    Public Function check1()
        If CheckBox1.Checked = True Then
            Do While x = 0
                Try
                    Net.ServicePointManager.CheckCertificateRevocationList = False
                    Net.ServicePointManager.DefaultConnectionLimit = 300
                    Net.ServicePointManager.UseNagleAlgorithm = False
                    Net.ServicePointManager.Expect100Continue = False
                    Net.ServicePointManager.SecurityProtocol = 3072
                    Dim Encoding As New Text.UTF8Encoding
                    Dim Bytes As Byte() = Encoding.GetBytes("remoteAddress=" + TextBox1.Text + "&portNumber=" + TextBox2.Text + "")
                    Dim Request As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create("https://ports.yougetsignal.com/check-port.php"), Net.HttpWebRequest)
                    With Request
                        .Method = "POST"
                        .KeepAlive = False
                        .UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"
                        .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                        .Accept = "text/javascript, text/html, application/xml, text/xml, */*"
                        .Headers.Add("Accept-Language: en-US,en;q=0.9,ar;q=0.8")
                        .AutomaticDecompression = Net.DecompressionMethods.Deflate Or Net.DecompressionMethods.GZip
                        .ContentLength = Bytes.Length
                    End With
                    Dim Stream As IO.Stream = Request.GetRequestStream()
                    Stream.Write(Bytes, 0, Bytes.Length)
                    Stream.Dispose()
                    Stream.Close()
                    Dim Reader As New IO.StreamReader(DirectCast(Request.GetResponse(), Net.HttpWebResponse).GetResponseStream())
                    Dim Text As String = Reader.ReadToEnd
                    Reader.Dispose()
                    Reader.Close()
                    If Text.Contains("<p><img src=""/img/flag_green.gif"" alt=""Open"" style=""height: 1em; width: 1em;"" /> Port <a href=""https://en.wikipedia.org/wiki/Port_" + TextBox2.Text + """ target=""_blank"" />" + TextBox2.Text + "</a> is open on " + TextBox1.Text + ".</p>") Then
                        Label6.Text += 1
                    Else
                        Label7.Text += 1
                        If CheckBox2.Checked = True Then
                            send_message("The Ports Tool Checker Couldn't reach Port " + TextBox2.Text + " on " + TextBox1.Text + " Right now , Please investigate immediately.")
                        End If
                    End If
                    Thread.Sleep(TextBox3.Text * 1000)
                Catch ex As WebException
                    Label8.Text += 1
                    send_message("Exception: There is Unknown Error with the connection , please fix")
                End Try
            Loop
        Else
            Try
                Net.ServicePointManager.CheckCertificateRevocationList = False
                Net.ServicePointManager.DefaultConnectionLimit = 300
                Net.ServicePointManager.UseNagleAlgorithm = False
                Net.ServicePointManager.Expect100Continue = False
                Net.ServicePointManager.SecurityProtocol = 3072
                Dim Encoding As New Text.UTF8Encoding
                Dim Bytes As Byte() = Encoding.GetBytes("remoteAddress=" + TextBox1.Text + "&portNumber=" + TextBox2.Text + "")
                Dim Request As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create("https://ports.yougetsignal.com/check-port.php"), Net.HttpWebRequest)
                With Request
                    .Method = "POST"
                    .KeepAlive = False
                    .UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"
                    .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                    .Accept = "text/javascript, text/html, application/xml, text/xml, */*"
                    .Headers.Add("Accept-Language: en-US,en;q=0.9,ar;q=0.8")
                    .AutomaticDecompression = Net.DecompressionMethods.Deflate Or Net.DecompressionMethods.GZip
                    .ContentLength = Bytes.Length
                End With
                Dim Stream As IO.Stream = Request.GetRequestStream()
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Dispose()
                Stream.Close()
                Dim Reader As New IO.StreamReader(DirectCast(Request.GetResponse(), Net.HttpWebResponse).GetResponseStream())
                Dim Text As String = Reader.ReadToEnd
                Reader.Dispose()
                Reader.Close()
                If Text.Contains("<p><img src=""/img/flag_green.gif"" alt=""Open"" style=""height: 1em; width: 1em;"" /> Port <a href=""https://en.wikipedia.org/wiki/Port_" + TextBox2.Text + """ target=""_blank"" />" + TextBox2.Text + "</a> is open on " + TextBox1.Text + ".</p>") Then
                    Label6.Text += 1
                Else
                    Label7.Text += 1
                    If CheckBox2.Checked = True Then
                        send_message("The Ports Tool Checker Couldn't reach Port " + TextBox2.Text + " on " + TextBox1.Text + " Right now , Please investigate immediately.")
                    End If
                End If
            Catch ex As WebException
                Label8.Text += 1
                send_message("Exception: There is Unknown Error with the connection , please fix")
            End Try
        End If

    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Try
            TeleInfo = File.ReadAllText("Telegram.txt")
            bottoken = Regex.Match(TeleInfo, """BotToken=(.*?)""").Groups(1).Value
            acid = Regex.Match(TeleInfo, """ID=(.*?)""").Groups(1).Value
        Catch ex As Exception
            File.WriteAllText("Telegram.txt", """BotToken=Your Bot Token Here""" + vbCrLf + """ID=YourID""")
        End Try
        Dim b As String = MsgBox("Do you want Me" + vbCrLf + "To grab your Public IP?", MessageBoxButtons.YesNo)
        If b = 6 Then
            grabIB()
        End If
    End Sub
    Function grabIB()
        Try
            Net.ServicePointManager.CheckCertificateRevocationList = False
            Net.ServicePointManager.DefaultConnectionLimit = 300
            Net.ServicePointManager.UseNagleAlgorithm = False
            Net.ServicePointManager.Expect100Continue = False
            Net.ServicePointManager.SecurityProtocol = 3072
            Dim Request As Net.HttpWebRequest = DirectCast(Net.WebRequest.Create("https://ip.me/"), Net.HttpWebRequest)
            With Request
                .Method = "GET"
                .Proxy = Nothing
                .Host = "ip.me"
                .KeepAlive = False
                .UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36"
                .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"
                .Headers.Add("Accept-Language: en-US,en;q=0.9,ar;q=0.8")
                .AutomaticDecompression = Net.DecompressionMethods.Deflate Or Net.DecompressionMethods.GZip
            End With
            Dim Response As Net.HttpWebResponse = DirectCast(Request.GetResponse, Net.HttpWebResponse)
            Dim reader As New IO.StreamReader(Response.GetResponseStream)
            Dim text As String = reader.ReadToEnd
            reader.Dispose()
            reader.Close()
            Response.Dispose()
            Response.Close()
            TextBox1.Text = Regex.Match(text, "<input type=""text"" name=""ip"" value=""(.*?)"" class=""form-control"" id=""ip-lookup""").Groups(1).Value
        Catch ex As WebException
            MsgBox("Sorry, Something went wrong while Grabbing Your IP", MessageBoxIcon.Error)
        End Try
    End Function
    Dim t As Thread = New Thread(AddressOf check1)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            CheckBox1.Enabled = False
            t.Start()
            t.IsBackground = True
            Button1.Text = "Stop"
        ElseIf Button1.Text = "Stop" Then
            t.Abort()
            Button1.Text = "Start"
        Else
            check1()
        End If
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs) Handles Label1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += Control.MousePosition - Pos
        End If
        Pos = Control.MousePosition
    End Sub
End Class
