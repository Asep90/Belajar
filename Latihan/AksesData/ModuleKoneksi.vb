Imports MySql.Data.MySqlClient
Module Modulekoneksi
    Public Koneksi As MySqlConnection
    Public RD As MySqlDataReader
    Public Strcon As String
    Public ds As DataSet
    Public olecn As New MySqlConnection
    Public DA As New MySqlDataAdapter
    Public dataset As New DataSet
    Dim muser As New MySqlCommand
    Dim Mtotal As Double
    Public Sub bukaDB()
        Koneksi = New MySqlConnection
        Koneksi.ConnectionString = "server =localhost;Userid=root;Password=;database=dbberobat"
        Koneksi.Open()
    End Sub
    Public Sub statuskoneksi()
        Koneksi.Close()
        Koneksi.Open()
    End Sub
End Module
