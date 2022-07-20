
Imports MySql.Data.MySqlClient
Public Class FormLogin
    Dim conn As New AksesData.koneksiDB

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtusername.Text = ""
        txtpassword.Text = ""
        btnMasuk.Enabled = False
        txtusername.Focus()
    End Sub

    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnBatal_Click(sender, e)
    End Sub

    Private Sub FormLogin_FormClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        FormUtama.Enabled = True
    End Sub


    Private Sub btnMasuk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMasuk.Click
        Try
            Dim StrSQl As String = "SELECT * FROM login where username=username and password =password"
            Dim myCommmand As New MySqlCommand(StrSQl, conn.open)
            myCommmand.Parameters.Add("asip", MySqlDbType.Text).Value = txtusername.Text
            myCommmand.Parameters.Add("1234", MySqlDbType.Text).Value = txtpassword.Text
            Dim rdr As MySqlDataReader = myCommmand.ExecuteReader
            If rdr.Read = False Then
                MsgBox("Data Tidak Ada!!!", vbYes, "Login")
                txtusername.Text = "asip"
                txtpassword.Text = "1234"
                txtusername.Focus()
            Else
                FormUtama.Show()
                Me.Hide()
            End If
            rdr.Close()
        Catch sqlex As MySqlException
            Throw New Exception(sqlex.Message.ToString())
        End Try
    End Sub

    Private Sub txtPassword_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpassword.Leave
        btnMasuk.Enabled = True
        btnMasuk.Focus()
    End Sub

    Private Sub btnKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub
End Class









